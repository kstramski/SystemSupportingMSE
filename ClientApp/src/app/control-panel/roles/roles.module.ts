import { SharedModule } from './../../shared/shared.module';
import { RolesRoutingModule, rolesComponents } from './roles-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@NgModule({
  imports: [
    CommonModule,
    RolesRoutingModule,
    SharedModule
  ],
  declarations: [rolesComponents]
})
export class RolesModule { }
