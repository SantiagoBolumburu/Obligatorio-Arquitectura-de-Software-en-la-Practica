import { Component, OnInit, inject } from '@angular/core';
import { Subscription } from 'rxjs';
import { DialogServiceService } from 'src/app/basic/dialog/dialog-service.service';
import { ApiRequestService } from 'src/app/customServices/api-request.service';
import { ProductoOut } from 'src/app/models/out/productoOut';
import { SubscripcionOut } from 'src/app/models/out/subscripcionOut';
import { SubscripcionesSetupOut } from 'src/app/models/out/subscripcionesSetupOut';
import { Subscripcion } from 'src/app/models/propios/subscripcion';

@Component({
  selector: 'app-gestion-subscripciones',
  templateUrl: './gestion-subscripciones.component.html',
  styleUrls: ['./gestion-subscripciones.component.css']
})
export class GestionSubscripcionesComponent implements OnInit{
  private apiService:ApiRequestService = inject(ApiRequestService);
  private dialogService:DialogServiceService = inject(DialogServiceService);

  subscripciones:Subscripcion[] = [];
  
  
  
  ngOnInit(): void {
    this.ObtenerDatosYActualizarTablas();
  }



  private ObtenerDatosYActualizarTablas(){
    this.apiService.Get_Productos().subscribe({
      next: (productos:ProductoOut[]) => {
        console.log(productos);

        this.apiService.Get_Subscriptions().subscribe({
          next: (subscripciones:SubscripcionesSetupOut) => {
        console.log(subscripciones);
            this.ActualizarTablas(productos, subscripciones);

          },
          error: (value:ErrorEvent) => {},  
          complete: () => {}
        });
      },
      error: (value:ErrorEvent) => {},  
      complete: () => {}
    });
  }


  private ActualizarTablas(productos:ProductoOut[], subscripcionesSetupOut:SubscripcionesSetupOut){
    let subscripciones : Subscripcion[] = []

    productos.forEach( producto => {
      subscripciones = subscripciones.concat({
        ProductoId : producto.id,
        PorductoNombre : producto.nombre,
        SubscriptoCompraVenta : subscripcionesSetupOut.compraVentaSubscriptions.some(s => s.productoId === producto.id),
        SubscriptoStock : subscripcionesSetupOut.stockSubscription.some(s => s.productoId === producto.id),
      })
    })

    console.log(subscripciones);
    this.subscripciones = subscripciones
  }

  SubscribirACompraVenta(producto:Subscripcion){
    this.apiService.Post_Suscripcion_Compraventa(producto.ProductoId).subscribe({ 
      next: (value:SubscripcionOut) =>{
        this.dialogService.DisplayObject("Te suscribiste exitosamente!" ,value);
        this.ObtenerDatosYActualizarTablas();
      },
      error: (value:ErrorEvent) => {},  
      complete: () => {}
    });
  }

  DesubscribirACompraVenta(producto:Subscripcion){
    this.apiService.Delete_Suscripcion_Compraventa(producto.ProductoId).subscribe({ 
      next: () =>{
        this.dialogService.DisplayMessage("Suscripcion cancelada exitosamente");
        this.ObtenerDatosYActualizarTablas();
      },
      error: (value:ErrorEvent) => {},  
      complete: () => {}
    });
  }

  SubscribirAStock(producto:Subscripcion){
    this.apiService.Post_Suscripcion_Stock(producto.ProductoId).subscribe({ 
      next: (value:SubscripcionOut) =>{
        this.dialogService.DisplayObject("Te suscribiste exitosamente!" ,value);
        this.ObtenerDatosYActualizarTablas();
      },
      error: (value:ErrorEvent) => {},  
      complete: () => {}
    });
  }

  DesubscribirAStock(producto:Subscripcion){
    this.apiService.Delete_Suscripcion_Stock(producto.ProductoId).subscribe({ 
      next: () =>{
        this.dialogService.DisplayMessage("Suscripcion cancelada exitosamente");
        this.ObtenerDatosYActualizarTablas();
      },
      error: (value:ErrorEvent) => {},  
      complete: () => {}
    });
  }
}