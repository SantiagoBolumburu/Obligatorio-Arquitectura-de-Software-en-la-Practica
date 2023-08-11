package requests

import (
	"io"
	"net/http"
)

type ProductsRequester struct {
	serviceAddress string
}

const health string = "health"

func NewProductsRequester(address string) *ProductsRequester {
	return &ProductsRequester{
		serviceAddress: address,
	}
}

func (s *ProductsRequester) GetHealth() (string, error) {
	resp, err := http.Get(s.serviceAddress + health)
	if err != nil {
		return "", err
	}
	defer resp.Body.Close()

	body, err2 := io.ReadAll(resp.Body)

	return string(body), err2
}
