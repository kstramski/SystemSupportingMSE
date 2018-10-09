import { CompetitionService } from './../../../../services/competition.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-competitions-list',
  templateUrl: './competitions-list.component.html',
  styleUrls: ['./competitions-list.component.css']
})
export class CompetitionsListComponent implements OnInit {
  competitions: any;

  columns: Array<any> = [
    {title: "id", size: 1},
    {title: "Name", size: 5},
    {title: "Groups", size: 2},
    {title: "Group Size", size: 2},
    {title: "Action", size: 2},
  ];

  constructor(
    private competitionService: CompetitionService
  ) { }

  ngOnInit() {
    this.competitionService.getCompetitions()
      .subscribe(c => {
        this.competitions = c;
      });
  }

}
