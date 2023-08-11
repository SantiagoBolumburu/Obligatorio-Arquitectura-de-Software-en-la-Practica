package services

import (
	"encoding/json"
	"net/http"
)

type AuthenticationService struct {
	serviceAddress string
}

// const content_type string = "application/json"
const auth_health string = "health"

func NewAuthenticationService(address string) *AuthenticationService {
	return &AuthenticationService{
		serviceAddress: address,
	}
}

func (s *AuthenticationService) GetHealth(target interface{}) error {
	resp, err := http.Get(s.serviceAddress + auth_health)
	if err != nil {
		return err
	}
	defer resp.Body.Close()

	return json.NewDecoder(resp.Body).Decode(target)
}

/*
func (s *AuthenticationService) Post(target interface{}) error {
	resp, err := http.Post(s.serviceAddress + auth_health)
	if err != nil {
		return err
	}
	defer resp.Body.Close()

	return json.NewDecoder(resp.Body).Decode(target)
}
*/
/*
server.GET("/API/v2/health/auth", s.HandleAuthHealth)
server.POST("/API/v2/usuarios", s.HandleAuthUsuarios)
//POST: /usuarios ? invitacionId=aaa
server.POST("/API/v2/invitaciones", s.GinHandleHealth)
server.GET("/API/v2/invitaciones/:invitacionid", s.GinHandleHealth)

server.POST("/API/v2/sessions", s.GinHandleHealth)
server.GET("/API/v2/sessions/health", s.GinHandleHealth)
server.DELETE("/API/v2/sessions", s.GinHandleHealth)
server.POST("/API/v2/appkey", s.GinHandleHealth)
server.GET("/API/v2/appkey", s.GinHandleHealth)
server.DELETE("/API/v2/appkey", s.GinHandleHealth)
*/
