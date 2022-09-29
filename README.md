# Technical Stack
- ASP .NET Core 6
- Dapper + SQLite

# How to start the project

## .NET
Run the following command under the root of the repository

``dotnet run --project ./src/SleekFlow.ToDo.WebApi``

Then open swagger in:

``https://localhost:7046/swagger/index.html``

## Docker

Image Url: https://hub.docker.com/r/nicholassiew1991/sleekflow-todo-api

Run the following docker command to start this application in docker container

``docker run -p 10000:80 nicholassiew1991/sleekflow-todo-api:latest``

Then open swagger in:

``http://localhost:10000/swagger/index.html``