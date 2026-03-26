// --- הגרסה הסופית والמשולבת (הלוגיקה של חברתך + העיצוב שלך) ---

// 1. ה-Imports המאוחדים
import { Component, OnInit, signal } from '@angular/core';
import { RouterOutlet, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

// --- תוספות העיצוב שלך: ייבוא הקומפוננטות הוויזואליות ---
// (ודאי שהנתיב לתיקיות נכון אצלך בפרויקט!)
import { NavbarComponent } from './navbar/navbar'; 
import { Footer } from './footer/footer';

@Component({
  selector: 'app-root',
  standalone: true,
  
  // --- תוספות העיצוב שלך: הוספת ה-imports לרשימה ---
  imports: [RouterOutlet, RouterModule, CommonModule, NavbarComponent, Footer],
  
  // שימוש ב-HTML וה-CSS הסופיים (של חברתך, שהם המעודכנים ביותר)
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  // --- הלוגיקה המשותפת (כמעט זהה בשתי הגרסאות) ---
  
  // סיגנל לשם המשתמש - התחלה כריק
  public userName = signal<string | null>(null);
  protected readonly title = signal('RentalProject');

  constructor(private router: Router) {}

  ngOnInit() {
    // בדיקה ראשונית כשהאתר עולה אם יש מישהו מחובר
    this.checkUserStatus();
  }

  // פונקציה שבודקת את ה-SessionStorage
  checkUserStatus() {
    const userJson = sessionStorage.getItem('loggedInUser');
    if (userJson) {
      const user = JSON.parse(userJson);
      this.userName.set(user.firstName);
    } else {
      this.userName.set(null);
    }
  }

  // פונקציית התנתקות
  logout() {
    sessionStorage.removeItem('loggedInUser');
    this.userName.set(null);
    this.router.navigate(['/login']);
  }
}