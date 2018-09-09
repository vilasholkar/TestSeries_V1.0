import { Injectable } from '@angular/core';
import { Response, RequestOptions, Headers } from '@angular/http';
import { HttpClient, HttpClientModule, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HostName } from '../../shared/app-setting';
import { APIUrl } from '../../shared/API-end-points';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable({
  providedIn: 'root'
})
export class StudentOnlineTestService {
  private headers: Headers;
  private options: RequestOptions;
  constructor(private http: HttpClient) {
    // Creates header for post requests.
  this.headers = new Headers({ 'Content-Type': 'application/json' });
  this.options = new RequestOptions({ headers: this.headers });
 
   }

  getOnlineTestByStudentID(StudentID: number): Observable<any>{
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
    debugger;
    return this.http.get(HostName.API_StartPoint + APIUrl.GetOnlineTestByStudentID+"?StudentID="+StudentID)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
}

