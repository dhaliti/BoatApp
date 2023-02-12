import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient) {}

  public register(user: User): Observable<any> {
    return this.http.post<any>(
      'https://localhost:7114/api/Auth/register',
      user
    );
  }

  public login(user: User): Observable<string> {
    return this.http.post('https://localhost:7114/api/Auth/login', user, {
      responseType: 'text',
    });
  }

  public getBoats(): Observable<string> {
    return this.http.get('https://localhost:7114/api/Auth', {
      responseType: 'text',
    });
  }
}
