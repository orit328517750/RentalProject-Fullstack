import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common'; // פותר את שגיאת ה-ngIf
import { Router, RouterModule } from '@angular/router'; // פותר את שגיאות הקישורים

@Component({
  selector: 'app-navbar',
  standalone: true,
  // כאן אנחנו מוסיפים את המודולים שחסרים
  imports: [CommonModule, RouterModule], 
  templateUrl: './navbar.html',
  styleUrl: './navbar.css'
})
export class NavbarComponent implements OnInit {
  // העברנו את הסיגנל לכאן כדי שה-HTML של הנאבבר יכיר אותו
  public userName = signal<string | null>(null);

  constructor(private router: Router) {}

  ngOnInit() {
    this.checkUserStatus();
    
    // בונוס: האזנה לשינויים ב-Storage למקרה שהמשתמש מתחבר בדף אחר
    window.addEventListener('storage', () => this.checkUserStatus());
  }

 checkUserStatus() {
  try {
    const userJson = sessionStorage.getItem('loggedInUser');
    
    // בדיקה אם בכלל קיים ערך ב-Storage
    if (userJson && userJson !== "undefined" && userJson !== "null") {
      const user = JSON.parse(userJson);
      // עדכון ה-Signal עם השם
      this.userName.set(user.firstName || user.name || 'משתמש'); 
    } else {
      // אם אין משתמש, חייבים להגדיר null כדי שה-!userName() יעבוד
      this.userName.set(null);
    }
  } catch (e) {
    // במקרה של שגיאת Parse, נתייחס כאל אורח
    this.userName.set(null);
  }
}

  logout() {
    sessionStorage.removeItem('loggedInUser');
    this.userName.set(null);
    this.router.navigate(['/login']);
  }
}