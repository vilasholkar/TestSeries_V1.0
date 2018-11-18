import { TestBed, inject } from '@angular/core/testing';

import { ResultAnalysisService } from './result-analysis.service';

describe('ResultAnalysisService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ResultAnalysisService]
    });
  });

  it('should be created', inject([ResultAnalysisService], (service: ResultAnalysisService) => {
    expect(service).toBeTruthy();
  }));
});
