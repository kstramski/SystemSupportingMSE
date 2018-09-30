import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../../services/guards/auth-guard.service';
import { TeamsComponent } from './teams.component';
import { TeamsListComponent } from './teams-list/teams-list.component';
import { TeamViewComponent } from './team-view/team-view.component';

export const usersRoutes: Routes = [
  {
    path: '', component: TeamsComponent,
    children: [
      { path: '', component: TeamsListComponent, canActivate: [AuthGuard] },
      // { path: 'edit/:id', component: UserEditComponent, canActivate: [AuthGuard] },
      { path: ':id', component: TeamViewComponent, canActivate: [AuthGuard] },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(usersRoutes)],
  exports: [RouterModule],
})
export class TeamsRoutingModule { }

export const teamsComponents = [
  TeamsComponent,
  TeamsListComponent,
  TeamViewComponent
];