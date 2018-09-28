import { NavbarService } from './../../../services/navbar.service';
import { Router } from '@angular/router';
import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  

  constructor(
    private router: Router,
    private navbar: NavbarService
  ) {}

}
