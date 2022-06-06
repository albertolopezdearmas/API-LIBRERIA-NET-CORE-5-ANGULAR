import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './vistas/login/login.component';
import { BooksFilterComponent } from './vistas/booksFilter/books-filter/books-filter.component';
import { DashboardComponent } from './vistas/dashboard/dashboard.component';

import { SynchronizeComponent } from './vistas/synchronize/synchronize/synchronize.component';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'dashboard', component: DashboardComponent },

  { path: 'buscar', component: BooksFilterComponent },
  { path: 'sincronizar', component: SynchronizeComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
export const routingComponents = [
  LoginComponent,
  DashboardComponent,
  BooksFilterComponent,
  SynchronizeComponent,
];
