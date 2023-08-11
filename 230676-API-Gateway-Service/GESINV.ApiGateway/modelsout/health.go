package modelsout

import "fmt"

type Health struct {
	EnvVars                        string `json:"envVars"`
	BackingService_DataBase        string `json:"backingService_DataBase"`
	BackingService_EmailService    string `json:"backingService_EmailService"`
	BackingService_KeyValueStorage string `json:"backingService_KeyValueStorage"`
	BackingService_EventPusblisher string `json:"backingService_EventPusblisher"`
}

func (h *Health) GetHealthReport() string {
	var count int = 0
	var countOk int = 0

	if h.EnvVars != "" {
		count++
		if h.EnvVars == "Ok" {
			countOk++
		}
	}

	if h.BackingService_DataBase != "" {
		count++
		if h.BackingService_DataBase == "Ok" {
			countOk++
		}
	}

	if h.BackingService_EmailService != "" {
		count++
		if h.BackingService_EmailService == "Ok" {
			countOk++
		}
	}

	if h.BackingService_KeyValueStorage != "" {
		count++
		if h.BackingService_KeyValueStorage == "Ok" {
			countOk++
		}
	}

	if h.BackingService_EventPusblisher != "" {
		count++
		if h.BackingService_EventPusblisher == "Ok" {
			countOk++
		}
	}

	if count == countOk {
		return "Ok"
	} else {
		return fmt.Sprint(countOk) + " Ok out of " + fmt.Sprint(count)
	}
}
