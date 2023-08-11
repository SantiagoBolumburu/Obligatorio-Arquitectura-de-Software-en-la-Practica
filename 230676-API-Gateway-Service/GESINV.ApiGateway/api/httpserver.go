package api

import (
	"encoding/json"
	"gesinvapigateway/modelsout"
	"gesinvapigateway/services"

	"github.com/gin-gonic/gin"
)

type HttpServer struct {
	listenPort                  string
	authServiceAddress          string
	emailServiceAddress         string
	productsServiceAddress      string
	subscriptionsServiceAddress string
}

type HalthReport struct {
	AuthenticationSevice string
	EmailService         string
	ProductsService      string
	SubscriptionsService string
}

func NewHttpServer(listenPort string, authAddr string, emailAddr string,
	productsAddr string, subscriptionsAddr string) *HttpServer {

	return &HttpServer{
		listenPort:                  listenPort,
		authServiceAddress:          authAddr,
		emailServiceAddress:         emailAddr,
		productsServiceAddress:      productsAddr,
		subscriptionsServiceAddress: subscriptionsAddr,
	}
}

const ROL_ADMINISTRADOR string = "administrador"
const ROL_EMPLEADO string = "empleado"
const ROL_APLICACION string = "aplicacion"
const ROL_INTERNAL_APLICACION string = "intaplicacion"

func (s *HttpServer) Start() error {
	server := gin.Default()

	server.GET("/API/v2/health", s.GinHandleHealth)

	server.Use(CORSMiddleware())

	s.SetAuthServiceEndpoints(server)
	s.SetProductsServiceEndpoints(server)
	s.SetSubscriptionsServiceEndpoints(server)
	s.SetEmailServiceEndpoints(server)

	return server.Run(s.listenPort)
}

func CORSMiddleware() gin.HandlerFunc {
	return func(c *gin.Context) {

		c.Header("Access-Control-Allow-Origin", "*")
		c.Header("Access-Control-Allow-Credentials", "true")
		c.Header("Access-Control-Allow-Headers", "*")
		c.Header("Access-Control-Allow-Methods", "POST, HEAD, PATCH, DELETE, GET, PUT")
		/*
			c.Header("Access-Control-Allow-Origin", "*")
			c.Header("Access-Control-Allow-Credentials", "true")
			c.Header("Access-Control-Allow-Headers", "Content-Type, Content-Length, Accept-Encoding, X-CSRF-Token, Authorization, accept, origin, Cache-Control, X-Requested-With")
			c.Header("Access-Control-Allow-Methods", "POST,HEAD,PATCH, DELETE, GET, PUT")
		*/

		if c.Request.Method == "OPTIONS" {
			c.AbortWithStatus(204)
			return
		}

		c.Next()

		c.Header("Access-Control-Allow-Origin", "*")
		c.Header("Access-Control-Allow-Headers", "*")
		c.Header("Access-Control-Allow-Methods", "POST,HEAD,PATCH, DELETE, GET, PUT")
	}
}

func (s *HttpServer) GinHandleHealth(c *gin.Context) {
	healthReport, err := s.CreateHahealthReport2()

	if err == nil {
		c.JSON(200, healthReport)
	} else {
		c.JSON(400, err.Error())
	}
}

func (s *HttpServer) CreateHahealthReport2() (*HalthReport, error) {
	var healthAuth modelsout.Health
	var healthEmail modelsout.Health
	var healthProd modelsout.Health
	var healthSubs modelsout.Health

	/*
		empyHeaders := services.GetZeroLength_CustomHeaders_Array()

			errAuth := services.NewHttpRequester(s.authServiceAddress).Get("health", empyHeaders)
			errProd
			errProd
			errSubs
	*/
	errAuth := services.NewAuthenticationService(s.authServiceAddress).GetHealth(&healthAuth)
	errEmail := services.NewEmailService(s.emailServiceAddress).GetHealth(&healthEmail)
	errProd := services.NewProductsService(s.productsServiceAddress).GetHealth(&healthProd)
	errSubs := services.NewSubscriptionsService(s.subscriptionsServiceAddress).GetHealth(&healthSubs)
	var reportAuth string = "Can't make contact"
	var reportEmail string = "Can't make contact"
	var reportProd string = "Can't make contact"
	var reportSubs string = "Can't make contact"

	if errAuth == nil {
		reportAuth = healthAuth.GetHealthReport()
	}
	if errEmail == nil {
		reportEmail = healthEmail.GetHealthReport()
	}
	if errProd == nil {
		reportProd = healthProd.GetHealthReport()
	}
	if errSubs == nil {
		reportSubs = healthSubs.GetHealthReport()
	}

	healthReport := &HalthReport{
		AuthenticationSevice: reportAuth,
		EmailService:         reportEmail,
		ProductsService:      reportProd,
		SubscriptionsService: reportSubs,
	}

	//healthReportJson, err := json.Marshal(healthReport)

	return healthReport, nil
}

func (s *HttpServer) CreateHahealthReport() (string, error) {
	var healthAuth modelsout.Health
	var healthEmail modelsout.Health
	var healthProd modelsout.Health
	var healthSubs modelsout.Health
	errAuth := services.NewAuthenticationService(s.authServiceAddress).GetHealth(&healthAuth)
	errEmail := services.NewEmailService(s.emailServiceAddress).GetHealth(&healthEmail)
	errProd := services.NewProductsService(s.productsServiceAddress).GetHealth(&healthProd)
	errSubs := services.NewSubscriptionsService(s.subscriptionsServiceAddress).GetHealth(&healthSubs)

	var reportAuth string = "Can't make contact"
	var reportEmail string = "Can't make contact"
	var reportProd string = "Can't make contact"
	var reportSubs string = "Can't make contact"

	if errAuth == nil {
		reportAuth = healthAuth.GetHealthReport()
	}
	if errEmail == nil {
		reportEmail = healthEmail.GetHealthReport()
	}
	if errProd == nil {
		reportProd = healthProd.GetHealthReport()
	}
	if errSubs == nil {
		reportSubs = healthSubs.GetHealthReport()
	}

	healthReport := &HalthReport{
		AuthenticationSevice: reportAuth,
		EmailService:         reportEmail,
		ProductsService:      reportProd,
		SubscriptionsService: reportSubs,
	}

	healthReportJson, err := json.Marshal(healthReport)

	return string(healthReportJson), err
}
