import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventCompetitionViewComponent } from './event-competition-view.component';

describe('EventCompetitionViewComponent', () => {
  let component: EventCompetitionViewComponent;
  let fixture: ComponentFixture<EventCompetitionViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventCompetitionViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventCompetitionViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
