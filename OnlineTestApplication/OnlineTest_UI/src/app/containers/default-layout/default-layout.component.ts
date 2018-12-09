import { Component, Input ,OnInit} from '@angular/core';
import { navItemsAdmin } from '../../_navAdmin';
import { navItemsStudent } from '../../_navStudent';
import { navItems } from '../../_nav';

import { Router } from '@angular/router';
@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html'
})
export class DefaultLayoutComponent {
  public navItemsAdmin = navItemsAdmin;
  public navItemsStudent = navItemsStudent;
  public navItems = navItems;
  public sidebarMinimized = true;
  public isAdmin=false;
  public isStudent=false;
  public isSuperAdmin=false;
  private changes: MutationObserver;
  public element: HTMLElement = document.body;
  public userRole:string;
  public FirstName:string;
  public currentUserRole:string;
  constructor(private router : Router) {

    this.changes = new MutationObserver((mutations) => {
      this.sidebarMinimized = document.body.classList.contains('sidebar-minimized');
     this.userRole=sessionStorage.getItem("userRoles");
     this.FirstName=sessionStorage.getItem("FirstName");
      
              if(this.userRole==="Admin")
              {
              this.isAdmin=true;
              }
              else if(this.userRole==="Student")
              {
              this.isStudent=true;
              }
              else if(this.userRole==="SuperAdmin")
              {
              this.isSuperAdmin=true;
              }
    });
    this.changes.observe(<Element>this.element, {
      attributes: true
    });
   
  }
   logout()
    {
            sessionStorage.removeItem('userToken');
             sessionStorage.removeItem('userRoles');
            this.router.navigate(['/login']);
    }
}
