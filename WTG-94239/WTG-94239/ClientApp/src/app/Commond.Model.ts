import { Injectable, OnInit, Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { BehaviorSubject } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';


@Injectable()
export class AlertMsgDataService {
  private messageSource = new BehaviorSubject<string>('defaultMsg');
  currencyMsg = this.messageSource.asObservable();
  constructor() { }
  changeMessage(message: string) {
    this.messageSource.next(message);
  }
}



export interface Account {
  Id: number;
  UserName: string;
  Account1: string;
  Password: string;
  Md5passowrd: string;
  IsDeleted: string;
  Privilege: number;
  IsBanned: boolean;
}

/* interface */

export interface WebUserLogin {
  Account: string;
  Password: string;
  SetCookie30Day: boolean;
}
export interface WebRegist {
  Account: string;
  Password: string;
  SiteUserName: string;
  Md5Password: string; //注册带空
  Email: string;
}

export interface ChatMessage {
  UserName: string;
  Message: string;
  SendMessage: string;
  FromServerMessage: string;
}

/* Class */


export class ServerResponseMessage{
  /*回傳的JsonContent*/
  Content: string | null;
  ResultCode: number | null;
}

export class ForumSelectClass {
  TopicName: string = '';
  ContentTitlePart: string='';
  UserName: string='';
}

export class Topic {
  Id: string;
  Name: string;
  HotRating: number;
}

export class Title {
  Id: string;
  Name: string;
}
export class Content {
  Id: number;
  TopicId: number;
  TitleId: number;
  UserId: number;
  ContentTitle: string;
  ContentData: string;
  PostTime: any;
  LastTime: any;
  IsDeleted: boolean;
  Edit: boolean;
  IsFirstContentTitle: boolean;
  HotRating: number;
}



@Injectable({ providedIn: 'root' })
export  class QuickLoginCheck {
  account: Account;
  headers = new HttpHeaders({
    'Content-Type': 'application/json',
  });
  IsLogin: boolean = false;
  options = { headers: this.headers };
  constructor(private httpClient: HttpClient, private cookieService: CookieService) { }


  QuickLoginCheck() {
    
      this.account = JSON.parse(this.cookieService.get("User"));
      let url: string = window.location.origin + '/api/Login/QuickLoginCheck';

     return this.httpClient.post<boolean>(url, JSON.stringify(this.account), this.options);
    
  }
}

@Injectable({ providedIn: 'root' })
export class ForumSelector {
  url: string = window.location.origin;

  headers = new HttpHeaders({
    'Content-Type': 'application/json',
  });
  options = { headers: this.headers };
  
  constructor(private httpClient: HttpClient) { }

  /*回傳 List<Title>*/
  Topic() {
    return this.httpClient.post(this.url+'/api/Forum/Topic', '');
  }

  /**
   * 回傳 List<Title>
   * @param Title default ''
   */
  Title(Title: string = '') {
    console.log(this.url + '/api/Forum/Title');
    return this.httpClient.post(this.url+'/api/Forum/Title', Title);
  }
  /*搜尋樓主文
   * 返回List<Content>
   * @param forumSelect
   */
  FirstContentTitle(forumSelect: ForumSelectClass) {
    console.log(JSON.stringify(forumSelect));
    return this.httpClient.post(this.url + '/api/Forum/FirstContentTitle', JSON.stringify(forumSelect),this.options);
  }
  /*
   *搜尋樓內文章 
   * @param forumSelect
   */
  ContentTitleData(forumSelect: ForumSelectClass) {
    return this.httpClient.post(this.url + '/api/Forum/ContentTitle', forumSelect);
  }


}
