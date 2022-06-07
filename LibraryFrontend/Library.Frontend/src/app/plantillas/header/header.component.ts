import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  constructor(private router: Router) {}
  checkTokenCookies() {
    let token = localStorage.getItem('token');
    if (token === null || token === '' || token == undefined) {
      this.router.navigate(['/']);
    }
  }
  ngOnInit(): void {
    this.checkTokenCookies();
  }
  salir() {
    localStorage.clear();

    this.router.navigate(['']);
  }
}
