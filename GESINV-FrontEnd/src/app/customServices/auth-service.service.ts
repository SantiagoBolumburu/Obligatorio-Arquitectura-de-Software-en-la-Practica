import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { ApiRequestService } from './api-request.service';
import { tap } from 'rxjs/operators';
import { TokenOut } from '../models/out/tokenOut';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {
  private _isLoggedIn$ = new BehaviorSubject<boolean>(false);
  isLoggedIn$ = this._isLoggedIn$.asObservable();

  constructor(private apiService: ApiRequestService) {
    const token = localStorage.getItem('auth_token');
    this._isLoggedIn$.next(!!token);
  }
/*
  login(username: string, password: string) {
    return this.apiService.Post_Session(username, password).pipe(
      tap((response: TokenOut) => {
        this._isLoggedIn$.next(true);
        localStorage.setItem('auth_token', response.token);
      })
    );
  }*/
}
//import jwt_decode from 'jwt-decode';
