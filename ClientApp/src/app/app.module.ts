//Angular
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';

//Custom Modules
import { AppRoutingModule } from './app-routing.module';

//JWT
import { JwtModule } from '@auth0/angular-jwt';

//Toastr
import { ToastrModule } from 'ngx-toastr';

//Bootstrap
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';
//--------------------------------------------------------

//Services
import { AuthGuard } from './../services/guards/auth-guard.service';
import { AuthService } from '../services/auth.service';
import { UserService } from './../services/user.service';

//Components
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { UserViewComponent } from './user-view/user-view.component';
import { UsersListComponent } from './users-list/users-list.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { NavbarComponent } from './navbar/navbar.component';
import { DashboardComponent } from './dashboard/dashboard.component';

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
    DashboardComponent
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    CommonModule,
    FormsModule,
    HttpClientModule,

    AppRoutingModule,

    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ['localhost:5001'],
        blacklistedRoutes: ['localhost:5001/auth/']
      }
    }),
    ToastrModule.forRoot(),

    BsDropdownModule.forRoot(),
    TooltipModule.forRoot(),
    ModalModule.forRoot()
  ],
  providers: [
    AuthGuard,
    AuthService,
    UserService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
