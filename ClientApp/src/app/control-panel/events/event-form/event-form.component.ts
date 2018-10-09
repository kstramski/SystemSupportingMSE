import { CompetitionService } from './../../../../services/competition.service';
import { EventService } from './../../../../services/event.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-event-form',
  templateUrl: './event-form.component.html',
  styleUrls: ['./event-form.component.css']
})
export class EventFormComponent implements OnInit {
  title: string = "New Event"

  event: SaveEvent = {
    id: 0,
    name: "",
    description: "",
    eventStarts: "",
    eventEnds: "",
    competitions: [],
  };

  competitions: any;
  competitionsId: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private eventService: EventService,
    private competitionService: CompetitionService,
    private toastr: ToastrService
  ) {
    route.paramMap.subscribe(p => {
      this.event.id = +p.get('id');
    });
  }

  ngOnInit() {
    if (this.event.id > 0)
      this.eventService.getEvent(this.event.id)
        .subscribe(e => {
          this.setEvent(e);
          this.title = "Edit Event";
        });
    this.competitionService.getCompetitions()
      .subscribe(c => {
        this.competitions = c;
      });
  }

  setEvent(e) {
    this.event.id = e.id,
      this.event.name = e.name,
      this.event.description = e.description,
      this.event.eventStarts = e.eventStarts,
      this.event.eventEnds = e.eventEnds,
      e.competitions.forEach(c => {
        this.event.competitions.push(c.id)
      });
  }

  submit() {
    if (this.event.id) {
      this.eventService.update(this.event)
        .subscribe(e => {
          this.toastr.success("Event was successfuly updated.", "Success", { timeOut: 5000 });
          this.router.navigate(['/panel/events/', this.event.id]);
        });
    } else {
      this.eventService.create(this.event)
        .subscribe(e => {
          this.toastr.success("Event was successfuly added.", "Success", { timeOut: 5000 });
          this.router.navigate(['/panel/events']);
        });
    }
  }

  addCompetition(id) {
    var c = parseInt(id);
    if (isNaN(c))
      return;
    console.log(c);
    console.log(this.event.competitions);
    if (!this.event.competitions.includes(c)) {
      console.log(!this.event.competitions.includes(c));

      this.event.competitions.push(c);
    }

  }

  removeCompetition(id) {
    console.log(id);
    var index = this.event.competitions.indexOf(id);
    this.event.competitions.splice(index, 1);
  }

}

export interface SaveEvent {
  id: number,
  name: string,
  description: string,
  eventStarts: string,
  eventEnds: string,
  competitions: Array<number>,
}