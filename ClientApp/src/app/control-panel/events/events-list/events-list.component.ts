import { EventService } from './../../../../services/event.service';
import { Component, OnInit } from '@angular/core';
import * as feather from 'feather-icons';

@Component({
  selector: 'app-events-list',
  templateUrl: './events-list.component.html',
  styleUrls: ['./events-list.component.css']
})
export class EventsListComponent implements OnInit {
  private readonly PAGE_SIZE = 10;
  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE
  };
  showSpinner: boolean = true;

  columns: Array<any> = [
    { title: "Id", size: 1, center: true },
    { title: "Name", size: 5, key: 'name', isSortable: true },
    { title: "Event Starts", size: 2, key: 'eventStarts', isSortable: true },
    { title: "Event Ends", size: 2, key: 'eventEnds', isSortable: true },
    { title: "Competitons", size: 1, center: true },
    { title: "Action", size: 1, center: true },
  ];

  constructor(
    private eventService: EventService
  ) { }

  ngOnInit() {
    this.populateEvents()
    console.log(this.queryResult);

  }

  private populateEvents() {
    this.eventService.getEvents(this.query)
      .subscribe(result => {
        this.queryResult = result;
        this.showSpinner = false;
      });
  }

  sortBy(columnName) {
    this.showSpinner = true;
    if (this.query.sortBy === columnName)
      this.query.isSortAscending = !this.query.isSortAscending;
    else {
      this.query.isSortAscending = true;
      this.query.sortBy = columnName;
    }
    feather.replace();
    this.populateEvents();
  }

  onChangePage(page) {
    this.showSpinner = true;
    this.query.page = page;
    this.populateEvents();
  }

}
