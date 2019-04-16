import { Component, OnInit } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { CookieService } from 'ngx-cookie-service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Account, WebUserLogin, ChatMessage, ServerResponseMessage } from '../Commond.Model';





@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  account: Account;
  webUserLogin: WebUserLogin = { Account: '', Password: '', SetCookie30Day: false };
  // http option
  headers = new HttpHeaders({
    'Content-Type': 'application/json',
  });
  options = { headers: this.headers };


  constructor(private httpClient: HttpClient, private cookieService: CookieService) { }




  ngOnInit() {
    if (this.cookieService.get("IsLogin") == "true") {
      // this.account = JSON.parse(this.cookieService.get("User"));  後端有設定僅限http使用
      alert("您已經登入了，無須重複登入。");
      window.location.href = window.location.origin;
    }
    else {
      console.log("使用者未登入");
    }
  }

  LoginCheck() {
    console.log(JSON.stringify(this.webUserLogin));
    let url: string = window.location.origin + '/api/Login/DefaultUserLogin';
    return this.httpClient.post<string>(url, JSON.stringify(this.webUserLogin), this.options).subscribe((data) => {
      //也可以上面直接繼承，不過ts會一直寫錯很煩
      let serverResponseMessage: ServerResponseMessage;
      serverResponseMessage = JSON.parse(JSON.stringify(data));
      // console.log(serverResponseMessage.ResultCode);
      if (serverResponseMessage.ResultCode != 0) {
        alert('登入失敗。' + serverResponseMessage.Content);
      }
      else {
        alert(serverResponseMessage.Content);
        window.location.href = window.location.origin;
      }
    });
  }
}
