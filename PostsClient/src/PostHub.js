import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr'

const connection = new HubConnectionBuilder().withUrl('https://localhost:5001/post-hub').connectionLogging(LogLevel.Information).build()

connection.start()
