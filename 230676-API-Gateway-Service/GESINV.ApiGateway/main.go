package main

import (
	"fmt"
	"gesinvapigateway/api"
	"gesinvapigateway/utils"
	"log"
	"net/http"
	"os"
)

//GESINV_PORT_APIGATEWAYSERVICE
//GESINV_URL_HTTP_API_AUTHENTICATIONSERVICE
//GESINV_URL_HTTP_API_EMAILSERVICE
//GESINV_URL_HTTP_API_PRODUCTSSERVICE
//GESINV_URL_HTTP_API_SUBSCRIPTIONSSERVICE

func main() {
	var port_number string = "5000" //os.Getenv("GESINV_PORT_APIGATEWAYSERVICE")
	var addr_auth_service string = os.Getenv(utils.GetEnvVar_GESINV_URL_HTTP_API_AUTHENTICATIONSERVICE())
	var addr_email_service string = os.Getenv(utils.GetEnvVar_GESINV_URL_HTTP_API_EMAILSERVICE())
	var addr_porducts_service string = os.Getenv(utils.GetEnvVar_GESINV_URL_HTTP_API_PRODUCTSSERVICE())
	var addr_subscriptions_service string = os.Getenv(utils.GetEnvVar_GESINV_URL_HTTP_API_SUBSCRIPTIONSSERVICE())

	server := api.NewHttpServer(
		":"+port_number,
		addr_auth_service,
		addr_email_service,
		addr_porducts_service,
		addr_subscriptions_service,
	)

	log.Fatal(server.Start())
}

func respond(w http.ResponseWriter, body string, status int) {
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(status)
	w.Write(
		[]byte(
			fmt.Sprintf(body)))
}
