import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { EventService } from '../../../../services/event.service';

@Component({
  selector: 'app-event-view',
  templateUrl: './event-view.component.html',
  styleUrls: ['./event-view.component.css']
})
export class EventViewComponent implements OnInit {
  event: any;
  eventId: number;

  columns: Array<any> = [
    { title: "#", size: 1, center: true },
    { title: "Name", size: 6 },
    { title: "Competitiors", size: 3, center: true },
    { title: "Action", size: 2, center: true }
  ];

  modal: Array<string> = [
    "Delete Event",
    "Are you sure want to delete this event?"
  ];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private eventService: EventService,
    private toastr: ToastrService
  ) {
    route.paramMap.subscribe(p => {
      this.eventId = +p.get('id');
      if (isNaN(this.eventId) && this.eventId == undefined) {
        router.navigate(['/panel/events']);
        return;
      }
    });
  }

  ngOnInit() {
    this.eventService.getEvent(this.eventId)
      .subscribe(e => {
        this.event = e;
      });
  }

  delete(id) {
    this.eventService.remove(id)
      .subscribe(e => {
        this.toastr.success("Event was successfully removed.", "Success", { timeOut: 5000 });
        this.router.navigate(["/panel/events"]);
      });
  }

}
