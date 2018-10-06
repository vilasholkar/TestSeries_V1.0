import { Injectable, Provider } from '@angular/core';
import { Response, RequestOptions, Headers } from '@angular/http';
import { HttpClient, HttpClientModule, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import {HostName} from '../../shared/app-setting';
import {APIUrl} from '../../shared/API-end-points';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import {Question} from '../../views/test/quiz/models'
@Injectable()
export class QuizService {
  private headers: Headers;
  private options: RequestOptions;
  constructor(private http: HttpClient) {
    // Creates header for post requests.
    this.headers = new Headers({ 'Content-Type': 'application/json' });
    this.options = new RequestOptions({ headers: this.headers });
}
  getQuiz(testID: Question): Observable<any> {
    return this.http.get(HostName.API_StartPoint + APIUrl.GET_Quiz + '?testID=' + testID)
        .map((response: Response) => {
            const data = response;
            return data;
        })
        .catch((error: any) => {
            return Observable.throw(error);
        });
  }
  SubmitQuiz(QuizResponse:Question[]): Observable<any> {
    debugger
     return this.http.post(HostName.API_StartPoint + APIUrl.Submit_Quiz, QuizResponse)
        .map((response: Response) => {
             const data = response;
             return data;
        })
        .catch((error: any) => {
             return Observable.throw(error);
        });
  }    
}
