import { Component, Input } from '@angular/core';
import { NgbAlert } from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  constructor() { }
  @Input() IsLogin: boolean;
  BtnTest() {
   
  }
}
