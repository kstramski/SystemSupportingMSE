import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from './../../../services/user.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {
  user: any;
  userId: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
    private toastr: ToastrService
  ) { 
    this.route.paramMap
    .subscribe(p => {
      this.userId = +p.get('id');  
      if(isNaN(this.userId) || this.userId <= 0) {
        router.navigate(['/users']);
        return;
      }
    });
  }

  ngOnInit() {
    this.userService.getUser(this.userId)
    .subscribe(u => {
      this.user = u;
    // }, err => {
    //   if(err.status == 404) {
    //     this.toastr.error("User does not exist.", "Error", { timeOut: 5000});
    //     this.router.navigate(['/users']);
    //   }
    });
  }

  submit() {
    this.userService.update(this.user)
    .subscribe(u => {
      this.toastr.success("User was successfuly updated.", "Success", { timeOut: 5000});
      this.router.navigate(['/users/', this.user.id]);
    });
  }

}
