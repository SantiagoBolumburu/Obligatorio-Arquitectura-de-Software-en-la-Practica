package modelsout

type Suscripciones struct {
	CompraVentaSubscriptions []Suscripcion `json:"compraVentaSubscriptions"`
	StockSubscription        []Suscripcion `json:"stockSubscription"`
}
