import { AuthService } from './../services/auth.service';
import { Component } from '@angular/core';
import * as feather from 'feather-icons';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ClientApp';

  constructor(private auth: AuthService) {}

  ngAfterViewInit() {
    feather.replace();
  }
}
