import {Component, Input, OnInit} from '@angular/core';
import {FormGroup, FormControl, Validators} from '@angular/forms';

@Component({
  selector: 'app-date-picker',
  templateUrl: './date-picker.component.html',
  styleUrls: ['./date-picker.component.css']
})
export class DatePickerComponent implements OnInit{
  @Input()
  name!:string;

  @Input()
  disabled:boolean = false;

  @Input()
  required!:boolean;

  @Input()
  defaultval:Date|undefined;

  formControl!:FormControl;

  ngOnInit(): void {
    this.formControl = this.ObtenerFormControl(this.required);

    if(this.disabled){
      this.formControl.disable();
    }

    this.formControl.setValue( this.defaultval);
  }

  private ObtenerFormControl(required:boolean): FormControl{
    let toReturn!:FormControl;

    if(required){
      toReturn = new FormControl('', [Validators.required]);
    }
    else{
      toReturn = new FormControl('', []);
    }

    return toReturn; 
  }

  public HasValidValueOrIsDisabled():boolean{
    return this.formControl.valid || this.formControl.disabled;
  }

  public GetInputValue():Date{
    return this.formControl.value;
  }
  
  public MarkAsTouched(){
    this.formControl.markAllAsTouched();
  }

  public Desabilitar(){
    this.formControl.disable();
  }

  public Habilitar(){
    this.formControl.enable();
  }

  public LimpiarValor(){
    this.formControl.setValue(undefined);
    this.formControl.markAsUntouched();
  }
}