import { Injectable } from '@angular/core';
import { Response, RequestOptions, Headers } from '@angular/http';
import { HttpClient, HttpClientModule, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HostName } from '../../shared/app-setting';
import { APIUrl } from '../../shared/API-end-points';
import {EligibleStudent} from '../../models/test';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

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

  //  getEligibleStudent(OnlieTestID:number): Observable<any> {
  //   debugger;
  //   return this.http.get(HostName.API_StartPoint + APIUrl.GetEligibleStudent+"?OnlineTestID="+OnlieTestID)
  //     .map((response: Response) => {
  //       const data = response;
  //       return data;
  //     })
  //     .catch((error: any) => {
  //       return Observable.throw(error);
  //     });
  // }

  getEligibleStudent(OnlieTestID:number): Observable<EligibleStudent[]> {
    return this.http.get<EligibleStudent[]>(HostName.API_StartPoint + APIUrl.GetEligibleStudent+"?OnlineTestID="+OnlieTestID);
  }

  addEligibleStudent(EligibleStudentData:any): Observable<any>{
    debugger;
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
     
    return this.http.post(HostName.API_StartPoint + APIUrl.AddEligibleStudent,EligibleStudentData)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
}
