import { ProductoOut } from "./productoOut";
import { ProveedorOut } from "./proveedorOut";

export interface CompraSetupOut{
    productos:ProductoOut[];
    proveedores:ProveedorOut[];
}