import { AuthService } from './../services/auth.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  invalidLogin: boolean;

  constructor(
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
  }

  login(form: NgForm) {
    this.authService.login(form.value)
    .subscribe(response => {
      var token = (<any>response).token;
      localStorage.setItem('access_token', token);
      this.invalidLogin = false;
      this.toastr.success("You have successfully logged in.", "Success", { timeOut: 5000 });
      this.router.navigate(['']);
    }, err => {
      this.invalidLogin = true;
    });
  }

}
