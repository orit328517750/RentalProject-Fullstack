import { Component, OnInit } from '@angular/core';
import { RentService } from '../../services/rent.service'; 
import { RentModel } from '../../models/rent.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-rent',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './rent.html',
  styleUrl: './rent.css'
})
export class Rent implements OnInit {
    rentHistory: RentModel[] = [];

    constructor(private rentService: RentService) { }

    ngOnInit() {
      // 1. שליפת המשתמש מהזיכרון של הדפדפן
      const userJson = sessionStorage.getItem('loggedInUser');
      
      if (userJson) {
        const user = JSON.parse(userJson);
        //console.log('המשתמש ששלפנו מהסשן:', user); 
        
        this.rentService.getRentsByCustomerId(user.Id).subscribe({
          next: (data) => {
            this.rentHistory = data;
            console.log('ההיסטוריה נטענה בהצלחה:', this.rentHistory);
          },
          error: (err) => {
            console.error('שגיאה בטעינת ההיסטוריה:', err);
          }
        });
      }
    }
}