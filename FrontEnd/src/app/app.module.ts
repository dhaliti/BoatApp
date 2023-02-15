import {CUSTOM_ELEMENTS_SCHEMA, NgModule, NO_ERRORS_SCHEMA} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import {MatSlideToggleModule} from "@angular/material/slide-toggle";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {AuthInterceptor} from "./services/auth.interceptor";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MAT_FORM_FIELD_DEFAULT_OPTIONS, MatFormFieldModule} from "@angular/material/form-field";
import {MatButtonModule} from "@angular/material/button";
import {MatInputModule} from "@angular/material/input";
import { DashboardComponent } from './dashboard/dashboard.component';
import { EditDialogComponent } from './dialog/edit-dialog/edit-dialog.component';
import { AddDialogComponent } from './dialog/add-dialog/add-dialog.component';
import { DeleteDialogComponent } from './dialog/delete-dialog/delete-dialog.component';
import {MatDialog, MatDialogModule, MatDialogRef} from "@angular/material/dialog";
import {MatIconModule} from "@angular/material/icon";
import {MatCardModule} from "@angular/material/card";
import {MatSnackBarModule} from "@angular/material/snack-bar";
import { DetailsDialogComponent } from './dialog/details-dialog/details-dialog.component';


@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    EditDialogComponent,
    AddDialogComponent,
    DeleteDialogComponent,
    DetailsDialogComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MatInputModule,
    MatSnackBarModule,
    FormsModule,
    MatDialogModule,
    MatSlideToggleModule,
    BrowserAnimationsModule,
    MatFormFieldModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    ReactiveFormsModule
  ],
  providers: [
    {provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: {appearance: 'outline'}},
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA,
    NO_ERRORS_SCHEMA,
    CUSTOM_ELEMENTS_SCHEMA,
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
