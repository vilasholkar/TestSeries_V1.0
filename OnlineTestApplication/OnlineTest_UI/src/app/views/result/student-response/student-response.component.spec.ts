import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentResponseComponent } from './student-response.component';

describe('StudentResponseComponent', () => {
  let component: StudentResponseComponent;
  let fixture: ComponentFixture<StudentResponseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StudentResponseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentResponseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
