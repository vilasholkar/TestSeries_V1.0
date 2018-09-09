import { TestBed, inject } from '@angular/core/testing';

import { StudentOnlineTestService } from './student-online-test.service';

describe('StudentOnlineTestService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [StudentOnlineTestService]
    });
  });

  it('should be created', inject([StudentOnlineTestService], (service: StudentOnlineTestService) => {
    expect(service).toBeTruthy();
  }));
});
