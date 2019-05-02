import { Component, OnInit, Input } from '@angular/core';
import { MatGridList } from '@angular/material';
import { NgbAlert } from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-alert-msg',
  templateUrl: './alert-msg.component.html',
  styleUrls: ['./alert-msg.component.css']
})
export class AlertMsgComponent implements OnInit {
  constructor() { }
  @Input() StyleText: { [key: string]: string; } = {'display': 'none'};
  @Input() alertMessage: string='测试Msg';
  @Input() MsgType: string ='secondary';
  ngOnInit() {
  }

}
