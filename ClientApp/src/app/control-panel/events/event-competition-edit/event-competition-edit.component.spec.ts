import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventCompetitionEditComponent } from './event-competition-edit.component';

describe('EventCompetitionEditComponent', () => {
  let component: EventCompetitionEditComponent;
  let fixture: ComponentFixture<EventCompetitionEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventCompetitionEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventCompetitionEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
