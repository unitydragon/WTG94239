import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KabiGridComponent } from './kabi-grid.component';

describe('KabiGridComponent', () => {
  let component: KabiGridComponent;
  let fixture: ComponentFixture<KabiGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KabiGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KabiGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
