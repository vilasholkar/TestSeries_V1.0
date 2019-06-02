import { Component, OnInit } from '@angular/core';
import { HelperService } from "../../../services/helper.service";
import { APIUrl } from "../../../shared/API-end-points";
@Component({
  selector: 'app-forget-password',
  templateUrl: './forget-password.component.html',
  styleUrls: ['./forget-password.component.scss']
})
export class ForgetPasswordComponent implements OnInit {
  IsSendOTPSection = true;
  IsNewPasswordSection = false;
  forgetPasswordViewModel: any = {};
  OTP: any;
  UserTypeID: any;
  constructor(private helperSvc: HelperService) { }

  ngOnInit() {

  }

  SendOTP() {
    debugger;
    if (this.IsSendOTPSection && !this.IsNewPasswordSection) {
      this.forgetPasswordViewModel.Action = "SendOTP";
      this.helperSvc.postService(APIUrl.ForgetPassword, this.forgetPasswordViewModel).subscribe((data: any) => {
        if (data.Object.ConfirmOTP > 0) {
          this.OTP = data.Object.ConfirmOTP;
          this.UserTypeID = data.Object.UserTypeID;
          this.IsSendOTPSection = false;
          this.IsNewPasswordSection = false;
        }
        else {
          this.helperSvc.notifyError("UserName not valid.");
        }
      });

    }
    else if (!this.IsSendOTPSection && !this.IsNewPasswordSection) {
      this.forgetPasswordViewModel.Action = "ConfirmOTP";
      if (this.forgetPasswordViewModel.ConfirmOTP === this.OTP) {
        this.IsSendOTPSection = false;
        this.IsNewPasswordSection = true;
      }
      else {
        this.helperSvc.notifyError("OTP not valid.");
        this.IsSendOTPSection = true;
        this.IsNewPasswordSection = false;
        this.forgetPasswordViewModel={};
      }
    }
    else if (!this.IsSendOTPSection && this.IsNewPasswordSection) {
      if (this.forgetPasswordViewModel.NewPassword === this.forgetPasswordViewModel.ConfirmPassword) {
        this.forgetPasswordViewModel.Action = "UpdatePassword";
        this.forgetPasswordViewModel.UserTypeID = this.UserTypeID;
        this.helperSvc.postService(APIUrl.ForgetPassword, this.forgetPasswordViewModel).subscribe((data: any) => {
          if (data.Object.Status=== "Success") {
        this.helperSvc.notifySuccess("Password changed sucessfully.");
            this.IsSendOTPSection = true;
            this.IsNewPasswordSection = false;
            this.forgetPasswordViewModel={};
          }
          else {
            this.helperSvc.notifyError("Password not changed.");
          }
        });
      }
      else {
        this.helperSvc.notifyError("New Password and Confirm Password not match.");
      }
    }
  }
  //  changePassword() {
  //    debugger;
  //   // this.changePasswordVM = this.changePasswordViewModel;
  //    if (this.changePasswordViewModel.NewPassword === this.changePasswordViewModel.ConfirmPassword) {
  //      if(sessionStorage.getItem("userRoles")==='Admin')
  //      {
  //        this.changePasswordViewModel.UserID=sessionStorage.getItem("UserID");
  //        this.changePasswordViewModel.UserTypeID=1;
  //      }
  //      else if(sessionStorage.getItem("userRoles")==='Student')
  //      {
  //        this.changePasswordViewModel.UserID=sessionStorage.getItem("StudentID");
  //        this.changePasswordViewModel.UserTypeID=2;
  //      }
  //      this.helperSvc.postService(APIUrl.ChangePassword, this.changePasswordViewModel)
  //        .subscribe(data => {
  //          if (data.Message === 'Success') {
  //          var a=  data.Object==1?this.helperSvc.notifySuccess('Password changed sucessfully.'):this.helperSvc.notifyError('Password mismatch, please try again.');
  //           // this.changePasswordViewModel = {};

  //            ;
  //          }
  //        }, error => {
  //          this.helperSvc.errorHandler(error);
  //          console.log(error);
  //        });
  //    }
  //    else{
  //      this.helperSvc.notifyError("New Password and Confirm Password not match.");
  //    }
  //  }

}
