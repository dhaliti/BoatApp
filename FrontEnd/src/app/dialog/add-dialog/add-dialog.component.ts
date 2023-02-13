import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../services/auth.service";
import {MatDialogRef} from "@angular/material/dialog";
import {Boat} from "../../models/boat";

@Component({
  selector: 'app-add-dialog',
  template:
  `
    <div style="padding: 50px;">
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
    <button  mat-raised-button color="primary">Add</button>
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

  constructor(public dialogRef: MatDialogRef<AddDialogComponent>, private authService: AuthService) {}
  ngOnInit() {
    this.dialogRef.updateSize('50%', 'auto');
  }

  close() {
    this.dialogRef.close();
  }

  // addBoat() {
  //   this.authService.addBoat(this.boat).subscribe(result => {
  //     this.dialogRef.close();
  //   });
  // }

}
