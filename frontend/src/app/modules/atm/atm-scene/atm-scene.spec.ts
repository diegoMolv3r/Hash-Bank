import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AtmScene } from './atm-scene';

describe('AtmScene', () => {
  let component: AtmScene;
  let fixture: ComponentFixture<AtmScene>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AtmScene]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AtmScene);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
