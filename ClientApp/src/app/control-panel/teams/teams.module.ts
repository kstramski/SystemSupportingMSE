import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { teamsComponents, TeamsRoutingModule } from './teams-routing.module';

@NgModule({
  imports: [
    CommonModule,
    TeamsRoutingModule,
    SharedModule
  ],
  declarations: [teamsComponents ]
})
export class TeamsModule { }
