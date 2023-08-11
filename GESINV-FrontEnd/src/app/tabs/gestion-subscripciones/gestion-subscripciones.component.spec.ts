import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GestionSubscripcionesComponent } from './gestion-subscripciones.component';

describe('GestionSubscripcionesComponent', () => {
  let component: GestionSubscripcionesComponent;
  let fixture: ComponentFixture<GestionSubscripcionesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GestionSubscripcionesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GestionSubscripcionesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
