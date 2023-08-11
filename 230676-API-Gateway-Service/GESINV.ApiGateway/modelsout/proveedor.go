package modelsout

type Proveedor struct {
	Id        string `json:"id"`
	Nombre    string `json:"nombre"`
	Direccion string `json:"direccion"`
	Email     string `json:"email"`
	Telefono  string `json:"telefono"`
}
