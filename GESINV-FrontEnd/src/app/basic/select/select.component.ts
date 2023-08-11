import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroupDirective, NgForm, Validators } from '@angular/forms';

export interface SelectOption{
  Id:string;
  Valor:string;
  Texto:string;
  Desabilitado:boolean;
  Seleccionado:boolean;
}

@Component({
  selector: 'app-select',
  templateUrl: './select.component.html',
  styleUrls: ['./select.component.css']
})
export class SelectComponent implements OnInit {
  @Input()
  Tittle!:string;

  @Input()
  Enabled:boolean = true;

  @Input()
  Required!:boolean;

  @Input()
  Options!:SelectOption[];

  selectFormControl:FormControl =  new FormControl(null);  //new  FormControl('valid', [Validators.required, Validators.pattern('valid')]);// new FormControl(null);
  selectedOption!:string|undefined;


  ngOnInit(): void {
    this.Options.forEach(option => {
      if(option.Seleccionado){
        this.selectedOption = option.Valor;
      }
    });
    this.ColocarFormControl(this.Required)
  }

  ColocarFormControl(requerido:boolean){
    this.selectFormControl = this.ObtenerFormControl(requerido);
  }

  private ObtenerFormControl(required:boolean): FormControl{
    let toReturn!:FormControl;
    if(required){
      toReturn =new FormControl<string | null>(null, Validators.required);
    }
    else{
      toReturn = new FormControl(null);
    }
    
    return toReturn; 
  }

  public ClearSelection(){
    this.selectedOption = undefined;
    this.selectFormControl.markAsUntouched();
  }

  public HasValidValueOrIsDisabled():boolean{
    return (this.selectedOption !== undefined && this.selectedOption !== null) || !this.Enabled;
  }
  
  public GetSelectedValue():string{
    let value = "";

    if(this.selectedOption){
      value = this.selectedOption;
    }

    return value;
  }

  public GetSelectedOption():SelectOption|undefined{
    let opcion:SelectOption|undefined = undefined;

    if(this.selectedOption){
      opcion = this.Options.find(o => o.Texto == this.selectedOption);
    }

    return opcion;
  }

  public MarkAsTouched(){
    this.selectFormControl.markAsTouched();
  }
}