import { Component, OnInit, Input,ViewChild } from '@angular/core';
import { MatGridList } from '@angular/material';
import { NgbAlert } from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-alert-msg',
  templateUrl: './alert-msg.component.html',
  styleUrls: ['./alert-msg.component.css']
})
export class AlertMsgComponent implements OnInit {
 
  constructor() { }
  @Input() display: string;
  @Input() alertMessage: string='测试Msg';
  @Input() MsgType: string ='secondary';
  ngOnInit() {
    console.log('test');
  }
  AlertMessage(display: string, MsgType: string, alertMessage: string) {
    this.display = 'fixed';
    this.MsgType = MsgType;
    this.alertMessage = alertMessage;
  }

}
