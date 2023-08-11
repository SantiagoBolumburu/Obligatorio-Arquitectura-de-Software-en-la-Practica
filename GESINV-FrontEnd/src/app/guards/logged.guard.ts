import { inject } from "@angular/core";
import { ApiRequestService } from "../customServices/api-request.service";
import { Router } from "@angular/router";

export const LoggedGuard = () => {
    const apiService = inject(ApiRequestService);
    const router = inject(Router);

    let estaLoggeado: boolean = apiService.IsUserLoggedIn();

    if(!estaLoggeado){
        router.navigate(["/login"]);
    }

    return estaLoggeado;
}