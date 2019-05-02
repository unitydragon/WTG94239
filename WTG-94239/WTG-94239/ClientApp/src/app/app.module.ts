import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
// Componenet 
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './login/login.component';
import { RegistComponent } from './regist/regist.component';
import { AlertMsgComponent } from './alert-msg/alert-msg.component';
// Model
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CookieService } from 'ngx-cookie-service';
import { FlexLayoutModule } from '@angular/flex-layout';
import { DemoMaterialModule } from './materialModel';
import { DemoNgBootStraplModule } from './NgBootStrapModel';





@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    RegistComponent,
    AlertMsgComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'login', component: LoginComponent },
      { path: 'regist', component: RegistComponent }
    ]),
    DemoMaterialModule,
    DemoNgBootStraplModule,
    BrowserAnimationsModule,
    FlexLayoutModule,
  ],
  providers: [CookieService],
  bootstrap: [AppComponent]
})
export class AppModule {
  
}
