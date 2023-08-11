package api

import (
	"gesinvapigateway/modelsout"
	"gesinvapigateway/services"

	"github.com/gin-gonic/gin"
)

const route_products = "productos"
const route_products_masvendidos = "productos/masvendidos"
const route_products_syncsubscripciones = "productos/syncsubscripciones"
const route_products_proveedores = "proveedores"
const route_products_compras = "compras"
const route_products_compras_proveedor = "compras/proveedor"
const route_products_compras_setup = "compras/setup"
const route_products_ventas = "ventas"
const route_products_ventasprogramadas = "ventasprogramadas"
const route_products_reporte_comprasyventas = "reportes/comprasyventas"

func (s *HttpServer) SetProductsServiceEndpoints(server *gin.Engine) {

	server.GET("/API/v2/health/productos", s.Handle_Products_Health(false, []string{}))

	server.POST("/API/v2/productos", s.Handle_Products_Post(true, []string{ROL_ADMINISTRADOR}))
	server.PUT("/API/v2/productos/:productoid", s.Handle_Products_Put(true, []string{ROL_ADMINISTRADOR}))
	server.GET("/API/v2/productos", s.Handle_Products_Get(true, []string{ROL_ADMINISTRADOR, ROL_EMPLEADO}))
	server.GET("/API/v2/productos/:productoid", s.Handle_Products_Get_By_Id(true, []string{ROL_ADMINISTRADOR}))
	server.DELETE("/API/v2/productos/:productoid", s.Handle_Products_Delete(true, []string{ROL_ADMINISTRADOR}))
	server.GET("/API/v2/productos/masvendidos", s.Handle_Products_MasVendidos(true, []string{ROL_ADMINISTRADOR, ROL_APLICACION}))
	server.POST("/API/v2/productos/syncsubscripciones", s.Handle_Products_Syncsubscripciones(true, []string{ROL_ADMINISTRADOR}))

	server.POST("/API/v2/proveedores", s.Handle_Products_Proveedores_Post(true, []string{ROL_ADMINISTRADOR}))
	server.PUT("/API/v2/proveedores/:proveedorid", s.Handle_Products_Proveedores_Put(true, []string{ROL_ADMINISTRADOR}))
	server.GET("/API/v2/proveedores", s.Handle_Products_Proveedores_Get(true, []string{ROL_ADMINISTRADOR}))
	server.GET("/API/v2/proveedores/:proveedorid", s.Handle_Products_Proveedores_Get_By_Id(true, []string{ROL_ADMINISTRADOR}))
	server.DELETE("/API/v2/proveedores/:proveedorid", s.Handle_Products_Proveedores_Delete(true, []string{ROL_ADMINISTRADOR}))

	server.POST("/API/v2/compras", s.Handle_Products_Compras_Post(true, []string{ROL_ADMINISTRADOR}))
	server.GET("/API/v2/compras", s.Handle_Products_Compras_Get(true, []string{ROL_ADMINISTRADOR}))
	server.GET("/API/v2/compras/:compraid", s.Handle_Products_Compras_Get_By_Id(true, []string{ROL_ADMINISTRADOR}))
	server.GET("/API/v2/compras/setup", s.Handle_Products_Compras_Get_Setup(true, []string{ROL_ADMINISTRADOR}))
	server.GET("/API/v2/compras/proveedor/:proveedorid", s.Handle_Products_Compras_Proveedor_Get_By_Id(true, []string{ROL_ADMINISTRADOR, ROL_APLICACION}))

	server.POST("/API/v2/ventas", s.Handle_Products_Ventas_Post(true, []string{ROL_ADMINISTRADOR, ROL_EMPLEADO}))
	server.GET("/API/v2/ventas", s.Handle_Products_Ventas_Get(true, []string{ROL_ADMINISTRADOR, ROL_EMPLEADO}))
	server.GET("/API/v2/ventas/:ventaid", s.Handle_Products_Ventas_Get_By_Id(true, []string{ROL_ADMINISTRADOR, ROL_EMPLEADO}))

	server.POST("/API/v2/ventasprogramadas", s.Handle_Products_Ventasprogramadas_Post(true, []string{ROL_ADMINISTRADOR, ROL_EMPLEADO}))
	server.GET("/API/v2/ventasprogramadas", s.Handle_Products_Ventasprogramadas_Get(true, []string{ROL_ADMINISTRADOR}))

	server.POST("/API/v2/reportes/comprasyventas", s.Handle_Products_Reporte_ComprasYVentas_Post(true, []string{ROL_ADMINISTRADOR, ROL_EMPLEADO}))
}

func (s *HttpServer) Handle_Products_Health(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		var health modelsout.Health
		err := services.NewAuthenticationService(s.productsServiceAddress).GetHealth(&health)

		if err == nil {
			c.JSON(200, health)
		} else {
			c.JSON(400, err.Error())
		}
	}
}

func (s *HttpServer) Handle_Products_Post(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		jsonDataBytes := GetBody(c)

		route := route_products

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.productsServiceAddress).Post(route, jsonDataBytes, headers)

		var obj modelsout.Producto

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Products_Put(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		jsonDataBytes := GetBody(c)

		route := AddUriParamIfPresent(c, route_products, "productoid")

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.productsServiceAddress).Put(route, jsonDataBytes, headers)

		var obj modelsout.Producto

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Products_Get_By_Id(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		route := AddUriParamIfPresent(c, route_products, "productoid")

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.productsServiceAddress).Get(route, headers)

		var obj modelsout.Producto

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Products_Get(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		route := route_products

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.productsServiceAddress).Get(route, headers)

		var obj []modelsout.Producto

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Products_Delete(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		route := AddUriParamIfPresent(c, route_products, "productoid")

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.productsServiceAddress).Delete(route, headers)

		RespondNoBodyOnSucces(c, err, statusCode, body)
	}
}

func (s *HttpServer) Handle_Products_MasVendidos(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		route := route_products_masvendidos

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.productsServiceAddress).Get(route, headers)

		var obj []modelsout.Producto

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Products_Syncsubscripciones(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		route := route_products_syncsubscripciones

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.productsServiceAddress).Post(route, []byte{}, headers)

		RespondNoBodyOnSucces(c, err, statusCode, body)
	}
}

func (s *HttpServer) Handle_Products_Proveedores_Post(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		jsonDataBytes := GetBody(c)

		route := route_products_proveedores

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.productsServiceAddress).Post(route, jsonDataBytes, headers)

		var obj modelsout.Producto

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Products_Proveedores_Put(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		jsonDataBytes := GetBody(c)

		route := AddUriParamIfPresent(c, route_products_proveedores, "proveedorid")

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.productsServiceAddress).Put(route, jsonDataBytes, headers)

		var obj modelsout.Proveedor

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Products_Proveedores_Get(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		route := route_products_proveedores

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.productsServiceAddress).Get(route, headers)

		var obj []modelsout.Proveedor

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Products_Proveedores_Get_By_Id(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		route := AddUriParamIfPresent(c, route_products_proveedores, "proveedorid")

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.productsServiceAddress).Get(route, headers)

		var obj modelsout.Proveedor

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Products_Proveedores_Delete(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		route := AddUriParamIfPresent(c, route_products_proveedores, "proveedorid")

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.productsServiceAddress).Delete(route, headers)

		RespondNoBodyOnSucces(c, err, statusCode, body)
	}
}

func (s *HttpServer) Handle_Products_Compras_Post(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		jsonDataBytes := GetBody(c)

		route := route_products_compras

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.productsServiceAddress).Post(route, jsonDataBytes, headers)

		var obj modelsout.Compra

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Products_Compras_Get(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		route := route_products_compras

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.productsServiceAddress).Get(route, headers)

		var obj []modelsout.Compra

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Products_Compras_Get_By_Id(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		route := AddUriParamIfPresent(c, route_products_compras, "compraid")

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.productsServiceAddress).Get(route, headers)

		var obj modelsout.Compra

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Products_Compras_Get_Setup(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		route := route_products_compras_setup

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.productsServiceAddress).Get(route, headers)

		var obj modelsout.CompraSetUp

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Products_Compras_Proveedor_Get_By_Id(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		route := AddUriParamIfPresent(c, route_products_compras_proveedor, "proveedorid")

		routeWithQuery := AddQueryParamsIfPresent(c, route, []string{"fechaDesde", "fechaHasta"})

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.productsServiceAddress).Get(routeWithQuery, headers)

		var obj []modelsout.Compra

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Products_Ventas_Post(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		jsonDataBytes := GetBody(c)

		route := route_products_ventas

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.productsServiceAddress).Post(route, jsonDataBytes, headers)

		var obj modelsout.Venta

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Products_Ventas_Get(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		route := route_products_ventas

		routeWithQuerys := AddQueryParamsIfPresent(c, route, []string{
			"fechaDesde",
			"fechaHasta",
			"indicePagina",
			"cantidadPorPagina"})

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.productsServiceAddress).Get(routeWithQuerys, headers)

		var obj []modelsout.Venta

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Products_Ventas_Get_By_Id(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		route := AddUriParamIfPresent(c, route_products_ventas, "ventaid")

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.productsServiceAddress).Get(route, headers)

		var obj modelsout.Venta

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Products_Ventasprogramadas_Post(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		jsonDataBytes := GetBody(c)

		route := route_products_ventasprogramadas

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.productsServiceAddress).Post(route, jsonDataBytes, headers)

		var obj modelsout.Venta

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Products_Ventasprogramadas_Get(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		route := route_products_ventasprogramadas

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.productsServiceAddress).Get(route, headers)

		var obj []modelsout.Venta

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Products_Reporte_ComprasYVentas_Post(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		route := route_products_reporte_comprasyventas

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.productsServiceAddress).Post(route, []byte{}, headers)

		RespondNoBodyOnSucces(c, err, statusCode, body)
	}
}
