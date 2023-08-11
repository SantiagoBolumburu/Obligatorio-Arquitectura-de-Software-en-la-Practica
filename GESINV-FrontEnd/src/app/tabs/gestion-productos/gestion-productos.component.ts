import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatTable } from '@angular/material/table';
import { Observable } from 'rxjs';
import { DialogServiceService } from 'src/app/basic/dialog/dialog-service.service';
import { InputComponent } from 'src/app/basic/input-basic/input.component';
import { TableComponent } from 'src/app/basic/table/table.component';
import { TextAreaComponent } from 'src/app/basic/text-area/text-area.component';
import { ApiRequestService } from 'src/app/customServices/api-request.service';
import { ProductoIn } from 'src/app/models/in/productoIn';
import { ProductoOut } from 'src/app/models/out/productoOut';

@Component({
  selector: 'app-gestion-productos',
  templateUrl: './gestion-productos.component.html',
  styleUrls: ['./gestion-productos.component.css']
})
export class GestionProductosComponent implements OnInit{
  private apiRequest!:ApiRequestService;
  private dialogService!:DialogServiceService;

  productos:ProductoOut[] = [];

  estructuraColumnas = PRODUCTO_TABLE_COLUMN_STRUCTURE;

  constructor(api_service:ApiRequestService, dialog_Service:DialogServiceService){
    this.apiRequest = api_service;
    this.dialogService = dialog_Service;
  }

  ngOnInit(): void {
    this.ObtenerProductosYActualizarTabla();
  }

  //CREAR
  @ViewChild("nombreInput")
  inputNombre!:InputComponent;

  @ViewChild("descripcionInput")
  inputDescripcion!:TextAreaComponent;

  @ViewChild("imgPathInput")
  inputImgPath!:InputComponent;

  @ViewChild("precioInput")
  inputPrecio!:InputComponent;

  @ViewChild("cantidadEnInvInput")
  inputCantidadEnInv!:InputComponent;

  //EDITAR
  @ViewChild("nombreEditarInput")
  inputEditarNombre!:InputComponent;

  @ViewChild("descripcionEditarInput")
  inputEditarDescripcion!:TextAreaComponent;

  @ViewChild("imgPathEditarInput")
  inputEditarImgPath!:InputComponent;

  @ViewChild("precioEditarInput")
  inputEditarPrecio!:InputComponent;

  @ViewChild("cantidadEnInvEditarInput")
  inputEditarCantidadEnInv!:InputComponent;

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

  productoSeleccionado?:ProductoOut;

  ObtenerProductosYActualizarTabla(){
    this.apiRequest.Get_Productos().subscribe({
      next: (value:ProductoOut[]) => {
        this.productos = value;
        this.tabla.ActualizarFilas(this.productos);
      },
      error: (value:ErrorEvent) => {},  
      complete: () => console.info('complete') 
    });
  }

  IngresarProducto(){
    if(this.TodosLosInputsSonValidos()){
      let producto: ProductoIn = {
        Nombre : this.inputNombre.GetInputValue(),
        Descripcion: this.inputDescripcion.GetInputValue(),
        ImagenPath : this.inputImgPath.GetInputValue(),
        Precio : this.inputPrecio.GetInputValueAsNumber(),
        CantidadEnInventario : this.inputCantidadEnInv.GetInputValueAsNumber() 
    }

      this.apiRequest.Post_Producto(producto).subscribe({
        next: (value:ProductoOut) =>{
          this.dialogService.DisplayObject("Producto creado exitosamente!" ,value);
          this.ObtenerProductosYActualizarTabla();
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
        && this.inputDescripcion.HasValidValueOrIsDisabled()
        && this.inputImgPath.HasValidValueOrIsDisabled()
        && this.inputPrecio.HasValidValueOrIsDisabled()
        && this.inputCantidadEnInv.HasValidValueOrIsDisabled();
  }

  private TocarTodosLosInputs(){
    this.inputNombre.MarkAsTouched();
    this.inputDescripcion.MarkAsTouched();
    this.inputImgPath.MarkAsTouched();
    this.inputPrecio.MarkAsTouched();
    this.inputCantidadEnInv.MarkAsTouched();
  }

  EditarProducto(event:ProductoOut){
    this.productoSeleccionado = event;

    this.inputEditarNombre.ColocarValor(event.nombre);
    this.inputEditarNombre.Habilitar();
  
    this.inputEditarDescripcion.ColocarValor(event.descripcion);
    this.inputEditarDescripcion.Habilitar();
  
    this.inputEditarImgPath.ColocarValor(event.imagenPath);
    this.inputEditarImgPath.Habilitar();
  
    this.inputEditarPrecio.ColocarValor(event.precio);
    this.inputEditarPrecio.Habilitar();
  
    this.inputEditarCantidadEnInv.ColocarValor(event.cantidadEnInventario);
    this.inputEditarCantidadEnInv.Habilitar();

    this.desabilitado.botonEditar = false;
    this.desabilitado.botonEliminar = false;
    this.desabilitado.botonLimpiar = false;
  }

  ConfirmarEditar(){
    if(this.TodosLosInputsEditarSonValidos()){
      let producto: ProductoIn = {
        Nombre : this.inputEditarNombre.GetInputValue(),
        Descripcion: this.inputEditarDescripcion.GetInputValue(),
        ImagenPath : this.inputEditarImgPath.GetInputValue(),
        Precio : this.inputEditarPrecio.GetInputValueAsNumber(),
        CantidadEnInventario : this.inputEditarCantidadEnInv.GetInputValueAsNumber() 
    }
      let id :string = this.productoSeleccionado?.id ?? "no-selection-available";
      this.apiRequest.Put_Producto(producto, id).subscribe({
        next: (value:ProductoOut) => {
          this.dialogService.DisplayObject("Producto editado exitosamente!", value);
          this.ObtenerProductosYActualizarTabla()},
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
        && this.inputEditarDescripcion.HasValidValueOrIsDisabled()
        && this.inputEditarImgPath.HasValidValueOrIsDisabled()
        && this.inputEditarPrecio.HasValidValueOrIsDisabled()
        && this.inputEditarCantidadEnInv.HasValidValueOrIsDisabled();
  }

  private TocarTodosLosInputsEditar(){
    this.inputEditarNombre.MarkAsTouched();
    this.inputEditarDescripcion.MarkAsTouched();
    this.inputEditarImgPath.MarkAsTouched();
    this.inputEditarPrecio.MarkAsTouched();
    this.inputEditarCantidadEnInv.MarkAsTouched();
  }



  ConfirmarEliminar(){
    let id :string = this.productoSeleccionado?.id ?? "no-selection-available";
    this.apiRequest.Delete_Producto(id).subscribe({
      next: () => {
        this.dialogService.DisplayMessage("Producto Eliminado exitosamente.");
        this.LimpiarSeleccion();
        this.ObtenerProductosYActualizarTabla()
      },        
      error: (value:ErrorEvent) => {},  
      complete: () => console.info('complete') 
    });
  }

  LimpiarSeleccion(){
    this.productoSeleccionado = undefined;

    this.inputEditarNombre.ColocarValor("");
    this.inputEditarNombre.Desabilitar();
  
    this.inputEditarDescripcion.ColocarValor("");
    this.inputEditarDescripcion.Desabilitar();
  
    this.inputEditarImgPath.ColocarValor("");
    this.inputEditarImgPath.Desabilitar();
  
    this.inputEditarPrecio.ColocarValor("");
    this.inputEditarPrecio.Desabilitar();
  
    this.inputEditarCantidadEnInv.ColocarValor("");
    this.inputEditarCantidadEnInv.Desabilitar();

    this.desabilitado.botonEditar = true;
    this.desabilitado.botonEliminar = true;
    this.desabilitado.botonLimpiar = true;
  }
}

/*
  {
    columnDef: 'Id',
    header: 'Id',
    cell: (element: ProductoOut) => `${element.id}`,
  },
*/

const PRODUCTO_TABLE_COLUMN_STRUCTURE = [
  {
    columnDef: 'nombre',
    header: 'Nombre',
    cell: (element: ProductoOut) => `${element.nombre}`,
  },
  {
    columnDef: 'descripcion',
    header: 'Descripcion',
    cell: (element: ProductoOut) => `${element.descripcion}`,
  },
  {
    columnDef: 'imagenPath',
    header: 'ImagenPath',
    cell: (element: ProductoOut) => `${element.imagenPath}`,
  },
  {
    columnDef: 'precio',
    header: 'Precio',
    cell: (element: ProductoOut) => `${element.precio}`,
  },
  {
    columnDef: 'cantidadEnInventario',
    header: 'Cantidad en Inventario',
    cell: (element: ProductoOut) => `${element.cantidadEnInventario}`,
  },
  {
    columnDef: 'cantidadComprada',
    header: 'Cantidad Comprada',
    cell: (element: ProductoOut) => `${element.cantidadComprada}`,
  },
  {
    columnDef: 'cantidadVendida',
    header: 'Cantidad Vendida',
    cell: (element: ProductoOut) => `${element.cantidadVendida}`,
  },
];