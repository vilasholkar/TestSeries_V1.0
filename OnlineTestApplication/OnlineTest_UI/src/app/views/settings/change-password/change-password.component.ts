import { Component, OnInit } from '@angular/core';
import { HelperService } from "../../../services/helper.service";
import { APIUrl } from "../../../shared/API-end-points";
// import { ChangePassword } from "../../../models/settings";
@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {
 // changePasswordVM: ChangePassword;
  changePasswordViewModel: any = {};
  constructor(private helperSvc: HelperService) { }

  ngOnInit() {
  }
  changePassword() {
    debugger;
   // this.changePasswordVM = this.changePasswordViewModel;
    if (this.changePasswordViewModel.NewPassword === this.changePasswordViewModel.ConfirmPassword) {
      if(sessionStorage.getItem("userRoles")==='Admin')
      {
        this.changePasswordViewModel.UserID=sessionStorage.getItem("UserID");
        this.changePasswordViewModel.UserTypeID=1;
      }
      else if(sessionStorage.getItem("userRoles")==='Student')
      {
        this.changePasswordViewModel.UserID=sessionStorage.getItem("StudentID");
        this.changePasswordViewModel.UserTypeID=2;
      }
      this.helperSvc.postService(APIUrl.ChangePassword, this.changePasswordViewModel)
        .subscribe(data => {
          if (data.Message === 'Success') {
          var a=  data.Object==1?this.helperSvc.notifySuccess('Password changed sucessfully.'):this.helperSvc.notifyError('Password mismatch, please try again.');
           // this.changePasswordViewModel = {};
           
            ;
          }
        }, error => {
          this.helperSvc.errorHandler(error);
          console.log(error);
        });
    }
    else{
      this.helperSvc.notifyError("New Password and Confirm Password not match.");
    }
  }

}
