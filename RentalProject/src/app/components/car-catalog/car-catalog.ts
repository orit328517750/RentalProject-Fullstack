import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CarService } from '../../services/car';
import { Car } from '../../models/car';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-car-catalog',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './car-catalog.html',
  styleUrl: './car-catalog.css',
})
export class CarCatalog implements OnInit {
  cars = signal<Car[]>([]);

  // הגדרת משתנים כ-null מאפשרת לנו לדעת אם המשתמש נגע בהם
  filterPlaces: number | null = null;
  filterLevel: number | null = null;
  filterPrice: number | null = null;

  constructor(private carService: CarService) {}

  ngOnInit() {
    this.loadAllCars();
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

    // לוגיקה לבחירת פונקציית הסינון הנכונה
    if (hasPlace && hasLevel && hasPrice) {
      obs = this.carService.getCarsByCriteria(this.filterPlaces!, this.filterLevel!, this.filterPrice!);
    } else if (hasPlace && !hasLevel && !hasPrice) {
      obs = this.carService.getCarByPlace(this.filterPlaces!);
    } else if (!hasPlace && hasLevel && !hasPrice) {
      obs = this.carService.getCarByLevel(this.filterLevel!);
    } else if (!hasPlace && !hasLevel && hasPrice) {
      obs = this.carService.getCarByPrice(this.filterPrice!);
    } else {
      // אם לא נבחר כלום או שילוב חלקי, נטען הכל (או תוכלי להוסיף שילובי ביניים)
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