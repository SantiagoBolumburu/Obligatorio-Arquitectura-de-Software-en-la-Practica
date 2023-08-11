export interface CompraOut {
    id:string;
    proveedorId:string;
    fechaCompra:Date;
    costoTotal:number;
    productosYCantidad:{item1:string,item2:string,item3:number}[]
}