package services

type CustomHeaders struct {
	Name  string
	Value string
}

func NewZeroLength_CustomHeaders_Array() []CustomHeaders {
	//	var array = []CustomHeaders{}
	var array []CustomHeaders
	return array
}

func NewHeader(name string, value string) CustomHeaders {
	return CustomHeaders{
		Name:  name,
		Value: value,
	}
}
