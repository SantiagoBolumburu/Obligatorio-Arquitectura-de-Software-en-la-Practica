import { Component, Input, OnInit, ViewChild } from '@angular/core';
import {FormControl, Validators} from '@angular/forms';

@Component({
  selector: 'app-text-area',
  templateUrl: './text-area.component.html',
  styleUrls: ['./text-area.component.css']
})
export class TextAreaComponent implements OnInit{
  @Input()
  name!: string;
  
  @Input()
  type!:string;

  @Input()
  placeholder!:string

  @Input()
  disabled!:boolean;

  @Input()
  defaultval!:string;

  inputFormControl!:FormControl;

  constructor() {
    
  }

  ngOnInit(): void {
    this.inputFormControl = this.ObtenerFormControl(this.type);

    if(this.disabled){
      this.inputFormControl.disable();
    }
    this.inputFormControl.setValue( this.defaultval);
  }


  private ObtenerFormControl(type:string): FormControl{
    let toReturn!:FormControl;

    switch(type) { 
      case "email": { 
        toReturn = new FormControl('', [Validators.required, Validators.email]);
        break; 
      } 
      case "digits": { 
        toReturn = new FormControl('', [Validators.required, Validators.pattern("^[0-9]*$")]);
        break; 
      } 
      default: { 
        toReturn = new FormControl('', [Validators.required,]);
         break; 
      } 
   } 
    return toReturn; 
  }

  public HasValidValueOrIsDisabled():boolean{
    return this.inputFormControl.valid || this.inputFormControl.disabled;
  }

  public GetInputValue():string{
    return this.inputFormControl.value;
  }

  public MarkAsTouched(){
    this.inputFormControl.markAllAsTouched();
  }

  public Desabilitar(){
    this.inputFormControl.disable();
  }

  public Habilitar(){
    this.inputFormControl.enable();
  }

  public ColocarValor(newNalue:string|number){
    this.inputFormControl.setValue(newNalue);
  }
}
