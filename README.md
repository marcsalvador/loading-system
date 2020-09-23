# Getting Started with ASP.NET Web API and Angular CLI

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) and ASP.NET Web API

## Usage

A sample web application about card loading system for train, with origin and destination with corresponding travel fare.

## Technology

### Database
 - Microsoft SQL Server 2012

### API
 - ASP.NET Web Framework 4.6
 - Entity Framework 4.6
 - ADO.NET Entity Data Model

### Web Application
 - Angular 10
 - Angular Material 
 - Bootstrap
 - ngx-spinner

## Installation and Configuration

### Database Setup
1. Restore database (LoadingSystem.bak)
2. Remember SQL Credential and database name for Web API Setup

### Web API (develop using .NET Framework 4.6)
1. Open LoadingSystem.sln in Visual Studio 2019
2. Open Solution Explorer
3. Righ click to the Solution and click Restore NuGet Packages
4. Go to web.config and change the connection settings of the database
4. Build API
5. Setup it to IIS and get the url for Angular APP Setup

### Angular APP
1. Open LoadingSystem.App on Visual Code
2. Open a terminal window and run the following command: npm install
3. After the packages installation open the file 'src/app/api/customer.service.ts' and change the baseUrl variable value to your API URL
4. Run the following comman on terminal: ng serve 
