import {Component, OnInit} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {User} from "../models/user";
import {Boat} from "../models/boat";
import {AuthService} from "../services/auth.service";
import {EditDialogComponent} from "../dialog/edit-dialog/edit-dialog.component";
import {DeleteDialogComponent} from "../dialog/delete-dialog/delete-dialog.component";
import {AddDialogComponent} from "../dialog/add-dialog/add-dialog.component";
import {AuthInterceptor} from "../services/auth.interceptor";


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
  constructor(public dialog: MatDialog, private authService: AuthService) {}

  user: User = {
    username: '',
    password: '',
  }

  member: Member = {
username: '',
boats: [],
  }

  register(user: User) {
    this.authService.register(user).subscribe(result => console.log(result));
  }

  login(user: User) {
    console.log(user);
    this.authService.login(user).subscribe(response => {
      localStorage.setItem('authToken', response.body.token);
      this.authService.getBoats().subscribe(response => {
        this.member = response.body;
        this.logged = true;
      });
     },
      error => {
        alert("Wrong username or password");
    });
  }

  logged: boolean = false;

  getBoats() {
    this.authService.getBoats().subscribe(result => console.log(result));
  }

  edit:boolean = false;

  editMode()
  {
    this.edit = !this.edit;
  }

  editBoat(boat: Boat) {
    const dialogRef = this.dialog.open(EditDialogComponent, { data: boat});
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

  addBoat() {
    const dialogRef = this.dialog.open(AddDialogComponent, { disableClose: true });
    dialogRef.afterClosed().subscribe((result) => {
      this.user = result;
    });
  }

  logOut(): void {
    localStorage.removeItem('authToken');
    this.logged = false;
  }

  ngOnInit(): void {
    if (localStorage.getItem('authToken') !== null) {
      this.authService.getBoats().subscribe(response =>
      {
        console.log(response);
        this.member = response.body;
        this.logged = true;
      },
        error => {
        console.log("Expired token");
        });
    }
  }
}
