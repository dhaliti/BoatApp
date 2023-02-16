import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {Boat} from "../../models/boat";

@Component({
  selector: 'app-details-dialog',
  template:
  `
    <div class="title">
      <h2>{{boat.name}}</h2>
    </div>
    <div style="text-align: center; padding: 20px;" >
    <div style="padding: 30px;display: flex; flex-direction: column; justify-content: center; align-items: center">
      <img *ngIf="boat.image_url" src="{{boat.image_url}}" class="image_url">
      <p *ngIf="!boat.image_url">
        <mat-icon>image_not_supported</mat-icon>
          No image
      </p>
      <p><i>{{boat.description}}</i></p>
      <p *ngIf="!boat.description">
        <mat-icon>speaker_notes_off</mat-icon>
        No description
      </p>
      </div>
      <div style="display: flex; justify-content: flex-end; align-content: space-between">
        <button (click)="close()" mat-raised-button color="basic">Close</button>
      </div>
    </div>
  `,
  styles: [`
    .image_url {
        width: 50%;
    }
    .title {
      text-align: center;
      background: #3d5a80;
      color: white;
      display: flex;
      justify-content: center;
      align-items: center;
      padding-top: 20px;
    }
    @media only screen and (max-width: 600px) {
      .image_url {
        width: 100%;
      }
    }
      `]
})
export class DetailsDialogComponent implements OnInit {
  constructor(
    public dialogRef: MatDialogRef<DetailsDialogComponent>,
  @Inject(MAT_DIALOG_DATA) public data: Boat) {
    this.boat = this.data;
  }

  ngOnInit(): void {
    this.dialogRef.updateSize('80%', 'auto');
  }

  close() : void {
    this.dialogRef.close();
  }

  boat: Boat
}

