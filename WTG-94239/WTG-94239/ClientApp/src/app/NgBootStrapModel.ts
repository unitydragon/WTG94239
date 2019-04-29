import { NgbModal,NgbAlertModule} from '@ng-bootstrap/ng-bootstrap';\
import { NgModule } from '@angular/core';

@NgModule({
  exports: [
    NgbModal,
    NgbAlertModule,
  ]
})
export class DemoNgBootStraplModule { }
