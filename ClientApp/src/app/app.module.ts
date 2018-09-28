import { AppComponent } from './app.component';
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



export function tokenGetter() {
  return localStorage.getItem('access_token');
}

@NgModule({
  declarations: [
   AppComponent
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
