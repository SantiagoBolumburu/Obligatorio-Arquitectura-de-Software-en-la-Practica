export interface VentaOut{
    id:string;
    nombreCliente:string,
    fechaVenta:Date,
    montoTotalEnPesos:number,
    productosYCantidad:{item1:string,item2:string,item3:number}
}