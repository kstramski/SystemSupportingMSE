import { EventService } from './../../../../services/event.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import * as feather from 'feather-icons';

@Component({
  selector: 'app-event-competition-view',
  templateUrl: './event-competition-view.component.html',
  styleUrls: ['./event-competition-view.component.css']
})
export class EventCompetitionViewComponent implements OnInit {
  private readonly PAGE_SIZE = 10;
  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE,
    paging: false,
  };
  eventId: number;
  competitionId: number;
  competitionInfo: any;
  stages: Array<any> = [];
  groups: Array<Array<number>> = [];

  columns: Array<any> = [
    { title: "#", size: 1, isSortable: false },
    { title: "Name", size: 3, key: 'name', isSortable: true },
    { title: "Surname", size: 3, key: 'surname', isSortable: true },
    { title: "Result", size: 3, key: 'result', isSortable: true },
    { title: "Action", size: 2, isSortable: false },
  ];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private eventService: EventService,
  ) {
    route.paramMap.subscribe(p => {
      this.eventId = +p.get('eventId');
      this.competitionId = +p.get('competitionId');
      if (isNaN(this.eventId) || isNaN(this.competitionId)
        || this.eventId <= 0 || this.competitionId <= 0) {
        router.navigate(['/panel/events']);
        return;
      }
    });
  }

  ngOnInit() {
    this.eventService.getEventCompetition(this.eventId, this.competitionId)
      .subscribe(e => {
        this.competitionInfo = e;
      });
    this.eventService.getEventCompetitionParticipants(this.eventId, this.competitionId, this.query)
      .subscribe(e => {
        this.queryResult = e;
        if (this.queryResult.totalItems)
          this.setStages(this.queryResult.items);
        this.queryResult = {};
        this.query.paging = true;
      });
  }

  setStages(participants) {
    var map = new Map();
    participants.forEach(participant => {
      if (!map.has(participant.stage.id)) {
        map.set(participant.stage.id, true);
        this.stages.push(participant.stage);
      }
    });
    this.setGroups(participants, this.stages);
  }

  setGroups(participants, stages) {
    for (let i = 0; i < stages[stages.length - 1].id; i++) {
      this.groups.push([]);
    }
    stages.forEach(stage => {
      var map = new Map();
      participants.forEach(participant => {
        if (!map.has(participant.groupId) && participant.stage.id == stage.id) {
          map.set(participant.groupId, true);
          this.groups[stage.id - 1].push(participant.groupId);
        }
      });
    });

  }

  onStageChange() {
    this.populateParticipants();
    console.log(this.query);

  }

  onGroupChange() {

  }

  private populateParticipants() {
    this.eventService.getEventCompetitionParticipants(this.eventId, this.competitionId, this.query)
      .subscribe(e => {
        this.queryResult = e;
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
    this.populateParticipants();
  }

  onChangePage(page) {
    this.query.page = page;
    this.populateParticipants();
  }


}
