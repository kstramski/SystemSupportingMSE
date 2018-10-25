import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-loading-spinner',
  template: `
    <div class="spinner">
        <div class="rect1"></div>
        <div class="rect2"></div>
        <div class="rect3"></div>
        <div class="rect4"></div>
        <div class="rect5"></div>
    </div>
    `,
  styleUrls: ['./loading-spinner.component.css']
})

export class LoadingSpinnerComponent implements OnInit {
  constructor() { }

  ngOnInit() { }
}