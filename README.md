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

# API Documentation

## Query All with Filter and Sorting

Url: ``GET /api/todo``

Query String Parameters

| Parameter | Description      | Example | Remark                                     |
|-----------|------------------|---------|--------------------------------------------|
| id        | ID of To Do Task | 1       |                                            |
| name      | To Do Task Name  | string  |                                            |
| from      | Due Date Range (From) | 2022-10-01T13:46:03.81+00:00 | The value should be encoded in URL         |
| to        | Due Date Range (To) | 2022-10-01T13:46:03.81+00:00 | The value should be encoded in URL         |
| status    | Status of To Do Task | Done |                                            |
| sort      | Sorting | -id | Format: [+\|-]property, + is ASC, - is DESC |

## Get

Url: ``GET /api/todo/{id}``

## Create

Url: ``POST /api/todo``

Request Body:
```json
{
  "name": "Task 6",
  "dueDate": "2022-10-01T13:46:03.81+00:00"
}
```

## Update

Url: ``PUT /api/todo/{id}``

Request Body:
```json
{
    "name": "Task 6",
    "dueDate": "2022-10-01T13:46:03.81+00:00",
    "status": "Done"
}
```

## Delete

Url: ``DELETE /api/todo/{id}``