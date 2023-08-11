import { Component, OnInit, ViewChild } from '@angular/core';
import { DatePickerComponent } from 'src/app/basic/date-picker/date-picker.component';
import { DialogServiceService } from 'src/app/basic/dialog/dialog-service.service';
import { InputComponent } from 'src/app/basic/input-basic/input.component';
import { SelectComponent, SelectOption } from 'src/app/basic/select/select.component';
import { InputColumnOptions, TableComponent } from 'src/app/basic/table/table.component';
import { ApiRequestService } from 'src/app/customServices/api-request.service';
import { VentaIn } from 'src/app/models/in/ventaIn';
import { ProductoOut } from 'src/app/models/out/productoOut';
import { VentaOut } from 'src/app/models/out/ventaOut';

interface DatosRegistroVentas{
  datosTabla_productosDisponibles:ProductoOut[],
}

@Component({
  selector: 'app-registro-ventas',
  templateUrl: './registro-ventas.component.html',
  styleUrls: ['./registro-ventas.component.css']
})
export class RegistroVentasComponent implements OnInit{
  private apiRequest!:ApiRequestService;
  private dialogService!:DialogServiceService;
  
  esVentaProgramada:boolean = false;

  estructuraColumnas_productosDisponibles = PRODUCTOS_DISPONIBLES_TABLE_COLUMN_STRUCTURE;
  estructuraColumnas_productosSeleccionados = PRODUCTOS_SELECCIONADOS_TABLE_COLUMN_STRUCTURE;
  
  @ViewChild("fechaDatePicker")
  fechaDatePicker!:DatePickerComponent;

  @ViewChild("costoTotalInput")
  costoTotalInput!:InputComponent;

  @ViewChild("nombreClienteInput")
  nombreClienteInput!:InputComponent;

  @ViewChild("tablaProductosSeleccionados")
  tablaProductosSeleccionados!:TableComponent;

  @ViewChild("tablaProductosDisponibles")
  tablaProductosDisponibles!:TableComponent;

  datos:DatosRegistroVentas = {
    datosTabla_productosDisponibles:[]
  }

  datosCampos = {
    fechaVenta : undefined,
    costoTotal : undefined,
    nombreCliente : undefined
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
    this.ObtenerProductos();
  }

  ObtenerProductos(){
    this.apiRequest.Get_Productos().subscribe({
      next: (value:ProductoOut[]) => {
        this.datos.datosTabla_productosDisponibles = value;
        this.tablaProductosDisponibles.ActualizarFilas(value);
      },
      error: (value:ErrorEvent) => {},  
      complete: () => {} 
    });
  }

  LimpiarDatos(){
    this.fechaDatePicker.LimpiarValor();
    this.costoTotalInput.LimpiarValor();
    this.nombreClienteInput.LimpiarValor();
    this.tablaProductosSeleccionados.ActualizarFilas([]);
  }

  EsVentaProgramadaValueChanged(){
    this.esVentaProgramada = !this.esVentaProgramada;
    console.log(this.esVentaProgramada);
  }


  IngresarVenta(){
    if(this.TodoValido()){
      let venta:VentaIn = this.CrearVenta();

      if(!this.esVentaProgramada){
        this.apiRequest.Post_Venta(venta).subscribe({
          next: (value:VentaOut) =>{
            this.dialogService.DisplayObject("Venta creada exitosamente!", value);
            this.LimpiarDatos();
          },
          error: (value:ErrorEvent) => {},  
          complete: () => {}
        });
      }
      else{
        this.apiRequest.Post_Venta_Programada(venta).subscribe({
          next: (value:VentaOut) =>{
            this.dialogService.DisplayObject("Venta Programada creada exitosamente!", value);
            this.LimpiarDatos();
          },
          error: (value:ErrorEvent) => {},  
          complete: () => {}
        });
      }
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
      && this.nombreClienteInput.HasValidValueOrIsDisabled()
      && this.tablaProductosSeleccionados.InputColumn_TodosLosValoresSonValidos()
      && this.tablaProductosSeleccionados.ObtenerCantidadDeFilas() >= 1;
  }

  private TocarTodo(){
    this.fechaDatePicker.MarkAsTouched();
    this.costoTotalInput.MarkAsTouched();
    this.nombreClienteInput.MarkAsTouched();
    this.tablaProductosSeleccionados.InputColumn_TocarTodosLosInput();
  }

  private MensajeHayQueSeleccionarUnProducto(){
    this.dialogService.DisplayMessage("Hay que seleccionar minimo un producto.")
  }

  private CrearVenta():VentaIn{
    let cliente_nombre:string = this.nombreClienteInput.GetInputValue();
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

    let nuevaVenta:VentaIn ={
      nombreCliente : cliente_nombre,
      fechaVenta : fecha,
      montoTotalEnPesos : costo_Total,
      productosYCantidad : prodYCant
    } 
    
    return nuevaVenta;
  }

  ColocarEnTablaElementosSeleccionados(event:ProductoOut){
    this.tablaProductosSeleccionados.AgregarElemento(event);
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