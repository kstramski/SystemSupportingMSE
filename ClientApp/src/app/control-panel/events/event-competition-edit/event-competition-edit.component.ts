import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EventService } from '../../../../services/event.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-event-competition-edit',
  templateUrl: './event-competition-edit.component.html',
  styleUrls: ['./event-competition-edit.component.css']
})
export class EventCompetitionEditComponent implements OnInit {
  title: string = "";

  event: SaveEventCompetition = {
    eventId: 0,
    competitionId: 0,
    registrationStarts: "",
    registrationEnds: "",
    competitionDate: "",
    timePerGroup: "",
    groupRequired: false,
  };
  ecDate: Array<any> = [[], [], []];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private eventService: EventService,
    private toastr: ToastrService
  ) {
    route.paramMap.subscribe(p => {
      this.event.eventId = +p.get('eventId');
      this.event.competitionId = +p.get('competitionId');
      if (this.event.eventId <= 0 || isNaN(this.event.eventId)
        || this.event.competitionId <= 0 || isNaN(this.event.competitionId)) {
        router.navigate(['/panel/events']);
        return;
      }
    });
  }

  ngOnInit() {
    this.eventService.getEventCompetition(this.event.eventId, this.event.competitionId)
      .subscribe(e => {
        this.setEventCompetition(e);
        console.log(this.ecDate);

      });
  }

  setEventCompetition(e) {
    this.event.eventId = e.event.id;
    this.event.competitionId = e.competition.id;
    this.event.timePerGroup = e.timePerGroup;
    this.event.groupRequired = e.groupRequired;

    this.ecDate[0] = e.registrationStarts.split('T');
    this.ecDate[0][1] = this.ecDate[0][1].split('.')[0];
    this.ecDate[1] = e.registrationEnds.split('T');
    this.ecDate[2] = e.competitionDate.split('T');

    this.title = e.competition.name;
  }

  submit() {
    this.event.registrationStarts = this.ecDate[0].join('T');
    this.event.registrationEnds = this.ecDate[1].join('T');
    this.event.competitionDate = this.ecDate[2].join('T');
    console.log(this.event);
    
    this.eventService.updateEventCompetition(this.event)
      .subscribe(e => {
        this.toastr.success("Competition was successfully updated", "Success", { timeOut: 5000 });
        this.router.navigate(['/panel/events/', this.event.eventId, 'competitions', this.event.competitionId]);
      });
  }

}

export interface SaveEventCompetition {
  eventId: number;
  competitionId: number;
  registrationStarts: string;
  registrationEnds: string;
  competitionDate: string;
  timePerGroup: string;
  groupRequired: boolean;
}