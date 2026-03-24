// import { Routes } from '@angular/router';
// import { RegisterComponent } from './components/register/register';
// import { LoginComponent } from './components/login/login'; // ודאי שהנתיב לקומפוננטה נכון

// export const routes: Routes = [
//   { path: '', redirectTo: 'login', pathMatch: 'full' }, // כשנכנסים לאתר, הוא יעבור אוטומטית ללוגין
//   { path: 'login', component: LoginComponent },
//   { path: 'register', component: RegisterComponent }
// ];

import { Routes } from '@angular/router';
import { RegisterComponent } from './components/register/register';
import { LoginComponent } from './components/login/login';
import { CarCatalog } from './components/car-catalog/car-catalog';
import { authGuard } from './guards/auth-guard'; // ייבוא הגארד
import { PaymentComponent } from './components/payment/payment'; // ייבוא קומפוננטת התשלום
export const routes: Routes = [
  { path: '', redirectTo: 'catalog', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'payment', component: PaymentComponent}, 
  {
    path: 'catalog', 
    component: CarCatalog, 
    canActivate: [authGuard] // <--- כאן קורה הקסם!
  }
];