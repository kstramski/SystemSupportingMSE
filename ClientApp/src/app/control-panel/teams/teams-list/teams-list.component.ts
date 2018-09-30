import { TeamService } from './../../../../services/team.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-teams-list',
  templateUrl: './teams-list.component.html',
  styleUrls: ['./teams-list.component.css']
})
export class TeamsListComponent implements OnInit {
  queryResult: any;
  columns: Array<any> = [
    {title: 'Id'},
    {title: 'Team'},
    {title: 'Action'},
  ];

  constructor(
    private router: Router,
    private teamService: TeamService
  ) { }

  ngOnInit() {
    this.teamService.getTeams()
      .subscribe(t => {
        this.queryResult = t;
      });
  }
}
