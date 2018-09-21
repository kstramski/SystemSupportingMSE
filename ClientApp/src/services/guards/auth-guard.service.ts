import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';

@Injectable()
export class AuthGuard {

    constructor(
        private auth: AuthService,
        private router: Router
    ) { }

    canActivate() {
        if(this.auth.isLoggedIn()) {
            return true;
        }
        localStorage.removeItem("access_token");
        this.router.navigate(['/login']);
        return false;
    }
}