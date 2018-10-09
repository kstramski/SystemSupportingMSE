import { EventService } from './../../../../services/event.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-events-list',
  templateUrl: './events-list.component.html',
  styleUrls: ['./events-list.component.css']
})
export class EventsListComponent implements OnInit {
  events: any;

  columns: Array<any> = [
    {title: "Id", size: 1},
    {title: "Name", size: 5},
    {title: "Event Starts", size: 2},
    {title: "Event Ends", size: 2},
    {title: "Competitons", size: 1},
    {title: "Action", size: 1},
  ];

  constructor(
    private eventService: EventService
  ) { }

  ngOnInit() {
    this.eventService.getEvents()
      .subscribe(e => {
        this.events = e;
      });
  }

}
