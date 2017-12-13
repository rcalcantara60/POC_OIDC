import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { SecuredComponent } from './secured/secured.component';
import { AuthGuardService } from './common/guards/auth.guard';
import { AuthService } from 'app/common/services/auth.service';
import { RouterModule } from '@angular/router';
import { appRoutes } from "app/app.routing";
import { UnsecuredComponent } from './unsecured/unsecured.component';
import { HttpModule } from "@angular/http";
import { HttpClientModule } from '@angular/common/http';
import { HttpClient } from '@angular/common/http/src/client';

@NgModule({
  declarations: [
    AppComponent,
    SecuredComponent,
    UnsecuredComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes),
  ],
  providers: [
    AuthGuardService,
    AuthService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
