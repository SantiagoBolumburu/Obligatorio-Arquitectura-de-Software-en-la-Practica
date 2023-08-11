import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms'; 

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { LayoutModule } from '@angular/cdk/layout';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatPaginatorModule } from '@angular/material/paginator';

import { ReactiveFormsModule } from '@angular/forms';

import { SignInTabComponent } from './tabs/signin-tab/signin-tab.component';
import { InputComponent } from './basic/input-basic/input.component';
import { ApiRequestService } from './customServices/api-request.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { LoginTabComponent } from './tabs/login-tab/login-tab.component';
import { DialogComponent } from './basic/dialog/dialog.component';
import { InviteUserTabComponent } from './tabs/invite-user-tab/invite-user-tab.component';
import { SelectComponent } from './basic/select/select.component';
import { GestionProductosComponent } from './tabs/gestion-productos/gestion-productos.component';
import { TableComponent } from './basic/table/table.component';
import { TextAreaComponent } from './basic/text-area/text-area.component';
import { GestionProveedoresComponent } from './tabs/gestion-proveedores/gestion-proveedores.component';
import { RegistroComprasComponent } from './tabs/registro-compras/registro-compras.component';
import { DatePickerComponent } from './basic/date-picker/date-picker.component';
import { RegistroVentasComponent } from './tabs/registro-ventas/registro-ventas.component';
import { GestionInventarioComponent } from './tabs/gestion-inventario/gestion-inventario.component';
import { PaginaInicioEmpresaComponent } from './tabs/pagina-inicio-empresa/pagina-inicio-empresa.component';
import { GenerarAppKeyComponent } from './tabs/generar-app-key/generar-app-key.component';
import { GestionSubscripcionesComponent } from './tabs/gestion-subscripciones/gestion-subscripciones.component';
import { ReportesComponent } from './tabs/reportes/reportes.component';



@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    SignInTabComponent,
    InputComponent,
    LoginTabComponent,
    DialogComponent,
    InviteUserTabComponent,
    SelectComponent,
    GestionProductosComponent,
    TableComponent,
    TextAreaComponent,
    GestionProveedoresComponent,
    RegistroComprasComponent,
    DatePickerComponent,
    RegistroVentasComponent,
    GestionInventarioComponent,
    PaginaInicioEmpresaComponent,
    GenerarAppKeyComponent,
    GestionSubscripcionesComponent,
    ReportesComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    LayoutModule,
    FormsModule,

    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatCardModule,
    MatInputModule,
    MatFormFieldModule,
    MatDialogModule,
    MatSelectModule,
    MatTableModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatPaginatorModule,

    ReactiveFormsModule.withConfig({callSetDisabledState: 'whenDisabledForLegacyCode'}),
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [
    ApiRequestService,
    HttpClient,
    MatNativeDateModule,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
