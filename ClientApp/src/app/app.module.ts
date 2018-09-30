import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppErrorHandler } from './app.error-handler';
import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared/shared.module';
import { JwtModule } from '@auth0/angular-jwt';

import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';

import { AuthGuard } from './../services/guards/auth-guard.service';
import { AuthService } from '../services/auth.service';
import { NavbarService } from './../services/navbar.service';

import { RoleService } from './../services/role.service';
import { TeamService } from '../services/team.service';
import { UserService } from './../services/user.service';

import { AppComponent } from './app.component';

export function tokenGetter() {
  return localStorage.getItem('access_token');
}

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    SharedModule,

    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ['localhost:5001'],
        blacklistedRoutes: ['localhost:5001/auth/']
      }
    }),
  ],
  providers: [
    { provide: ErrorHandler, useClass: AppErrorHandler },
    AuthGuard,
    AuthService,
    NavbarService,
    RoleService,
    TeamService,
    UserService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
