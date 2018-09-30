import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import * as feather from 'feather-icons';

@Component({
  selector: 'app-control-panel',
  template: `
<div *ngIf="auth.isLoggedIn()">
  <app-navbar></app-navbar>
  <div class="container-fluid">
      <div class="row navbar-expand-md">
          <app-nav-menu></app-nav-menu>
          <main role="main" class="col-md-9 ml-sm-auto col-lg-10 pt-3 px-4">
              <router-outlet></router-outlet>
          </main>
      </div>
  </div>
</div>
<div *ngIf="!auth.isLoggedIn()">
  <panel-login></panel-login>
</div>`
})
export class ControlPanelComponent {

  constructor(private auth: AuthService) { }

  ngAfterViewInit() {
    feather.replace();
  }

}
