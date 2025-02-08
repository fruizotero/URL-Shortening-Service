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
    let { method } = formData;

    switch (method) {
      case 'get':
        this.getShortUrl(formData);
        break;

      case 'post':
        this.postShorUrl(formData);
        break;
      case 'put':
        this.putShorUrl(formData);
        break;
      case 'delete':
        this.deleteShortUrl(formData);
        break;

      default:
        break;
    }
  }

  private getShortUrl(formData: FormShortUrl) {
    this.shorturlService
      .getShortUrl(formData.shortUrl)
      .subscribe((response) => {
        if (response.success) {

          this.updateResponse(response.data);
        } else {
          this.updateResponse(response);
         }

      });

    // this.updateResponse(data);
  }

  private postShorUrl(formData: FormShortUrl) {
   this.shorturlService
      .postShowUrl(formData.shortUrl)
      .subscribe((response) => {
        if (response.success) {
          this.updateResponse(response.data);
        } else {
          this.updateResponse(response);
        }
      });

  }

  private putShorUrl(formData: FormShortUrl) {
    this.shorturlService
      .putShowUrl(formData.shortUrl, formData.newUrl)
      .subscribe((response) => {
        if (response.success) {
          this.updateResponse(response.data);
        } else {
          this.updateResponse(response);
        }
      });

  }

  private deleteShortUrl(formData: FormShortUrl) {
    this.shorturlService
      .deleteShowUrl(formData.shortUrl)
      .subscribe((response) => {
        if (response.success) {
          this.updateResponse(response.data);
        } else {
          this.updateResponse(response);
        }
      });
   }

  updateResponse(response: any) {
    this.responseRequest.set(response);
  }
}
