import {Component, OnInit} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {User} from "../models/user";
import {Boat} from "../models/boat";
import {AuthService} from "../services/auth.service";
import {EditDialogComponent} from "../dialog/edit-dialog/edit-dialog.component";
import {DeleteDialogComponent} from "../dialog/delete-dialog/delete-dialog.component";
import {AddDialogComponent} from "../dialog/add-dialog/add-dialog.component";
import {AuthInterceptor} from "../services/auth.interceptor";
import {FormControl, Validators} from "@angular/forms";
import {DetailsDialogComponent} from "../dialog/details-dialog/details-dialog.component";
import {MatSnackBar} from "@angular/material/snack-bar";


interface Member {
  username: string;
  boats: Boat[];
}

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  constructor(
    public dialog: MatDialog,
    private authService: AuthService,
    private _snackBar: MatSnackBar) {
  }

  member: Member = {
    username: '',
    boats: [],
  }

  register() {
    this.authService.register({username: this.name.value, password: this.pass.value}).subscribe(result => {
      console.log(result)
        this.pass.setValue('');
        this.name.setValue('');
    },
      error => {
      this._snackBar.open('Error: ' + error.error, 'ok');
      });
  }

  login() {
    this.authService.login({username: this.name.value, password: this.pass.value}).subscribe(response => {
      console.log(response);
        localStorage.setItem('authToken', response.body.token);
        this.member.username = response.body.user.username;
        this.member.boats = response.body.user.boats;
        this.logged = true;
      },
      error => {
        this._snackBar.open('Error: ' + error.error, 'Ok');
      });
  }

  logged: boolean = false;

  getBoats() {
    this.authService.getBoats().subscribe(result => console.log(result));
  }

  pass = new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(10)]);
  name = new FormControl('', [Validators.required]);

  getErrorMessage(): string {
    if (this.pass.hasError('minLength')) {return ''};
    return ''
  }

  edit: boolean = false;

  editBoat(boat: Boat) {
    const dialogRef = this.dialog.open(EditDialogComponent, {data: boat});
    dialogRef.afterClosed().subscribe((response) => {
      this.member.username = response.body.username;
      this.member.boats = response.body.boats;
    });
  }

  deleteBoat(boat: Boat): void {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {data: boat});
    dialogRef.afterClosed().subscribe((response) => {
      if (response) {
        this.member.username = response.body.username;
        this.member.boats = response.body.boats;
      }
    });
  }

  openDetails(boat: Boat): void {
    const dialogRef = this.dialog.open(DetailsDialogComponent, {data: boat});
  }

  addBoat(): void {
    const dialogRef = this.dialog.open(AddDialogComponent, {disableClose: false});
    dialogRef.afterClosed().subscribe((response) => {
      if (response) {
        console.log(response);
        this.member.username = response.body.username;
        this.member.boats = response.body.boats;
      }
    });
  }

  logOut(): void {
    localStorage.removeItem('authToken');
    this.logged = false;
  }

  ngOnInit(): void {
    if (localStorage.getItem('authToken') !== null) {
      this.authService.getBoats().subscribe(response => {
          console.log(response);
          this.member.username = response.body.username;
          this.member.boats = response.body.boats;
          this.logged = true;
        },
        error => {
          console.log("Expired token");
        });
    }
  }
}
