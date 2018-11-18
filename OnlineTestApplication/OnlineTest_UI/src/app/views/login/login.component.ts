import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/auth-service/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
@Component({
  selector: 'app-dashboard',
  templateUrl: 'login.component.html'
})
export class LoginComponent {
  isLoginError : boolean = false;
  constructor(private userService : UserService,private router : Router) { }
  ngOnInit() {
  }
  OnSubmit(userName,password){
     this.userService.userAuthentication(userName,password).subscribe((data : any)=>{
       
      sessionStorage.setItem('userToken',data.access_token);
      sessionStorage.setItem('userRoles',data.Role);
      this.router.navigate(['/dashboard']);
    },
    (err : HttpErrorResponse)=>{
      this.isLoginError = true;
    });
  }
 }
