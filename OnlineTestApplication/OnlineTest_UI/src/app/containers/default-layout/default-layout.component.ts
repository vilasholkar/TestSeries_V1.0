import { Component, Input } from '@angular/core';
import { navItems } from '../../_nav';
// import {GlobalVariables} from '../../../app/models/global-variables';
@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html'
})
export class DefaultLayoutComponent {
  public navItems = navItems;
  public sidebarMinimized = true;
  private changes: MutationObserver;
  public element: HTMLElement = document.body;
  constructor() {

    this.changes = new MutationObserver((mutations) => {
      this.sidebarMinimized = document.body.classList.contains('sidebar-minimized');
    });
    // globalVariables.showSideBar = true;
    this.changes.observe(<Element>this.element, {
      attributes: true
    });
  }
}
