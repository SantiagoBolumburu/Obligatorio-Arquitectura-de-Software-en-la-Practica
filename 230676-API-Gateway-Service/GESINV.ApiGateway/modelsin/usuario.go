package modelsin

type Usuario struct {
	Nombre        string `json:"nombre"`
	Email         string `json:"email"`
	Contrasenia   string `json:"contrasenia"`
	NombreEmpresa string `json:"nombreEmpresa"`
}
