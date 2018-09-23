import { NavbarService } from './../../services/navbar.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { AuthService } from './../../services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {

  constructor(
    private auth: AuthService,
    private router: Router,
    private toastr: ToastrService,
    private navbar: NavbarService
  ) { }

  profile() {
    this.router.navigate(['/users/', this.auth.getUserId()]);
  }

  messages() {

  }

  logout() {
    this.auth.logout();
    this.toastr.success("User has been successfully logged out.", "Success", { timeOut: 5000 });
    this.router.navigate(['/login']);
  }


}
