import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class AuthService {

    constructor(
        private http: HttpClient,
        private jwtHelper: JwtHelperService
        ) { }

    login(credencials) {
        var body = JSON.stringify(credencials);
        return this.http.post("/api/users/login", body, httpOptions);
    }

    logout() {
        localStorage.removeItem("access_token");

    }

    getToken() {
        return this.jwtHelper.tokenGetter();
    }

    getUserId() {
        var decodedToken = this.jwtHelper.decodeToken(this.getToken());
        return decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
    }

    getUserName() {
        var decodedToken = this.jwtHelper.decodeToken(this.getToken());
        return decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
    }

    isLoggedIn() {
        var token = this.getToken();

        if(token && !this.jwtHelper.isTokenExpired(token)) {
            return true;
        }
        return false;
    }
}