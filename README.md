
 # MsCoreOne - Simple Ecomerce

<br/>

This is a simple ecommerce to practice technologies.

# Onion Architecture
![alt text](/docs/imgs/onion_architecture.png)

## Technologies and frameworks used
* .NET Core 3.1
* ASP .NET Core 3.1
* Entity Framework Core 3.1
* Swagger UI
* IdentityServer4
* MediatR
* FluentValidation
* XUnit
* React + typescript + Redux
* Material UI


## Getting Started

Welcome to MsCoreOne!


# Store demo on Azure
 - [Front-office](https://mscoreonemvc.azurewebsites.net/): Mscoreone - mvc
 - [Back-office](https://mscoreone.z23.web.core.windows.net/): Mscoreone - react
 - [Swagger-api](https://mscoreone.azurewebsites.net/): Mscoreone - api


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
