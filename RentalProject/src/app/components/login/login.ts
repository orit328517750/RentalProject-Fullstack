import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CustomerService } from '../../services/customer';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router'; // לייבוא הניווט אחרי התחברות
import { RouterModule } from '@angular/router';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, RouterModule],
  templateUrl: './login.html',
  styleUrl: './login.css' // משתמשים באותו CSS של ההרשמה
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;

  constructor(
    private fb: FormBuilder, 
    private customerService: CustomerService,
    private router: Router
  ) { }

  ngOnInit() {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }

  onLogin() {
    const email = this.loginForm.value.email;

    this.customerService.login(email).subscribe({
      next: (user) => {
        alert(`ברוך הבא, ${user.firstName}!`);
        // כאן כדאי לשמור את המשתמש ב-SessionStorage כדי שהאתר יזכור אותו
        sessionStorage.setItem('loggedInUser', JSON.stringify(user));
        // אחרי ששמרת את המשתמש ב-sessionStorage
        window.location.href = '/'; // זה יזרוק אותו לדף הבית וירענן את ה-Navbar
        // ניווט לדף הבית למשל
        this.router.navigate(['/home']);
      },
      error: (err) => {
        console.error(err);
        alert('ההתחברות נכשלה. בדוק את המייל או הירשם קודם.');
      }
    });
  }
}