import { Routes } from '@angular/router';
import { RegisterComponent } from './components/register/register';

export const routes: Routes = [
  { path: '', component: RegisterComponent }, // דף הבית יהיה דף ההרשמה
  { path: 'register', component: RegisterComponent }
];