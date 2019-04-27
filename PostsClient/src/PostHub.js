import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr'

export default {
  install (Vue) {
    const connection = new HubConnectionBuilder()
      .withUrl(`${Vue.proptotype.$http.defaults.baseURL}/post-hub`)
      .connectionLogging(LogLevel.Information)
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
      return startPromise
        .then(() => connection.invoke('JoinPostGroup', postId))
        .catch(console.error)
    }
    postHub.postClosed = (postId) => {
      return startPromise
        .then(() => connection.invoke('LeavePostGroup', postId))
        .catch(console.error)
    }

    Vue.proptotype.$postHub = postHub

    let startPromise = null
    function start () {
      startPromise = connection.start().catch(err => {
        console.error('Failed to connect to hub', err)
        return new Promise((resolve, reject) => setTimeout(() => start().then(resolve)
          .catch(reject), 5000))
      })
      return startPromise()
    }
    connection.onclose(() => start())

    start()
  }
}
