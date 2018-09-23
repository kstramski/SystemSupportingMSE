import { Injectable } from '@angular/core';

@Injectable()
export class NavbarService {
  isExpandedSidenav: boolean = false;
  isExpandedNav: boolean = false;

  constructor() { }

  collapse() {
    this.isExpandedSidenav = false;
    this.isExpandedNav = false;
  }

  toggleSidenav() {
    this.isExpandedSidenav = !this.isExpandedSidenav;
    this.isExpandedNav = false;
  }

  toggleNav() {
    this.isExpandedNav = !this.isExpandedNav;
    this.isExpandedSidenav = false;
  }
}