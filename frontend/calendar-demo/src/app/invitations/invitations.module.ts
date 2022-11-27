import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InvitationDetailComponent } from './invitation-detail/invitation-detail.component';
import { InvitationRoutingModule } from './invitations-routing.module';
import { InvitationsListComponent } from '../invitations-list/invitations-list.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    InvitationDetailComponent,
    InvitationsListComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    InvitationRoutingModule
  ]
})
export class InvitationsModule { }
