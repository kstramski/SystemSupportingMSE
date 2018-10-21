import { NavMenuComponent } from './../../nav-menu/nav-menu.component';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CompetitionService } from '../../../../services/competition.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-competition-form',
  templateUrl: './competition-form.component.html',
  styleUrls: ['./competition-form.component.css']
})
export class CompetitionFormComponent implements OnInit {
  title: string = "New Competition";
  competition: SaveCompetition = {
    id: 0,
    name: "",
    description: "",
    groupsRequired: false,
    groupSize: null
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private competitionsService: CompetitionService,
    private toastr: ToastrService
  ) {
    this.route.paramMap.subscribe(p => {
      if (p.get('id'))
        this.competition.id = +p.get('id');
    });
  }

  ngOnInit() {
    if (this.competition.id > 0) {
      this.competitionsService.getCompetition(this.competition.id)
        .subscribe(c => {
          this.setCompetition(c);
        });
      this.title = "Edit Competition";
    }
  }

  setCompetition(data) {
    this.competition.id = data.id;
    this.competition.name = data.name;
    this.competition.description = data.description;
    this.competition.groupsRequired = data.groupsRequired;
    this.competition.groupSize = data.groupSize;
  }

  submit() {
    if (this.competition.id) {
      this.competitionsService.update(this.competition)
        .subscribe(x => {
          this.toastr.success("Competition was succesfully updated.", "Success", { timeOut: 5000 });
          this.navigate(x);
        });
    } else {
      this.competitionsService.create(this.competition)
        .subscribe(x => {
          this.toastr.success("New competiton was succesfully added.", "Success", { timeOut: 5000 });
          this.navigate(x);
        });
    }
  }

  navigate(c) {
    this.router.navigate(['/panel/competitions/', c.id]);
  }
}

export interface SaveCompetition {
  id: number;
  name: string;
  description: string;
  groupsRequired: boolean;
  groupSize: number;
}
