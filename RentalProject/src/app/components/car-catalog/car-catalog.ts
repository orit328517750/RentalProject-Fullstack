import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CarService } from '../../services/car';
import { Car } from '../../models/car';
import { Router, RouterModule } from '@angular/router'; // נוסף Router

@Component({
  selector: 'app-car-catalog',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './car-catalog.html',
  styleUrl: './car-catalog.css',
})
export class CarCatalog implements OnInit {
  cars = signal<Car[]>([]);

  filterPlaces: number | null = null;
  filterLevel: number | null = null;
  filterPrice: number | null = null;

  // הוספנו את ה-Router ב-Constructor
  constructor(private carService: CarService, private router: Router) {}

  ngOnInit() {
    this.loadAllCars();
  }

  // פונקציה חדשה: בחירת רכב ומעבר לתשלום
 selectCar(car: Car) {
  // 1. שמירת קוד הרכב הנבחר
  sessionStorage.setItem('selectedCarCode', car.carCode.toString());
  
  // 2. תיקון הניווט: במקום '/payment', עוברים לדף הפרטים החדש
  this.router.navigate(['/rental-details']); 
}

  loadAllCars() {
    this.carService.getAllCars().subscribe({
      next: (data) => this.cars.set(data),
      error: (err) => console.error('Error:', err)
    });
  }

  applyFilter() {
    const hasPlace = this.filterPlaces !== null && this.filterPlaces > 0;
    const hasLevel = this.filterLevel !== null && this.filterLevel > 0;
    const hasPrice = this.filterPrice !== null && this.filterPrice > 0;

    let obs: any;

    if (hasPlace && hasLevel && hasPrice) {
      obs = this.carService.getCarsByCriteria(this.filterPlaces!, this.filterLevel!, this.filterPrice!);
    } else if (hasPlace && !hasLevel && !hasPrice) {
      obs = this.carService.getCarByPlace(this.filterPlaces!);
    } else if (!hasPlace && hasLevel && !hasPrice) {
      obs = this.carService.getCarByLevel(this.filterLevel!);
    } else if (!hasPlace && !hasLevel && hasPrice) {
      obs = this.carService.getCarByPrice(this.filterPrice!);
    } else {
      this.loadAllCars();
      return;
    }

    obs.subscribe({
      next: (data: Car[]) => this.cars.set(data),
      error: (err: any) => {
        console.error('Filter error:', err);
        this.cars.set([]); 
      }
    });
  }

  resetFilter() {
    this.filterPlaces = null;
    this.filterLevel = null;
    this.filterPrice = null;
    this.loadAllCars();
  }
}