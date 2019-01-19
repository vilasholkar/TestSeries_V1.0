import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/auth-service/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { MatSnackBar,MatSnackBarVerticalPosition } from '@angular/material';
import { NgxSpinnerService } from 'ngx-spinner';
import { HelperService } from "../../services/helper.service";
import { APIUrl } from "../../shared/API-end-points";
@Component({
  selector: 'app-dashboard',
  templateUrl: 'login.component.html'
})
export class LoginComponent {
  isLoginError : boolean = false;
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  constructor(
    private userService : UserService,
    private router : Router, 
    private snackBar: MatSnackBar,
    private spinner: NgxSpinnerService,
    private helperSvc: HelperService
  ) { }
  ngOnInit() {
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
  OnSubmit(userName,password,UserTypeID){
    this.spinner.show();
    debugger;
    var data ="?username=" + userName + "&password=" + password + "&UserTypeID="+UserTypeID;
     // this.userService.userAuthentication(userName,password,UserTypeID).subscribe((data : any)=>{
      
    this.helperSvc.getService(APIUrl.GetLoginInfo+data).subscribe((data : any)=>{
     if(data.Object.UserID > 0)
     {
      sessionStorage.setItem('userToken',data.access_token);
      sessionStorage.setItem('userRoles',data.Object.UserType);
      sessionStorage.setItem('FirstName',data.Object.FirstName);
      if(data.Object.UserType==='Admin')
      {
      sessionStorage.setItem('UserID',data.Object.UserID);
      }
      else if(data.Object.UserType==='Student')
      {
      sessionStorage.setItem('StudentID',data.Object.UserID);
      }
      this.router.navigate(['/dashboard']);
      this.spinner.hide();
      this.helperSvc.notifySuccess(' Welcome ' + data.Object.FirstName + ' ' + data.Object.LastName);
    }
    else{
      this.router.navigate(['/login']);
      this.spinner.hide();
      this.helperSvc.notifyError("Error: Incorrect username or password");
    }
    },
    (err : HttpErrorResponse)=>{
     // this.isLoginError = true;
      // this.openSnackBar("Error: Incorrect username or password", "Close");
      this.helperSvc.notifyError("Error: Incorrect username or password");
       this.spinner.hide();

    });
  }
  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 4000,
      verticalPosition: this.verticalPosition,
    });
}
 }
