import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ApiService } from '../../servicios/api/api.service';
import { LoginI } from '../../modelos/login.interface';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginForm = new FormGroup({
    username: new FormControl('User 1', Validators.required),
    password: new FormControl('Password1', Validators.required),
  });

  constructor(private api: ApiService, private router: Router) {}

  errorStratus: boolean = false;
  errorMsj: string = '';

  ngOnInit(): void {
    this.checkTokenCookies();
  }

  checkTokenCookies() {
    let token = localStorage.getItem('token');
    if (token) {
      this.router.navigate(['dashboard']);
    }
  }

  onLogin(form: LoginI) {
    console.log(form.password);
    this.api.loginByUser(form).subscribe({
      next: (data) => {
        if (data.success) {
          localStorage.setItem('token', data.token);

          this.router.navigate(['dashboard']);
        } else {
          this.errorStratus = true;
          this.errorMsj = data.errors;
        }
      },
      error: (error) => {
        this.errorStratus = true;
        this.errorMsj = 'Error al realizar la solicitud';
      },
    });
  }
}
