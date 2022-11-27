import { Injectable } from '@angular/core';
import { HttpClient, HttpEvent, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class InvitationService {
  private env = environment;
  readonly apiUrl = this.env.apiService;

  constructor(private http: HttpClient) { }

  // Users
  getUsers(): Observable<any> {
    console.log(this.apiUrl );
    return this.http.get<any[]>('https://localhost:5001/api/user/user-dropdown');
  }

  createUser(user: any): Observable<any> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<any>('https://localhost:5001/api/user', user, httpOptions);
  }

  createInvitaion(invitation: any): Observable<any> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<any>('https://localhost:5001/api/invitation/create', invitation, httpOptions);
  }
}
