import 'zone.js'; // השורה החשובה ביותר לפתרון השגיאה NG0908
import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { App } from './app/app';

bootstrapApplication(App, appConfig)
  .catch((err) => console.error(err));



  //https://localhost:44372/api/customer/getallcustomers