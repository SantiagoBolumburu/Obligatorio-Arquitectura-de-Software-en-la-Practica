import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignInTabComponent } from './tabs/signin-tab/signin-tab.component';
import { LoginTabComponent } from './tabs/login-tab/login-tab.component';
import { GestionInventarioComponent } from './tabs/gestion-inventario/gestion-inventario.component';
import { GestionProductosComponent } from './tabs/gestion-productos/gestion-productos.component';
import { GestionProveedoresComponent } from './tabs/gestion-proveedores/gestion-proveedores.component';
import { InviteUserTabComponent } from './tabs/invite-user-tab/invite-user-tab.component';
import { PaginaInicioEmpresaComponent } from './tabs/pagina-inicio-empresa/pagina-inicio-empresa.component';
import { RegistroComprasComponent } from './tabs/registro-compras/registro-compras.component';
import { RegistroVentasComponent } from './tabs/registro-ventas/registro-ventas.component';
import { LoggedGuard } from './guards/logged.guard';
import { AdminGuard } from './guards/admin.guard';
import { GenerarAppKeyComponent } from './tabs/generar-app-key/generar-app-key.component';
import { GestionSubscripcionesComponent } from './tabs/gestion-subscripciones/gestion-subscripciones.component';
import { ReportesComponent } from './tabs/reportes/reportes.component';

const routes: Routes = [
  { path: '', redirectTo: 'inicio', pathMatch: 'full', },
  { path: 'singin/invitaciones/:id',               
        title: "Registrarse",
        component: SignInTabComponent,},
  { path: 'singin',               
      title: "Registrarse",
      component: SignInTabComponent,},
  { path: 'login',
      title: "Ingresar",
      component: LoginTabComponent},
  { path: 'gestion/inventario',
      title: "Gestionar Inventario",
      component: GestionInventarioComponent,
      canActivate:[LoggedGuard]},
  { path: 'gestion/productos',
      title: "Gestionar Productos",
      component: GestionProductosComponent,
      canActivate:[LoggedGuard,AdminGuard]},
  { path: 'gestion/proveedores',
      title: "Gestionar Proveedores",
      component: GestionProveedoresComponent,
      canActivate:[LoggedGuard,AdminGuard]},
  { path: 'user/invite',
      title: "Invitar Usuarios",      
      component: InviteUserTabComponent,
      canActivate:[LoggedGuard,AdminGuard]},
  { path: 'inicio',
      title: "Inicio",                
      component: PaginaInicioEmpresaComponent,
      canActivate:[LoggedGuard]},
  { path: 'registro/compras',
      title: "Registrar Compras",     
      component: RegistroComprasComponent,
      canActivate:[LoggedGuard,AdminGuard]},
  { path: 'registro/ventas',
      title: "Registrar Ventas",     
      component: RegistroVentasComponent,
      canActivate:[LoggedGuard]},
  { path: 'appkey',
      title: "Administrar Application Key", 
      component:GenerarAppKeyComponent,
      canActivate:[LoggedGuard,AdminGuard]},
  { path: 'suscripciones',
      title: "Administrar Suscripciones", 
      component:GestionSubscripcionesComponent,
      canActivate:[LoggedGuard,AdminGuard]},
  { path: 'reportes',
      title: "Generar Reportes", 
      component:ReportesComponent,
      canActivate:[LoggedGuard]},
  { path: '**', redirectTo: 'inicio',}
];

/*
const routes: Routes =[
  { path: '', redirectTo: 'login', pathMatch: 'full',},
  { path: '', component: AdminLayoutComponent, children: [{
      path: '',
      loadChildren: () => import('./layouts/admin-layout/admin-layout.module').then(m => m.AdminLayoutModule)
    }]
  }
];
*/
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }