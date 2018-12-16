import { Injectable } from '@angular/core';
import { Response, RequestOptions, Headers } from '@angular/http';
import { HttpClient, HttpClientModule, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HostName } from '../../shared/app-setting';
import { APIUrl } from '../../shared/API-end-points';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { mapTo, delay } from 'rxjs/operators';

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
    return this.http.get(HostName.API_StartPoint + APIUrl.GET_Stream)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
  getCourseByStream(StreamId: Int32Array): Observable<any> {
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
     
    return this.http.post(HostName.API_StartPoint + APIUrl.GET_CourseByStream,StreamId)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
  getBatchByCourse(CourseId: Int32Array): Observable<any> {
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
     
    return this.http.post(HostName.API_StartPoint + APIUrl.GET_BatchByCourse,CourseId)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
  getOnlineTest(): Observable<any> {
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
    debugger;
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
     
    return this.http.get(HostName.API_StartPoint + APIUrl.GetOnlineTestById+"?OnlineTestId="+OnlineTestId)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
  getMasterData(): Observable<any> {
    return this.http.get(HostName.API_StartPoint + APIUrl.GetMasterData)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
 
}
