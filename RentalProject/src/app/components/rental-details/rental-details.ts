import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CarService } from '../../services/car';
import { Car } from '../../models/car';

@Component({
  selector: 'app-rental-details',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './rental-details.html',
  styleUrl: './rental-details.css'
})
export class RentalDetails implements OnInit {
  selectedCar: Car | null = null;
  carCode: string | null = '';
  daysToRent: number = 1;
  rentGoal: string = 'טיול';
  otherGoal: string = '';

  constructor(private router: Router, private carService: CarService) {}

  ngOnInit() {
  const rawCode = sessionStorage.getItem('selectedCarCode');
  
  if (rawCode) {
    // פיצול המחרוזת למקרה שיש בה תווים מיותרים כמו נקודתיים
    const cleanCode = parseInt(rawCode.split(':')[0]); 
    this.carCode = cleanCode.toString();

    this.carService.getCarByCode(cleanCode).subscribe({
      next: (car: Car) => {
        console.log("הרכב שהגיע מהשרת:", car);
        this.selectedCar = car;
      },
      error: (err: any) => {
        console.error("שגיאה בטעינת נתוני רכב", err);
      }
    });
  } else {
    this.router.navigate(['/catalog']);
  }
}

  getTotalPrice(): number {
    // החישוב מסתמך על הנתונים שהגיעו מה-DB
    return this.selectedCar ? this.selectedCar.priceOfDay * this.daysToRent : 0;
  }

  proceedToPayment() {
    let finishDate = new Date();
    finishDate.setDate(finishDate.getDate() + this.daysToRent);

    const finalGoal = this.rentGoal === 'אחר' ? this.otherGoal : this.rentGoal;

    sessionStorage.setItem('rentDays', this.daysToRent.toString());
    sessionStorage.setItem('rentGoal', finalGoal);
    sessionStorage.setItem('finishDate', finishDate.toISOString());

    this.router.navigate(['/payment', this.carCode]);
  }
}