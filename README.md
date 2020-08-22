
 # MsCoreOne - Simple Ecommerce

 This is a simple ecommerce to practice technologies.

## Build Status
| Build server    | Platform       | Status      |
|-----------------|----------------|-------------|
|Travis           | Linux / MacOS  |[![Build Status](https://travis-ci.com/trungcaot/MsCoreOne.svg?branch=master)](https://travis-ci.com/github/trungcaot/MsCoreOne) |
|Azure DevOps           | Linux  |[![Build Status](https://dev.azure.com/trungcaot/MsCoreOne/_apis/build/status/trungcaot.MsCoreOne?branchName=master)](https://dev.azure.com/trungcaot/MsCoreOne/_build/latest?definitionId=1&branchName=master) |

![alt text](/docs/imgs/mscoreone_architecture.png)

## The technologies have implemented as image above.
| Technologies    | Yes/No       |
|-----------------|----------------|
|ASP .NET Core | Yes |
|ASP .NET Core Mvc | Yes |
|Entity Framework Core | Yes |
|Identity Server 4 | Yes |
|Swagger UI | Yes |
|React + Typescript | Yes |
|Sql Server | Yes |
|PostgresQL | No |
|Vue js/Blazor/Angular | No |

# Onion Architecture
![alt text](/docs/imgs/onion_architecture.png)

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
