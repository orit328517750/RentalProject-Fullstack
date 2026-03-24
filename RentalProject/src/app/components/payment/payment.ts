import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PaymentService } from '../../services/payment';
import { Router, RouterModule } from '@angular/router'; // 1. ייבוא הראוטר והמודל

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule], // 2. הוספת RouterModule לרשימה
  templateUrl: './payment.html',
  styleUrl: './payment.css'
})
export class PaymentComponent {
  paymentData = {
    creditCard: '',
    validit: '',
    cvc: null
  };

  isLoading = false; 
  isSuccess = false; 
  isFlipped = false; 
  generatedOrderNum = Math.floor(Math.random() * 90000) + 10000;

  // *** כאן התיקון! הוספנו private router: Router ***
  constructor(private paymentService: PaymentService, private router: Router) {}

  confirmPayment() {
    this.isLoading = true;
    this.paymentService.addPayment(this.paymentData).subscribe({
      next: (res) => {
        this.isLoading = false;
        if (res === true) {
          this.isSuccess = true;
        } else {
          alert("השרת נכשל בשמירה");
        }
      },
      error: (err) => {
        this.isLoading = false;
        alert("שגיאה בתקשורת!");
      }
    });
  }

  // הפונקציה שמשתמשת בראוטר
 backToCatalog() {
  // המילה catalog חייבת להיות זהה ל-path שהגדרת ב-routes
  this.router.navigate(['/catalog']); 
}
}