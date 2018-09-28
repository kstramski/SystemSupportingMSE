import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import * as feather from 'feather-icons';

@Component({
  selector: 'app-control-panel',
  templateUrl: './control-panel.component.html',
  styleUrls: ['./control-panel.component.css']
})
export class ControlPanelComponent {

  constructor(private auth: AuthService) {}

  ngAfterViewInit() {
    feather.replace();
  }

}
