# BoatApp

<div align="center">
<img src="https://user-images.githubusercontent.com/105823790/219724872-65cf02fb-337b-4cfc-9725-6a9d25160e44.png" width=600>
  
<p><br>BoatApp is a simple web application that allows users to manage a list of boats.<br>Users must log in before they can access the boat list, and only authenticated users can add, update, or delete boats.</p>
 </div> 

## Features
### Authentication / Authorization
BoatApp uses JWTs for authentication and authorization. 
Users must provide a valid username and password to obtain a token, which is then included in the Authorization header of subsequent requests.
The API validates the token to ensure that only authenticated users can access the boat resources.

## Getting started

Clone the repository and run:
```
docker-compose up
```
This will build and start the necessary containers for the project. Once the containers are running, you can access the application in your web browser at http://localhost:4200.

## Technologies used

<ul>
<li>Angular</li>
<li>Material UI</li>
<li>.NET Core</li>
<li>PostgreSQL</li>
<li>Docker</li>
</ul>

## Licence
This project is realeased under the MIT Licence.


