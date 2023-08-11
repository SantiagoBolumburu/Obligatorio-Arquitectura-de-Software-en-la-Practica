import { Component, OnInit } from '@angular/core';
import { DialogServiceService } from 'src/app/basic/dialog/dialog-service.service';
import { ApiRequestService } from 'src/app/customServices/api-request.service';
import { AppKeyOut } from 'src/app/models/out/appkeyOut';

@Component({
  selector: 'app-generar-app-key',
  templateUrl: './generar-app-key.component.html',
  styleUrls: ['./generar-app-key.component.css']
})
export class GenerarAppKeyComponent implements OnInit{
  private apiRequest!:ApiRequestService;
  private dialogService!:DialogServiceService;
  currentAppKey:string = "";

  constructor(api_service:ApiRequestService, dialog_Service:DialogServiceService){
    this.apiRequest = api_service;
    this.dialogService = dialog_Service;
  }
  
  ngOnInit(): void {
    this.ObtenerYColocarApplicationKey();
  }

  GenerarAppKey(){
    this.apiRequest.Post_Appkey().subscribe({
      next: (value:AppKeyOut) => {
        this.currentAppKey = value.appkey;
      },
      error: (value:ErrorEvent) => {},  
      complete: () => {} 
    });
  }

  ObtenerYColocarApplicationKey(){
    this.apiRequest.Get_Appkey().subscribe({
      next: (value:AppKeyOut) => {
        this.currentAppKey = value.appkey;
      },
      error: (value:ErrorEvent) => {},  
      complete: () => {} 
    });
  }

  EliminarAppKey(){
    this.apiRequest.Delete_Appkey().subscribe({
      next: () => {
        this.currentAppKey = "";
      },
      error: (value:ErrorEvent) => {},  
      complete: () => {} 
    });
  }
}
