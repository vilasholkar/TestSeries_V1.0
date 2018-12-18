import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GenerateResultComponent } from './generate-result.component';

describe('GenerateResultComponent', () => {
  let component: GenerateResultComponent;
  let fixture: ComponentFixture<GenerateResultComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GenerateResultComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GenerateResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
