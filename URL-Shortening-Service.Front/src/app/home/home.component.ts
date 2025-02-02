import { Component, signal } from '@angular/core';
import { ShortUrlFormComponent } from '../components/forms/short-url-form/short-url-form.component';
import { ResponseComponent } from '../components/response/response.component';

@Component({
  selector: 'app-home',
  imports: [ShortUrlFormComponent, ResponseComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  responseRequest = signal('responseRequest');
}
