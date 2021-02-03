import { Component, OnInit } from '@angular/core';
import { ModalService } from '../../services/modal-service.service';
import { RestServiceService } from '../../services/rest-service.service';
import { AuthService } from '../../services/auth-service.service';
import { IUsuarios } from '../../interfaces/iusuarios';
import { IPersonas } from '../../interfaces/ipersonas';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registrar',
  templateUrl: './registrar.component.html',
  styleUrls: ['./registrar.component.css']
})
export class RegistrarComponent implements OnInit {

  collectionSize: number;
  page: number;
  pageSize: number;
  enable: boolean;
  DataGrid: Array<IUsuarios>;

  model: IUsuarios;
  persona: IPersonas;

  error = {
    nombres: { error: false, mensaje: '' },
    apellidos: { error: false, mensaje: '' },
    usuario: { error: false, mensaje: '' },
    clave: { error: false, mensaje: '' }
  };

  constructor(private rest: RestServiceService, private modal: ModalService, private auth: AuthService, private router: Router) {
    this.page = 1;
    this.pageSize = 5;
    this.collectionSize = 0;
    this.DataGrid = [];

    this.persona = {
      id: 0,
      nombres: '',
      apellidos: '',
    };
    this.model = {
      id: 0,
      idPersona: 0,
      usuario: '',
      clave: '',
      persona: this.persona
    };
  }

  ngOnInit() {
    this.modal.showLoading('Cargando Información');
    this.cargarGrid();
  }
  onChange(e) {
    this.page = e;
    this.cargarGrid();
  }

  cargarGrid() {
    this.modal.showLoading('Cargando Información');
    this.rest.get('api/usuarios?take=' + this.pageSize + '&skip=' + (((this.page - 1) * this.pageSize))).then((res) => {
      this.DataGrid = res.data;
      this.collectionSize = res.count;
      this.modal.hideLoading();
    }).catch(err => {
      this.modal.hideLoading();
    });
  }

  cancelar() {
    this.persona = {
      id: 0,
      nombres: '',
      apellidos: '',
    };
    this.model = {
      id: 0,
      idPersona: 0,
      usuario: '',
      clave: '',
      persona: this.persona
    };
  }
  validar(): number {

    this.error = {
      nombres: { error: false, mensaje: '' },
      apellidos: { error: false, mensaje: '' },
      usuario: { error: false, mensaje: '' },
      clave: { error: false, mensaje: '' }
    };

    let error = 0;

    if (this.model.persona.nombres.trim() == '') {
      this.error.nombres.error = true;
      this.error.nombres.mensaje = "Debe ingresar el campo 'Nombres'";
      error++;
    }
    if (this.model.persona.nombres.length > 45) {
      this.error.nombres.error = true;
      this.error.nombres.mensaje = "El valor del campo 'Nombres' no debe superar los 45 carácteres";
      error++;
    }

    if (this.model.persona.apellidos.trim() == '') {
      this.error.apellidos.error = true;
      this.error.apellidos.mensaje = "Debe ingresar el campo 'Apellidos'";
      error++;
    }
    if (this.model.persona.apellidos.length > 45) {
      this.error.apellidos.error = true;
      this.error.apellidos.mensaje = "El valor del campo 'Apellidos' no debe superar los 45 carácteres";
      error++;
    }

    if (this.model.usuario.trim() == '') {
      this.error.usuario.error = true;
      this.error.usuario.mensaje = "Debe ingresar el campo 'Usuario'";
      error++;
    }
    if (this.model.usuario.length > 45) {
      this.error.usuario.error = true;
      this.error.usuario.mensaje = "El valor del campo 'Usuario' no debe superar los 45 carácteres";
      error++;
    }

    if (this.model.clave.trim() == '') {
      this.error.clave.error = true;
      this.error.clave.mensaje = "Debe ingresar el campo 'Clave'";
      error++;
    }
    if (this.model.clave.length > 45) {
      this.error.clave.error = true;
      this.error.clave.mensaje = "El valor del campo 'Clave' no debe superar los 45 carácteres";
      error++;
    }

    return error;
  }

  registrar() {
    if (this.validar() == 0) {
      this.modal.showLoading('Registrando usuario');
      this.rest.post('api/usuarios', this.model).then(<IUsuarios>(res) => {
        this.modal.showAlert('Bienvenido', 2);
        this.cancelar();
        this.cargarGrid();
        this.auth.auth(true);
        this.auth.setUser(res.id);
        this.router.navigate(['/']);   
        this.modal.hideLoading();
      }).catch(err => {
        this.modal.hideLoading();
      });
    } else {
      this.modal.showAlert("Se han presentado errores al validar los campos, por favor reviselos e intentelo de nuevo", 3);
    }
  }

}
