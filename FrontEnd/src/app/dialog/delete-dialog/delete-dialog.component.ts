import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {AuthService} from "../../services/auth.service";
import {AddDialogComponent} from "../add-dialog/add-dialog.component";
import {BoatService} from "../../services/boat.service";
import {Boat} from "../../models/boat";

@Component({
  selector: 'app-delete-dialog',
  template:
    `
      <div style="padding:20px; text-align: center">
        <h4>Are you sure you want to delete this boat?</h4>
        <button (click)="delete()" mat-raised-button color="primary">Delete</button>
        <button (click)="close()" mat-raised-button color="basic">Close</button>
      </div>
    `,

})
export class DeleteDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<AddDialogComponent>,
    private boatService: BoatService,
    @Inject(MAT_DIALOG_DATA) public data: Boat) {
    this.boat = data;
  }

  close() {
    this.dialogRef.close();
  }

  boat: Boat;

  delete() {
    this.boatService.delete(this.boat).subscribe(response => {
      if (response.status !== 200) {
        alert('Error deleting boat' + response.status);
        this.dialogRef.close();
      } else {
        this.dialogRef.close(response);
      }
    })
  }
}
