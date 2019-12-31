import { Injectable, Renderer2 } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, Observer } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';
import { HostName } from '../shared/app-setting';
import { APIUrl } from "../shared/API-end-points";
import { Http, Response, Headers, RequestOptions, ResponseContentType } from '@angular/http';
import { DatePipe } from '@angular/common';

import 'rxjs/add/operator/map';
@Injectable()
export class HelperService {
  public PaginationConfig: any[] = [50, 100, 500];
  public ShowSidebar: boolean;
  public sidebarToggler: any;
  public LoginLogoPath: any;
  public MainLogoPath: any;
  constructor(
    private http: HttpClient,
    private toasterSvc: ToastrService,
    private ngxSpinnerSvc: NgxSpinnerService,
    private http1:Http,
    private datePipe: DatePipe
  ) {
  this.ShowSidebar = true;
    this.sidebarToggler = 'lg';
    this.onPageLoad();
  }

  onPageLoad() {
    this.getService(APIUrl.GetGeneralSettings).subscribe((data: any) => {
      if (data.Message === 'Success') {
        this.LoginLogoPath = data.Object.filter(option => option.Key === 'Login_Logo')[0].Value;
        this.MainLogoPath = data.Object.filter(option => option.Key === 'Main_Logo')[0].Value;
      }
    });
  }
  hide_Sidebar() {
    this.ShowSidebar = false;
    this.sidebarToggler = false;
  }

  show_Sidebar() {
  this.ShowSidebar = true;
    this.sidebarToggler = 'lg';
  }

  static toBool(val) {
    if (val === 'undefined' || val == null || val === '' || val === 'false' || val === 'False') {
      return false;
    } else { // if (val == true || val == 'true' || val == 'True')
      return true;
    }
  }
  static shuffle(array) {
    let currentIndex = array.length, temp, randomIndex;

    while (0 !== currentIndex) {
      randomIndex = Math.floor(Math.random() * currentIndex);
      currentIndex -= 1;

      temp = array[currentIndex];
      array[currentIndex] = array[randomIndex];
      array[randomIndex] = temp;
    }
    return array;
  }
  static extend(out) {
    out = out || {};

    for (let i = 1; i < arguments.length; i++) {
      if (!arguments[i]) {
        continue;
      }

      for (const key in arguments[i]) {
        if (arguments[i].hasOwnProperty(key)) {
          out[key] = arguments[i][key];
        }
      }
    }
    return out;
  }
  public notifySuccess(msg: string = null) {
    this.toasterSvc.success(msg, null, {
      // timeOut: 3000,
      // positionClass: 'toast-top-right'
    });
  }
  public notifyError(msg: string = null) {
    this.toasterSvc.error(msg);
  }

  postService(url, data = null): Observable<any> {
    return new Observable<any>((observer: Observer<any>) => {
      this.ngxSpinnerSvc.show();
      this.http.post(HostName.API_StartPoint + url, data)
        .subscribe(
          res => {
            observer.next(res);
            observer.complete();
            this.ngxSpinnerSvc.hide();
          },
          err => {
            this.errorHandler(err);
            observer.error(err);
            observer.complete();
            this.ngxSpinnerSvc.hide();
          }
        );
    });

  }
  getService(url, params = null): Observable<any> {
    return new Observable<any>((observer: Observer<any>) => {
      this.ngxSpinnerSvc.show();
      this.http.get(HostName.API_StartPoint + url).subscribe((res) => {
        observer.next(res);
        observer.complete();
        this.ngxSpinnerSvc.hide();
      },
        (err) => {
          this.errorHandler(err);
          observer.complete();
          this.ngxSpinnerSvc.hide();
        }
      );
    });

  }

  postService_WithoutSpinner(url, data = null): Observable<any> {
    return new Observable<any>((observer: Observer<any>) => {
      this.http.post(HostName.API_StartPoint + url, data)
        .subscribe(
          res => {
            observer.next(res);
            observer.complete();
          },
          err => {
            this.errorHandler(err);
            observer.error(err);
            observer.complete();
          }
        );
    });

  }
  getService_WithoutSpinner(url, params = null): Observable<any> {
    return new Observable<any>((observer: Observer<any>) => {
      this.http.get(HostName.API_StartPoint + url).subscribe((res) => {
        observer.next(res);
        observer.complete();
      },
        (err) => {
          this.errorHandler(err);
          observer.complete();
        }
      );
    });

  }
  postService_WithHeader(url, data = null): Observable<any> {
    return new Observable<any>((observer: Observer<any>) => {
      this.http.post(HostName.API_StartPoint + url, data)
        .subscribe(
          res => {
            observer.next(res);
            observer.complete();
          },
          err => {
            this.errorHandler(err);
            observer.error(err);
            observer.complete();
          }
        );
    });
  }
  postFile(url,fileUpload:File) { 
    const formData:FormData=new FormData();
    formData.append('file', fileUpload, fileUpload.name);
    return this.http.post(HostName.API_StartPoint + url,formData);
  }
  
  public errorHandler(error) {
    switch (error.status) {
      case 400:
        this.notifyError(error.error.ExceptionMessage);
        break;
      case 401:
        this.notifyError(error.error.ExceptionMessage);
        break;
      case 402:
        this.notifyError(error.error.ExceptionMessage);
        break;
      case 403:
        this.notifyError(error.error.ExceptionMessage);
        break;
      case 404:
        this.notifyError(error.error.ExceptionMessage);
        break;
      case 500:
        this.notifyError(error.error.ExceptionMessage);
        break;
      default:
        this.notifyError('Some Error Occured.');
        break;
    }
  }

  public getFile(filepath: string):Observable<any>{
    
    let options = new RequestOptions({responseType: ResponseContentType.Blob});
    return this.http1.get(HostName.API_StartPoint+filepath , options)
        .map((response: Response) => <Blob>response.blob());
  }
  convertDate(date) {
    return this.datePipe.transform(date, 'yyyy-MM-ddT00:00:00.000-00:00');
  }
}
