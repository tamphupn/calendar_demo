import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InvitationsListComponent } from '../invitations-list/invitations-list.component';
import { InvitationDetailComponent } from './invitation-detail/invitation-detail.component';

const routes: Routes = [
  {
    path: '',
    component: InvitationsListComponent,
  },
  {
    path: 'create',
    component: InvitationDetailComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class InvitationRoutingModule { }
