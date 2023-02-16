import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../services/auth.service";
import {MatDialogRef} from "@angular/material/dialog";
import {Boat} from "../../models/boat";
import {BoatService} from "../../services/boat.service";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-add-dialog',
  template:
  `
    <div class="title">
    <h2>New Boat</h2>
    </div>
    <div style="padding: 30px;">
    <h4>Name</h4>
    <mat-form-field style="width:100%">
    <input [(ngModel)]="boat.name" matInput placeholder="My Fantasctic Boat">
    </mat-form-field>
    <h4>Description</h4>
    <mat-form-field style="width:100%">
    <textarea [(ngModel)]="boat.description" matInput placeholder="This boat is fantastic..."></textarea>
    </mat-form-field>
    <h4>Image URL</h4>
    <mat-form-field style="width:100%">
    <input [(ngModel)]="boat.image_url" matInput placeholder="Fantastic image URL">
    </mat-form-field>
      <div style="display: flex; justify-content: flex-end; align-content: space-between">
    <button (click)="addBoat()"  mat-raised-button color="primary">Add</button>
    <button (click)="close()" mat-raised-button color="basic">Close</button>
      </div>
    </div>
  `,
  styles: [
    `
      .title {
        text-align: center;
        background: #4758b8;
        color: white;
        display: flex;
        justify-content: center;
        align-items: center;
        padding-top: 20px;
      }
    `]
})
export class AddDialogComponent implements OnInit {

  boat: Boat = {
    name: '',
    description: '',
    image_url: '',
  }

  constructor(
    public dialogRef: MatDialogRef<AddDialogComponent>,
    private boatService: BoatService,
    private _snackBar: MatSnackBar) {}
  ngOnInit() {
    this.dialogRef.updateSize('80%', 'auto');
  }

  close() {
    this.dialogRef.close();
  }

  async addBoat() : Promise<any> {
    await this.boatService.add(this.boat).subscribe(response => {
      this.dialogRef.close(response);
      console.log(response);
      },
      error => {
      console.log(error);
      this._snackBar.open('Error: ' + error.error, 'ok');
      })
  }

}
