import { Component, OnInit, ViewChild } from '@angular/core';
import { DatePickerComponent } from 'src/app/basic/date-picker/date-picker.component';
import { DialogServiceService } from 'src/app/basic/dialog/dialog-service.service';
import { InputComponent } from 'src/app/basic/input-basic/input.component';
import { TableComponent } from 'src/app/basic/table/table.component';
import { ApiRequestService } from 'src/app/customServices/api-request.service';
import { GetVentasParams } from 'src/app/models/inOut/getVentasParams';
import { VentaOut } from 'src/app/models/out/ventaOut';

@Component({
  selector: 'app-pagina-inicio-empresa',
  templateUrl: './pagina-inicio-empresa.component.html',
  styleUrls: ['./pagina-inicio-empresa.component.css']
})
export class PaginaInicioEmpresaComponent implements OnInit{
  private apiRequest!:ApiRequestService;
  private dialogService!:DialogServiceService;

  estructuraColumnas_productosDisponibles = PRODUCTOS_DISPONIBLES_TABLE_COLUMN_STRUCTURE;

  @ViewChild("fechaDesdePicker")
  fechaDesdePicker!:DatePickerComponent;

  @ViewChild("fechaHastaPicker")
  fechaHastaPicker!:DatePickerComponent;


  @ViewChild("tablaProductosSeleccionados")
  tablaProductosSeleccionados!:TableComponent;

  @ViewChild("tablaProductosDisponibles")
  tablaProductosDisponibles!:TableComponent;

  datosTabla_productosDisponibles!:VentaOut[]

  filtros:GetVentasParams = {
    fechaDesde : undefined,
    fechaHasta : undefined,
    indicePagina : undefined,
    cantidadPorPagina : undefined,
  }
  
  constructor(api_service:ApiRequestService, dialog_Service:DialogServiceService){
    this.apiRequest = api_service;
    this.dialogService = dialog_Service;
  }

  ngOnInit(): void {
    this.ObtenerYColocarVentas();
  }

  ObtenerYColocarVentas(){
    this.apiRequest.Get_Ventas(this.filtros).subscribe({
      next: (value:VentaOut[]) => {
        let ventasOrdenadasPorFecha = value.sort(this.CompararVentasPorFecha);

        this.datosTabla_productosDisponibles = ventasOrdenadasPorFecha;
        this.tablaProductosDisponibles.ActualizarFilas(ventasOrdenadasPorFecha);
      },
      error: (value:ErrorEvent) => {},  
      complete: () => {} 
    });
  }

  CompararVentasPorFecha(a:VentaOut, b:VentaOut){
    if ( (new Date(a.fechaVenta).getTime()) < (new Date(b.fechaVenta).getTime())){
      return -1;
    }
    if ( (new Date(a.fechaVenta).getTime()) > (new Date(b.fechaVenta).getTime()) ){
      return 1;
    }
    return 0;
    //return (new Date(a.fechaVenta).getMilliseconds()) - (new Date(b.fechaVenta).getMilliseconds());
  }

  LimpiarDatosFechaDesde(){
    this.fechaDesdePicker.LimpiarValor();
    this.filtros.fechaDesde = undefined;
    this.ObtenerYColocarVentas();
  }

  FiltrarFechaDesde(){
    if(this.fechaDesdePicker.HasValidValueOrIsDisabled()){

      let fechaDesde:Date = this.fechaDesdePicker.GetInputValue();
      this.filtros.fechaDesde = fechaDesde;

      this.ObtenerYColocarVentas();
    }
  }

  LimpiarDatosFechaHasta(){
    this.fechaHastaPicker.LimpiarValor();
    this.filtros.fechaHasta = undefined;
    this.ObtenerYColocarVentas();
  }

  FiltrarFechaHasta(){
    if(this.fechaHastaPicker.HasValidValueOrIsDisabled()){

      let fechaHasta:Date = this.fechaHastaPicker.GetInputValue();
      this.filtros.fechaHasta = fechaHasta;

      this.ObtenerYColocarVentas();
    }
  }
}

//fecha, productos y monto
const PRODUCTOS_DISPONIBLES_TABLE_COLUMN_STRUCTURE=[
  {
    columnDef: 'fecha',
    header: 'Fecha',
    cell: (element: VentaOut) => `${element.fechaVenta}`,
  },
  {
    columnDef: 'nombreCliente',
    header: 'Nombre Cliente',
    cell: (element: VentaOut) => `${element.nombreCliente}`,
  },
  {
    columnDef: 'monto',
    header: 'Monto Total',
    cell: (element: VentaOut) => `${element.montoTotalEnPesos}`,
  },
]