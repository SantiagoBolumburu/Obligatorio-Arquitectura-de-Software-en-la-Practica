import { Component, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { Router } from '@angular/router';
import { DialogServiceService } from 'src/app/basic/dialog/dialog-service.service';
import { InputComponent } from 'src/app/basic/input-basic/input.component';
import { ApiRequestService } from 'src/app/customServices/api-request.service';
import { AuthServiceService } from 'src/app/customServices/auth-service.service';


@Component({
  selector: 'app-login-tab',
  templateUrl: './login-tab.component.html',
  styleUrls: ['./login-tab.component.css']
})
export class LoginTabComponent {
  private apiRequestService!:ApiRequestService;
  private authService!:AuthServiceService;
  private dialogService!:DialogServiceService;
  private router!:Router; 
  
  inputNames = {
    Email : "Email",
    Telefono : "Telefono",
    Contrasenia : "Contraseña"
  }
  inputTypes = {
    Email : "email",
    Digits : "digits"
  }

  @ViewChildren(InputComponent)
  inputs! : QueryList<InputComponent>;

  @ViewChild('emailInput')
  inputEmail!:InputComponent;

  @ViewChild('passwordInput')
  inputPassword!:InputComponent;


  constructor(api_request_handler:ApiRequestService, auth_Service:AuthServiceService,
              dialog_Service:DialogServiceService, router:Router){
    this.apiRequestService = api_request_handler;
    this.authService = auth_Service;
    this.dialogService = dialog_Service;
    this.router = router;
  }


  LoggearUsuario(){

    if(this.inputPassword.HasValidValueOrIsDisabled() && this.inputEmail.HasValidValueOrIsDisabled()){
      let email = this.inputEmail.GetInputValue();
      let password = this.inputPassword.GetInputValue();

      this.apiRequestService.Post_Session(
        email, password
      ).subscribe({
        next: (value) =>{this.apiRequestService.SetToken(value.token);
                         this.router.navigate(["/inicio"]);
                         this.NotificarUsuario();},
        error: (value:ErrorEvent) => {},
        complete: () => {}
      });
      
    }
    else{
      this.inputEmail.MarkAsTouched();
      this.inputPassword.MarkAsTouched();
    }
    
  }
      
  private NotificarUsuario(){
    this.dialogService.openDialog("Loggeado exitosamente!", "");
  }
}
