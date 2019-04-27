import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr'

export default {
  install (Vue) {
    const connection = new HubConnectionBuilder()
      .withUrl(`${Vue.prototype.$http.defaults.baseURL}/post-hub`)
      .configureLogging(LogLevel.Information)
      .build()

    const postHub = new Vue()

    // Forward hub events through the event, so we can listen for them in the Vue components
    connection.on('PostScoreChange', (postId, score) => {
      postHub.$emit('score-changed', { postId, score })
    })
    connection.on('ReplyCountChange', (postId, replyCount) => {
      postHub.$emit('relpy-count-changed', { postId, replyCount })
    })
    connection.on('ReplyAdded', relpy => {
      postHub.$emit('relpy-added', relpy)
    })

    // Provide methods for components to send messages back to server
    // Make sure no invocation happens until the connection is established
    postHub.postOpened = (postId) => {
      return startedPromise
        .then(() => connection.invoke('JoinPostGroup', postId))
        .catch(console.error)
    }
    postHub.postClosed = (postId) => {
      return startedPromise
        .then(() => connection.invoke('LeavePostGroup', postId))
        .catch(console.error)
    }

    // Add the hub to the Vue prototype, so every component can listen to events or send new events using this.$questionHub
    Vue.prototype.$postHub = postHub

    let startedPromise = null
    function start () {
      startedPromise = connection.start()
        .catch(err => {
          console.error('Failed to connect with hub', err)
          return new Promise((resolve, reject) => setTimeout(() => start().then(resolve).catch(reject), 5000))
        })
      return startedPromise
    }
    connection.onclose(() => start())

    start()
  }
}
