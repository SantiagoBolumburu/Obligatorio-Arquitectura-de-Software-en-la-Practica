import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InviteUserTabComponent } from './invite-user-tab.component';

describe('InviteUserTabComponent', () => {
  let component: InviteUserTabComponent;
  let fixture: ComponentFixture<InviteUserTabComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InviteUserTabComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InviteUserTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
