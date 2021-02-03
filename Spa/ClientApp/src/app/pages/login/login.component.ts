import { Component, OnInit } from '@angular/core';
import { ModalService } from '../../services/modal-service.service';
import { RestServiceService } from '../../services/rest-service.service';
import { AuthService } from '../../services/auth-service.service';
import { IUsuarios } from '../../interfaces/iusuarios';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})

export class LoginComponent implements OnInit {

  model: IUsuarios;
  error = {
    usuario: { error: false, mensaje: '' },
    clave: { error: false, mensaje: '' }
  };

  constructor(private rest: RestServiceService, private modal: ModalService, private auth: AuthService, private router: Router) {
    this.model = {
      id: 0,
      idPersona: 0,
      usuario: '',
      clave: '',
      persona: null
    };
  }

  ngOnInit() {
  }

  cancelar() {
    this.model = {
      id: 0,
      idPersona: 0,
      usuario: '',
      clave: '',
      persona: null
    };
    this.error = {
      usuario: { error: false, mensaje: '' },
      clave: { error: false, mensaje: '' }
    };
  }
  validar(): number {

    this.error = {
      usuario: { error: false, mensaje: '' },
      clave: { error: false, mensaje: '' }
    };

    let error = 0;

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

  login() {
    if (this.validar() == 0) {
      this.modal.showLoading('Intentando iniciar sesión');
      
      this.rest.login(this.model.usuario, this.model.clave).then(<IUsuarios>(res) => {
        this.modal.showAlert('Bienvenido', 2);
        this.modal.hideLoading();
        this.auth.auth(true);
        this.auth.setUser(res.id);
        this.cancelar();
        this.router.navigate(['/']);   
      }).catch(err => {console.log(err)
        this.modal.hideLoading();
      });
    } else {
      this.modal.showAlert("Se han presentado errores al validar los campos, por favor reviselos e intentelo de nuevo", 3);
    }
  }

}






