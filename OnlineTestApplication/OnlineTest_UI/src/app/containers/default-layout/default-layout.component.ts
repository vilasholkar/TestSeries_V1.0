import { Component, Input ,OnInit} from '@angular/core';
import { navItemsAdmin } from '../../_navAdmin';
import { navItemsStudent } from '../../_navStudent';

import { Router } from '@angular/router';
@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html'
})
export class DefaultLayoutComponent {
  public navItemsAdmin = navItemsAdmin;
  public navItemsStudent = navItemsStudent;
  public sidebarMinimized = true;
  public isAdmin=false;
  public isStudent=false;
  public isSuperAdmin=false;
  private changes: MutationObserver;
  public element: HTMLElement = document.body;

  constructor(private router : Router) {

    this.changes = new MutationObserver((mutations) => {
      this.sidebarMinimized = document.body.classList.contains('sidebar-minimized');
      let userRole=localStorage.getItem("userRoles");
          debugger
          if(userRole==="Admin")
              {
              this.isAdmin=true;
              }
              if(userRole==="Student")
              {
              this.isStudent=true;
              }
              if(userRole==="SuperAdmin")
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
            localStorage.removeItem('userToken');
             localStorage.removeItem('userRoles');
            this.router.navigate(['/login']);
    }
}
