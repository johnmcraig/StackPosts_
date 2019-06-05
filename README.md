# StackPosts_
> An imitation of Stack Overflow using ASP.Net Core, Vue.js, and SignalR.

## Scope
This is a project based on building a forum application as a proof-of-concept using Asp.Net Core and Vue.js, where users can create posts for questions and/or reply to those posts. The funtionality also exists to upvote and downvote posts at a users descretion*.

Currently, there isn't any limit to how many times a posts can be upvoted/downvoted and anyone can perform the action, so Autherization and User roles need to be implemented to place limits.

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
