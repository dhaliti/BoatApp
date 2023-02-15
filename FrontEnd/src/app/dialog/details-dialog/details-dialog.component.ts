import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {Boat} from "../../models/boat";

@Component({
  selector: 'app-details-dialog',
  template:
  `
    <div style="text-align: center" >
    <div style="padding: 10px;text-align: center; background: darkblue;color: white">
      <h2>{{boat.name}}</h2>
    </div>
    <div style="padding: 30px;">
      <img src="{{boat.image_url}}">
      <p *ngIf="!boat.image_url">No image</p>
      <p>{{boat.description}}</p>
      </div>
    </div>
    <div style="display: flex; justify-content: flex-end; align-content: space-between">
      <button (click)="close()" mat-raised-button color="basic">Close</button>
    </div>
  `,
})
export class DetailsDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<DetailsDialogComponent>,
  @Inject(MAT_DIALOG_DATA) public data: Boat) {
    this.boat = this.data;
  }

  close() : void {
    this.dialogRef.close();
  }

  boat: Boat
}

