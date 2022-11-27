import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { InvitationService } from 'src/app/services/invitation.service';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css'],
})
export class UserDetailComponent implements OnInit {
  public userForm: FormGroup;
  constructor(
    public fb: FormBuilder,
    private invitationService: InvitationService
  ) //public toastr: ToastrService
  {}

  Users: any = [];

  ngOnInit() {
    this.initUserForm();
  }
  initUserForm() {
    this.userForm = this.fb.group({
      username: ['', [Validators.required, Validators.minLength(2)]],
      email: [
        '',
        [
          Validators.required,
          Validators.email
        ],
      ],
    });
  }

  get name() {
    return this.userForm.get('name');
  }

  get externalEmails() {
    return this.userForm.get('title');
  }

  resetForm() {
    this.userForm.reset();
  }
  submit() {
    this.invitationService
      .createUser(this.userForm.getRawValue())
      .subscribe((x) => {
        if (x) {
          console.log('create success');
          this.resetForm();
        }
      });
  }
}
