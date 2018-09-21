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
        console.log(body);
        return this.http.post("/api/users/login", body, httpOptions);
    }

    logout() {
        localStorage.removeItem("access_token");

    }

    getToken() {
        return this.jwtHelper.tokenGetter();
    }

    isLoggedIn() {
        var token = this.getToken();

        if(token && !this.jwtHelper.isTokenExpired(token)) {
            return true;
        }
        return false;
    }
}