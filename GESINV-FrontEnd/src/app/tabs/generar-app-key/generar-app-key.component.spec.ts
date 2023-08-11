import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenerarAppKeyComponent } from './generar-app-key.component';

describe('GenerarAppKeyComponent', () => {
  let component: GenerarAppKeyComponent;
  let fixture: ComponentFixture<GenerarAppKeyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GenerarAppKeyComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GenerarAppKeyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
