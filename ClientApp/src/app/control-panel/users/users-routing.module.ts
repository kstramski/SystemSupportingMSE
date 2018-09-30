import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UsersListComponent } from './users-list/users-list.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { UserViewComponent } from './user-view/user-view.component';
import { AuthGuard } from '../../../services/guards/auth-guard.service';
import { UsersComponent } from './users.component';

export const usersRoutes: Routes = [
  {
    path: '', component: UsersComponent,
    children: [
      { path: '', component: UsersListComponent, canActivate: [AuthGuard] },
      { path: 'list', component: UsersListComponent, canActivate: [AuthGuard] },
      { path: 'edit/:id', component: UserEditComponent, canActivate: [AuthGuard] },
      { path: ':id', component: UserViewComponent, canActivate: [AuthGuard] },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(usersRoutes)],
  exports: [RouterModule],
})
export class UsersRoutingModule { }
