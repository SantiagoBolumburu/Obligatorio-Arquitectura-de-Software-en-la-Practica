import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { NgSelectOption } from '@angular/forms';
import { every } from 'rxjs';
import { DatePickerComponent } from 'src/app/basic/date-picker/date-picker.component';
import { DialogServiceService } from 'src/app/basic/dialog/dialog-service.service';
import { InputComponent } from 'src/app/basic/input-basic/input.component';
import { SelectComponent, SelectOption } from 'src/app/basic/select/select.component';
import { InputColumnOptions, TableComponent } from 'src/app/basic/table/table.component';
import { ApiRequestService } from 'src/app/customServices/api-request.service';
import { CompraIn } from 'src/app/models/in/compraIn';
import { CompraOut } from 'src/app/models/out/compraOut';
import { CompraSetupOut } from 'src/app/models/out/compraSetupOut';
import { ProductoOut } from 'src/app/models/out/productoOut';
import { ProveedorOut } from 'src/app/models/out/proveedorOut';

interface DatosRegistroCompras{
  datosTabla_productosDisponibles:ProductoOut[],
  opcionesSelect_Proveedores:SelectOption[]
}

@Component({
  selector: 'app-registro-compras',
  templateUrl: './registro-compras.component.html',
  styleUrls: ['./registro-compras.component.css']
})
export class RegistroComprasComponent implements OnInit{
  private apiRequest!:ApiRequestService;
  private dialogService!:DialogServiceService;

  private compraSetUp!:CompraSetupOut
  
  estructuraColumnas_productosDisponibles = PRODUCTOS_DISPONIBLES_TABLE_COLUMN_STRUCTURE;
  estructuraColumnas_productosSeleccionados = PRODUCTOS_SELECCIONADOS_TABLE_COLUMN_STRUCTURE;
  
  @ViewChild("fechaDatePicker")
  fechaDatePicker!:DatePickerComponent;

  @ViewChild("costoTotalInput")
  costoTotalInput!:InputComponent;

  @ViewChild("proveedorSelect")
  proveedorSelect!:SelectComponent;

  @ViewChild("tablaProductosSeleccionados")
  tablaProductosSeleccionados!:TableComponent;

  @ViewChild("tablaProductosDisponibles")
  tablaProductosDisponibles!:TableComponent;

  datos:DatosRegistroCompras = {
    datosTabla_productosDisponibles:[],
    opcionesSelect_Proveedores: [],
  }

  datosCampos = {
    fechaCompra : undefined,
    costoTotal : undefined,
    proveedor : undefined
  }

  inputColumnOptions:InputColumnOptions = {
    nombre : "",
    tipo : "digits",
    defaultval : "",
    disabled : false,
    placeholder : ""
  }

  constructor(api_service:ApiRequestService, dialog_Service:DialogServiceService){
    this.apiRequest = api_service;
    this.dialogService = dialog_Service;

  }
  
  ngOnInit(): void {
    this.ObtenerCompraSetUp();
  }

  ObtenerCompraSetUp(){
    this.apiRequest.Get_Compra_Setup().subscribe({
      next: (value:CompraSetupOut) => {
        this.compraSetUp = value;
        this.datos.datosTabla_productosDisponibles = value.productos;
        this.datos.opcionesSelect_Proveedores = this.ConvertirProveedoresASelectOptions(value.proveedores);
        this.tablaProductosDisponibles.ActualizarFilas(value.productos);
      },
      error: (value:ErrorEvent) => {},  
      complete: () => {} 
    });
  }

  LimpiarDatos(){
    this.fechaDatePicker.LimpiarValor();
    this.costoTotalInput.LimpiarValor();
    this.proveedorSelect.ClearSelection();
    this.tablaProductosSeleccionados.ActualizarFilas([]);
  }

  IngresarCompra(){
    if(this.TodoValido()){
      let compra:CompraIn = this.CrearCompra();
      this.apiRequest.Post_Compra(compra).subscribe({
        next: (value:CompraOut) =>{
          this.dialogService.DisplayObject("Compra creada exitosamente!" ,value);
          this.LimpiarDatos();
        },
        error: (value:ErrorEvent) => {},  
        complete: () => {}
      });
    }
    else{
      this.TocarTodo();
      if(this.tablaProductosSeleccionados.ObtenerCantidadDeFilas() < 1){
        this.MensajeHayQueSeleccionarUnProducto();
      }
    }
  }

  private TodoValido():boolean{
    return this.fechaDatePicker.HasValidValueOrIsDisabled()
      && this.costoTotalInput.HasValidValueOrIsDisabled()
      && this.proveedorSelect.HasValidValueOrIsDisabled()
      && this.tablaProductosSeleccionados.InputColumn_TodosLosValoresSonValidos()
      && this.tablaProductosSeleccionados.ObtenerCantidadDeFilas() >= 1;
  }

  private TocarTodo(){
    this.fechaDatePicker.MarkAsTouched();
    this.costoTotalInput.MarkAsTouched();
    this.proveedorSelect.MarkAsTouched();
    this.tablaProductosSeleccionados.InputColumn_TocarTodosLosInput();
  }

  private MensajeHayQueSeleccionarUnProducto(){
    this.dialogService.DisplayMessage("Hay que seleccionar minimo un producto.")
  }

  private CrearCompra():CompraIn{
    let peovedor_Id:string = this.proveedorSelect.GetSelectedOption()?.Id ?? "";
    let fecha:Date = this.fechaDatePicker.GetInputValue();
    let costo_Total:number = this.costoTotalInput.GetInputValueAsNumber();
    let prodYCantRaw:{item: any,numero: number}[] = this.tablaProductosSeleccionados.InputColumn_ObtenerTodosLosValoresComoNumberYDatos();
    
    let prodYCant: {item1:string,item2:number}[] = [];

    for (let i = 0; i < prodYCantRaw.length; i++) {
      prodYCant = prodYCant.concat({
        item1 : prodYCantRaw[i].item.id,
        item2 : prodYCantRaw[i].numero,
      });
    }

    let nuevaCompra:CompraIn ={
      proveedorId : peovedor_Id,
      fechaCompra : fecha,
      costoTotal : costo_Total,
      productosYCantidad : prodYCant
    } 
    
    return nuevaCompra;
  }

  ColocarEnTablaElementosSeleccionados(event:ProductoOut){
    this.tablaProductosSeleccionados.AgregarElemento(event);
  }

  ConvertirProveedoresASelectOptions(proveedores:ProveedorOut[]):SelectOption[]{
    let options:SelectOption[] = [];

    if(proveedores){
      proveedores.forEach(proveedor => {
        options = options.concat({
          Id : proveedor.id,
          Valor : proveedor.nombre,
          Texto : proveedor.nombre,
          Desabilitado : false,
          Seleccionado : false,
        });
      });
    }

    return options;
  }
}

const PRODUCTOS_DISPONIBLES_TABLE_COLUMN_STRUCTURE=[
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
    columnDef: 'precio',
    header: 'Precio',
    cell: (element: ProductoOut) => `${element.precio}`,
  },
]

const PRODUCTOS_SELECCIONADOS_TABLE_COLUMN_STRUCTURE=[
  {
    columnDef: 'nombre',
    header: 'Nombre',
    cell: (element: ProductoOut) => `${element.nombre}`,
  }
]