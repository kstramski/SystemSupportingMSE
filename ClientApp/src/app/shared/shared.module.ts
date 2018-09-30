import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { ToastrModule } from 'ngx-toastr';

import {
  BsDropdownModule,
  CollapseModule,
  ModalModule,
  PaginationModule,
  TabsModule,
  TooltipModule
} from 'ngx-bootstrap';

import { PaginationComponent } from './pagination.component';

@NgModule({
  exports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    ToastrModule,
    BsDropdownModule,
    CollapseModule,
    ModalModule,
    PaginationModule,
    TabsModule,
    TooltipModule,

    PaginationComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,

    
    ToastrModule.forRoot(),

    BsDropdownModule.forRoot(),
    CollapseModule.forRoot(),
    ModalModule.forRoot(),
    PaginationModule.forRoot(),
    TabsModule.forRoot(),
    TooltipModule.forRoot()
  ],
  declarations: [
    PaginationComponent
  ]
})
export class SharedModule { }
