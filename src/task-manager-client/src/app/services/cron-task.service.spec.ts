import { TestBed } from '@angular/core/testing';

import { CronTaskService } from './cron-task.service';

describe('CronTaskService', () => {
  let service: CronTaskService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CronTaskService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
