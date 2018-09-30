import { ControlPanelRoutingModule } from './control-panel-routing.module';
import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';

import { LoginComponent } from './login/login.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { NavbarComponent } from './navbar/navbar.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ChartsModule } from 'ng2-charts';
import { ControlPanelComponent } from './control-panel.component';

@NgModule({
  imports: [
    SharedModule,
    ChartsModule,
    ControlPanelRoutingModule
  ],
  declarations: [
    LoginComponent,
    NavMenuComponent,
    NavbarComponent,
    DashboardComponent,
    ControlPanelComponent,
  ],
  // bootstrap: [ControlPanelComponent]
})
export class ControlPanelModule { }
