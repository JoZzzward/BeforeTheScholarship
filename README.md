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
Test email is: jozzzwardtm@mail.ru
3. File ```emailpassword.txt``` should contain the specific password for sending out messages.
Test password is: vNRJB1qegA5WnpfKDMX8

> Email and password must be presented in first line without any spaces or new lines

---
# How to launch the project

1. Fill ```emailusername.txt``` with information about the email from which all letters to the site will come.

2. File ```emailpassword.txt``` with information about the specific password for sending out messages.

3. Build the project with ```docker-compose build``` command.

4. Open main project folder in terminal and than run ```docker-compose up``` command.

After all off this steps project will run currectly.

> If your WebAPI dont start while initializing database you can run WebAPI again and it will start correctly

# How to communicate with project

* Access to WebAPI from Swagger by this address [http://localhost:7000/api/index.html](http://localhost:7000/api/index.html)
* For authorization in Swagger use next information:  
    * ClientId is: ```frontend```
    * ClientSecret specified in ```clientsecret.txt```

---

# XUnit and Integration tests

1. XUnit tests passing all just in one time launch.

2. Integration tests. 
    Advise: Ideally integration tests must be launched one by one. In other case you may get unpredictable errors.

---
# How to communicate with Blazor WebAssembly Web UI component:

1. Register in application by username or email and password and than you can sign in and do your work.

2. Initialize ```emailusername.txt``` and ```emailpassword.txt``` files correct by using test data from <Shared information> module 
