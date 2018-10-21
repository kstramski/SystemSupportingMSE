import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../../services/guards/auth-guard.service';
import { RolesComponent } from './roles.component';
import { RolesListComponent } from './roles-list/roles-list.component';
import { RoleEditComponent } from './role-edit/role-edit.component';
import { RoleEditUserComponent } from './role-edit-user/role-edit-user.component';
import { UsersListComponent } from './users-list/users-list.component';

export const routes: Routes = [
  {
    path: '', component: RolesComponent,
    children: [
      { path: '', component: RolesListComponent, canActivate: [AuthGuard] },
      { path: 'users', component: UsersListComponent, canActivate: [AuthGuard] },
      { path: ':id', component: RoleEditComponent, canActivate: [AuthGuard] },
      { path: 'users/:id', component: RoleEditUserComponent, canActivate: [AuthGuard] },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class RolesRoutingModule { }

export const rolesComponents = [
  RolesComponent,
  RolesListComponent,
  RoleEditComponent,
  RoleEditUserComponent,
  UsersListComponent
];