import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RoleEditUserComponent } from './role-edit-user.component';

describe('RoleEditUserComponent', () => {
  let component: RoleEditUserComponent;
  let fixture: ComponentFixture<RoleEditUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RoleEditUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RoleEditUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
