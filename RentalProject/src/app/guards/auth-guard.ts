import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  
  // בדיקה אם המשתמש קיים ב-SessionStorage
  const user = sessionStorage.getItem('loggedInUser');

  if (user) {
    // יש משתמש - מאשרים כניסה
    return true;
  } else {
    // אין משתמש - חוסמים ומעבירים ללוגין
    alert('גישה חסומה! עליך להתחבר קודם.');
    router.navigate(['/login']);
    return false;
  }
};