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
      // 1. שמירת המשתמש ב-sessionStorage (כדי שהאתר ידע מי מחובר)
      sessionStorage.setItem('loggedInUser', JSON.stringify(user));

      alert(`ברוך הבא, ${user.firstName}!`);

      // 2. רענון ומעבר לקטלוג - זה מה שיגרום לכפתור ההיסטוריה להופיע ב-Navbar
      window.location.href = '/catalog'; 
    },
    error: (err) => {
      console.error(err);
      alert('ההתחברות נכשלה. בדוק את המייל או הירשם קודם.');
    }
  });
}
}