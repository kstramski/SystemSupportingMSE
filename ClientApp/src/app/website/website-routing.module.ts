import { LoginComponent } from './login/login.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { WebsiteComponent } from './website.component';
import { HomeComponent } from './home/home.component';


export const websiteRoutes: Routes = [
    { path: '', component: WebsiteComponent,
    children: [
      { path: '', component: HomeComponent },
      { path: 'home', component: HomeComponent },
      { path: 'login', component: LoginComponent },
    ] }
  ];
@NgModule({
  imports: [RouterModule.forChild(websiteRoutes)],
  exports: [RouterModule]
})
export class WebsiteRoutingModule { }
