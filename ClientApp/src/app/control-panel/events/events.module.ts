import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { eventsComponents, EventsRoutingModule } from './events-routing.module';
import { EventCompetitionViewComponent } from './event-competition-view/event-competition-view.component';

@NgModule({
  imports: [
    CommonModule,
    EventsRoutingModule,
    SharedModule
  ],
  declarations: [eventsComponents]
})
export class EventsModule { }
