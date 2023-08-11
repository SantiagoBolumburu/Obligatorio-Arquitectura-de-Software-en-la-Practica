package services

import (
	"bytes"
	"io/ioutil"
	"net/http"
)

type HttpRequester struct {
	serviceAddress string
}

const content_type string = "application/json"

func NewHttpRequester(address string) *HttpRequester {
	return &HttpRequester{
		serviceAddress: address,
	}
}

func (r *HttpRequester) Post(route string, body []byte, headers []CustomHeaders) (error, []byte, int) {
	req, reqErr := http.NewRequest("POST", r.serviceAddress+route, bytes.NewBuffer(body))
	if reqErr != nil {
		zeroArr := [0]byte{}
		return reqErr, zeroArr[:], 0
	}

	req.Header.Add("Content-Type", "application/json")
	if len(headers) > 0 {
		for _, header := range headers {
			req.Header.Set(header.Name, header.Value)
		}
	}

	client := &http.Client{}
	resp, err := client.Do(req)

	if err != nil {
		zeroArr := [1]byte{0}
		return err, zeroArr[:], 0
	}
	defer resp.Body.Close()

	jsonDataBytes, err := ioutil.ReadAll(resp.Body)

	return nil, jsonDataBytes, resp.StatusCode
}

func (r *HttpRequester) Put(route string, body []byte, headers []CustomHeaders) (error, []byte, int) {
	req, reqErr := http.NewRequest("PUT", r.serviceAddress+route, bytes.NewBuffer(body))
	if reqErr != nil {
		zeroArr := [0]byte{}
		return reqErr, zeroArr[:], 0
	}

	req.Header.Add("Content-Type", "application/json")
	if len(headers) > 0 {
		for _, header := range headers {
			req.Header.Set(header.Name, header.Value)
		}
	}

	client := &http.Client{}
	resp, err := client.Do(req)

	if err != nil {
		zeroArr := [1]byte{0}
		return err, zeroArr[:], 0
	}
	defer resp.Body.Close()

	jsonDataBytes, err := ioutil.ReadAll(resp.Body)

	return nil, jsonDataBytes, resp.StatusCode
}

func (r *HttpRequester) Get(route string, headers []CustomHeaders) (error, []byte, int) {
	req, reqErr := http.NewRequest("GET", r.serviceAddress+route, nil)
	if reqErr != nil {
		zeroArr := [0]byte{}
		return reqErr, zeroArr[:], 0
	}

	req.Header.Add("Content-Type", "application/json")
	if len(headers) > 0 {
		for _, header := range headers {
			req.Header.Set(header.Name, header.Value)
		}
	}

	client := &http.Client{}
	resp, err := client.Do(req)

	if err != nil {
		zeroArr := [1]byte{0}
		return err, zeroArr[:], 0
	}
	defer resp.Body.Close()

	jsonDataBytes, err := ioutil.ReadAll(resp.Body)

	return nil, jsonDataBytes, resp.StatusCode
}

func (r *HttpRequester) Delete(route string, headers []CustomHeaders) (error, []byte, int) {
	req, reqErr := http.NewRequest("DELETE", r.serviceAddress+route, nil)
	if reqErr != nil {
		zeroArr := [0]byte{}
		return reqErr, zeroArr[:], 0
	}

	req.Header.Add("Content-Type", "application/json")
	if len(headers) > 0 {
		for _, header := range headers {
			req.Header.Set(header.Name, header.Value)
		}
	}

	client := &http.Client{}
	resp, err := client.Do(req)

	if err != nil {
		zeroArr := [1]byte{0}
		return err, zeroArr[:], 0
	}
	defer resp.Body.Close()

	jsonDataBytes, err := ioutil.ReadAll(resp.Body)

	return nil, jsonDataBytes, resp.StatusCode
}

/*
	bytes, err := ioutil.ReadAll(resp.Body)
	fmt.Println("HOLAAAAAAAAAAAA")
	fmt.Println(resp.Body)
	fmt.Println(string(bytes))
*/
