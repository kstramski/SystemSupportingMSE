import { TeamService } from './../../../../services/team.service';
import { Component, OnInit } from '@angular/core';
import * as feather from 'feather-icons';

@Component({
  selector: 'app-teams-list',
  templateUrl: './teams-list.component.html',
  styleUrls: ['./teams-list.component.css']
})
export class TeamsListComponent implements OnInit {
  private readonly PAGE_SIZE = 10;
  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE
  };

  columns: Array<any> = [
    { title: 'Id', size: 1, center: true },
    { title: 'Team Name', key: 'name', isSortable: true, size: 8 },
    { title: 'In Team', size: 1, center: true },
    { title: 'Action', size: 2, center: true },
  ];

  constructor(
    private teamService: TeamService
  ) { }

  ngOnInit() {
    this.populateTeams()
  }

  private populateTeams() {
    this.teamService.getTeams(this.query)
      .subscribe(result => {
        this.queryResult = result;
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
    this.populateTeams();
  }

  onChangePage(page) {
    this.query.page = page;
    this.populateTeams();
  }
}
