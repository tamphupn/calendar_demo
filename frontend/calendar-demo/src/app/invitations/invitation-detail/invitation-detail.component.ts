import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { InvitationService } from 'src/app/services/invitation.service';

@Component({
  selector: 'app-invitation-detail',
  templateUrl: './invitation-detail.component.html',
  styleUrls: ['./invitation-detail.component.css']
})
export class InvitationDetailComponent implements OnInit {
  public invitationForm: FormGroup;
  constructor(
    public fb: FormBuilder,
    private invitationService: InvitationService
    //public toastr: ToastrService
  ) {}

  Users: any = []
  UserRequests: any = []
  userSelected = []

  ngOnInit() {
    //this.crudApi.GetStudentsList();
    this.initInvitationForm();
  }
  initInvitationForm() {
    this.invitationForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      title: ['', [Validators.required, Validators.minLength(2)]],
      externalEmails: [[]],
      userRequestId: [null, Validators.required],
      userResponseIds: [[''], Validators.required],
      eventDateStart: [null],
      eventDateFinish: [null],
    });

    this.invitationService.getUsers().subscribe(data => {
      this.Users = data;
      this.UserRequests = data;
    });

    this.invitationForm.get("userResponseIds").valueChanges.subscribe(x => {
      console.log(x);
   })
  }

  get name() {
    return this.invitationForm.get('name');
  }

  get externalEmails() {
    return this.invitationForm.get('title');
  }

  resetForm() {
    this.invitationForm.reset();
  }
  submitEvent() {
    this.invitationService
      .createInvitaion(this.invitationForm.getRawValue())
      .subscribe((x) => {
        if (x) {
          console.log('create success');
          this.resetForm();
        }
      });
  }

}
