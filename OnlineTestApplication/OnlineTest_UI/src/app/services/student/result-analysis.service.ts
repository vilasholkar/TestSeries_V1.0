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
export class ResultAnalysisService {
  private headers: Headers;
  private options: RequestOptions;
  constructor(private http: HttpClient) {
    // Creates header for post requests.
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
  }
  GetOnlineTestResultByTestID(TestID: number): Observable<any>{
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
    return this.http.get(HostName.API_StartPoint + APIUrl.GetOnlineTestResultByTestID+"?TestID="+TestID)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
  GetOnlineTestResultByStudentID(StudentID: number): Observable<any>{
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
    return this.http.get(HostName.API_StartPoint + APIUrl.GetOnlineTestResultByStudentID+"?StudentID="+StudentID)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
  ResultAnalysis(TestID: number): Observable<any>{
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
    return this.http.get(HostName.API_StartPoint + APIUrl.ResultAnalysis+"?TestID="+TestID)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
  GetPaperAnalysis(TestID: number): Observable<any>{
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
    return this.http.get(HostName.API_StartPoint + APIUrl.GetPaperAnalysis+"?TestID="+TestID)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }      
  GetStudentMarksReview(TestID: number): Observable<any>{
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
    return this.http.get(HostName.API_StartPoint + APIUrl.GetStudentMarksReview+"?TestID="+TestID)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
  GetTopper_Average(TestID: number): Observable<any>{
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
    return this.http.get(HostName.API_StartPoint + APIUrl.GetTopper_Average+"?TestID="+TestID)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }    
}
