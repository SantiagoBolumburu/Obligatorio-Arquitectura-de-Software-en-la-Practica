package services

import (
	"encoding/json"
	"net/http"
)

type ProductsService struct {
	serviceAddress string
}

const products_health string = "health"

func NewProductsService(address string) *ProductsService {
	return &ProductsService{
		serviceAddress: address,
	}
}

func (s *ProductsService) GetHealth(target interface{}) error {
	resp, err := http.Get(s.serviceAddress + products_health)
	if err != nil {
		return err
	}
	defer resp.Body.Close()

	return json.NewDecoder(resp.Body).Decode(target)
}
