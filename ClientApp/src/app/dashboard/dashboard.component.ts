import { Component, OnInit } from '@angular/core';
import * as feather from 'feather-icons';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})

export class DashboardComponent implements OnInit {
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

  genderLabels: string[] = ['Male', 'Female'];
  genderData: number[] = [20, 41];

  ageLabels: string[] = ['<=15', '16-20', '21-25', '26-30', '31-40', '41-50', '>50'];
  ageData: number[] = [2, 14, 10, 16, 4, 3, 0];

  emailLabels: string[] = ['Confirmed', 'Not Confirmed'];
  emailData: number[] = [25, 12];

  registerLabels: string[] = ['09-01', '09-02', '09-03', '09-04', '09-05', '09-06', '09-07'];
  registerData: number[] = [2,5,1,12,4,0,9];

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

  constructor() { }

  ngOnInit() {
    feather.replace();
  }

  public chartClicked(e: any): void {
    console.log(e);
  }

  public chartHovered(e: any): void {
    console.log(e);
  }
}
