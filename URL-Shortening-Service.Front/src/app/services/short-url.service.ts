import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { ResponseOkShortUrl } from '../interfaces/response-ok-short-url';
import { catchError, of } from 'rxjs';
import { Response } from '../interfaces/response';

@Injectable({
  providedIn: 'root',
})
export class ShortUrlService {
  constructor(private http: HttpClient) {}

  getShortUrl(url: string) {
    return this.http.get<Response>(`${environment.apiUrl}/shorten/${url}`).pipe(
      catchError((error) => {
        if (error.status === 404) {
          return of(error.error as Response);
        } else {
          return of(error.error as Response);
        }
      })
    );
  }

  postShowUrl(url: string) {
    return this.http
      .post<Response>(`${environment.apiUrl}/shorten`, { url })
      .pipe(
        catchError((error) => {
          if (error.status === 400) {
            return of(error.error as Response);
          } else {
            return of(error.error as Response);
          }
        })
      );
  }

  putShowUrl(shortUrl: string, newUrl: string) {
    return this.http
      .put<Response>(`${environment.apiUrl}/shorten/${shortUrl}`, {
        url: newUrl,
      })
      .pipe(
        catchError((error) => {
          if (error.status === 400) {
            return of(error.error as Response);
          } else {
            return of(error.error as Response);
          }
        })
      );
  }

  deleteShowUrl(shortUrl: string) {
    return this.http
      .delete<Response>(`${environment.apiUrl}/shorten/${shortUrl}`)
      .pipe(
        catchError((error) => {
          if (error.status === 404) {
            return of(error.error as Response);
          } else {
            return of(error.error as Response);
          }
        })
      );
  }
}
