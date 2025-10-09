import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SummaryTileComponent } from './summary-tile.component';

describe('SummaryTileComponent', () => {
  let component: SummaryTileComponent;
  let fixture: ComponentFixture<SummaryTileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SummaryTileComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SummaryTileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
