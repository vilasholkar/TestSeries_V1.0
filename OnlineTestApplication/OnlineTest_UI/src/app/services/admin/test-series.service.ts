import { Injectable, Provider } from '@angular/core';
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
export class TestSeriesService {
  private headers: Headers;
  private options: RequestOptions;
  constructor(private http: HttpClient) {
    // Creates header for post requests.
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
  }
  getTestSeries(): Observable<any> {
     
    return this.http.get(HostName.API_StartPoint + APIUrl.GET_TestSeries)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
  addUpdateTestSeries(TestSeries: any): Observable<any> {
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
    return this.http.post(HostName.API_StartPoint + APIUrl.AddUpdateTestSeries, TestSeries)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
  deleteTestSeriesById(TestSeries: any): Observable<any> {
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
    return this.http.post(HostName.API_StartPoint + APIUrl.DeleteTestSeries, TestSeries)
      .map((response: Response) => {
        const data = response;
        return data;
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
}
