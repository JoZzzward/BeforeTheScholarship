# BeforeTheScholarship Project
---
# Get started

In this session, you'll setup your machine for WebAPI and Blazor development.

## Setup

#### WebAPI Setup
To get started with WebAPI, follow the instructions with [this link](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-7.0&tabs=visual-studio).

#### Blazor Setup
To get started with Blazor, follow the instructions with [this link](https://aka.ms/blazor-getting-started).

#### Docker Setup
If you not familiar with Docker, follow [this link](https://docs.docker.com/get-docker/) for review and download.

---

# Shared information 

1. File ```clientsecret.txt``` contains ClientSecret for IdentityServer4 Configuration.

2. File ```emailusername.txt``` should contain the email from which all letters to the site will come.

3. File ```emailpassword.txt``` should contain the specific password for sending out messages.

---
# How to launch the project

1. Fill ```emailusername.txt``` with information about the email from which all letters to the site will come.

2. File ```emailpassword.txt``` with information about the specific password for sending out messages.

3. Open main project folder in terminal and than run ```docker-compose up``` command.

After all off this steps project will run currectly.

> If you want to debug separate service you should run all services in docker and than stop specific service and launch him in IDE. Services support separate debug configuration.

# How to communicate with project

* Access to WebAPI from Swagger by this address [http://localhost:7000/api/index.html](http://localhost:7000/api/index.html)
* For authorization in Swagger use next information:  
    * ClientId is: ```frontend```
    * ClientSecret specified in ```clientsecret.txt```
