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
  query: any = {};

  columns: Array<any> = [
    {title: "#",       size: 1, isSortable: false},
    {title: "Name",    size: 3, isSortable: true},
    {title: "Surname", size: 3, isSortable: true},
    {title: "Result",  size: 3, isSortable: true},
    {title: "Action",  size: 2, isSortable: false},
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
      .subscribe(ec => {
        this.competitionInfo = ec;
      });
  }

  onStageChange() {

  }

  onGroupChange() {
    
  }

}
