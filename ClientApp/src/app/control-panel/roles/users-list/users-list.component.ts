import { Component, OnInit } from '@angular/core';
import { RoleService } from 'src/services/role.service';
import * as feather from 'feather-icons';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.css']
})
export class UsersListComponent implements OnInit {
  private readonly PAGE_SIZE = 10;
  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE,
    roleId: 1,
  };
  roles: any;
  columns: Array<any> = [
    { title: "Id", size: 1, center: true },
    { title: "Name", key: 'name', isSortable: true, size: 2 },
    { title: 'Surname', key: 'surname', isSortable: true, size: 3 },
    { title: "Email", key: 'email', isSortable: true, size: 4 },
    { title: 'Action', size: 2, center: true }
  ]
  constructor(private roleService: RoleService) { }

  ngOnInit() {
    this.roleService.getRoles()
      .subscribe(r => {
        this.roles = r;
      });
    this.populateUsers();
  }

  onFilterChange(id) {
    this.query.roleId = +id;
    console.log(this.query);

    this.populateUsers();
  }

  private populateUsers() {
    this.roleService.getUsers(this.query)
      .subscribe(u => {
        this.queryResult = u;
        console.log(this.queryResult);

      });
  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName)
      this.query.isSortAscending = !this.query.isSortAscending;
    else {
      this.query.isSortAscending = true;
      this.query.sortBy = columnName;
    }
    feather.replace();
    this.populateUsers();
  }

  onChangePage(page) {
    this.query.page = page;
    this.populateUsers();
  }

}
