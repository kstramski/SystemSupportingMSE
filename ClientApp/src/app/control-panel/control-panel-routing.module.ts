import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ControlPanelComponent } from './control-panel.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from '../../services/guards/auth-guard.service';

export const panelRoutes: Routes = [
  { path: '', component: ControlPanelComponent,
  children: [
    { path: '', component: DashboardComponent },
    { path: 'dashboard', component: DashboardComponent },
    { path: 'login', component: LoginComponent },
    { path: 'roles', loadChildren: './roles/roles.module#RolesModule', canActivate: [AuthGuard] },
    { path: 'teams', loadChildren: './teams/teams.module#TeamsModule', canActivate: [AuthGuard] },
    { path: 'users', loadChildren: './users/users.module#UsersModule', canActivate: [AuthGuard] },
  ] }
];

@NgModule({
  imports: [RouterModule.forChild(panelRoutes)],
  exports: [RouterModule],
})
export class ControlPanelRoutingModule { }
