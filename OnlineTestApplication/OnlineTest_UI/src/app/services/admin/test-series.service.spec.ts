import { TestBed, inject } from '@angular/core/testing';

import { TestSeriesService } from './test-series.service';

describe('TestSeriesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TestSeriesService]
    });
  });

  it('should be created', inject([TestSeriesService], (service: TestSeriesService) => {
    expect(service).toBeTruthy();
  }));
});
