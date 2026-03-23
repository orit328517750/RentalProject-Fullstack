import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from '../models/customer.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  // הכתובת שנתת לי מה-localhost
  private baseUrl = 'https://localhost:44372/api/customer'; 

  constructor(private http: HttpClient) { }

  // קבלת כל הלקוחות
  getAllCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(`${this.baseUrl}/getallcustomers`);
  }

  // קבלת לקוח לפי ID (שימי לב לנתיב המדויק מה-Controller שלך)
  getCustomerById(id: number): Observable<Customer> {
    return this.http.get<Customer>(`${this.baseUrl}/GetCostomerByID/${id}`);
  }

  // הוספת לקוח חדש
  insertCustomer(customer: Customer): Observable<any> {
    return this.http.post(`${this.baseUrl}/insertclient`, customer);
  }

  // מחיקת לקוח (ה-API שלך מצפה לאובייקט ב-Post)
  deleteCustomer(customer: Customer): Observable<boolean> {
    return this.http.post<boolean>(`${this.baseUrl}/delete/c`, customer);
  }

  // פונקציית התחברות
login(email: string): Observable<Customer> {
  // אנחנו שולחים אובייקט עם אימייל כי השרת מצפה ל-CustomersDTO
  return this.http.post<Customer>(`${this.baseUrl}/login`, { email: email });
}
}

