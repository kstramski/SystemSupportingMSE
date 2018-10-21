import { Component, OnInit } from '@angular/core';
import { RoleService } from 'src/services/role.service';

@Component({
  selector: 'app-roles-list',
  templateUrl: './roles-list.component.html',
  styleUrls: ['./roles-list.component.css']
})

export class RolesListComponent implements OnInit {
  roles: any;
  columns: Array<any> = [
    { title: "Id", size: 1, center: true },
    { title: "Role", size: 3 },
    { title: "Description", size: 6 },
    { title: "Action", size: 2, center: true }
  ];

  constructor(private roleService: RoleService) { }

  ngOnInit() {
    this.roleService.getRoles()
      .subscribe(r => {
        this.roles = r;
      });
  }
}