import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewOnlineTestComponent } from './view-online-test.component';

describe('ViewOnlineTestComponent', () => {
  let component: ViewOnlineTestComponent;
  let fixture: ComponentFixture<ViewOnlineTestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewOnlineTestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewOnlineTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
