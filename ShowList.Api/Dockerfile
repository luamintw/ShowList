FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app
EXPOSE 5001

COPY *.sln .
COPY *.csproj ./
RUN dotnet restore

COPY . .
WORKDIR /app
#RUN dotnet build -c Release
RUN dotnet publish -c Release -o out
#RUN dotnet build 

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "ShowList.Api.dll"]

