import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class ShortUrlService {

  responseRequestService = signal("response");

  constructor(private http: HttpClient) { }

  getShortUrl(url: string) {

    return this.http.get(`${environment.apiUrl}/shorten/${url}`);
   }
}
