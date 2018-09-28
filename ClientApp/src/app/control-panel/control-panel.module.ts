import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ControlPanelRoutingModule } from './control-panel-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';


import { LoginComponent } from './login/login.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { NavbarComponent } from './navbar/navbar.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ChartsModule } from 'ng2-charts';
import { ControlPanelComponent } from './control-panel.component';
import { BsDropdownModule, CollapseModule, ModalModule, PaginationModule, TabsModule, TooltipModule } from 'ngx-bootstrap';
import { UserEditComponent } from './user-edit/user-edit.component';
import { UserViewComponent } from './user-view/user-view.component';
import { RolesListComponent } from './roles-list/roles-list.component';
import { RoleEditComponent } from './role-edit/role-edit.component';
import { RoleEditUserComponent } from './role-edit-user/role-edit-user.component';
import { UsersListComponent } from './users-list/users-list.component';
import { PaginationComponent } from '../shared/pagination.component';
@NgModule({
  imports: [
    CommonModule,
    ControlPanelRoutingModule,
    FormsModule,
    ChartsModule,

    BsDropdownModule.forRoot(),
    CollapseModule.forRoot(),
    ModalModule.forRoot(),
    PaginationModule.forRoot(),
    TabsModule.forRoot(),
    TooltipModule.forRoot(),
  ],
  declarations: [
    LoginComponent,
    NavMenuComponent,
    NavbarComponent,
    DashboardComponent,
    ControlPanelComponent,

    UserEditComponent,
    UserViewComponent,
    UsersListComponent,
    PaginationComponent,
    RolesListComponent,
    RoleEditComponent,
    RoleEditUserComponent
  ],
  // bootstrap: [ControlPanelComponent]
})
export class ControlPanelModule { }
