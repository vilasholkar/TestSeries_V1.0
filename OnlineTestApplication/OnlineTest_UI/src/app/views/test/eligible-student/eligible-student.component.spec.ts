import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EligibleStudentComponent } from './eligible-student.component';

describe('EligibleStudentComponent', () => {
  let component: EligibleStudentComponent;
  let fixture: ComponentFixture<EligibleStudentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EligibleStudentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EligibleStudentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
