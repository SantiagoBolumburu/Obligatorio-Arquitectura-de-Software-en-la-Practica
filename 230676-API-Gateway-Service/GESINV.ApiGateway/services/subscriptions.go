package services

import (
	"encoding/json"
	"net/http"
)

type SubscriptionsService struct {
	serviceAddress string
}

const subscriptions_health string = "health"

func NewSubscriptionsService(address string) *SubscriptionsService {
	return &SubscriptionsService{
		serviceAddress: address,
	}
}

func (s *SubscriptionsService) GetHealth(target interface{}) error {
	resp, err := http.Get(s.serviceAddress + subscriptions_health)
	if err != nil {
		return err
	}
	defer resp.Body.Close()

	return json.NewDecoder(resp.Body).Decode(target)
}
