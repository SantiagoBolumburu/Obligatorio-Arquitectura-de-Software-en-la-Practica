package api

import (
	"gesinvapigateway/modelsin"
	"gesinvapigateway/modelsout"
	"gesinvapigateway/services"

	"github.com/gin-gonic/gin"
)

const route_auth_usuarios = "usuarios"
const route_auth_invitaciones = "invitaciones"
const route_auth_sessions = "sessions"
const route_auth_sessions_health = "sessions/health"
const route_auth_appkey = "appkey"

func (s *HttpServer) SetAuthServiceEndpoints(server *gin.Engine) {

	server.GET("/API/v2/health/auth", s.Handle_Auth_Health(false, []string{}))

	server.POST("/API/v2/usuarios", s.Handle_Auth_Usuarios_Post(false, []string{})) //POST: /usuarios ? invitacionId=aaa

	server.POST("/API/v2/invitaciones", s.Handle_Auth_Invitaciones_Post(true, []string{ROL_ADMINISTRADOR}))
	server.GET("/API/v2/invitaciones/:invitacionid", s.Handle_Auth_Invitaciones_Get(false, []string{}))

	server.POST("/API/v2/sessions", s.Handle_Auth_Sessions_Post(false, []string{}))
	server.GET("/API/v2/sessions/health", s.Handle_Auth_Sessions_Get_Health(false, []string{}))
	server.DELETE("/API/v2/sessions", s.Handle_Auth_Sessions_Delete(false, []string{}))

	server.POST("/API/v2/sessions/appkey", s.Handle_Auth_AppKey_Post(true, []string{ROL_ADMINISTRADOR}))
	server.GET("/API/v2/sessions/appkey", s.Handle_Auth_AppKey_Get(true, []string{ROL_ADMINISTRADOR}))
	server.DELETE("/API/v2/sessions/appkey", s.Handle_Auth_AppKey_Delete(true, []string{ROL_ADMINISTRADOR}))
}

func (s *HttpServer) Handle_Auth_Health(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		var health modelsout.Health
		err := services.NewAuthenticationService(s.authServiceAddress).GetHealth(&health)

		if err == nil {
			c.JSON(200, health)
		} else {
			c.JSON(400, err.Error())
		}
	}
}

func (s *HttpServer) Handle_Auth_Usuarios_Post(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		jsonDataBytes := GetBody(c)

		route := AddQueryParamsIfPresent(c, route_auth_usuarios, []string{"invitacionid"})

		noHeaders := services.NewZeroLength_CustomHeaders_Array()

		err, body, statusCode := services.NewHttpRequester(
			s.authServiceAddress).Post(route, jsonDataBytes, noHeaders)

		var obj modelsin.Usuario

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Auth_Invitaciones_Post(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		jsonDataBytes := GetBody(c)

		headers := GetHeaders(c)

		route := route_auth_invitaciones

		err, body, statusCode := services.NewHttpRequester(
			s.authServiceAddress).Post(route, jsonDataBytes, headers)

		var obj modelsout.Invitacion

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Auth_Invitaciones_Get(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		headers := GetHeaders(c)

		route := AddUriParamIfPresent(c, route_auth_invitaciones, "invitacionid")

		err, body, statusCode := services.NewHttpRequester(
			s.authServiceAddress).Get(route, headers)

		var obj modelsout.Invitacion

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Auth_Sessions_Post(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		jsonDataBytes := GetBody(c)

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.authServiceAddress).Post(route_auth_sessions, jsonDataBytes, headers)

		var obj modelsout.TokenObj

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Auth_Sessions_Get_Health(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.authServiceAddress).Get(route_auth_sessions_health, headers)

		RespondNoBodyOnSucces(c, err, statusCode, body)
	}
}

func (s *HttpServer) Handle_Auth_Sessions_Delete(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.authServiceAddress).Delete(route_auth_sessions, headers)

		RespondNoBodyOnSucces(c, err, statusCode, body)
	}
}

func (s *HttpServer) Handle_Auth_AppKey_Post(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.authServiceAddress).Post(route_auth_appkey, []byte{}, headers)

		var obj modelsout.AppKeyObj

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Auth_AppKey_Get(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.authServiceAddress).Get(route_auth_appkey, headers)

		var obj modelsout.AppKeyObj

		Respond(c, err, statusCode, &obj, body)
	}
}

func (s *HttpServer) Handle_Auth_AppKey_Delete(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.authServiceAddress).Delete(route_auth_appkey, headers)

		RespondNoBodyOnSucces(c, err, statusCode, body)
	}
}
