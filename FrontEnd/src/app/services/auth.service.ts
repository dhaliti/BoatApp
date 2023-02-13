import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient) {}

  httpOptions =  new HttpHeaders({
      'Content-Type': 'application/json',
      "Access-Control-Allow-Origin": "*",
    'Accept': 'text/html, application/xhtml+xml, */*',
    });

  public register(user: User): Observable<any> {
    console.log(user.username);
    return this.http.post('https://localhost:7114/api/Auth/register', user, {headers: this.httpOptions});
  }

  public login(user: User): Observable<any> {
    console.log(user.username);
    return this.http.post('https://localhost:7114/api/Auth/login', user, {headers: this.httpOptions, observe: 'response'});
  }

  public getBoats(): Observable<any> {
    return this.http.get('https://localhost:7114/api/Auth', {headers: this.httpOptions, observe: 'response'});
  }
}
