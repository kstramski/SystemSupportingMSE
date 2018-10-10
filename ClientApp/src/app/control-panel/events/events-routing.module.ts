import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EventsComponent } from './events.component';
import { EventsListComponent } from './events-list/events-list.component';
import { EventFormComponent } from './event-form/event-form.component';
import { EventViewComponent } from './event-view/event-view.component';
import { AuthGuard } from '../../../services/guards/auth-guard.service';
import { EventCompetitionViewComponent } from './event-competition-view/event-competition-view.component';
import { EventCompetitionEditComponent } from './event-competition-edit/event-competition-edit.component';

const routes: Routes = [
  { path: '', component: EventsComponent,
  children: [
    { path: '', component: EventsListComponent, canActivate: [AuthGuard] },
    { path: 'new', component: EventFormComponent, canActivate: [AuthGuard] },
    { path: 'edit/:id', component: EventFormComponent, canActivate: [AuthGuard] },
    { path: ':id', component: EventViewComponent, canActivate: [AuthGuard] },
    { path: ':eventId/competitions/:competitionId', component: EventCompetitionViewComponent, canActivate: [AuthGuard] },
    { path: ':eventId/competitions/:competitionId/edit', component: EventCompetitionEditComponent, canActivate: [AuthGuard] },
  ]
},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EventsRoutingModule { }

export const eventsComponents = [
    EventsComponent,
    EventsListComponent,
    EventFormComponent,
    EventViewComponent,
    EventCompetitionViewComponent,
    EventCompetitionEditComponent,
];