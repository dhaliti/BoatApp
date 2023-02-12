import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import {MatSlideToggleModule} from "@angular/material/slide-toggle";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {AuthInterceptor} from "./services/auth.interceptor";
import {FormsModule} from "@angular/forms";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatButtonModule} from "@angular/material/button";
import {MatInputModule} from "@angular/material/input";
import { DashboardComponent } from './dashboard/dashboard.component';
import { EditDialogComponent } from './dialog/edit-dialog/edit-dialog.component';
import { AddDialogComponent } from './dialog/add-dialog/add-dialog.component';
import { DeleteDialogComponent } from './dialog/delete-dialog/delete-dialog.component';


@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    EditDialogComponent,
    AddDialogComponent,
    DeleteDialogComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MatInputModule,
    FormsModule,
    MatSlideToggleModule,
    BrowserAnimationsModule,
    MatFormFieldModule,
    MatButtonModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
