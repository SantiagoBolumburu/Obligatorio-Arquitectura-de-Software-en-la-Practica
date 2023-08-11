import { Component, ViewChild } from '@angular/core';
import { DialogServiceService } from 'src/app/basic/dialog/dialog-service.service';
import { InputComponent } from 'src/app/basic/input-basic/input.component';
import { SelectComponent } from 'src/app/basic/select/select.component';
import { ApiRequestService } from 'src/app/customServices/api-request.service';
import { InvitacionIn } from 'src/app/models/in/invitacionIn';
import { InvitacionOut } from 'src/app/models/out/invitacionOut';

@Component({
  selector: 'app-invite-user-tab',
  templateUrl: './invite-user-tab.component.html',
  styleUrls: ['./invite-user-tab.component.css']
})
export class InviteUserTabComponent {
  inputNames = {
    Email : "Email",
    Telefono : "Telefono",
    Contrasenia : "Contraseña"
  }
  inputTypes = {
    Email : "email",
    Digits : "digits",
    Texto : "texto"
  }
  opcionesRoles = [
    {Id:"1", Valor:"empleado", Texto:"Empleado", Desabilitado:false,  Seleccionado:true},
    {Id:"2", Valor:"administrador", Texto:"Administrador", Desabilitado:false, Seleccionado:false}];

  @ViewChild("emailInput")
  inputEmailComp!:InputComponent;

  @ViewChild("rolSelect")
  selectRolCompr!:SelectComponent;

  private apiRequest!:ApiRequestService;
  private dialogService!:DialogServiceService;

  constructor(api_service:ApiRequestService, dialog_Service:DialogServiceService){
    this.apiRequest = api_service;
    this.dialogService = dialog_Service;
  }
  
  InvitarUsaurio(){
    if(this.inputEmailComp.HasValidValueOrIsDisabled() && this.selectRolCompr.HasValidValueOrIsDisabled()){
      let email = this.inputEmailComp.GetInputValue();
      let rol = this.selectRolCompr.GetSelectedValue();

      this.apiRequest.Post_Invitacion(email, rol).subscribe({
        next: (value) => this.ManejarNuevaInvitacion(value),
        error: (value:ErrorEvent) => {},
        complete: () => console.info('complete') 
      });
    }
    else{
      this.inputEmailComp.MarkAsTouched();
    }
  }

  private ManejarNuevaInvitacion(value:InvitacionOut){
    let niceValue:string[] =[ `Identificador: ${value.invitacionId}`,
                              `Valida Hasta: ${value.validaHasta}`];

    this.dialogService.openDialog("Invitacion creada exitosamente!",
      niceValue, "650px");
  }
}
