import {Component, OnInit} from '@angular/core';
import {AuthService} from "./services/auth.service";
import {User} from "./models/user";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'BoatApp';
  user = new User();

  constructor(private authService: AuthService) {}

  register(user: User) {
    this.authService.register(user).subscribe();
  }

  login(user: User) {
    console.log(user);
    this.authService.login(user).subscribe((token: string) => {
      localStorage.setItem('authToken', token);
    });
  }

  ngOnInit() {
      this.authService.getBoats().subscribe((user: any) => {
        console.log(user);
      });
    }

}
