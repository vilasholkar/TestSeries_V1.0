import { Injectable, Provider } from '@angular/core';
import { Response, RequestOptions, Headers } from '@angular/http';
import { HttpClient, HttpClientModule, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {HostName} from '../../shared/app-setting';
import {APIUrl} from '../../shared/API-end-points';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { mapTo, delay } from 'rxjs/operators';

@Injectable()
export class TestTypeService {
  private headers: Headers;
  private options: RequestOptions;
  constructor(private http: HttpClient) {
    // Creates header for post requests.
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
}
  getTestTypes(): Observable<any> {
       
    return this.http.get(HostName.API_StartPoint + APIUrl.GET_TestTypes)
        .map((response: Response) => {
            const data = response;
            return data;
        })
        .pipe(delay(2000))
        .catch((error: any) => {
            return Observable.throw(error);
        });
  }
  addUpdateTestTypes(TestType: any): Observable<any> {
     this.headers = new Headers({ 'Content-Type': 'application/json' });
     this.options = new RequestOptions({ headers: this.headers });
     return this.http.post(HostName.API_StartPoint + APIUrl.AddUpdateTestTypes, TestType)
        .map((response: Response) => {
             const data = response;
             return data;
        })
        .pipe(delay(2000))
        .catch((error: any) => {
             return Observable.throw(error);
        });
  }
  deleteTestTypeById(TestType: any): Observable<any> {
     this.headers = new Headers({ 'Content-Type': 'application/json' });
     this.options = new RequestOptions({ headers: this.headers });
     return this.http.post(HostName.API_StartPoint + APIUrl.DeleteTestTypes, TestType)
        .map((response: Response) => {
             const data = response;
             return data;
        })
        .pipe(delay(2000))
        .catch((error: any) => {
             return Observable.throw(error);
        });
  }
}
