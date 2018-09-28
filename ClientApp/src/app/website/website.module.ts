import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WebsiteComponent } from './website.component';
import { WebsiteRoutingModule } from './website-routing.module';
import { LoginComponent } from './login/login.component';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    WebsiteRoutingModule
  ],
  declarations: [
    LoginComponent,
    WebsiteComponent,
    HomeComponent
  ],
  bootstrap: [WebsiteComponent]
})
export class WebsiteModule { }
