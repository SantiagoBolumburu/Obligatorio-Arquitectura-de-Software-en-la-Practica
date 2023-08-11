package api

import (
	"gesinvapigateway/modelsout"
	"gesinvapigateway/services"

	"github.com/gin-gonic/gin"
)

const route_subscriptions = "suscripciones"
const route_subscriptions_eventos = "eventos"
const route_subscriptions_health = "health"
const route_subscriptions_productos = "productos"
const route_subscriptions_compraventa = "suscripciones/compraventa"
const route_subscriptions_stock = "suscripciones/stock"

func (s *HttpServer) SetSubscriptionsServiceEndpoints(server *gin.Engine) {

	server.GET("/API/v2/health/suscripciones", s.Handle_Subscriptions_Health(false, []string{}))

	server.POST("/API/v2/eventos", s.Handle_Subscriptions_Eventos_Post(false, []string{ROL_ADMINISTRADOR, ROL_EMPLEADO, ROL_INTERNAL_APLICACION}))

	server.POST("/API/v2/suscripciones/productos", s.Handle_Subscriptions_Productos_Post(true, []string{ROL_ADMINISTRADOR, ROL_INTERNAL_APLICACION}))

	server.GET("/API/v2/suscripciones", s.Handle_Subscriptions_Get(true, []string{ROL_ADMINISTRADOR}))
	server.POST("/API/v2/suscripciones/compraventa/:productoid", s.Handle_Subscriptions_Compraventa_Post_By_ProductoId(true, []string{ROL_ADMINISTRADOR}))
	server.DELETE("/API/v2/suscripciones/compraventa/:productoid", s.Handle_Subscriptions_Compraventa_Delete_By_ProductoId(true, []string{ROL_ADMINISTRADOR}))
	server.GET("/API/v2/suscripciones/compraventa", s.Handle_Subscriptions_Compraventa_Get(true, []string{ROL_ADMINISTRADOR}))
	server.POST("/API/v2/suscripciones/stock/:productoid", s.Handle_Subscriptions_Stock_Post_By_ProductoId(true, []string{ROL_ADMINISTRADOR}))
	server.DELETE("/API/v2/suscripciones/stock/:productoid", s.Handle_Subscriptions_Stock_Delete_By_ProductoId(true, []string{ROL_ADMINISTRADOR}))
	server.GET("/API/v2/suscripciones/stock", s.Handle_Subscriptions_Stock_Get(true, []string{ROL_ADMINISTRADOR}))
}

func (s *HttpServer) Handle_Subscriptions_Health(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		var health modelsout.Health
		err := services.NewAuthenticationService(s.subscriptionsServiceAddress).GetHealth(&health)

		if err == nil {
			c.JSON(200, health)
		} else {
			c.JSON(400, err.Error())
		}
	}
}

func (s *HttpServer) Handle_Subscriptions_Eventos_Post(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		jsonDataBytes := GetBody(c)

		route := route_subscriptions_eventos

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.subscriptionsServiceAddress).Post(route, jsonDataBytes, headers)

		RespondNoBodyOnSucces(c, err, statusCode, body)
	}
}

func (s *HttpServer) Handle_Subscriptions_Productos_Post(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		jsonDataBytes := GetBody(c)

		route := route_subscriptions_productos

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.subscriptionsServiceAddress).Post(route, jsonDataBytes, headers)

		var obj modelsout.SuscripcionProducto

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Subscriptions_Get(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		route := route_subscriptions

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.subscriptionsServiceAddress).Get(route, headers)

		var obj modelsout.Suscripciones

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Subscriptions_Compraventa_Post_By_ProductoId(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		routeRaw := route_subscriptions_compraventa
		route := AddUriParamIfPresent(c, routeRaw, "productoid")

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.subscriptionsServiceAddress).Post(route, []byte{}, headers)

		var obj modelsout.Suscripcion

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Subscriptions_Compraventa_Delete_By_ProductoId(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		routeRaw := route_subscriptions_compraventa
		route := AddUriParamIfPresent(c, routeRaw, "productoid")

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.subscriptionsServiceAddress).Delete(route, headers)

		RespondNoBodyOnSucces(c, err, statusCode, body)
	}
}

func (s *HttpServer) Handle_Subscriptions_Compraventa_Get(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		route := route_subscriptions_compraventa

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.subscriptionsServiceAddress).Get(route, headers)

		var obj []modelsout.Suscripcion

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Subscriptions_Stock_Post_By_ProductoId(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		routeRaw := route_subscriptions_stock
		route := AddUriParamIfPresent(c, routeRaw, "productoid")

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.subscriptionsServiceAddress).Post(route, []byte{}, headers)

		var obj modelsout.Suscripcion

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Subscriptions_Stock_Delete_By_ProductoId(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		routeRaw := route_subscriptions_stock
		route := AddUriParamIfPresent(c, routeRaw, "productoid")

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.subscriptionsServiceAddress).Delete(route, headers)

		RespondNoBodyOnSucces(c, err, statusCode, body)
	}
}

func (s *HttpServer) Handle_Subscriptions_Stock_Get(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		route := route_subscriptions_stock

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.subscriptionsServiceAddress).Get(route, headers)

		var obj []modelsout.Suscripcion

		Respond(c, err, statusCode, &obj, body)
	}
}

/*
GET: /health
POST: /evento
POST: /productos

GET: /suscripciones
POST: /suscripciones/compraventa/:productoid
DELETE: /suscripciones/compraventa/:productoid
GET: / suscripciones/compraventa
POST: /suscripciones/stock/:productoid
DELETE: /suscripciones/stock/:productoid
GET: / suscripciones/stock




*/
