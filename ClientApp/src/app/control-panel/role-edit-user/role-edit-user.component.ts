import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RoleService } from '../../../services/role.service';

@Component({
  selector: 'app-role-edit-user',
  templateUrl: './role-edit-user.component.html',
  styleUrls: ['./role-edit-user.component.css']
})
export class RoleEditUserComponent implements OnInit {
  roles: any;
  user: any = {
    id: 0,
    name: "",
    surname: "",
    email: "",
    roles: []
  };
  userId: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private roleService: RoleService,
    private toastr: ToastrService
  ) {
    this.route.paramMap
      .subscribe(p => {
        this.userId = +p.get('id');
        if (isNaN(this.userId) || this.userId <= 0) {
          router.navigate(['/roles']);
          return;
        }
      });
  }

  ngOnInit() {
    this.roleService.getRoles()
      .subscribe(r => {
        this.roles = r;
      });
    this.roleService.getUser(this.userId)
      .subscribe(u => {
        this.setUser(u);
      });
  }

  setUser(u) {
    this.user.id = u.id;
    this.user.name = u.name;
    this.user.surname = u.surname;
    this.user.email = u.email;
    this.user.roles = u.roles.map(r => r.id);
  }

  onRoleToggle(roleId, $event) {
    if ($event.target.checked) {
      if (roleId == 1) {
        this.addRole(1);
        this.addRole(2);
        this.addRole(3);
      } else if (roleId == 2) {
        this.addRole(2);
        this.addRole(3);
      } else if (roleId == 3)
        this.addRole(3);
    }
    else {
      var index = this.user.roles.indexOf(roleId);
      this.user.roles.splice(index, 1);
    }
  }

  addRole(roleId) {
    if (!this.user.roles.includes(roleId))
      this.user.roles.push(roleId);
  }

  submit() {
    this.roleService.updateUser(this.user)
      .subscribe(u => {
        this.toastr.success("Roles was successfully updated.", "Success", { timeOut: 5000 });
        this.router.navigate(['/roles']);
      });
  }
}
