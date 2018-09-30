import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { RoleService } from '../../../../services/role.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-role-edit',
  templateUrl: './role-edit.component.html',
  styleUrls: ['./role-edit.component.css']
})
export class RoleEditComponent implements OnInit {
  role: any;
  roleId: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private roleService: RoleService,
    private toastr: ToastrService
  ) {
    this.route.paramMap
      .subscribe(p => {
        this.roleId = +p.get('id');
        if (isNaN(this.roleId) || this.roleId <= 0) {
          router.navigate(['/panel/roles']);
          return;
        }
      });
  }

  ngOnInit() {
    this.roleService.getRole(this.roleId)
      .subscribe(r => {
        this.role = r;
      });
  }

  submit() {
    this.roleService.update(this.role)
      .subscribe(r => {
        this.toastr.success("Description was successfully updated.", "Success", { timeOut: 5000 });
        this.router.navigate(['/panel/roles']);
      });
  }

}
