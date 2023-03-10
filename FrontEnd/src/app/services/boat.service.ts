import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { User } from '../models/user';
import {Boat} from "../models/boat";

@Injectable({
  providedIn: 'root',
})
export class BoatService {
  constructor(private http: HttpClient) {}

  public add(boat: Boat): Observable<any> {
    return this.http.post<any>(
      'https://localhost:7114/api/Boat/add',
      boat, {observe: 'response'}
    );
  }

  public edit(boat: Boat): Observable<any> {
    return this.http.put<any>(
      'https://localhost:7114/api/Boat/edit',
      boat, {observe: 'response'}
    );
  }

  public delete(boat: Boat): Observable<any> {
    return this.http.post<any>(
      'https://localhost:7114/api/Boat/delete',
      boat, {observe: 'response'}
    );
  }
}
