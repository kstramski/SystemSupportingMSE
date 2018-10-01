import { UserService } from './../../../services/user.service';
import { Component, OnInit, Inject, LOCALE_ID } from '@angular/core';
import * as feather from 'feather-icons';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})

export class DashboardComponent implements OnInit {
  dashboardData: any;

  legendDisplay: boolean = false;

  rectangleColors: Array<any> = [{ backgroundColor: ['rgba(78, 137, 255, 0.7)', 'rgba(78, 137, 255, 0.7)', 'rgba(78, 137, 255, 0.7)', 'rgba(78, 137, 255, 0.7)', 'rgba(78, 137, 255, 0.7)', 'rgba(78, 137, 255, 0.7)', 'rgba(78, 137, 255, 0.7)'] }];
  circleColors: Array<any> = [{ backgroundColor: ['rgba(78, 137, 255, 0.7)', 'rgba(255,64, 124, 0.7)', 'rgba(255, 219, 124, 0.7)', 'rgba(192, 219, 124, 0.7)'] }];

  rectangleChart: string = 'bar';
  circleChart: string = 'pie';
  lineChart: string = 'line';

  rectangleOptions: any = {
    scales: {
      yAxes: [{
        ticks: {
          beginAtZero: true
        }
      }]
    }
  }

  genderLabels: string[];
  genderData: number[];

  ageLabels: string[];
  ageData: number[];

  emailLabels: string[];
  emailData: number[];

  registerLabels: string[];
  registerData: number[];

  public doughnutChartLabels: string[] = ['Download Sales', 'In-Store Sales', 'Mail-Order Sales', 'Test'];
  public doughnutChartData: number[] = [350, 450, 200, 150];
  public doughnutChartType: string = 'doughnut';
  public barChartType: string = 'bar';
  public pieChartType: string = 'pie';
  public barChartOptions: any = {
    scaleShowVerticalLines: false,
    responsive: true
  };
  public barChartData: number[] = [350, 450, 200, 150];

  constructor(
    private userService: UserService
  ) { }

  ngOnInit() {
    this.userService.getDashboardData()
      .subscribe(d => {
        this.dashboardData = d;
        this.populateCharts(this.dashboardData);
      });
    feather.replace();
  }

  public chartClicked(e: any): void {
    console.log(e);
  }

  public chartHovered(e: any): void {
    console.log(e);
  }

  populateCharts(data) {
    this.genderData = [];
    this.genderLabels = [];
    data.genders.forEach(g => {
      this.genderLabels.push(g.name);
      this.genderData.push(g.items);
    });

    this.emailLabels = [];
    this.emailData = [];
    data.emails.forEach(e => {
      this.emailLabels.push(e.name);
      this.emailData.push(e.items);
    });

    this.ageLabels = [];
    this.ageData = [];
    data.usersAge.forEach(a => {
      this.ageLabels.push(a.name);
      this.ageData.push(a.items);
    });

    this.registerLabels = [];
    this.registerData = [];
    data.usersRegistered.forEach(r => {
      this.registerLabels.push(r.name);
      this.registerData.push(r.items);
    });
  }
}
