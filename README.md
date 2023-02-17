# BoatApp

<img src="" width=500>
<p>BoatApp is a simple web application that allows users to manage a list of boats. Users must log in before they can access the boat list, and only authenticated users can add, update, or delete boats.</p>

## Technologies used

<li>Angular</li>
<li>Material UI</li>
<li>.NET Core</li>
<li>PostgreSQL</li>
<li>Docker</li>

## Features
### Authentication / Authorization
BoatApp uses JWTs for authentication and authorization. 
Users must provide a valid username and password to obtain a token, which is then included in the Authorization header of subsequent requests.
The API validates the token to ensure that only authenticated users can access the boat resources.

## Getting started

Clone the repository and run:
```
docker-compose up --build
```
This will build and start the necessary containers for the project. Once the containers are running, you can access the application in your web browser at http://localhost:4200.

## Licence
This project is realeased under the MIT Licence.


