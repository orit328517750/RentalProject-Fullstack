import { Routes } from '@angular/router';
import { RegisterComponent } from './components/register/register';
import { LoginComponent } from './components/login/login';
import { CarCatalog } from './components/car-catalog/car-catalog';
import { HomeComponent } from './home/home'; 
import { authGuard } from './guards/auth-guard'; 
import { PaymentComponent } from './components/payment/payment'; 
import { Rent } from './components/rent/rent';
import { RentalDetails } from './components/rental-details/rental-details';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  
  // הנתיב המרכזי למעבר מהקטלוג
  { path: 'rental-details', component: RentalDetails },

  // נתיב חלופי במידה ויש שימוש ישיר ב-ID
  { path: 'payment/:id', component: PaymentComponent }, 
  
  { path: 'history', component: Rent, canActivate: [authGuard] },
  { path: 'catalog', component: CarCatalog, canActivate: [authGuard] },
  
  // תמיד אחרון ברשימה
  { path: '**', redirectTo: 'home' }
];