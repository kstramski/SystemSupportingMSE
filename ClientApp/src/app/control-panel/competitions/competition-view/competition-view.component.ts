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

}
