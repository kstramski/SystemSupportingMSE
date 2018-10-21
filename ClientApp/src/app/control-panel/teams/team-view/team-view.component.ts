import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { TeamService } from '../../../../services/team.service';
import * as feather from 'feather-icons';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-team-view',
  templateUrl: './team-view.component.html',
  styleUrls: ['./team-view.component.css']
})
export class TeamViewComponent implements OnInit {
  team: any;
  teamId: number;
  modal: Array<any> = [
    { title: "Delete Team", body: "Are you sure want to delete this team?" },
    { title: "Remove User", body: "Are you sure want to remove this user from team?" },
  ];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private teamService: TeamService,
    private toastr: ToastrService
  ) {
    this.route.paramMap
      .subscribe(p => {
        this.teamId = +p.get('id');
        if (isNaN(this.teamId) || this.teamId <= 0) {
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
    feather.replace();
  }

  delete() {
    if (this.team.users.length == 1)
      this.teamService.remove(this.team.id)
        .subscribe(t => {
          this.toastr.success("Team was successfully deleted", "Success", { timeOut: 5000 });
          this.router.navigate(['/panel/teams']);
        });
  }
}
