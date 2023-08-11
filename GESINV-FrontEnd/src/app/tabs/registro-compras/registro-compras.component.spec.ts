import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistroComprasComponent } from './registro-compras.component';

describe('RegistroComprasComponent', () => {
  let component: RegistroComprasComponent;
  let fixture: ComponentFixture<RegistroComprasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegistroComprasComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegistroComprasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
