export interface CompraIn {
    proveedorId:string; 
    fechaCompra:Date;
    costoTotal:number;
    productosYCantidad:{item1:string,item2:number}[];
}