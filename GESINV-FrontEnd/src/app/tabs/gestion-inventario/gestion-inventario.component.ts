import { Component, OnInit, ViewChild } from '@angular/core';
import { DialogServiceService } from 'src/app/basic/dialog/dialog-service.service';
import { TableComponent } from 'src/app/basic/table/table.component';
import { ApiRequestService } from 'src/app/customServices/api-request.service';
import { ProductoOut } from 'src/app/models/out/productoOut';

@Component({
  selector: 'app-gestion-inventario',
  templateUrl: './gestion-inventario.component.html',
  styleUrls: ['./gestion-inventario.component.css']
})
export class GestionInventarioComponent implements OnInit{
  private apiRequest!:ApiRequestService;
  private dialogService!:DialogServiceService;

  estructuraColumnas_productosDisponibles = PRODUCTOS_DISPONIBLES_TABLE_COLUMN_STRUCTURE;
  datosTabla_productosDisponibles!:ProductoOut[];

  @ViewChild("tablaProductosDisponibles")
  tablaProductosDisponibles!:TableComponent;
  
  constructor(api_service:ApiRequestService, dialog_Service:DialogServiceService){
    this.apiRequest = api_service;
    this.dialogService = dialog_Service;
  }

  ngOnInit(): void {
    this.ObtenerYColocarProductos();
  }


  ObtenerYColocarProductos(){
    this.apiRequest.Get_Productos().subscribe({
      next: (value:ProductoOut[]) => {
        this.datosTabla_productosDisponibles = value;
        this.tablaProductosDisponibles.ActualizarFilas(value);
      },
      error: (value:ErrorEvent) => {},  
      complete: () => {} 
    });
  }

  /*ProdutoOut
    id:string;
    nombre:string;
    descripcion:string;
    imagenPath:string;
    precio:number;
    cantidadEnInventario:number; 
    cantidadComprada:number; 
    cantidadVendida:number; 
  */
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
  {
    columnDef: 'imagenPath',
    header: 'Ruta Imagen',
    cell: (element: ProductoOut) => `${element.imagenPath}`,
  },
  {
    columnDef: 'cantActInv',
    header: 'Cant. Actual en Inventario',
    cell: (element: ProductoOut) => `${element.cantidadEnInventario}`,
  },
  {
    columnDef: 'cantIniInv',
    header: 'Cant. Inicial en Inv.',
    cell: (element: ProductoOut) => `${element.cantidadEnInventarioInicial}`,
  },
  {
    columnDef: 'cantCompr',
    header: 'Cant. Comprada',
    cell: (element: ProductoOut) => `${element.cantidadComprada}`,
  },
  {
    columnDef: 'cantVendida',
    header: 'Cant. Vendida',
    cell: (element: ProductoOut) => `${element.cantidadVendida}`,
  },
]