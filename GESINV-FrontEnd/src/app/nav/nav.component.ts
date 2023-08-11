import { Component, OnInit, ViewChild, inject } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { ApiRequestService } from '../customServices/api-request.service';
import { MatButton } from '@angular/material/button';
import { SelectComponent } from '../basic/select/select.component';
import { Router } from '@angular/router';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit{
  private apiService:ApiRequestService = inject(ApiRequestService)
  private router:Router = inject(Router)

  estaLoggeado = false
  esAdmin = false


  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  constructor(private breakpointObserver: BreakpointObserver) {

  }

  ngOnInit(): void {
    this.apiService.Observable_JWT_CLAIMS$.subscribe(claims => {
      this.estaLoggeado = this.apiService.IsUserLoggedIn();
      this.esAdmin = this.apiService.IsUserAdmin();
      console.log(this.esAdmin + " " + this.estaLoggeado)
    });
  }

  rutas = {
    signin : "/singin",
    login : "/login",
    gestionInventario : "/gestion/inventario",
    gestionProducto : "/gestion/productos",
    gestionProveedores : "/gestion/proveedores",
    invitarUsaurio : "/user/invite",
    inicio : "inicio",
    registroCompras : "/registro/compras",
    registroVentas : "/registro/ventas",
    appkey : "/appkey",
    suscripciones : "/suscripciones",
    reportes : "/reportes"
  }

  EsAdmin(){
    return this.apiService.IsUserAdmin();
  }

  EstaLogeado(){
    return this.apiService.IsUserLoggedIn();
  }

  RedirigiarA(path:string){
    this.router.navigate([path]);
  }

  CerrarSession(){
    this.apiService.Delete_Session().subscribe({
      next: (value) => 
      {
        this.apiService.RemoveToken();
        this.RedirigiarA(this.rutas.login);
        this.ManejarLoggout();
      },
      error: (value:ErrorEvent) => {},
      complete: () => console.info('complete') 
    });
  }

  private ManejarLoggout(){

  }

}
