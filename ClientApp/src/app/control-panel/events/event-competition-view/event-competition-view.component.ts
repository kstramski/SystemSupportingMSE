import { EventService } from './../../../../services/event.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-event-competition-view',
  templateUrl: './event-competition-view.component.html',
  styleUrls: ['./event-competition-view.component.css']
})
export class EventCompetitionViewComponent implements OnInit {
  eventId: number;
  competitionId: number;
  competitionInfo: any;
  participants: any;
  stages: Array<any> = [];
  groups: Array<Array<number>> = [];

  query: any = {};

  columns: Array<any> = [
    { title: "#", size: 1, isSortable: false },
    { title: "Name", size: 3, isSortable: true },
    { title: "Surname", size: 3, isSortable: true },
    { title: "Result", size: 3, isSortable: true },
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
    this.eventService.getEventCompetitionParticipants(this.eventId, this.competitionId)
      .subscribe(e => {
        this.participants = e;
        this.setStages(this.participants);
        delete this.participants;
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
    for (let i = 0; i < stages[stages.length-1].id; i++) {
      this.groups.push([]);   
    }
    stages.forEach(stage => {
      var map = new Map();
      participants.forEach(participant => {
        if (!map.has(participant.groupId) && participant.stage.id == stage.id) {
          map.set(participant.groupId, true);
          this.groups[stage.id-1].push(participant.groupId);
        }
      });
    });

  }

  onStageChange() {
    this.populateParticipants();
  }

  onGroupChange() {

  }

  private populateParticipants() {
    this.eventService.getEventCompetitionParticipants(this.eventId, this.competitionId)
      .subscribe(e => {
        this.participants = e;
      });
  }

}
