import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CompetitionsRoutingModule, competitionsComponents } from './competitions-routing.module';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    CompetitionsRoutingModule,
    SharedModule
  ],
  declarations: [competitionsComponents]
})
export class CompetitionsModule { }
