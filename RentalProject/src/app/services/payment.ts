import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class PaymentService {
  // וודאי שזה הפורט של השרת שלך
  private apiUrl = 'https://localhost:44372/api/payments/add'; 

  constructor(private http: HttpClient) {}

  addPayment(payment: any): Observable<boolean> {
    return this.http.post<boolean>(this.apiUrl, payment);
  }
}