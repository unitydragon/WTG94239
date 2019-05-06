import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NgbAlert } from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  constructor() { }
  @Input() IsLogin: boolean;
  @Output() alertChange: EventEmitter<string> = new EventEmitter<string>();
  BtnTest() {
    console.log('BtnTest');
    this.alertChange.emit('true');
  }
  
}
