import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PaymentService } from '../../services/payment';
import { RentService } from '../../services/rent.service'; 
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './payment.html',
  styleUrl: './payment.css'
})
export class PaymentComponent implements OnInit {
  paymentData = {
    creditCard: '',
    validit: '',
    cvc: null
  };

  isLoading = false; 
  isSuccess = false; 
  isFlipped = false;
  generatedOrderNum = Math.floor(Math.random() * 90000) + 10000;

  constructor(
    private paymentService: PaymentService, 
    private rentService: RentService, 
    private router: Router
  ) {}

  ngOnInit() {
    // בדיקה אם המשתמש הגיע לכאן בלי לבחור רכב
    if (!sessionStorage.getItem('selectedCarCode')) {
      alert("לא נבחר רכב. חוזר לקטלוג.");
      this.router.navigate(['/catalog']);
    }
  }

  confirmPayment() {
  // 1. בדיקת תקינות בסיסית לפני שמתחילים
  if (!this.paymentData.creditCard || this.paymentData.creditCard.length < 16) {
    alert("נא להזין מספר כרטיס אשראי תקין (16 ספרות)");
    return;
  }

  // 2. הפעלת הספינר הכתום (הצגת ה-loader-container)
  this.isLoading = true;

  // 3. קריאה לשרת התשלומים
  this.paymentService.addPayment(this.paymentData).subscribe({
    next: (res: any) => {
      // אנחנו מוסיפים השהיה קטנה של 2 שניות כדי שהמשתמש יראה את האנימציה היוקרתית
      setTimeout(() => {
        if (res === true) {
          // אם התשלום עבר, עוברים ליצירת הרשומה ב-DB (הלוגיקה של חברתך)
          this.createRentRecord();
        } else {
          this.isLoading = false; // כיבוי הספינר במקרה של שגיאה
          alert("התשלום נדחה על ידי חברת האשראי");
        }
      }, 2000); // 2000 מילישניות = 2 שניות של ספינר
    },
    error: (err: any) => {
      // כיבוי הספינר במקרה של תקלה בתקשורת
      this.isLoading = false;
      console.error("Payment Error:", err);
      alert("שגיאה בתקשורת עם השרת, נסה שנית מאוחר יותר");
    }
  });
}

createRentRecord() {
  const userJson = sessionStorage.getItem('loggedInUser');
  if (!userJson) return;
  const user = JSON.parse(userJson);

  // שליפת הנתונים ששמרנו בשלבים הקודמים
  const selectedCarCode = sessionStorage.getItem('selectedCarCode');
  const storedFinishDate = sessionStorage.getItem('finishDate');
  const storedRentGoal = sessionStorage.getItem('rentGoal');

  const newRent = {
    customerId: user.Id, 
    carCode: selectedCarCode ? parseInt(selectedCarCode) : 1, 
    beginDate: new Date(), 
    
    // תיקון: שימוש בתאריך שחושב בדף הקודם (אם לא קיים, ברירת מחדל היום)
    finishDate: storedFinishDate ? new Date(storedFinishDate) : new Date(), 
    
    // תיקון: שימוש במטרה שהמשתמש בחר (אם לא קיים, ברירת מחדל Vacation)
    rentGoal: storedRentGoal || 'Vacation',
    
    rensCode: 0
  };

  this.rentService.addRent(newRent).subscribe({
    next: (response: any) => {
      this.isLoading = false;
      this.isSuccess = true;
      console.log("השכרה נרשמה בהצלחה ב-DB");
      
      // ניקוי כל נתוני הבחירה לאחר הצלחה
      sessionStorage.removeItem('selectedCarCode');
      sessionStorage.removeItem('finishDate');
      sessionStorage.removeItem('rentGoal');
      sessionStorage.removeItem('rentDays');
    },
    error: (err: any) => {
      this.isLoading = false;
      alert("התשלום עבר, אך רישום ההשכרה בבסיס הנתונים נכשל.");
    }
  });
}

  backToCatalog() {
    this.router.navigate(['/catalog']); 
  }
}