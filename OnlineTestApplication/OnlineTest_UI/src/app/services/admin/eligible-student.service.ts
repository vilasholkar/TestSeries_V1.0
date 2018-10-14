import { Injectable } from '@angular/core';
import { Response, RequestOptions, Headers } from '@angular/http';
import { HttpClient, HttpClientModule, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HostName } from '../../shared/app-setting';
import { APIUrl } from '../../shared/API-end-points';

@Injectable({
  providedIn: 'root'
})
export class EligibleStudentService {
  private headers: Headers;
  private options: RequestOptions;
  constructor(private http: HttpClient) {
  // Creates header for post requests.
  this.headers = new Headers({ 'Content-Type': 'application/json' });
  this.options = new RequestOptions({ headers: this.headers });
   }
   getEligibleStudent(OnlieTestID:number): Observable<any> {
    return this.http.get(HostName.API_StartPoint + APIUrl.GetEligibleStudent+"?OnlineTestID="+OnlieTestID)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
}
