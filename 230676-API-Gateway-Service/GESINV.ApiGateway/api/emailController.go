package api

import (
	"gesinvapigateway/modelsout"
	"gesinvapigateway/services"

	"github.com/gin-gonic/gin"
)

const route_emails = "emails"

func (s *HttpServer) SetEmailServiceEndpoints(server *gin.Engine) {

	server.GET("/API/v2/health/email", s.Handle_Emails_Health(false, []string{}))

	server.POST("/API/v2/emails", s.Handle_Emails_Post(false, []string{}))
}

func (s *HttpServer) Handle_Emails_Health(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		var health modelsout.Health
		err := services.NewAuthenticationService(s.emailServiceAddress).GetHealth(&health)

		if err == nil {
			c.JSON(200, health)
		} else {
			c.JSON(400, err.Error())
		}
	}
}

func (s *HttpServer) Handle_Emails_Post(requiresSession bool, allowedRoles []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		if !CheckForValidRole(allowedRoles, c) || !CheckForValidSession(requiresSession, c, s) {
			return
		}

		jsonDataBytes := GetBody(c)

		route := route_emails

		headers := GetHeaders(c)

		err, body, statusCode := services.NewHttpRequester(
			s.emailServiceAddress).Post(route, jsonDataBytes, headers)

		RespondNoBodyOnSucces(c, err, statusCode, body)
	}
}
