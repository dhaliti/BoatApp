import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../services/auth.service";
import {MatDialogRef} from "@angular/material/dialog";
import {Boat} from "../../models/boat";
import {BoatService} from "../../services/boat.service";

@Component({
  selector: 'app-add-dialog',
  template:
  `
    <div style="padding: 10px;text-align: center; background: darkblue;color: white ">
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
})
export class AddDialogComponent implements OnInit {

  boat: Boat = {
    name: '',
    description: '',
    image_url: '',
  }

  constructor(public dialogRef: MatDialogRef<AddDialogComponent>, private boatService: BoatService) {}
  ngOnInit() {
    this.dialogRef.updateSize('50%', 'auto');
  }

  close() {
    this.dialogRef.close();
  }

  addBoat() : void {
    this.boatService.add(this.boat).subscribe(response => {
      if (response.status !== 200) {
        alert('Error adding boat');
      } else {
        this.dialogRef.close(response);
      }
    });
  }

}
