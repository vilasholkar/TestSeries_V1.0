import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Response,RequestOptions,Headers } from "@angular/http";
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { User } from '../../models/auth-model/user.model';
import {HostName} from '../../shared/app-setting';
import {APIUrl} from '../../shared/API-end-points';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { HelperService } from '../../services/helper.service'
import { map, catchError } from 'rxjs/operators';
@Injectable()
export class UserService {
  private headers: Headers;
  private options: RequestOptions;
  public isAdmin = false;
  public isStudent = false;
  readonly rootUrl = 'http://localhost:35257';
  constructor(private http: HttpClient,
    private helperSvc: HelperService) { 
  }
 
  userAuthentication(userName, password,UserTypeID) {
    var data ="username=" + userName + "&password=" + password + "&grant_type=password&UserTypeID="+UserTypeID;
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application//x-www-form-urlencoded','No-Auth':'True' });
    return this.http.post(HostName.API_StartPoint+ 'token', data, { headers: reqHeader })
   .pipe(map((response: Response) => {
      const data = response;
      return data;
    }))
    .catch((error: any) => {
      return Observable.throw(error);
    });
  }
  getUserClaims(){
   return  this.http.get(this.rootUrl+'/api/GetUserClaims');
  }
  getAllRoles() {
    var reqHeader = new HttpHeaders({ 'No-Auth': 'True' });
    return this.http.get(this.rootUrl + '/api/GetAllRoles', { headers: reqHeader });
  }
  roleMatch(allowedRoles): boolean {
    var isMatch = false;
    var userRoles: string[] = JSON.parse(sessionStorage.getItem('userRoles'));
    allowedRoles.forEach(element => {
      if (userRoles.indexOf(element) > -1) {
        isMatch = true;
        return false;
      }
    });
    return isMatch;
  }
}