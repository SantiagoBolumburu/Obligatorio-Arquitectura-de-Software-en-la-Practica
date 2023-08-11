package modelsout

type Venta struct {
	Id                 string                    `json:"id"`
	NombreCliente      string                    `json:"nombreCliente"`
	FechaVenta         string                    `json:"fechaVenta"`
	MontoTotalEnPesos  float64                   `json:"montoTotalEnPesos"`
	Programada         bool                      `json:"programada"`
	Realizada          bool                      `json:"realizada"`
	ProductosYCantidad []ProductoNombreYCantidad `json:"productosYCantidad"`
}
