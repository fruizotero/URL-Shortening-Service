import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { ResponseOkShortUrl } from '../interfaces/response-ok-short-url';
import { catchError, of } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ShortUrlService {

  responseRequestService = signal("response");

  constructor(private http: HttpClient) { }

  getShortUrl(url: string) {

    return this.http.
      get<ResponseOkShortUrl>(`${environment.apiUrl}/shorten/${url}`)
      .pipe(
        catchError((error) => {
          if (error.status === 404) {
            return of({ message: 'Url not found' });
          } else {
            return of({ error: true, message: 'a problem occurred with the request' });
          }
        })
      );
   }
}
