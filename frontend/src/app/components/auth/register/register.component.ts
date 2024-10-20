import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../services/auth/auth-service.service';
import { RegisterDto } from '../../../models/interface/auth';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  profileImage: File | null = null;
  constructor(private authService: AuthService, private formBuilder: FormBuilder, private router: Router) {
    this.registerForm = formBuilder.group({
      userName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      phoneNumber: ['', [Validators.pattern('^[0-9]*$'), Validators.maxLength(10), Validators.minLength(10), Validators.required]],
      ProfilePicture: ['', [Validators.required]],
      PreferredLanguage: [''],
      PreferredCurrency: ['']
    });
  }

  //redirect to events if user logged in
  ngOnInit(): void {
    if (this.authService.isAuthenticated()) {
      // Redirect to events page if already logged in
      this.router.navigate(['/events']);
    }
  }
  onSubmit(): void {
    if (this.registerForm.valid) {
      let registerDto: RegisterDto = {
        userName: this.registerForm.get('userName')?.value,
        email: this.registerForm.get('email')?.value,
        password: this.registerForm.get('password')?.value,
        phoneNumber: this.registerForm.get('phoneNumber')?.value || undefined,
        ProfilePicture: this.profileImage,
        PreferredLanguage: this.registerForm.get('PreferredLanguage')?.value || 'INR',
        PreferredCurrency: this.registerForm.get('PreferredCurrency')?.value || 'English'
      };
      this.authService.signUp(registerDto).subscribe((data) => {
        alert("Registration Successful , Please Login");
        this.router.navigate(['/login']);
      }, (error) => {
        alert(error.error);
        console.error('Error occurred while registering user', error);
      });
    } else {
      alert('Please fill all the fields correctly');
    }
  }

  onFileChange(event: any): void {
    if (event.target.files.length > 0) {
      this.profileImage = event.target.files[0];
    }
  }
}
