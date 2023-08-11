import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Credenciales } from '../models/credenciales';
import { TokenOut } from '../models/out/tokenOut';
import { BehaviorSubject, Observable, catchError, tap, throwError } from 'rxjs';
import { UsuarioIn } from '../models/in/usuarioIn';
import { UsuarioOut } from '../models/out/usuarioOut';
import { DialogServiceService } from '../basic/dialog/dialog-service.service';
import jwt_decode from 'jwt-decode';
import { JwtTokenClaimsModel } from '../models/jwtTokenClaimsModel';
import { InvitacionIn } from '../models/in/invitacionIn';
import { InvitacionOut } from '../models/out/invitacionOut';
import { ProductoIn } from '../models/in/productoIn';
import { ProductoOut } from '../models/out/productoOut';
import { ProveedorIn } from '../models/in/proveedorIn';
import { ProveedorOut } from '../models/out/proveedorOut';
import { CompraOut } from '../models/out/compraOut';
import { CompraSetupOut } from '../models/out/compraSetupOut';
import { CompraIn } from '../models/in/compraIn';
import { VentaIn } from '../models/in/ventaIn';
import { VentaOut } from '../models/out/ventaOut';
import { GetVentasParams } from '../models/inOut/getVentasParams';
import { AppKeyOut } from '../models/out/appkeyOut';
import { SubscripcionesSetupOut } from '../models/out/subscripcionesSetupOut';
import { SubscripcionOut } from '../models/out/subscripcionOut';

@Injectable({
  providedIn: 'root'
})
export class ApiRequestService{
  private JWT_TOKEN!:string;
  private JWT_CLAIMS?:JwtTokenClaimsModel;
  private dataSubject: BehaviorSubject<JwtTokenClaimsModel | undefined> = new BehaviorSubject<JwtTokenClaimsModel | undefined>(this.JWT_CLAIMS);
  public Observable_JWT_CLAIMS$ = this.dataSubject.asObservable();

  private updateClaims(newValue: JwtTokenClaimsModel | undefined): void {
    this.JWT_CLAIMS = newValue
    this.dataSubject.next(newValue);
  }

  private API_ROUTE = environment.apiURL;

  private local_storage_auth_token_key = "auth_token";
  private header_auth = "Authorization";

  private route_usuarios                   = this.API_ROUTE + "/usuarios";
  private route_sessions                   = this.API_ROUTE + "/sessions";
  private route_sessions_appkey            = this.route_sessions + "/appkey"; 
  private route_compras                    = this.API_ROUTE + "/compras";
  private route_compras_setup              = this.route_compras + "/setup";
  private route_compras_proveedor          = this.route_compras + "/proveedor";
  private route_invitaciones               = this.API_ROUTE + "/invitaciones";
  private route_productos                  = this.API_ROUTE + "/productos";
  private route_productos_masvendidos      = this.route_productos + "/masvendidos";
  private route_proveedores                = this.API_ROUTE + "/proveedores";
  private route_ventas                     = this.API_ROUTE + "/ventas";
  private route_ventas_programadas         = this.API_ROUTE + "/ventasprogramadas";
  private route_subscriptions              = this.API_ROUTE + "/suscripciones";
  private route_subscriptions_compraventa  = this.route_subscriptions + "/compraventa";
  private route_subscriptions_stock        = this.route_subscriptions + "/stock";
  private route_reportes                   = this.API_ROUTE + "/reportes";
  private route_reportes_compraventa       = this.route_reportes + "/comprasyventas";


  private httpHeader = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      //'Accept' : "*/*"
    }),
  }; 

  constructor(private http:HttpClient, private dialogService:DialogServiceService){
    this.SetTokenAndClaimsFromLocalSorage();
    
    /*
    let token :string|null = localStorage.getItem(this.local_storage_auth_token_key);

    if(token){
      this.JWT_TOKEN = token;
      this.UpdateHeader(this.JWT_TOKEN);
    }
    */
  }


  IsUserLoggedIn():boolean{
    let usuarioEstaLoggeado:boolean = false;

    if(this.JWT_CLAIMS != undefined){
      usuarioEstaLoggeado = true;
    }

    return usuarioEstaLoggeado;
  }

  IsUserAdmin():boolean{
    let esAdmin :boolean = false;

    if(this.JWT_CLAIMS != undefined){
      esAdmin = this.JWT_CLAIMS.Rol === "administrador";
    }
    
    return esAdmin;
  }

  UpdateHeader(token:string|null=null){
    if(token){
      this.httpHeader = {
        headers: new HttpHeaders({
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`
        }),
      }; 
    }
    else{
      this.httpHeader = {
        headers: new HttpHeaders({
          'Content-Type': 'application/json',
        }),
      }; 
    }
  }

  RemoveToken(){
    this.JWT_TOKEN = "";
    this.updateClaims(undefined)
    //this.JWT_CLAIMS = undefined;

    this.httpHeader = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
    }; 

    localStorage.removeItem(this.local_storage_auth_token_key);
    this.UpdateHeader();
  }

  SetTokenAndClaimsFromLocalSorage() {
    try {
      let claims: JwtTokenClaimsModel;
      let token: string|null = localStorage.getItem(this.local_storage_auth_token_key);

      if(token){
        claims = jwt_decode(token);
        this.JWT_TOKEN = token;
        this.updateClaims(claims)
        //this.JWT_CLAIMS = claims;
  
        this.UpdateHeader(this.JWT_TOKEN);
      }
    } 
    catch(Error) { }
  }

  SetToken(token: string) {
    let claims: JwtTokenClaimsModel;

    try {
      claims = jwt_decode(token);
      this.JWT_TOKEN = token;
      this.updateClaims(claims);
      //this.JWT_CLAIMS = claims;

      this.UpdateHeader(this.JWT_TOKEN);
      localStorage.setItem(this.local_storage_auth_token_key, token);
    } 
    catch(Error) { }
  }

  PrintToken(){
    console.log(this.JWT_TOKEN);
  }

  Post_Invitacion(elEmail:string, elRol:string):Observable<InvitacionOut>{
    let invitacion:InvitacionIn = {
      email : elEmail,
      rol : elRol
    };

    return this.http.post<InvitacionOut>( this.route_invitaciones,
      JSON.stringify(invitacion),
      this.httpHeader
    ).pipe( catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  Get_Invitacion(invitacionId:string):Observable<InvitacionOut>{
    let ruta:string = this.route_invitaciones + "/" + invitacionId;

    return this.http.get<InvitacionOut>( ruta,
      this.httpHeader
    ).pipe( catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  //SESSIONES
  Post_Session(email:string, contrasenia: string):Observable<TokenOut>{
    let cred:Credenciales = {
        Email : email,
        Contrasenia : contrasenia
    };  

    return this.http.post<TokenOut>( this.route_sessions,
      JSON.stringify(cred),
      this.httpHeader
    ).pipe( catchError(error => {
        console.log(error);
        this.handleError(error);
        throw new Error();
      })
    );
  };

  Delete_Session():Observable<any>{
    return this.http.delete(this.route_sessions,
      this.httpHeader
    ).pipe( catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  //APP KEY
  Post_Appkey():Observable<AppKeyOut>{

    return this.http.post<AppKeyOut>( this.route_sessions_appkey,
      {},
      this.httpHeader
    ).pipe( catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  Get_Appkey():Observable<AppKeyOut>{
    return this.http.get<AppKeyOut>( this.route_sessions_appkey,
      this.httpHeader
    ).pipe( catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  Delete_Appkey(){
    return this.http.delete( this.route_sessions_appkey,
      this.httpHeader
    ).pipe( catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  //USUARIOS
  Post_Usuario(pNombre:string, pEmail:string, pContrasenia:string, nombreEmpresa:string):Observable<UsuarioOut>{
    let usuario:UsuarioIn = {
      Nombre : pNombre, Email : pEmail, Contrasenia : pContrasenia, NombreEmpresa : nombreEmpresa,
    }

    return this.http.post<UsuarioOut>( this.route_usuarios,
      JSON.stringify(usuario),
      this.httpHeader
    ).pipe( catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
    //).pipe(retry(1), catchError(this.processError));
  }

  Post_Usuario_por_Invitacion(nombre:string, email:string, password:string, invitacionId:string):Observable<UsuarioOut>{
    let ruta:string = this.route_usuarios + "?invitacionid=" + invitacionId;
    let usuario:UsuarioIn = {
      Nombre : nombre, Email : email, Contrasenia : password, NombreEmpresa : "",
    }

    return this.http.post<UsuarioOut>( ruta,
      JSON.stringify(usuario),
      this.httpHeader
    ).pipe( catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  //PRODUCTOS
  Post_Producto(producto:ProductoIn):Observable<ProductoOut>{
    return this.http.post<ProductoOut>( this.route_productos,
      JSON.stringify(producto),
      this.httpHeader
    ).pipe( catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  Put_Producto(producto:ProductoIn, id:string):Observable<ProductoOut>{
    let ruta:string = this.route_productos + "/" + id;

    return this.http.put<ProductoOut>( 
      ruta,
      JSON.stringify(producto),
      this.httpHeader
    ).pipe( catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  Get_Productos():Observable<ProductoOut[]>{
    return this.http.get<ProductoOut[]>( this.route_productos,
      this.httpHeader
    ).pipe( 
      catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  Delete_Producto(id:string):Observable<any>{
    let ruta:string = this.route_productos + "/" + id;

    return this.http.delete<any>( 
      ruta,
      this.httpHeader
    ).pipe( 
      catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  //PROVEEDORES
  Post_Proveedor(proveedor:ProveedorIn):Observable<ProveedorOut>{
    return this.http.post<ProveedorOut>( this.route_proveedores,
      JSON.stringify(proveedor),
      this.httpHeader
    ).pipe( catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  Put_Proveedor(proveedor:ProveedorIn, id:string):Observable<ProveedorOut>{
    let ruta:string = this.route_proveedores + "/" + id;

    return this.http.put<ProveedorOut>( 
      ruta,
      JSON.stringify(proveedor),
      this.httpHeader
    ).pipe( catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  Get_Proveedores():Observable<ProveedorOut[]>{
    return this.http.get<ProveedorOut[]>( this.route_proveedores,
      this.httpHeader
    ).pipe( 
      catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  Delete_Proveedor(id:string):Observable<any>{
    let ruta:string = this.route_proveedores + "/" + id;

    return this.http.delete<any>( 
      ruta,
      this.httpHeader
    ).pipe( 
      catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  //COMPRAS
  Post_Compra(nuevaCompra:CompraIn):Observable<CompraOut>{
    return this.http.post<CompraOut>( this.route_compras,
      JSON.stringify(nuevaCompra),
      this.httpHeader
    ).pipe( catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  Get_Compras():Observable<CompraOut[]>{
    return this.http.get<CompraOut[]>( this.route_compras,
      this.httpHeader
    ).pipe( 
      catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  Get_Compra_Setup():Observable<CompraSetupOut>{
    return this.http.get<CompraSetupOut>( this.route_compras_setup,
      this.httpHeader
    ).pipe( 
      catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  //VENTAS
  Post_Venta(nuevaVenta:VentaIn):Observable<VentaOut>{
    return this.http.post<VentaOut>( this.route_ventas,
      JSON.stringify(nuevaVenta),
      this.httpHeader
    ).pipe( catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  Post_Venta_Programada(nuevaVenta:VentaIn):Observable<VentaOut>{
    return this.http.post<VentaOut>( this.route_ventas_programadas,
      JSON.stringify(nuevaVenta),
      this.httpHeader
    ).pipe( catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  Get_Ventas(params:GetVentasParams):Observable<VentaOut[]>{
    /*
    let theParams = new HttpParams();
    if(params.cantidadPorPagin){
      theParams = theParams.set("cantidadPorPagina", params.cantidadPorPagin);
    }
    if(params.indicePagina){
      theParams = theParams.set("indicePagina", params.indicePagina);
    }
    if(params.fechaDesde){
      theParams = theParams.set("fechaDesde", params.fechaDesde.toString());
    }
    if(params.fechaHasta){
      theParams = theParams.set("fechaHasta", params.fechaHasta.toString());
    }*/
    let primero:boolean = true;
    let ruta:string = this.route_ventas;

    /*
    if(params.cantidadPorPagina){
      if(primero){
        ruta = ruta + "?cantidadPorPagina=" + params.cantidadPorPagina;
      }
      else{
        ruta = ruta + "&cantidadPorPagina=" + params.cantidadPorPagina;
      }
    }

    if(params.indicePagina){
      if(primero){
        ruta = ruta + "?indicePagina=" + params.indicePagina;
      }
      else{
        ruta = ruta + "&indicePagina=" + params.indicePagina;
      }
    }
    */

    if(params.fechaHasta){
      if(primero){
        ruta = ruta + "?fechaHasta=" + params.fechaHasta.toLocaleDateString();
        primero = false;
      }
      else{
        ruta = ruta + "&fechaHasta=" + params.fechaHasta.toLocaleDateString();
      }
    }

    if(params.fechaDesde){
      if(primero){
        ruta = ruta + "?fechaDesde=" + params.fechaDesde.toLocaleDateString();
        primero = false;
      }
      else{
        ruta = ruta + "&fechaDesde=" + params.fechaDesde.toLocaleDateString();
      }
    }

    return this.http.get<VentaOut[]>( ruta,
      this.httpHeader
    ).pipe( 
      catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  //SUSCRIPCIONES
  Get_Subscriptions():Observable<SubscripcionesSetupOut>{
    return this.http.get<SubscripcionesSetupOut>( this.route_subscriptions,
      this.httpHeader
    ).pipe( 
      catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  Post_Suscripcion_Compraventa(productoId:string):Observable<SubscripcionOut>{
    let ruta:string = this.route_subscriptions_compraventa + "/" + productoId; 

    return this.http.post<SubscripcionOut>( ruta,
      {},
      this.httpHeader
    ).pipe( 
      catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  Delete_Suscripcion_Compraventa(productoId:string):Observable<any>{
    let ruta:string = this.route_subscriptions_compraventa + "/" + productoId;

    return this.http.delete<any>( 
      ruta,
      this.httpHeader
    ).pipe( 
      catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  Post_Suscripcion_Stock(productoId:string):Observable<SubscripcionOut>{
    let ruta:string = this.route_subscriptions_stock + "/" + productoId; 

    return this.http.post<SubscripcionOut>( ruta,
      {},
      this.httpHeader
    ).pipe( 
      catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  Delete_Suscripcion_Stock(productoId:string):Observable<any>{
    let ruta:string = this.route_subscriptions_stock + "/" + productoId;

    return this.http.delete<any>( 
      ruta,
      this.httpHeader
    ).pipe( 
      catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }

  //REPORTES
  Post_Reportes_Compraventa(){
    return this.http.post<any>( this.route_reportes_compraventa,
      {},
      this.httpHeader
    ).pipe( 
      catchError(error => {
        this.handleError(error);
        throw new Error();
      })
    );
  }


/*
 private route_subscriptions_compraventa  = this.route_subscriptions + "/compraventa";
  private route_subscriptions_stock   

*/




  private handleError(error:any){
    /*
    console.log(error);
    console.log(error.error);
    console.log(error.headers);
    console.log(error.message);
    console.log(error.name);
    console.log(error.ok);
    console.log(error.status);
    console.log(error.statusText);
    console.log(error.type);
    console.log(error.url);*/

    let errorMsg: string[] = [];
    if (error.error instanceof ErrorEvent) {
        errorMsg[0] = `${error.error}`;
        errorMsg[1] = `${error.error?.message}`;
    } else {
        errorMsg = this.getServerErrorMessage(error);
    }
    this.dialogService.openDialog(errorMsg[0], [errorMsg[1]]);
  }
  
  private getServerErrorMessage(error: HttpErrorResponse): string[] {
    /*
    console.log( "in getServerErrorMessage:"+ error.error);
    console.log(error);
    console.log(error.error);
    console.log(error.headers);
    console.log(error.message);
    console.log(error.name);
    console.log(error.ok);
    console.log(error.status);
    console.log(error.statusText);
    console.log(error.type);
    console.log(error.url);
    */
    switch (error.status) {
        case 400: {
            return ['Argument Error (400):', error.error?.toString()];
        }
        case 401:{
            return ['Invalid Operation (401):', error.error?.toString()]
        }
        case 403: {
            return ['Access Denied (403):', error.error?.toString()];
        }
        case 404: {
            return ['Not Found (404):', error.error?.toString()];
        }
        case 500: {
            return ['Internal Server Error (500):', error.error?.toString()];
        }
        default: {
            return [`Unknown Server Error (${error.status}):`, error.error?.toString()];
        }
    }
  }
}

