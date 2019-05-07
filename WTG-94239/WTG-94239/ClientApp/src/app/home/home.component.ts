import { Component, Input, Output, EventEmitter, Directive, ViewChild } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { AlertMsgDataService } from '../Commond.Model';
import { AlertMsgComponent } from '../alert-msg/alert-msg.component';



@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  providers: [AlertMsgComponent]
})
export class HomeComponent {
  closeResult: string;
  constructor(private modalService: NgbModal, private alertMsgDataService: AlertMsgDataService) { }
  @Input() IsLogin: boolean;
  @Output() alertChange = new EventEmitter();
  BtnTest() {
    this.alertMsgDataService.changeMessage('fortest');
    this.modalService.open(AlertMsgComponent);
  }
  BtnTest2(content) {
    this.alertMsgDataService.changeMessage('fortest2');
    this.modalService.open(AlertMsgComponent);
  }

  

  


  }


  

