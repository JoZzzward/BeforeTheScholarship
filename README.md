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

>Tested information included in specific files.

> Email and password must be presented in first line without any spaces or new lines

---
# How to launch the project

1. Fill ```emailusername.txt``` with information about the email from which all letters to the site will come.
> Email must be ```some-work-testing@mail.ru``` or other if you prefer.

2. File ```emailpassword.txt``` with information about the specific password for sending out messages.
> Email password must be ```5DMVKn6tTJaBMD1df5he``` or other if you prefer.

3. Open main project folder in terminal and than build the project with ```docker-compose build``` command.

4. Run project by ```docker-compose up``` command.

After all off this steps project will run currectly.

> If your WebAPI dont start while initializing database you can run WebAPI again and it will start correctly

# How to communicate with project

* Access to WebAPI from Swagger by this address [http://localhost:7000/api/index.html](http://localhost:7000/api/index.html)
* For authorization in Swagger use next information:  
    * ClientId is: ```frontend```
    * ClientSecret specified in ```clientsecret.txt```

---
# XUnit and Integration tests

1. Make sure ```DbInitializer.Execute(app.Services);``` in ```BeforeTheScholarship.Api``` project is disabled otherwise integration tests will not run.
> Just comment this it's enough.

2. XUnit tests passing all just in one time launch.

3. Integration tests. 
    Advise: Ideally integration tests must be launched one by one. In other case you may get unpredictable errors.

---
# How to communicate with Blazor WebAssembly Web:

Url: [http://localhost:7002/](http://localhost:7002/)

1. Register in application by username or email and password and than you can sign in and do your work.

2. Make sure ```emailusername.txt``` and ```emailpassword.txt``` files are not empty.

---
