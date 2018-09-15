import { TestBed, inject } from '@angular/core/testing';

import { EligibleStudentService } from './eligible-student.service';

describe('EligibleStudentService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [EligibleStudentService]
    });
  });

  it('should be created', inject([EligibleStudentService], (service: EligibleStudentService) => {
    expect(service).toBeTruthy();
  }));
});
