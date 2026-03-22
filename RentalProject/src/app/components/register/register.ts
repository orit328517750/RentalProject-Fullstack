import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CustomerService } from '../../services/customer';
import { Customer } from '../../models/customer.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  standalone: true, // וודאי שזה קיים
  imports: [ReactiveFormsModule, CommonModule], // <--- זה חייב להיות כאן
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class RegisterComponent implements OnInit {

  registerForm!: FormGroup; // הטופס שנקשר ל-HTML

  // הזריקי את ה-FormBuilder ואת הסרוויס שבנינו
  constructor(private fb: FormBuilder, private customerService: CustomerService) { }

  ngOnInit() {
    // אתחול הטופס - השמות כאן חייבים להתאים למה שכתבת ב-DTO ב-C#
    this.registerForm = this.fb.group({
      firstName: ['', [Validators.required]], // שימי לב: f קטנה!
      lastName: ['', [Validators.required]],  // שימי לב: l קטנה!
      idCity: ['', [Validators.required, Validators.min(1)]],
      email: ['', [Validators.required, Validators.email]],
      numOfLending: [0], // ברירת מחדל
      codePayment: [1]   // ברירת מחדל
    });
  }

  // הפונקציה שתפעל כשלוחצים על הכפתור "הרשמה"
  onSubmit() {
    if (this.registerForm.invalid) {
      alert('נא למלא את כל השדות בצורה תקינה.');
      return;
    }

    const newCustomer = this.registerForm.value as Customer;
    // ודאי שהשדה Id תואם למה שה-API מצפה (לפעמים זה id בטילדה קטנה)
    newCustomer.Id = 0; 

    console.log('שולח נתונים לשרת:', newCustomer);

    this.customerService.insertCustomer(newCustomer).subscribe({
      next: (result) => {
        console.log('תשובה גולמית מהשרת:', result);
        
        // בדיקה אם השרת שלח משהו שנראה כמו שגיאה בתוך הצלחה
        if (result === 'error' || !result) {
          console.error('השרת החזיר שגיאה לוגית:', result);
          alert('השרת לא הצליח לשמור את הנתונים. בדקי את ה-Console ב-C#');
        } else {
          alert(`הלקוח נוסף בהצלחה! (תשובת שרת: ${JSON.stringify(result)})`);
          this.registerForm.reset();
        }
      },
      error: (err) => {
        // כאן זה יגיע רק אם יש בעיית תקשורת (כמו ERR_CONNECTION_REFUSED)
        console.error('שגיאת תקשורת (HTTP Error):', err);
        alert('שגיאה בחיבור לשרת. ודאי שה-API רץ ושהגדרת CORS.');
      }
    });
  }
}