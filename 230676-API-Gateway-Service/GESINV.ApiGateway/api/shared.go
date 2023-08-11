package api

import (
	"encoding/json"
	"gesinvapigateway/services"
	"gesinvapigateway/utils"
	"io/ioutil"
	"net/http"
	"strings"

	"github.com/gin-gonic/gin"
)

const http_header_auth = "Authorization"
const http_header_appKey = "API_KEY"

func Respond[T any](c *gin.Context, err error, statusCode int, tipo *T, body []byte) {
	if err == nil {
		if statusCode >= 200 && statusCode <= 299 {
			json.Unmarshal(body, &tipo)
			c.JSON(statusCode, tipo)
		} else {
			c.String(statusCode, string(body))
		}
	} else {
		c.JSON(400, err.Error())
	}
}

func RespondNoBodyOnSucces(c *gin.Context, err error, statusCode int, body []byte) {
	if err == nil {
		if statusCode >= 200 && statusCode <= 299 {
			c.Status(statusCode)
		} else {
			c.String(statusCode, string(body))
		}
	} else {
		c.JSON(500, err.Error())
	}
}

func RespondBodyFromPlainText(c *gin.Context, err error, statusCode int, body []byte) {
	if err == nil {
		if statusCode >= 200 && statusCode <= 299 {
			c.JSON(statusCode, string(body))
		} else {
			c.String(statusCode, string(body))
		}
	} else {
		c.JSON(400, err.Error())
	}
}

func GetHeaders(c *gin.Context) []services.CustomHeaders {
	headers := services.NewZeroLength_CustomHeaders_Array()

	if len(c.Request.Header["Authorization"]) > 0 {
		headers = append(headers, services.CustomHeaders{
			Name:  "Authorization",
			Value: c.Request.Header["Authorization"][0],
		})
	}

	if len(c.Request.Header["Api_key"]) > 0 {
		headers = append(headers, services.CustomHeaders{
			Name:  "API_KEY",
			Value: c.Request.Header["Api_key"][0],
		})
	}

	return headers
}

func GetBody(c *gin.Context) []byte {
	jsonDataBytes, err := ioutil.ReadAll(c.Request.Body)
	if err != nil {
		c.JSON(400, err.Error())
	}

	return jsonDataBytes
}

func AddUriParamIfPresent(c *gin.Context, baseUri string, paramToSearch string) string {
	route := baseUri
	if c.Param(paramToSearch) != "" {
		route = route + "/" + c.Param(paramToSearch)
	}

	return route
}

func AddQueryParamsIfPresent(c *gin.Context, baseUri string, paramsToSearch []string) string {
	route := baseUri
	first := true

	for _, param := range paramsToSearch {
		if c.Query(param) != "" {
			if first {
				route = route + "?" + param + "=" + c.Query(param)
				first = false
			} else {
				route = route + "&" + param + "=" + c.Query(param)
			}
		}
	}

	return route
}

func CheckForValidRole(allowedRoles []string, c *gin.Context) bool {
	if len(allowedRoles) == 0 {
		return true
	}
	headers := GetHeaders(c)

	if len(headers) < 1 {
		RespondBodyFromPlainText(c, nil, http.StatusUnauthorized, []byte("Token ausente/no valido"))
		return false
	}

	tokenValido, rol := utils.GetClaimAndValidateToken(noBearer(headers[0].Value), "Rol")

	if !tokenValido {
		RespondBodyFromPlainText(c, nil, http.StatusUnauthorized, []byte("Token ausente/no valido"))
		return false
	}

	if !contains(allowedRoles, rol) {
		RespondBodyFromPlainText(c, nil, http.StatusUnauthorized, []byte("No cuenta con el rol nesesario para realizar la operacion."))
		return false
	}

	return true
}

func CheckForValidSession(requiresSession bool, c *gin.Context, s *HttpServer) bool {
	if !requiresSession {
		return true
	}

	headers := GetHeaders(c)

	err, body, statusCode := services.NewHttpRequester(
		s.authServiceAddress).Get(route_auth_sessions_health, headers)

	if statusCode > 299 || statusCode < 200 {
		RespondBodyFromPlainText(c, err, statusCode, body)
		return false
	}
	return true
}

func contains(s []string, str string) bool {
	for _, v := range s {
		if v == str {
			return true
		}
	}

	return false
}

func noBearer(token string) string {
	return strings.ReplaceAll(token, "Bearer ", "")
}
