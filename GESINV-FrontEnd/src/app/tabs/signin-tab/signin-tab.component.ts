import { Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DialogServiceService } from 'src/app/basic/dialog/dialog-service.service';
import { InputComponent } from 'src/app/basic/input-basic/input.component';
import { ApiRequestService } from 'src/app/customServices/api-request.service';
import { AuthServiceService } from 'src/app/customServices/auth-service.service';
import { InvitacionOut } from 'src/app/models/out/invitacionOut';
import { UsuarioOut } from 'src/app/models/out/usuarioOut';

@Component({
  selector: 'app-signin-tab',
  templateUrl: './signin-tab.component.html',
  styleUrls: ['./signin-tab.component.css']
})
export class SignInTabComponent implements OnInit{
  private apiRequestService!:ApiRequestService;
  private authService!:AuthServiceService;
  private dialogService!:DialogServiceService;
  private activatedRoute!:ActivatedRoute;

  inputTypes = {
    Email : "email",
    Digits : "digits",
    Texto: "text"
  }

  texto = {
    titulo : "Registrar Administrador y Empresa"
  }

  private invitacionId!:string;
  private esPorInvitacion:boolean = false;

  @ViewChildren(InputComponent)
  inputs! : QueryList<InputComponent>;

  @ViewChild('nameInput')
  inputName!:InputComponent;
  
  @ViewChild('emailInput')
  inputEmail!:InputComponent;

  @ViewChild('passwordInput')
  inputPassword!:InputComponent;

  @ViewChild('nombreEmpresaInput')
  inputNombreEmpresa!:InputComponent;

  constructor(api_request_handler:ApiRequestService, auth_Service:AuthServiceService,
    dialogServiceService:DialogServiceService, activated_Route:ActivatedRoute){
    this.apiRequestService = api_request_handler;
    this.authService = auth_Service
    this.dialogService = dialogServiceService;
    this.activatedRoute = activated_Route;
  }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((param) => {
      this.invitacionId = param["id"];
      this.PropararIncitacion(this.invitacionId);
    });
  }

  PropararIncitacion(invitacionId:string){
    if(invitacionId){
      this.apiRequestService.Get_Invitacion(invitacionId).subscribe({
        next: (value:InvitacionOut) => {
          this.texto.titulo = "Registrar Usuario";
          this.esPorInvitacion = true;
          this.inputNombreEmpresa.Desabilitar();
          this.inputNombreEmpresa.ColocarValor(value.nombreEmpresa);
        },
        error: (value:ErrorEvent) => {},  
        complete: () => {} 
      });
    }
  }


  Registrar(){
    if(this.esPorInvitacion){
      this.RegistrarPorInvitacion();
    }
    else{
      this.RegistrarAdminYEmpresa();
    }
  }

  RegistrarPorInvitacion(){
    if(this.inputName.HasValidValueOrIsDisabled() && this.inputEmail.HasValidValueOrIsDisabled() 
        && this.inputPassword.HasValidValueOrIsDisabled())
    {
      let nombre = this.inputName.GetInputValue();
      let email = this.inputEmail.GetInputValue();
      let password = this.inputPassword.GetInputValue();

      this.apiRequestService.Post_Usuario_por_Invitacion(nombre, email, password, this.invitacionId
      ).subscribe({
        next: (value) => this.ManejarNuevoUsuario(value),
        error: (value:ErrorEvent) => {},
        complete: () => console.info('complete') 
      });
    }
  }

  RegistrarAdminYEmpresa(){
    if(this.inputName.HasValidValueOrIsDisabled() && this.inputEmail.HasValidValueOrIsDisabled() 
        && this.inputPassword.HasValidValueOrIsDisabled() && this.inputNombreEmpresa.HasValidValueOrIsDisabled())
    {
      let nombre = this.inputName.GetInputValue();
      let email = this.inputEmail.GetInputValue();
      let password = this.inputPassword.GetInputValue();
      let nombreEmpresa = this.inputNombreEmpresa.GetInputValue();

      this.apiRequestService.Post_Usuario(nombre, email, password, nombreEmpresa
      ).subscribe({
        next: (value) => this.ManejarAdminYEmpresa(value),
        error: (value:ErrorEvent) => {},
        complete: () => console.info('complete') 
      });

    }
    else{
      this.inputName.MarkAsTouched(); 
      this.inputEmail.MarkAsTouched(); 
      this.inputPassword.MarkAsTouched();
      this.inputNombreEmpresa.MarkAsTouched();
    }
  }

  private ManejarAdminYEmpresa(value:UsuarioOut){
    let niceValue:string[] =[ `Nombre: ${value.nombre}`,
                              `Email: ${value.email}`,
                              `Conrtraseña: ${value.contrasenia}`,
                              `Empresa: ${value.nombreEmpresa}`]

    this.dialogService.openDialog("Administrador y Empresa creados exitosamente!",
      niceValue, "650px");
  }

  private ManejarNuevoUsuario(value:UsuarioOut){
    let niceValue:string[] =[ `Nombre: ${value.nombre}`,
                              `Email: ${value.email}`,
                              `Conrtraseña: ${value.contrasenia}`,
                              `Empresa: ${value.nombreEmpresa}`]

    this.dialogService.openDialog("Usuario creado exitosamente!",
      niceValue, "650px");
  }
}
