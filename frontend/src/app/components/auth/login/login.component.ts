import { Component } from '@angular/core';
import { AuthService } from '../../../services/auth/auth-service.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LoginDto } from '../../../models/interface/auth';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm: FormGroup;
  constructor(private authService: AuthService, private formBuilder: FormBuilder, private router: Router) {
    this.loginForm = formBuilder.group({
      userNameOrEmail: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
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
    if (this.loginForm.valid) {
      let loginDto: LoginDto = {
        userNameOrEmail: this.loginForm.get('userNameOrEmail')?.value,
        password: this.loginForm.get('password')?.value
      };
      console.log(loginDto);
      this.authService.login(loginDto).subscribe((data) => {
        alert("Login Successful");
        this.router.navigate(['/profile']);
      }, (error) => {
        alert(error.error);
        console.error('Error occurred while registering user', error);
      });
    } else {
      alert('Please fill all the fields correctly');
    }
  }
}
