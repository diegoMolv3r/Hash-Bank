import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewWelcome } from './view-welcome';

describe('ViewWelcome', () => {
  let component: ViewWelcome;
  let fixture: ComponentFixture<ViewWelcome>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewWelcome]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewWelcome);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
