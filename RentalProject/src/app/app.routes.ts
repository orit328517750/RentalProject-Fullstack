import { Routes } from '@angular/router';
import { RegisterComponent } from './components/register/register';
import { LoginComponent } from './components/login/login'; // ודאי שהנתיב לקומפוננטה נכון

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' }, // כשנכנסים לאתר, הוא יעבור אוטומטית ללוגין
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent }
];