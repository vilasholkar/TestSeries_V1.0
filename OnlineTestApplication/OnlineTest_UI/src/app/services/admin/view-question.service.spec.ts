import { TestBed, inject } from '@angular/core/testing';

import { ViewQuestionService } from './view-question.service';

describe('ViewQuestionService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ViewQuestionService]
    });
  });

  it('should be created', inject([ViewQuestionService], (service: ViewQuestionService) => {
    expect(service).toBeTruthy();
  }));
});
