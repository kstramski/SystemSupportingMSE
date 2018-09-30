import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { TeamService } from '../../../../services/team.service';

@Component({
  selector: 'app-team-view',
  templateUrl: './team-view.component.html',
  styleUrls: ['./team-view.component.css']
})
export class TeamViewComponent implements OnInit {
  team: any;
  teamId: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private teamService: TeamService
  ) {
    this.route.paramMap
      .subscribe(p => {
        this.teamId = +p.get('id');
        if(isNaN(this.teamId) || this.teamId <= 0) {
          router.navigate(['/panel/teams']);
          return;
        }
      });
   }

  ngOnInit() {
    this.teamService.getTeam(this.teamId)
      .subscribe(t => {
        this.team = t;
      });
  }

}
