import { Injectable, Provider } from '@angular/core';
import { Response, RequestOptions, Headers } from '@angular/http';
import { HttpClient, HttpClientModule, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {HostName} from '../../shared/app-setting';
import {APIUrl} from '../../shared/API-end-points';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
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
            // let s=responce.status;
            return data;
        })
        .catch((error: any) => {
            return Observable.throw(error);
        });
}
}
