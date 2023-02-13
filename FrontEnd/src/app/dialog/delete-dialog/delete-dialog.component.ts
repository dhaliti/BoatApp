import { Component } from '@angular/core';
import {MatDialogRef} from "@angular/material/dialog";
import {AuthService} from "../../services/auth.service";
import {AddDialogComponent} from "../add-dialog/add-dialog.component";

@Component({
  selector: 'app-delete-dialog',
  template:
  `
  <div style="padding:20px; text-align: center">
    <h4>Are you sure you want to delete this boat?</h4>
    <button mat-raised-button color="primary">Delete</button>
    <button (click)="close()" mat-raised-button color="basic">Close</button>
</div>
  `,

})
export class DeleteDialogComponent {

  constructor(public dialogRef: MatDialogRef<AddDialogComponent>, private authService: AuthService) {}
  close() {
    this.dialogRef.close();
  }
}
