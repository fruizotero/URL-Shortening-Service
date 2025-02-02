import { Component, inject, signal } from '@angular/core';
import { ShortUrlFormComponent } from '../components/forms/short-url-form/short-url-form.component';
import { ResponseComponent } from '../components/response/response.component';
import { FormShortUrl } from '../interfaces/form-short-url';
import { ShortUrlService } from '../services/short-url.service';

@Component({
  selector: 'app-home',
  imports: [ShortUrlFormComponent, ResponseComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  responseRequest = signal('');
  shorturlService = inject(ShortUrlService);

  onFormSubmitted(formData: FormShortUrl) {
    let data = this.shorturlService
      .getShortUrl(formData.shortUrl)
      .subscribe((response) => {
        this.updateResponse(response);
      });

    this.updateResponse(data);
  }

  updateResponse(response: any) {
    this.responseRequest.set(response);
  }
}
