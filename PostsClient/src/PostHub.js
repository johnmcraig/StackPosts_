import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr'

export default {
  install (Vue) {
    const connection = new HubConnectionBuilder()
      .withUrl(`${Vue.proptotype.$http.defaults.baseURL}/post-hub`)
      .connectionLogging(LogLevel.Information)
      .build()

    let startPromise = null
    function start () {
      startPromise = connection.start().catch(err => {
        console.error('Failed to connect to hub', err)
        return new Promise((resolve, reject) =>
          setTimeout(() => start().then(resolve).catch(reject), 5000))
      })
      return startPromise()
    }
    connection.onclose(() => start())

    start()
  }
}
