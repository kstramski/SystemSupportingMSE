import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { CompetitionService } from '../../../../services/competition.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-competition-view',
  templateUrl: './competition-view.component.html',
  styleUrls: ['./competition-view.component.css']
})
export class CompetitionViewComponent implements OnInit {
  competition: any;
  competitionId: number;

  columns: Array<any> = [
    { title: '#', size: 1, center: true },
    { title: 'Name', size: 9, center: false },
    { title: 'Action', size: 2, center: true }
  ];

  modal: Array<string> = [
    "Delete Competition",
    "Are you sure want to delete this competition?"
  ];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private competitionsService: CompetitionService,
    private toastr: ToastrService
  ) {
    route.paramMap.subscribe(p => {
      this.competitionId = +p.get('id');
      if (isNaN(this.competitionId) && this.competitionId == undefined) {
        router.navigate(['/panel/competitions']);
        return;
      }
    });
  }

  ngOnInit() {
    this.competitionsService.getCompetition(this.competitionId)
      .subscribe(c => {
        this.competition = c;
      });
  }

  delete() {
    this.competitionsService.remove(this.competition.id)
      .subscribe(c => {
        this.toastr.success("Competition was successfully deleted.", "Success", { timeOut: 5000 });
        this.router.navigate(['/panel/competitions']);
      });
  }

}
