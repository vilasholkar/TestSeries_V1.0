import { Component, Input, OnInit } from '@angular/core';
import { navItemsAdmin } from '../../_navAdmin';
import { navItemsStudent } from '../../_navStudent';
import { navItems } from '../../_nav';
import { HelperService } from '../../services/helper.service';
import { APIUrl } from "../../shared/API-end-points";
import { Router } from '@angular/router';
@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html'
})
export class DefaultLayoutComponent {
  public ShowSidebar: any;
  public navItemsAdmin = navItemsAdmin;
  public navItemsStudent = navItemsStudent;
  public navItems = navItems;
  public sidebarMinimized = true;
  public isAdmin = false;
  public isStudent = false;
  public isSuperAdmin = false;
  private changes: MutationObserver;
  public element: HTMLElement = document.body;
  public userRole: string;
  public FirstName: string;
  public PhotoUrl: string;
  public currentUserRole: string;
  constructor(private router: Router, private helperSvc: HelperService) {
    this.changes = new MutationObserver((mutations) => {
      this.sidebarMinimized = document.body.classList.contains('sidebar-minimized');
      this.userRole = sessionStorage.getItem("userRoles");
      this.FirstName = sessionStorage.getItem("FirstName");
      this.PhotoUrl = sessionStorage.getItem("PhotoUrl");

      if (this.userRole === "Admin") {
        this.isAdmin = true;
      }
      else if (this.userRole === "Student") {
        this.isStudent = true;
      }
      else if (this.userRole === "SuperAdmin") {
        this.isSuperAdmin = true;
      }
    });
    this.changes.observe(<Element>this.element, {
      attributes: true
    });

  }
  ngOnInit() {
    // debugger;
    // this.ShowSidebar = this.helperSvc.ShowSidebar;
  }
  // onPageLoad()
  // {
  //   debugger;
  //   this.helperSvc.getService(APIUrl.GetGeneralSettings).subscribe((data: any) => {
  //     if (data.Message === 'Success')
  //     {
  //      let temp = data.Object.filter(option => option.Key === 'Login_Logo');
  //      this.LogoPath=temp[0].Value;
  //     }
  //   });
  // }
  logout() {
    debugger;
    if (sessionStorage.getItem('IsTestStarted') === 'false') {
      sessionStorage.removeItem('userToken');
      sessionStorage.removeItem('userRoles');
      sessionStorage.removeItem('FirstName');
      sessionStorage.removeItem('UserID');
      sessionStorage.removeItem('StudentID');
      sessionStorage.removeItem('PhotoUrl');
      sessionStorage.removeItem('IsTestStarted');

      this.router.navigate(['/login']);
    }
    else {
      this.helperSvc.notifyError("Submit test before logout.");
    }

  }
}
