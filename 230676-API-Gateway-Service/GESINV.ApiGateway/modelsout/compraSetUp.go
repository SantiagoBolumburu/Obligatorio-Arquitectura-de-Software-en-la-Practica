package modelsout

type CompraSetUp struct {
	Productos   []Producto  `json:"productos"`
	Proveedores []Proveedor `json:"proveedores"`
}
