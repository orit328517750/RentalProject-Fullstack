import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Car } from '../models/car';

@Injectable({
  providedIn: 'root'
})
export class CarService {
  private apiUrl = 'https://localhost:44372/api/cars'; 

  constructor(private http: HttpClient) {}

  getAllCars(): Observable<Car[]> {
    return this.http.get<Car[]>(`${this.apiUrl}/getallcars`);
  }

  // סינון משולב
  getCarsByCriteria(numMekomot: number, maxRama: number, maxPrice: number): Observable<Car[]> {
    return this.http.get<Car[]>(`${this.apiUrl}/getcarsbycriteria/${numMekomot}/${maxRama}/${maxPrice}`);
  }

  // סינונים בודדים
  getCarByPlace(numMekomot: number): Observable<Car[]> {
    return this.http.get<Car[]>(`${this.apiUrl}/getcarsbynummekomot/${numMekomot}`);
  }

  getCarByLevel(maxRama: number): Observable<Car[]> {
    return this.http.get<Car[]>(`${this.apiUrl}/getcarsbyrama/${maxRama}`);
  }

  getCarByPrice(maxPrice: number): Observable<Car[]> {
    return this.http.get<Car[]>(`${this.apiUrl}/getcarsbypriceperday/${maxPrice}`);
  }

  // בתוך CarService
  getCarByCode(code: number): Observable<Car> {
    // הוספנו את 'getcarsbyid' לנתיב כדי להתאים ל-C#
    return this.http.get<Car>(`${this.apiUrl}/getcarsbyid/${code}`); 
  }
}