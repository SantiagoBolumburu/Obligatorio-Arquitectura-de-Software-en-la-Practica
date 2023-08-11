import { Component, inject } from '@angular/core';
import { DialogServiceService } from 'src/app/basic/dialog/dialog-service.service';
import { ApiRequestService } from 'src/app/customServices/api-request.service';

@Component({
  selector: 'app-reportes',
  templateUrl: './reportes.component.html',
  styleUrls: ['./reportes.component.css']
})
export class ReportesComponent {
  private apiService:ApiRequestService = inject(ApiRequestService);
  private dialogService:DialogServiceService = inject(DialogServiceService);


//reportes/comprasyventas
  GenerarReporteCompraVentas(){
    this.apiService.Post_Reportes_Compraventa().subscribe({ 
      next: () =>{
        this.dialogService.DisplayMessage("El reporte fue enviado a su correo");
      },
      error: (value:ErrorEvent) => {},  
      complete: () => {}
    });
  }
}
