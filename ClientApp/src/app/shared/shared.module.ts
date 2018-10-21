import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { ToastrModule } from 'ngx-toastr';

import {
  BsDropdownModule,
  CollapseModule,
  ModalModule,
  TooltipModule,
  TabsModule,
} from 'ngx-bootstrap';

import { ModalDeleteComponent } from './modal-delete.component';
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
    TooltipModule,
    TabsModule,

    PaginationComponent,
    ModalDeleteComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,

    ToastrModule.forRoot(),

    BsDropdownModule.forRoot(),
    CollapseModule.forRoot(),
    ModalModule.forRoot(),
    TooltipModule.forRoot(),
    TabsModule.forRoot(),
  ],
  declarations: [
    ModalDeleteComponent,
    PaginationComponent
  ]
})
export class SharedModule { }
