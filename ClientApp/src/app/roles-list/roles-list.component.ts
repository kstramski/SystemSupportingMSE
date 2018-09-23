import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RoleService } from '../../services/role.service';

@Component({
  selector: 'app-roles-list',
  templateUrl: './roles-list.component.html',
  styleUrls: ['./roles-list.component.css']
})

export class RolesListComponent implements OnInit {
  roles: any;
  columns: Array<any> = [
    {title: "Id"},
    {title: "Role"},
    {title: "Description"},
    {title: "Action"}
  ]

  constructor(
    private router: Router,
    private roleService: RoleService
  ) { }

  ngOnInit() {
    this.roleService.getRoles()
      .subscribe(r => {
        this.roles = r;
      });
  }
}