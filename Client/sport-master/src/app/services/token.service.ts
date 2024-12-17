import { Injectable } from '@angular/core';
import { catchError, map, Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from "../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  private readonly ACCESS_TOKEN_KEY = 'accessToken';
  private readonly USER_ID_KEY = 'userId';
  private readonly baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) {}

  setTokens(accessToken: string, userId: string): void {
    localStorage.setItem(this.ACCESS_TOKEN_KEY, accessToken);
    localStorage.setItem(this.USER_ID_KEY, userId);
  }

  getAccessToken(): string | null {
    return localStorage.getItem(this.ACCESS_TOKEN_KEY);
  }

  getUserId(): string | null {
    return localStorage.getItem(this.USER_ID_KEY);
  }

  clearTokens(): void {
    localStorage.removeItem(this.ACCESS_TOKEN_KEY);
    localStorage.removeItem(this.USER_ID_KEY);
  }

  isLoggedIn(): Observable<boolean> {
    return this.checkTokenStatus();
  }

  checkTokenStatus(): Observable<boolean> {
    const accessToken = this.getAccessToken();
    console.log('Checking token status with token:', accessToken);

    if (!accessToken) {
      console.log('No token found');
      this.clearTokens();
      return of(false);
    }

    return this.http.get(`${this.baseUrl}/api/auth/token_status`, {
      headers: { 'Authorization': `Bearer ${accessToken}` },
      observe: 'response',
      responseType: 'text'
    }).pipe(
      map(response => {
        console.log('Token status response:', response);
        return response.status === 200;
      })
    );
  }


}
