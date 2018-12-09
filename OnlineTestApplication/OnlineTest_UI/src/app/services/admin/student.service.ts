import { Injectable } from '@angular/core';
import { Response, RequestOptions, Headers } from '@angular/http';
import { HttpClient, HttpClientModule, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HostName } from '../../shared/app-setting';
import { APIUrl } from '../../shared/API-end-points';
import { mapTo, delay } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class StudentService {
  private headers: Headers;
  private options: RequestOptions;
  constructor(private http: HttpClient) {
// Creates header for post requests.
  this.headers = new Headers({ 'Content-Type': 'application/json' });
  this.options = new RequestOptions({ headers: this.headers });
   }

   getStudentDetails(): Observable<any> {
    return this.http.get(HostName.API_StartPoint + APIUrl.GetStudentDetails)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .pipe(delay(3000))
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
}
