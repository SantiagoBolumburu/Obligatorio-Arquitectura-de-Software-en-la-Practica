export interface VentaIn{
    nombreCliente:string,
    fechaVenta:Date,
    montoTotalEnPesos:number,
    productosYCantidad:{item1:string,item2:number}[]
}