import { Component } from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {User} from "../models/user";
import {Boat} from "../models/boat";
import {AuthService} from "../services/auth.service";
import {EditDialogComponent} from "../dialog/edit-dialog/edit-dialog.component";
import {DeleteDialogComponent} from "../dialog/delete-dialog/delete-dialog.component";
import {AddDialogComponent} from "../dialog/add-dialog/add-dialog.component";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  constructor(public dialog: MatDialog, private authService: AuthService) {}

  user: User = {
    username: '',
    password: '',
  }

  register(user: User) {
    this.authService.register(user).subscribe(result => console.log(result));
  }

  login(user: User) {
    console.log(user);
    this.authService.login(user).subscribe(response => {
      console.log(response.status);
      localStorage.setItem('boatToken', response.body);
    });
  }

  logged: boolean = false;

  editBoat(boat: Boat) {
    const dialogRef = this.dialog.open(EditDialogComponent, { disableClose: true });
    dialogRef.afterClosed().subscribe((result) => {
      this.user = result;
    });
  }

  deleteBoat(boat: Boat) {
    const dialogRef = this.dialog.open(DeleteDialogComponent, { disableClose: true });
    dialogRef.afterClosed().subscribe((result) => {
      this.user = result;
    });
  }

  addBoat(boat: Boat) {
    const dialogRef = this.dialog.open(AddDialogComponent, { disableClose: true });
    dialogRef.afterClosed().subscribe((result) => {
      this.user = result;
    });
  }
}
