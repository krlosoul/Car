import { Component, OnInit } from '@angular/core';
import { ICarros } from '../../interfaces/icarros';
import { ModalService } from '../../services/modal-service.service';
import { RestServiceService } from '../../services/rest-service.service';
import { AuthService } from '../../services/auth-service.service';
import * as jquery from 'jquery';
import { Router } from '@angular/router';

@Component({
  selector: 'app-carro',
  templateUrl: './carro.component.html',
  styleUrls: ['./carro.component.css']
})
export class CarroComponent implements OnInit {

  collectionSize: number;
  page: number;
  pageSize: number;
  enable: boolean;
  DataGrid: Array<ICarros>;

  constructor(private rest: RestServiceService, private modal: ModalService, private auth: AuthService, private router: Router) {
    this.page = 1;
    this.pageSize = 5;
    this.collectionSize = 0;
    this.DataGrid = [];
  }

  ngOnInit() {
    if (!this.auth.isLogin) {
      this.router.navigate(['/login']);
    }

    this.modal.showLoading('Cargando Información');
    this.cargarGrid();
  }

  onChange(e) {
    this.page = e;
    this.cargarGrid();
  }
  onChangeCantidad(inputCantidad: any, referenciaProducto: string, valor: number) {
    let cantidad = inputCantidad.target.value * 1 ;
    let total = cantidad * valor;
    document.getElementById(referenciaProducto).innerText = total.toString();

    this.rest.put('api/carros/producto/' + referenciaProducto + '/' + this.auth.idUser, { estado: 2, cantidad: cantidad, total: total }).then(res => {
      this.modal.showAlert('Producto actualizado', 2);
      this.cargarGrid();
      this.modal.hideLoading();
    }).catch(err => {
      this.modal.hideLoading();
    });

  }

  cargarGrid() {
    this.modal.showLoading('Cargando Información');
    this.rest.get('api/carros?take=' + this.pageSize + '&skip=' + (((this.page - 1) * this.pageSize)) + '&usuario=' + this.auth.idUser).then((res) => {
      this.DataGrid = res.data;console.log(res)
      this.collectionSize = res.count;
      this.modal.hideLoading();
    }).catch(err => {
      this.modal.hideLoading();
    });
  }
  Quitar(referenciaProducto: string) {
    this.modal.showLoading('Eliminando información');
    this.rest.delete('api/carros/producto/' + referenciaProducto).then(res => {
      this.modal.showAlert('Producto eliminado del carrito satisfactoriamente', 2);
      this.cargarGrid();
      this.modal.hideLoading();
    }).catch(err => {
      this.modal.hideLoading();
    });
  }

  Guardar() {
    this.modal.showLoading('Eliminando información');
    this.rest.put('api/carros/guardar/' + this.auth.idUser, {}).then(res => {
      this.modal.showAlert('Compra realizada', 2);
      this.cargarGrid();
      this.modal.hideLoading();
    }).catch(err => {
      this.modal.hideLoading();
    });
  }
  Vaciar() {
    this.modal.showLoading('Eliminando información');
    this.rest.delete('api/carros/limpiar/' + this.auth.idUser).then(res => {
      this.modal.showAlert('Carrito limpiado satisfactoriamente', 2);
      this.cargarGrid();
      this.modal.hideLoading();
    }).catch(err => {
      this.modal.hideLoading();
    });
  }




}
