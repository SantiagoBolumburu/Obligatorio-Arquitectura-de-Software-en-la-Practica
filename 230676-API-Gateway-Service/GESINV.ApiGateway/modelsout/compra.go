package modelsout

type Compra struct {
	Id                       string                    `json:"id"`
	ProveedorId              string                    `json:"proveedorId"`
	NombreProveedor          string                    `json:"nombreProveedor"`
	FechaCompra              string                    `json:"fechaCompra"`
	CostoTotal               float64                   `json:"costoTotal"`
	ProductosNombreYCantidad []ProductoNombreYCantidad `json:"productosNombreYCantidad"`
}
