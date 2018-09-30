import { SharedModule } from './../../shared/shared.module';
import { UserViewComponent } from './user-view/user-view.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { UsersListComponent } from './users-list/users-list.component';
import { NgModule } from '@angular/core';
import { UsersRoutingModule } from './users-routing.module';
import { UsersComponent } from './users.component';


@NgModule({
  imports: [
    UsersRoutingModule,
    SharedModule
  ],
  declarations: [
    UsersListComponent,
    UserEditComponent,
    UserViewComponent,
    UsersComponent
  ]
})
export class UsersModule { }
