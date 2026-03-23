// import { Component, signal } from '@angular/core';
// import { RouterOutlet } from '@angular/router';

// @Component({
//   selector: 'app-root',
//   imports: [RouterOutlet],
//   templateUrl: './app.html',
//   styleUrl: './app.css'
// })
// export class App {
//   protected readonly title = signal('RentalProject');
// }
import { Component, OnInit, signal } from '@angular/core';
import { RouterOutlet, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterModule, CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
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