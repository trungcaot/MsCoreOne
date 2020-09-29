
 # MsCoreOne - Simple Ecommerce

 This is a simple ecommerce to practice technologies.

## Build Status
| Build server    | Platform       | Status      |
|-----------------|----------------|-------------|
|Travis           | Linux / MacOS  |[![Build Status](https://travis-ci.com/trungcaot/MsCoreOne.svg?branch=master)](https://travis-ci.com/github/trungcaot/MsCoreOne) |
|Azure DevOps     | Linux          |[![Build Status](https://dev.azure.com/trungcaot/MsCoreOne/_apis/build/status/trungcaot.MsCoreOne?branchName=master)](https://dev.azure.com/trungcaot/MsCoreOne/_build/latest?definitionId=1&branchName=master) |

![alt text](/docs/imgs/mscoreone_architecture_v1.png)

## The technologies have implemented as image above.
| Technologies    | Yes/No       |
|-----------------|----------------|
|ASP .NET Core | ✅ |
|ASP .NET Core Mvc | ✅ |
|Entity Framework Core | ✅ |
|Identity Server 4 | ✅ |
|Swagger UI | ✅ |
|React + Typescript | ✅ |
|Vue.js | ✅ |
|Sql Server | ✅ |
|PostgresQL |❌ |
|Blazor/Angular |❌ |

# Onion Architecture
![alt text](/docs/imgs/onion_architecture.png)

# ASP.NET Core architecture diagram following Clean Architecture
![alt text](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/media/image5-9.png)

You probally access to [link](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures) to get more detail for common web application architectures 

# Layers Example
![alt text](/docs/imgs/layers_example.png)

# Account default
 - Username: admin@mscoreone.com
 - Password: P@ssw0rd


# Docker build

#### Prerequisite

 - Installed Docker on your computer

#### Steps to run

- At deployment folder

```sh
  $ docker-compose -f docker-compose-infra.yml up
  $ docker-compose build
  $ docker-compose up
```

#### Notes

- Updating MSCOREONE_DB_HOST's value is ([your IP], 1433) in .env file. Ex. 192.168.131.97,1433
- Adding ([your IP] mscoreone-portal.local) in hosts file. Ex. 192.168.131.97 mscoreone-portal.local

#### Links store demo on docker

 - [Front-office](http://mscoreone-portal.local:5003/): Mscoreone - mvc
 - [Back-office](http://mscoreone-portal.local:3000/): Mscoreone - react
 - [Swagger-api](http://mscoreone-portal.local:5001/): Mscoreone - api

## Running integration test and watching code coverage

You need to some require external nuget packages. Install [Converlet](https://www.nuget.org/packages/coverlet.msbuild/) and [FluentAssertions](https://www.nuget.org/packages/FluentAssertions/) for your project using the following cli commands.

To get converlet to collect code coverage for your codebase, we need just to run the following command at the repository root.

```
dotnet test MsCoreOne.IntegrationTests.csproj  /p:CollectCoverage=true /p:CoverletOutputFormat=\"opencover\" /p:CoverletOutput=BuildReports\Coverage\ /p:ExcludeByFile=\"**/Persistence/ApplicationDbContextSeed.cs\" /p:Exclude=\"[*]MsCoreOne.Infrastructure.Migrations.*,[*]MsCoreOne.Pages.*,[*]MsCoreOne.Areas.*\"

```

## Distributed Caching with Redis
To testing with redis cache, you can install redis by the following way:
1. Install manually redis from [ github repo](https://github.com/microsoftarchive/redis/releases/tag/win-3.0.504) and that download zip file that is compatible with your windows, extract the zip folder, and open up redis-server.exe 
2. Using docker to up redis by access to development folder then open powershell and that run command bellow.
```
docker-compose -f .\docker-compose-infra.yml up redis
```
Note: You should update your IP in appsetting for redis configuration to make sure mscoreone-api connect to redis server successfully.


## :handshake: Contributing

Contributions, issues and feature requests are welcome!


## :man_astronaut: Show your support

Give a :star: if you like this project!
