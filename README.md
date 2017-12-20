# AGL Code Challenge

## Programming challenge
A json web service has been set up at the url: [Json link](http://agl-developer-test.azurewebsites.net/people.json) 

Need to write some code to consume the json and output a list of all the cats in alphabetical order under a heading of the gender of their owner.
```
Example:
- Male
    Angel
    Molly
    Tigger
- Female
    Gizmo
    Jasper
```

## Development Tool and Libraries

* **Development Language:** ASP .NET Core C#
* **Unit Testing Framework:** XUnit
* **Web Service Library:** HttpClient
* **Json Parse Library:** Newtonsoft.Json
* **IDE:** Visual Studio 2017
* **GitHub Link:** [GitHub](https://github.com/zbmahmood2018/agl-code-challenge)

## Description
The solution consist of two projects

* **agl-code-challenge:** Containing Source Code and actual implementation
* **agl-code-challenge.tests:** Containing unit tests

## Pre-Requisites
* Visual Studio 2017 15.3.3 or later
* .NET Core SDK 2.0 or later
* NuGet.Commandline 4.3 or later

## Deployment and Testing
Make sure you have .NET Core installed in your machine See Guide

In command terminal go the same location as the solution and execute the following commands

### Deploy application command
```
dotnet restore
```

### Run unit tests
```
dotnet test
```

## Running the application
In command terminal set the current directory same as project {not solution} and run the following command
```
dotnet run
```
Once the application successfully ran it, open up the broswer and put the URL as [http://localhost:57149](http://localhost:57149) or whatever listening port mentioned in the command line
