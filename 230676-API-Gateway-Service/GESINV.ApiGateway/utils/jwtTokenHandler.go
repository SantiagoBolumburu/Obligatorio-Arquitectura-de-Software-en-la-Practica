package utils

import (
	"fmt"
	"os"

	"github.com/golang-jwt/jwt"
)

func GetClaimAndValidateToken(tokenStr string, claim string) (bool, string) {
	var secretJwtKey = []byte(os.Getenv(GetEnvVar_GESINV_JWT_SECRET()))

	// SecurityAlgorithms.HmacSha256Signature
	token, err := jwt.Parse(tokenStr, func(token *jwt.Token) (interface{}, error) {
		if _, ok := token.Method.(*jwt.SigningMethodHMAC); !ok {
			return nil, fmt.Errorf("unexpected signing method: %v", token.Header["alg"])
		}
		return secretJwtKey, nil
	})

	if err != nil && token == nil {
		panic(err)
	}

	var success bool = false
	var claimValue string = ""

	if claims, ok := token.Claims.(jwt.MapClaims); ok {
		if tokenClaim, ok := claims[claim].(string); ok {
			claimValue = tokenClaim
			success = true
		} else {
			success = true
		}
	} else {
		success = false
	}

	return success, claimValue
}
