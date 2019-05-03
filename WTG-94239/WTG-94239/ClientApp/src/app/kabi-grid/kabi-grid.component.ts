import { Component, ViewEncapsulation} from '@angular/core';
import { FormControl, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}


@Component({
  selector: 'app-kabi-grid',
  templateUrl: './kabi-grid.component.html',
  styleUrls: ['./kabi-grid.component.css'],
  encapsulation: ViewEncapsulation.None
})

export class KabiGridComponent {
  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email,
  ])
  matcher = new MyErrorStateMatcher();
  panelColor = new FormControl('orange');
  public rippleColor = "rgba(255, 255, 255, 0.7)";//波纹颜色
  email = '';
  account = '';
  password = '';
}
