import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RentService { 
  // הכתובת הבסיסית של ה-API שלך
  private baseUrl = 'https://localhost:44372/api/rents';

  constructor(private http: HttpClient) { }

  // פונקציה לשליפת היסטוריה
  getRentsByCustomerId(customerId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/getrentsbycustomerid/${customerId}`);
  }

  // הפונקציה החדשה שאת מוסיפה כאן:
  addRent(rent: any): Observable<boolean> {
    // אנחנו משתמשים ב-baseUrl ובנתיב המתאים בשרת (למשל add)
    return this.http.post<boolean>(`${this.baseUrl}/add`, rent);
  }
}