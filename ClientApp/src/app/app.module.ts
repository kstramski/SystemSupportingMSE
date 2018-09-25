//****************************/
//Modules
//****************************/
//ErrorHandler
import { AppErrorHandler } from './app.error-handler';

//Angular
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgModule, ErrorHandler } from '@angular/core';

//Custom Modules
import { AppRoutingModule } from './app-routing.module';

//Charts
import { ChartsModule } from 'ng2-charts';

//JWT
import { JwtModule } from '@auth0/angular-jwt';

//Toastr
import { ToastrModule } from 'ngx-toastr';

//Bootstrap
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CollapseModule } from 'ngx-bootstrap';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TooltipModule } from 'ngx-bootstrap/tooltip';

//****************************/
//Services
//****************************/
import { AuthGuard } from './../services/guards/auth-guard.service';
import { AuthService } from '../services/auth.service';
import { NavbarService } from './../services/navbar.service';

import { RoleService } from './../services/role.service';
import { UserService } from './../services/user.service';

//****************************/
//Components
//****************************/
//Global
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { NavbarComponent } from './navbar/navbar.component';
import { PaginationComponent } from './shared/pagination.component';

//Dashboard
import { DashboardComponent } from './dashboard/dashboard.component';

//Events


//Login
import { LoginComponent } from './login/login.component';

//Roles


//Users
import { UserEditComponent } from './user-edit/user-edit.component';
import { UserViewComponent } from './user-view/user-view.component';
import { UsersListComponent } from './users-list/users-list.component';
import { RolesListComponent } from './roles-list/roles-list.component';
import { RoleEditComponent } from './role-edit/role-edit.component';
import { RoleEditUserComponent } from './role-edit-user/role-edit-user.component';


export function tokenGetter() {
  return localStorage.getItem('access_token');
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    UserEditComponent,
    UserViewComponent,
    UsersListComponent,
    NavMenuComponent,
    NavbarComponent,
    DashboardComponent,
    PaginationComponent,
    RolesListComponent,
    RoleEditComponent,
    RoleEditUserComponent
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    CommonModule,
    FormsModule,
    HttpClientModule,

    AppRoutingModule,

    ChartsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ['localhost:5001'],
        blacklistedRoutes: ['localhost:5001/auth/']
      }
    }),
    ToastrModule.forRoot(),

    BsDropdownModule.forRoot(),
    CollapseModule.forRoot(),
    ModalModule.forRoot(),
    PaginationModule.forRoot(),
    TabsModule.forRoot(),
    TooltipModule.forRoot(),
  ],
  providers: [
    { provide: ErrorHandler, useClass: AppErrorHandler },
    AuthGuard,
    AuthService,
    NavbarService,
    RoleService,
    UserService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
