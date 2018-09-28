import { ControlPanelComponent } from './control-panel.component';
import { LoginComponent } from './login/login.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
// import { UserViewComponent } from './user-view/user-view.component';
// import { UserEditComponent } from './user-edit/user-edit.component';
// import { UsersListComponent } from './users-list/users-list.component';
// import { AuthGuard } from '../services/guards/auth-guard.service';
import { DashboardComponent } from './dashboard/dashboard.component';
import { RolesListComponent } from './roles-list/roles-list.component';
import { RoleEditComponent } from './role-edit/role-edit.component';
import { RoleEditUserComponent } from './role-edit-user/role-edit-user.component';
import { UsersListComponent } from './users-list/users-list.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { UserViewComponent } from './user-view/user-view.component';
import { AuthGuard } from '../../services/guards/auth-guard.service';
// import { RolesListComponent } from './roles-list/roles-list.component';
// import { RoleEditComponent } from './role-edit/role-edit.component';
// import { RoleEditUserComponent } from './role-edit-user/role-edit-user.component';

export const panelRoutes: Routes = [
  { path: '', component: ControlPanelComponent,
  children: [
    { path: '', component: DashboardComponent },
    { path: 'dashboard', component: DashboardComponent },
    { path: 'login', component: LoginComponent },
    
    { path: 'roles', component: RolesListComponent, canActivate: [AuthGuard] },
    { path: 'roles/:id', component: RoleEditComponent, canActivate: [AuthGuard] },
    { path: 'roles/users/:id', component: RoleEditUserComponent, canActivate: [AuthGuard] },

    { path: 'users', component: UsersListComponent, canActivate: [AuthGuard] },
    { path: 'users/edit/:id', component: UserEditComponent, canActivate: [AuthGuard]},
    { path: 'users/:id', component: UserViewComponent, canActivate: [AuthGuard]},
  ] }
];

@NgModule({
  imports: [RouterModule.forChild(panelRoutes)],
  exports: [RouterModule],
})
export class ControlPanelRoutingModule { }
