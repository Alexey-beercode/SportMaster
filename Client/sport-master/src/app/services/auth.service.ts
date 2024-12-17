// src/app/services/auth.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { TokenService } from './token.service';
import { Router } from '@angular/router';
import {LoginDTO, UserRequestDTO} from "../models/dtos-request";
import {AuthResponseDTO} from "../models/dtos-response";

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly apiUrl = `${environment.baseUrl}/api/auth`;

  constructor(
    private http: HttpClient,
    private tokenService: TokenService,
    private router: Router
  ) {}

  login(loginDto: LoginDTO): Observable<AuthResponseDTO> {
    return this.http
      .post<AuthResponseDTO>(`${this.apiUrl}/login`, loginDto)
      .pipe(
        tap(response => {
          this.tokenService.setTokens(response.accessToken, response.userId.toString());
        })
      );
  }

  register(registerDto: UserRequestDTO): Observable<AuthResponseDTO> {
    return this.http
      .post<AuthResponseDTO>(`${this.apiUrl}/register`, registerDto)
      .pipe(
        tap(response => {
          this.tokenService.setTokens(response.accessToken, response.userId.toString());
        })
      );
  }

  logout(): void {
    this.tokenService.clearTokens();
    this.router.navigate(['/login']);
  }
}
