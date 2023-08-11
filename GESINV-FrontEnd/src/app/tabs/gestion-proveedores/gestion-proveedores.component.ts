import { Component, ElementRef, ViewChild } from '@angular/core';
import { DialogServiceService } from 'src/app/basic/dialog/dialog-service.service';
import { InputComponent } from 'src/app/basic/input-basic/input.component';
import { TableComponent } from 'src/app/basic/table/table.component';
import { TextAreaComponent } from 'src/app/basic/text-area/text-area.component';
import { ApiRequestService } from 'src/app/customServices/api-request.service';
import { ProveedorIn } from 'src/app/models/in/proveedorIn';
import { ProveedorOut } from 'src/app/models/out/proveedorOut';

@Component({
  selector: 'app-gestion-proveedores',
  templateUrl: './gestion-proveedores.component.html',
  styleUrls: ['./gestion-proveedores.component.css']
})
export class GestionProveedoresComponent {
  private apiRequest!:ApiRequestService;
  private dialogService!:DialogServiceService;

  proveedores:ProveedorOut[] = [];

  estructuraColumnas = PROVEEDOR_TABLE_COLUMN_STRUCTURE;

  constructor(api_service:ApiRequestService, dialog_Service:DialogServiceService){
    this.apiRequest = api_service;
    this.dialogService = dialog_Service;
  }

  ngOnInit(): void {
    this.ObtenerProveedoresYActualizarTabla();
  }

 //CREAR
 @ViewChild("nombreInput")
 inputNombre!:InputComponent;

 @ViewChild("direccionInput")
 inputDireccion!:TextAreaComponent;

 @ViewChild("emailInput")
 inputEmail!:InputComponent;

 @ViewChild("telefonoInput")
 inputTelefono!:InputComponent;

 //EDITAR
 @ViewChild("nombreEditarInput")
 inputEditarNombre!:InputComponent;

 @ViewChild("direccionEditarInput")
 inputEditarDireccion!:TextAreaComponent;

 @ViewChild("emailEditarInput")
 inputEditarEmail!:InputComponent;

 @ViewChild("telefonoEditarInput")
 inputEditarTelefono!:InputComponent;

 //OTROS ELEMENTOS
 @ViewChild("laTabla")
 tabla!:TableComponent;

 @ViewChild("botonEditar")
 botonEditar!:ElementRef;

 @ViewChild("botonEliminar")
 botonEliminar!:ElementRef;

 desabilitado = {
   botonEditar : true,
   botonEliminar : true,
   botonLimpiar : true
 }

 proveedorSeleccionado?:ProveedorOut;

 ObtenerProveedoresYActualizarTabla(){
    this.apiRequest.Get_Proveedores().subscribe({
      next: (value:ProveedorOut[]) => {
        this.proveedores = value;
        this.tabla.ActualizarFilas(this.proveedores);
      },
      error: (value:ErrorEvent) => {},  
      complete: () => console.info('complete') 
    });
  }
  
  IngresarProveedor(){
    if(this.TodosLosInputsSonValidos()){
      let proveedor: ProveedorIn = {
        Nombre : this.inputNombre.GetInputValue(),
        Direccion: this.inputDireccion.GetInputValue(),
        Email : this.inputEmail.GetInputValue(),
        Telefono : this.inputTelefono.GetInputValue(),
      };

      this.apiRequest.Post_Proveedor(proveedor).subscribe({ 
        next: (value:ProveedorOut) =>{
          this.dialogService.DisplayObject("Proveedor creado exitosamente!" ,value);
          this.ObtenerProveedoresYActualizarTabla();
        },
        error: (value:ErrorEvent) => {},  
        complete: () => console.info('complete') 
      });
    }
    else{
      this.TocarTodosLosInputs();
    }
  }

  private TodosLosInputsSonValidos():boolean{
    return this.inputNombre.HasValidValueOrIsDisabled()
        && this.inputDireccion.HasValidValueOrIsDisabled()
        && this.inputEmail.HasValidValueOrIsDisabled()
        && this.inputTelefono.HasValidValueOrIsDisabled();
  }

  private TocarTodosLosInputs(){
    this.inputNombre.MarkAsTouched();
    this.inputDireccion.MarkAsTouched();
    this.inputEmail.MarkAsTouched();
    this.inputTelefono.MarkAsTouched();
  }

  EditarProveedor(event:ProveedorOut){
    this.proveedorSeleccionado = event;

    this.inputEditarNombre.ColocarValor(event.nombre);
    this.inputEditarNombre.Habilitar();
  
    this.inputEditarDireccion.ColocarValor(event.direccion);
    this.inputEditarDireccion.Habilitar();
  
    this.inputEditarEmail.ColocarValor(event.email);
    this.inputEditarEmail.Habilitar();
  
    this.inputEditarTelefono.ColocarValor(event.telefono);
    this.inputEditarTelefono.Habilitar();
  

    this.desabilitado.botonEditar = false;
    this.desabilitado.botonEliminar = false;
    this.desabilitado.botonLimpiar = false;
  }

  ConfirmarEditar(){
    if(this.TodosLosInputsEditarSonValidos()){
      let proveedor: ProveedorIn = {
        Nombre : this.inputEditarNombre.GetInputValue(),
        Direccion : this.inputEditarDireccion.GetInputValue(),
        Email : this.inputEditarEmail.GetInputValue(),
        Telefono : this.inputEditarTelefono.GetInputValue(),
    }
      let id :string = this.proveedorSeleccionado?.id ?? "no-selection-available";
      this.apiRequest.Put_Proveedor(proveedor, id).subscribe({
        next: (value:ProveedorOut) => {
          this.dialogService.DisplayObject("Proveedor editado exitosamente!", value);
          this.ObtenerProveedoresYActualizarTabla()},
        error: (value:ErrorEvent) => {},  
        complete: () => console.info('complete') 
      });
    }
    else{
      this.TocarTodosLosInputsEditar();
    }
  }

  private TodosLosInputsEditarSonValidos():boolean{
    return this.inputEditarNombre.HasValidValueOrIsDisabled()
        && this.inputEditarDireccion.HasValidValueOrIsDisabled()
        && this.inputEditarEmail.HasValidValueOrIsDisabled()
        && this.inputEditarTelefono.HasValidValueOrIsDisabled();
  }

  private TocarTodosLosInputsEditar(){
    this.inputEditarNombre.MarkAsTouched();
    this.inputEditarDireccion.MarkAsTouched();
    this.inputEditarEmail.MarkAsTouched();
    this.inputEditarTelefono.MarkAsTouched();
  }

  ConfirmarEliminar(){
    let id :string = this.proveedorSeleccionado?.id ?? "no-selection-available";
    this.apiRequest.Delete_Proveedor(id).subscribe({
      next: () => {
        this.dialogService.DisplayMessage("Proveedor Eliminado exitosamente.");
        this.LimpiarSeleccion();
        this.ObtenerProveedoresYActualizarTabla()
      },        
      error: (value:ErrorEvent) => {},  
      complete: () => console.info('complete') 
    });
  }

  LimpiarSeleccion(){
    this.proveedorSeleccionado = undefined;

    this.inputEditarNombre.ColocarValor("");
    this.inputEditarNombre.Desabilitar();
  
    this.inputEditarDireccion.ColocarValor("");
    this.inputEditarDireccion.Desabilitar();
  
    this.inputEditarEmail.ColocarValor("");
    this.inputEditarEmail.Desabilitar();
  
    this.inputEditarTelefono.ColocarValor("");
    this.inputEditarTelefono.Desabilitar();
  
    this.desabilitado.botonEditar = true;
    this.desabilitado.botonEliminar = true;
    this.desabilitado.botonLimpiar = true;
  }
}

const PROVEEDOR_TABLE_COLUMN_STRUCTURE = [
  {
    columnDef: 'nombre',
    header: 'Nombre',
    cell: (element: ProveedorOut) => `${element.nombre}`,
  },
  {
    columnDef: 'direccion',
    header: 'Direccion',
    cell: (element: ProveedorOut) => `${element.direccion}`,
  },
  {
    columnDef: 'email',
    header: 'Email',
    cell: (element: ProveedorOut) => `${element.email}`,
  },
  {
    columnDef: 'telefono',
    header: 'Telefono',
    cell: (element: ProveedorOut) => `${element.telefono}`,
  },
];