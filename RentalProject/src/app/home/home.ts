import { Component } from '@angular/core';
import { RouterLink, RouterModule } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterLink, RouterModule], 
  // תיקנתי את הנתיב שיתאים לשם הקובץ המקובל (או לשם שיש לך בתיקייה)
  templateUrl: './home.html', 
  styleUrl: './home.css'
})
export class HomeComponent {}