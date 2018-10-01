import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from './../../../../services/user.service';
import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css'],
  providers: [DatePipe]
})
export class UserEditComponent implements OnInit {
  user: any;
  userId: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
    private toastr: ToastrService,
    private datePipe: DatePipe
  ) { 
    this.route.paramMap
    .subscribe(p => {
      this.userId = +p.get('id');  
      if(isNaN(this.userId) || this.userId <= 0) {
        router.navigate(['/panel/users']);
        return;
      }
    });
  }

  ngOnInit() {
    this.userService.getUser(this.userId)
    .subscribe(u => {
      this.user = u;
      
      this.user.birthDate = this.datePipe.transform(this.user.birthDate, 'dd/MM/yyyy');
    });
  }

  private formatDate(date) {
    var parts = date.split("/");
    return parts[2] + "-" + parts[1] + "-" + parts[0] + "T00:00:00";
  }

  submit() {
    this.user.birthDate = this.formatDate(this.user.birthDate);
    this.userService.update(this.user)
    .subscribe(u => {
      this.toastr.success("User was successfuly updated.", "Success", { timeOut: 5000});
      this.router.navigate(['/panel/users/', this.user.id]);
    });
  }

}
