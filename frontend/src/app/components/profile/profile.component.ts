import { Component, OnInit } from '@angular/core';
import { AppUser } from '../../models/interface/user';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { UserInfoService } from '../../services/user-info-service/user-info-service.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, RouterModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {
  user: AppUser | null = null;
  profileForm: FormGroup;
  isEditing = false;

  constructor(
    private userService: UserInfoService,
    private fb: FormBuilder
  ) {
    this.profileForm = this.fb.group({
      phoneNumber: [''],
      userName: [''],
      email: [''],
      oldPassword: [''],
      newPassword: ['']
    });
  }

  ngOnInit() {
    this.loadUserProfile();
  }

  loadUserProfile() {
    this.user = this.userService.getUser()
  }

  toggleEdit() {
    this.isEditing = !this.isEditing;
    if (!this.isEditing && this.user) {
      this.profileForm.patchValue(this.user);
    }
  }

  onSubmit() {
    if (this.profileForm.valid) {
      const updatedUser = {
        ...this.user,
        ...this.profileForm.value
      }
      this.userService.updateUser(updatedUser).subscribe(
        (response) => {
          this.user = this.userService.getUser();
          this.isEditing = false;
          console.log('Profile updated successfully');
        },
        error => {
          console.error('Error occurred while updating user profile', error);
          alert(error.error);
        }
      );
    }
  }
}
