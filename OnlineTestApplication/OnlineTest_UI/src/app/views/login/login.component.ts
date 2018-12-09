import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/auth-service/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { MatSnackBar,MatSnackBarVerticalPosition } from '@angular/material';
import { NgxSpinnerService } from 'ngx-spinner';
import { HelperService } from "../../services/helper.service";
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
  OnSubmit(userName,password,UserTypeID){
    this.spinner.show();
     this.userService.userAuthentication(userName,password,UserTypeID).subscribe((data : any)=>{
     debugger;
      sessionStorage.setItem('userToken',data.access_token);
      sessionStorage.setItem('userRoles',data.Role);
      sessionStorage.setItem('FirstName',data.FirstName);
      if(data.Role==='Admin')
      {
      sessionStorage.setItem('UserID',data.UserID);
      }
      else if(data.Role==='Student')
      {
      sessionStorage.setItem('StudentID',data.UserID);
      }
      this.router.navigate(['/dashboard']);
    this.spinner.hide();
      this.helperSvc.notifySuccess(' Welcome ' + data.FirstName + ' ' + data.LastName);
    },
    (err : HttpErrorResponse)=>{
     // this.isLoginError = true;
       this.openSnackBar("Error: Incorrect username or password", "Close");
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
