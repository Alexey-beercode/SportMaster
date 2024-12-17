import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { UserDto } from '../models/dtos-response';
import { UpdateUserRequestDTO } from '../models/dtos-request';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private readonly baseUrl = `${environment.baseUrl}/api/users`;

  constructor(private http: HttpClient) {}

  getUserById(userId: string): Observable<UserDto> {
    return this.http.get<UserDto>(`${this.baseUrl}/${userId}`);
  }

  updateUser(userId: string, updateUserRequest: UpdateUserRequestDTO): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${userId}`, updateUserRequest);
  }
}
