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
export class OnlineTestService {
  private headers: Headers;
  private options: RequestOptions;
  constructor(private http: HttpClient) {
// Creates header for post requests.
  this.headers = new Headers({ 'Content-Type': 'application/json' });
  this.options = new RequestOptions({ headers: this.headers });
   }
   getStream(): Observable<any> {
    debugger;
    return this.http.get(HostName.API_StartPoint + APIUrl.GET_Stream)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
  getCourseByStream(StreamId: any): Observable<any> {
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
    debugger;
    return this.http.get(HostName.API_StartPoint + APIUrl.GET_CourseByStream+"?StreamId="+StreamId)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
  getBatchByCourse(CourseId: any): Observable<any> {
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
    debugger;
    return this.http.get(HostName.API_StartPoint + APIUrl.GET_BatchByCourse+"?CourseId="+CourseId)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
  getOnlineTest(): Observable<any> {
    debugger;
    return this.http.get(HostName.API_StartPoint + APIUrl.GET_OnlineTest)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
  addUpdateOnlineTest(OnlineTest: any): Observable<any>{
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
    debugger;
    return this.http.post(HostName.API_StartPoint + APIUrl.AddUpdateOnlineTest,OnlineTest)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
  getSession(): Observable<any> {
    debugger;
    return this.http.get(HostName.API_StartPoint + APIUrl.GET_Session)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
  deleteOnlineTest(OnlineTestId: number): Observable<any>{
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
    debugger;
    return this.http.post(HostName.API_StartPoint + APIUrl.DeleteOnlineTest,OnlineTestId)
      .map((response: Response) => {
        const data = response;
        return data; 
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
  getOnlineTestById(OnlineTestId: number): Observable<any>{
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
    debugger;
    return this.http.get(HostName.API_StartPoint + APIUrl.GetOnlineTestById+"?OnlineTestId="+OnlineTestId)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
}