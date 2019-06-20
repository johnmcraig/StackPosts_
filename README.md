# StackPosts_
> An imitation of Stack Overflow using ASP.Net Core, Vue.js, and SignalR.

## Scope
This is a project based on building a forum application as a proof-of-concept using Asp.Net Core and Vue.js, where users can create posts for questions and/or reply to those posts. There also exists the ability to upvote and downvote posts at a users descretion*.

## Caveate
Currently, there isn't any limit to how many times a posts can be upvoted/downvoted and anyone can perform the action, so Autherization and User roles need to be implemented to place limits.

## Tech Stack
This application was build with:
- ASP.Net Core 2.2
- Vue.js 2.6
- SignleR (NuGet package)

## Setup
In order to test/use this application, you will need the following:
- Asp.Net Core 2.0 SDK, prefereably 2.2.1
- Node.js version 8 or higher
- The Vue cli

## Installation
Grab the repository either by downloading the zip file or clone the project:
```sh
~$ git clone https://github.com/johnmcraig/StackPosts_
```
After cloning or unzipping the files, navigate to the directory containing the solution file:
```sh
~$ cd src/StackPosts_/
```
In either order, navigate to the client or api/server side files and install their dependecies. Once again, you will need Node.js and `npm` installed along with the .Net Core 2.2 SDK.

For client side dependecies:
```sh
~$ cd PostsClient/
~$ npm install
```
Make sure the `@vue/cli` is installed as well:
```sh
~$ npm install -g @vue/cli
```

For server side code, build and restore dependecies and NuGet packages:
```sh
~$ cd PostsAPI/
~$ dotnet restore
```
## Running the Environment
To run a local environment on the client side:
Use `npm` script commands in a terminal/command box while in the `../client` directory:
```sh
~$ npm run watch
```
This outputs a minified JavaScript file in the `wwwroot/dist` directory of the API.

To run a local environment on the server side:
Use the `dotnet <COMMAND> <OPTIONS>` tool to run it in a terminal or use Visual Studio to run it with `CTL` + `F5`

Navigate to localhost:5001 in a browser to see the current build running.

## TODO
- Add Authorization and Login.
- Enable searching by Post title.
- Add the ability to delete or update a Post after Authorization. Then the same with Replies.

## Known Issues and Bugs
- The seed class in the data layer does not properly add a list of Replies to each Post entity.
- SignalR type errors in the post-hub occur with `Type Error: "cb is undefined"` needs to be fixed.