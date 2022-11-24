import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UrlParamsSetupComponent } from './url-params-setup.component';

describe('UrlParamsSetupComponent', () => {
  let component: UrlParamsSetupComponent;
  let fixture: ComponentFixture<UrlParamsSetupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UrlParamsSetupComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UrlParamsSetupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
