import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { HttpClient } from '@angular/common/http';
import { ServerResponseMessage } from '../Commond.Model';
@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  isLogin: boolean = false;
  constructor(private httpClient: HttpClient , private cookieService: CookieService) { }

  ngOnInit() {
    if (this.cookieService.get("IsLogin") == "true") {
      this.isLogin = true;
    }
  }
  LogOut() {
    let url: string = window.location.origin + '/api/Login/LogOut';
    this.httpClient.post<string>(url, "").subscribe((data) => {
      let serverResponseMessage: ServerResponseMessage;
      serverResponseMessage = JSON.parse(JSON.stringify(data));
      if (serverResponseMessage.ResultCode != 0) {
        alert('发生异常。' + serverResponseMessage.Content);
      }
      else {
        alert(serverResponseMessage.Content);
        window.location.href = window.location.origin;
      }
    });
  }

  collapse() {
    this.isExpanded = false;
  }
  
  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
