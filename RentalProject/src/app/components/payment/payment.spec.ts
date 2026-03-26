import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PaymentService } from '../../services/payment';
import { RentService } from '../../services/rent.service'; 
import { Router, RouterModule, ActivatedRoute } from '@angular/router';

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
  carIdFromUrl: number | null = null;

  constructor(
    private paymentService: PaymentService, 
    private rentService: RentService, 
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    // שליפת ה-ID מה-URL (שלך) ובדיקת Session (שלה)
    const id = this.route.snapshot.paramMap.get('id');
    if (id) this.carIdFromUrl = +id;

    if (!sessionStorage.getItem('selectedCarCode')) {
      alert("לא נבחר רכב. חוזר לקטלוג.");
      this.router.navigate(['/catalog']);
    }
  }

  confirmPayment() {
    if (!this.paymentData.creditCard || this.paymentData.creditCard.length < 16) {
        alert("נא להזין כרטיס תקין");
        return;
    }

    this.isLoading = true;
    
    this.paymentService.addPayment(this.paymentData).subscribe({
      next: (res: any) => {
        if (res === true) {
          // תשלום הצליח - עוברים ליצירת רשומת השכרה ב-DB (לוגיקה של חברתך)
          this.createRentRecord();
        } else {
          this.isLoading = false;
          alert("השרת נכשל באישור התשלום");
        }
      },
      error: (err: any) => {
        this.isLoading = false;
        alert("שגיאה בתקשורת מול שרת התשלומים!");
      }
    });
  }

  createRentRecord() {
    const userJson = sessionStorage.getItem('loggedInUser');
    if (!userJson) return;
    const user = JSON.parse(userJson);

    const selectedCarCode = sessionStorage.getItem('selectedCarCode');
    const storedFinishDate = sessionStorage.getItem('finishDate');
    const storedRentGoal = sessionStorage.getItem('rentGoal');

    const newRent = {
      customerId: user.Id, 
      carCode: selectedCarCode ? parseInt(selectedCarCode) : 1, 
      beginDate: new Date(), 
      finishDate: storedFinishDate ? new Date(storedFinishDate) : new Date(), 
      rentGoal: storedRentGoal || 'Vacation',
      rensCode: 0
    };

    this.rentService.addRent(newRent).subscribe({
      next: (response: any) => {
        // רק כאן אנחנו מסמנים הצלחה סופית
        setTimeout(() => { // השהיה קטנה לחוויית משתמש
            this.isLoading = false;
            this.isSuccess = true;
            this.clearSession();
        }, 1500);
      },
      error: (err: any) => {
        this.isLoading = false;
        alert("התשלום עבר, אך רישום ההשכרה נכשל.");
      }
    });
  }

  clearSession() {
    sessionStorage.removeItem('selectedCarCode');
    sessionStorage.removeItem('finishDate');
    sessionStorage.removeItem('rentGoal');
    sessionStorage.removeItem('rentDays');
  }

  backToCatalog() {
    this.router.navigate(['/catalog']); 
  }
}