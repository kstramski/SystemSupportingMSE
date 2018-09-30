import { UserService } from './../../../../services/user.service';
import { Component, OnInit } from '@angular/core';
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
    pageSize: this.PAGE_SIZE
  };
  columns: any = [
    { title: 'Id', size: 1 },
    { title: 'Name', key: 'name', isSortable: true, size: 2 },
    { title: 'Surname', key: 'surname', isSortable: true, size: 3 },
    { title: 'Last Login', key: 'lastLogin', isSortable: true, size: 5 },
    { title: 'Action', size: 1 }
  ];

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.populateUsers();
  }

  private populateUsers() {
    this.userService.getUsers(this.query)
      .subscribe(u => {
        this.queryResult = u;
      });
  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName)
      this.query.isSortAscending = !this.query.isSortAscending;
    else {
      this.query.isSortAscending = true;
      this.query.sortBy = columnName;
    }
    console.log(this.query.isSortAscending);
    feather.replace();
    this.populateUsers();
  }

  onChangePage(page) {
    this.query.page = page;
    this.populateUsers();
  }
}
