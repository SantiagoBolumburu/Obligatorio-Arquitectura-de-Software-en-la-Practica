package services

import (
	"encoding/json"
	"net/http"
)

type EmailService struct {
	serviceAddress string
}

const email_health string = "health"

func NewEmailService(address string) *EmailService {
	return &EmailService{
		serviceAddress: address,
	}
}

func (s *EmailService) GetHealth(target interface{}) error {
	resp, err := http.Get(s.serviceAddress + email_health)
	if err != nil {
		return err
	}
	defer resp.Body.Close()

	return json.NewDecoder(resp.Body).Decode(target)
}
