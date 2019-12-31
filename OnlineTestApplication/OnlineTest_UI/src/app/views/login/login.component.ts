import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/auth-service/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { HelperService } from "../../services/helper.service";
import { APIUrl } from "../../shared/API-end-points";
@Component({
  selector: 'app-dashboard',
  templateUrl: 'login.component.html'
})
export class LoginComponent {
  loginViewModel:any={};
  isLoginError: boolean = false;
  PhotoUrl: any;
  constructor(
    private userService: UserService,
    private router: Router,
    private spinner: NgxSpinnerService,
    private helperSvc: HelperService
  ) { }
  ngOnInit() {
   // this.loginViewModel.UserTypeID='1';
  }
  // OnSubmit(userName,password,UserTypeID){
  //   this.spinner.show();
  //    this.userService.userAuthentication(userName,password,UserTypeID).subscribe((data : any)=>{
  //    debugger;
  //     sessionStorage.setItem('userToken',data.access_token);
  //     sessionStorage.setItem('userRoles',data.Role);
  //     sessionStorage.setItem('FirstName',data.FirstName);
  //     if(data.Role==='Admin')
  //     {
  //     sessionStorage.setItem('UserID',data.UserID);
  //     }
  //     else if(data.Role==='Student')
  //     {
  //     sessionStorage.setItem('StudentID',data.UserID);
  //     }
  //     this.router.navigate(['/dashboard']);
  //   this.spinner.hide();
  //     this.helperSvc.notifySuccess(' Welcome ' + data.FirstName + ' ' + data.LastName);
  //   },
  //   (err : HttpErrorResponse)=>{
  //    // this.isLoginError = true;
  //      this.openSnackBar("Error: Incorrect username or password", "Close");
  //      this.spinner.hide();

  //   });
  // }
  OnSubmit() {
    debugger;
    if(this.loginViewModel.UserName!=''){
      if(this.loginViewModel.Password!=''){
    this.spinner.show();

    var data = "?username=" + this.loginViewModel.UserName + "&password=" + this.loginViewModel.Password + "&UserTypeID=" + this.loginViewModel.UserTypeID;
    // this.userService.userAuthentication(userName,password,UserTypeID).subscribe((data : any)=>{

    this.helperSvc.getService(APIUrl.GetLoginInfo + data).subscribe((data: any) => {
      if (data.Object.UserID > 0) {
        sessionStorage.setItem('userToken', data.access_token);
        sessionStorage.setItem('userRoles', data.Object.UserType);
        sessionStorage.setItem('FirstName', data.Object.FirstName);
        (data.Object.PhotoUrl != '' ? this.PhotoUrl = APIUrl.PhotoBaseURL + data.Object.PhotoUrl : this.PhotoUrl = 'assets/img/avatars/default-avatar.png');
        sessionStorage.setItem('PhotoUrl', this.PhotoUrl);
        sessionStorage.setItem('IsTestStarted', 'false');
        if (data.Object.UserType === 'Admin') {
          sessionStorage.setItem('UserID', data.Object.UserID);
          this.router.navigate(['/dashboard/admin-dashboard']);
        }
        else if (data.Object.UserType === 'Student') {
          sessionStorage.setItem('StudentID', data.Object.UserID);
          sessionStorage.setItem('BatchID', data.Object.BatchID);
          this.router.navigate(['/dashboard/student-dashboard']);
        }

        // this.router.navigate(['/dashboard']);
        this.spinner.hide();
        this.helperSvc.notifySuccess(' Welcome ' + data.Object.FirstName + ' ' + data.Object.LastName);
      }
      else {
        this.router.navigate(['/login']);
        this.spinner.hide();
        this.helperSvc.notifyError("Error: Incorrect username or password");
      }
    },
      (err: HttpErrorResponse) => {
        // this.isLoginError = true;
        // this.openSnackBar("Error: Incorrect username or password", "Close");
        this.helperSvc.notifyError("Error: Incorrect username or password");
        this.spinner.hide();

      });
    }
    else
    this.helperSvc.notifyError("Password is required.");
  }
    else
      this.helperSvc.notifyError("Username is required.");
  }
}
