# Question Answering Application

This application allow you to post questions and answers within tags, to use this application you should authenticate to start using the APIs, once you run the application, it will launch Swagger Docs for easy reading and testing.

Before running you should modify the connection string in appsettings.json file, at first run, the database will seed some users with below details to get access token and be able to use the APIs.

|User name   | Password  |
|---|---|
| test.user  | test  |
| test.user2  | test2  |
| test.user3  | test3  |

For bulk import, there is an endpoint to download the template, and then you can modify the file downloaded and then import using the API.

# Technologies used
<ul>
    <li>.Net Core 7</li>
    <li>Entity Framework core</li>
    <li>MediatR</li>
    <li>AutoMapper</li>
    <li>Dependency Injection</li>
    <li>CQRS</li>
    <li>JWT</li>
</ul>