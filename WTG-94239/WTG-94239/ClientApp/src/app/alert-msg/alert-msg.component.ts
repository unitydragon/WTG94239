import { Component, OnInit, Input, ViewChild, Injectable } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { AlertMsgDataService } from '../Commond.Model';
@Component({
  selector: 'app-alert-msg',
  templateUrl: './alert-msg.component.html',
  styleUrls: ['./alert-msg.component.css']
})
 export  class AlertMsgComponent implements OnInit {
  closeResult: string;
  Text: string = 'default';
  constructor(private modalService: NgbModal, private alertMsgDataService: AlertMsgDataService) { }
  ngOnInit() {
    this.alertMsgDataService.currencyMsg.subscribe(result => this.Text = result);
  }
  BtnTest(content_template) {
    this.modalService.open(content_template, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {
      this.closeResult = `關閉 觸及: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed${this.getDismissReason(reason)}`;
    });
  }


  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
}
