import { ToastrService } from 'ngx-toastr';
import { TeamService } from '../../../../services/team.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-team-form',
  templateUrl: './team-form.component.html',
  styleUrls: ['./team-form.component.css']
})
export class TeamFormComponent implements OnInit {
  teamId: number;
  team: Team;
  captain: any;
  title: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private teamService: TeamService,
    private toastr: ToastrService
  ) {
    route.paramMap.subscribe(p => {
      this.teamId = +p.get('id');
      this.team = {
        id: 0,
        name: "",
        captain: 0,
        users: []
      }
      this.captain = { id: 0, name: "" };
      this.title = "Create Team";
    });
  }

  ngOnInit() {
    if (this.teamId > 0) {
      this.title = "Edit Team";
      this.teamService.getTeam(this.teamId)
        .subscribe(t => {
          this.setTeam(t);
        });
    }
  }

  setTeam(t) {
    var captain: any;

    this.team.id = t.id;
    this.team.name = t.name;
    this.team.captain = t.captain;
    this.team.users = t.users;

    captain = this.team.users.find(u => u.id === this.team.captain);
    this.captain.id = captain.id;
    this.captain.name = captain.name + " " + captain.surname;
  }

  setCaptain(id) {
    this.team.captain = +id;
  }

  submit() {
    if (this.team.id > 0)
      this.teamService.update(this.team).subscribe(t => {
        this.toastr.success("Team was successfully updated.", "Success", { timeOut: 5000 });
        this.router.navigate(['/panel/teams/', this.team.id]);
      });
    else
      this.teamService.create(this.team).subscribe(t => {
        this.toastr.success("Team was successfully created.", "Success", { timeOut: 5000 });
        this.router.navigate(['/panel/teams/', t['id']]);
      })
  }

}

export interface Team {
  id: number;
  name: string;
  captain: number;
  users: any[];
}