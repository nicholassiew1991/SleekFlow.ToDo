FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /code

COPY ./SleekFlow.ToDo.sln .
COPY ./src/SleekFlow.ToDo.WebApi/SleekFlow.ToDo.WebApi.csproj ./src/SleekFlow.ToDo.WebApi/
COPY ./tests/SleekFlow.ToDo.WebApi.Test/SleekFlow.ToDo.WebApi.Test.csproj ./tests/SleekFlow.ToDo.WebApi.Test/

RUN dotnet restore

COPY . .

RUN dotnet build -c Release -o /output

FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app
COPY --from=build /output .

EXPOSE 80
ENTRYPOINT ["dotnet", "SleekFlow.ToDo.WebApi.dll"]
