package modelsout

type Producto struct {
	Id                          string  `json:"id"`
	Nombre                      string  `json:"nombre"`
	Descripcion                 string  `json:"descripcion"`
	ImagenPath                  string  `json:"imagenPath"`
	Precio                      float64 `json:"precio"`
	CantidadEnInventarioInicial int     `json:"cantidadEnInventarioInicial"`
	CantidadComprada            int     `json:"cantidadComprada"`
	CantidadVendida             int     `json:"cantidadVendida"`
	CantidadEnInventario        int     `json:"cantidadEnInventario"`
}
