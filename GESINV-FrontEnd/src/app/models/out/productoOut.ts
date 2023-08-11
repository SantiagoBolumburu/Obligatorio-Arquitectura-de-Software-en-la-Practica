export interface ProductoOut {
    id:string;
    nombre:string;
    descripcion:string;
    imagenPath:string;
    precio:number;
    cantidadEnInventarioInicial:number;
    cantidadEnInventario:number; 
    cantidadComprada:number; 
    cantidadVendida:number; 
}