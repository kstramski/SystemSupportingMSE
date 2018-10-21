import { CompetitionService } from './../../../../services/competition.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-competitions-list',
  templateUrl: './competitions-list.component.html',
  styleUrls: ['./competitions-list.component.css']
})
export class CompetitionsListComponent implements OnInit {
  competitions: any;
  rowNumber: number = 5;
  columns: Array<any> = [
    { title: "Id", size: 1, center: true },
    { title: "Name", size: 5 },
    { title: "Groups", size: 2, center: true },
    { title: "Group Size", size: 2, center: true },
    { title: "Action", size: 2, center: true },
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
