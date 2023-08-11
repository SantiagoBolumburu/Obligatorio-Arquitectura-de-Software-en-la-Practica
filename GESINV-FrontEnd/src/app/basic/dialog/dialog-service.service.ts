import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from './dialog.component';

@Injectable({
  providedIn: 'root'
})
export class DialogServiceService {

  constructor(public dialog: MatDialog) {}

  openDialog(titulo:string='Error',mensaje:string[]|string='Ocurrio algo inesperado',newWidth:string='450px'): void {
    let newMessage:string[];
    if(typeof mensaje === "string"){
      newMessage = [mensaje];
    }
    else{
      newMessage = mensaje;
    }

    this.dialog.open(DialogComponent, {
      width: newWidth,
      data :{
        titulo:titulo,
        message:newMessage,
      }
    });
  }

  openDialogTittleOnly(titulo:string='Error',newWidth:string='450px'): void {
    this.dialog.open(DialogComponent, {
      width: newWidth,
      data :{
        titulo:titulo,
      }
    });
  }

  DisplayObject(titulo:string, object:any){
    let properties:string[] = Object.keys(object);

    let niceValue:string[] = [];

    properties.forEach(property => {
      niceValue = niceValue.concat( `${property.toUpperCase()}: ${String(object[property] as keyof any)}` )
    });
    this.openDialog(titulo, niceValue, "650px");
  }

  DisplayMessage(titulo:string){
    this.openDialogTittleOnly(titulo);
  }
}

