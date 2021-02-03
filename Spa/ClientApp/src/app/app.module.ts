import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbModule, NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './app.component';
import { NavMenuComponent } from '../app/shared/nav-menu/nav-menu.component';

import { RestServiceService } from '../app/services/rest-service.service';
import { ModalService } from '../app/services/modal-service.service';
import { AuthService } from '../app/services/auth-service.service';

import { LoginComponent } from '../app/pages/login/login.component';
import { RegistrarComponent } from '../app/pages/registrar/registrar.component';
import { HomeComponent } from '../app/pages/home/home.component';
import { ProductosComponent } from '../app/pages/productos/productos.component';
import { CarroComponent } from '../app/pages/carro/carro.component';
import { HistorialComponent } from '../app/pages/historial/historial.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    RegistrarComponent,
    HomeComponent,
    ProductosComponent,
    CarroComponent,
    HistorialComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgbModule,
    NgbPaginationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'registrar', component: RegistrarComponent },
      { path: 'productos', component: ProductosComponent },
      { path: 'carro', component: CarroComponent },
      { path: 'historial', component: HistorialComponent },
    ])
  ],
  providers: [
    RestServiceService,
    ModalService,
    AuthService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
