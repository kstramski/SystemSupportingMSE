import { UserService } from './../../services/user.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.css']
})
export class UsersListComponent implements OnInit {
  users: any;
  columns: any =[
    {title: 'Id'},
    {title: 'Name'},
    {title: 'Surname'},
    {title: 'Last Login'},
    {title: 'View'}
  ];

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.userService.getUsers()
    .subscribe(u => {
      this.users = u;
    });
  }

  

}
