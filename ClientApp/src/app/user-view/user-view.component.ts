import { ToastrService } from 'ngx-toastr';
import { UserService } from './../services/user.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-user-view',
  templateUrl: './user-view.component.html',
  styleUrls: ['./user-view.component.css']
})
export class UserViewComponent implements OnInit {
  user: any;
  userId: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
    private toastr: ToastrService
  ) {
    this.route.paramMap.subscribe(p => {
      this.userId = +p.get("id");
      if(isNaN(this.userId) || this.userId <= 0) {
        this.router.navigate(['/users']);
        return;
      }
    });
   }

  ngOnInit() {
    this.userService.getUser(this.userId)
    .subscribe(u => {
      this.user = u;
    }, err => {
      if(err.status == 404){
        this.toastr.error("User does not exist.", "Error", { timeOut: 5000 });
        this.router.navigate(['/users']);
      }
    }); 
  }

  delete() {
    if(confirm("Are you sure to delete this user?"))
      this.userService.delete(this.userId)
      .subscribe(u => {
        this.toastr.success("User was succesfully deleted.", "Success", { timeOut: 5000 });
        this.router.navigate(['users']);
      });
  }

}
