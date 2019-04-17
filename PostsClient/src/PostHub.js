import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr'

export default {
  install (Vue) {
    const connection = new HubConnectionBuilder()
      .withUrl('https://localhost:5001/post-hub')
      .connectionLogging(LogLevel.Information)
      .build()

    connection.start()
  }
}
