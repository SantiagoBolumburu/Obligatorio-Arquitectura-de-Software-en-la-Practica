import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaginaInicioEmpresaComponent } from './pagina-inicio-empresa.component';

describe('PaginaInicioEmpresaComponent', () => {
  let component: PaginaInicioEmpresaComponent;
  let fixture: ComponentFixture<PaginaInicioEmpresaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaginaInicioEmpresaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PaginaInicioEmpresaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
