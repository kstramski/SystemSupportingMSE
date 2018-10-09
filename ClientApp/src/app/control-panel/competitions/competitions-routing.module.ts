import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../../services/guards/auth-guard.service';
import { CompetitionsComponent } from './competitions.component';
import { CompetitionsListComponent } from './competitions-list/competitions-list.component';
import { CompetitionViewComponent } from './competition-view/competition-view.component';
import { CompetitionFormComponent } from './competition-form/competition-form.component';

export const competitionsRoutes: Routes = [
  {
    path: '', component: CompetitionsComponent,
    children: [
      { path: '', component: CompetitionsListComponent, canActivate: [AuthGuard] },
      { path: 'new', component: CompetitionFormComponent, canActivate: [AuthGuard] },
      { path: 'edit/:id', component: CompetitionFormComponent, canActivate: [AuthGuard] },
      { path: ':id', component: CompetitionViewComponent, canActivate: [AuthGuard] },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(competitionsRoutes)],
  exports: [RouterModule],
})
export class CompetitionsRoutingModule { }

export const competitionsComponents = [
  CompetitionsComponent,
  CompetitionsListComponent,
  CompetitionFormComponent,
  CompetitionViewComponent
];