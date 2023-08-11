import { inject } from "@angular/core";
import { ApiRequestService } from "../customServices/api-request.service";
import { Router } from "@angular/router";

export const AdminGuard = () => {
    const apiService = inject(ApiRequestService);
    const router = inject(Router);

    let esAdmin: boolean = apiService.IsUserAdmin();

    if(!esAdmin){
        router.navigate(["/inicio"]);
    }
    
    return esAdmin;
}