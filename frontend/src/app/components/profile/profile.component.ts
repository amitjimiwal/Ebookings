import { Component, OnInit } from '@angular/core';
import { AppUser } from '../../models/interface/user';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
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
      email: ['', Validators.email],
      oldPassword: ['', [Validators.minLength(6)]],
      newPassword: ['', [Validators.minLength(6)]],
    });
  }

  ngOnInit() {
    this.loadUserProfile();
  }

  loadUserProfile() {
    this.user = this.userService.getUser();
  }

  toggleEdit() {
    this.isEditing = !this.isEditing;
    if (!this.isEditing && this.user) {
      this.profileForm.patchValue(this.user);
    }
  }

  onSubmit() {
    if (this.profileForm.valid) {
      if (this.profileForm.value.oldPassword && !this.profileForm.value.newPassword) {
        alert('Please enter both password');
        return;
      } else if (!this.profileForm.value.oldPassword && this.profileForm.value.newPassword) {
        alert('Please enter both password');
        return;
      }
      const updatedUser = {
        ...this.profileForm.value
      }
      console.log(updatedUser);
      this.userService.updateUser(updatedUser).subscribe(
        (response) => {
          this.user = this.userService.getUser();
          this.isEditing = false;
          this.profileForm.reset();
          console.log('Profile updated successfully');
        },
        error => {
          console.error('Error occurred while updating user profile', error);
          alert('Error occurred while updating user profile');
        }
      );
    }
  }

}
