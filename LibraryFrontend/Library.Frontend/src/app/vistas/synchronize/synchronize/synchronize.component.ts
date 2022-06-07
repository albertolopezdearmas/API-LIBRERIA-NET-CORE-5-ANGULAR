import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/servicios/api/api.service';
import { Router } from '@angular/router';
import { AuthorI } from 'src/app/modelos/author.interface';
import { DataI } from 'src/app/modelos/data.interface';
import { LanguageApp } from 'src/app/modelos/config.interface';
import { BookI } from 'src/app/modelos/book.interface';
import { AlertComponent } from '../../../components/share/alert/alert.component';
import { MatDialog } from '@angular/material/dialog';
declare const $: any;
@Component({
  selector: 'app-synchronize',
  templateUrl: './synchronize.component.html',
  styleUrls: ['./synchronize.component.css'],
})
export class SynchronizeComponent implements OnInit {
  constructor(private api: ApiService, public alert: MatDialog) {}
  dtOptionsLibros: DataTables.Settings = {};
  dtOptionsAutores: DataTables.Settings = {};

  dataSource: DataI[] = [];
  books: BookI[] = [];
  authors: AuthorI[] = [];
  sincronizado: boolean = false;
  errorStratus: boolean = false;

  successStratus: boolean = false;

  ngOnInit(): void {
    this.GetSyncrhonize();
  }
  GetSyncrhonize() {
    let lastPageLibro = 0;
    let lastSearchTextLibro = '';
    let lastPageAutor = 0;
    let lastSearchTextAutor = '';

    this.dtOptionsLibros = {
      language: LanguageApp.spanish_datatables,
      pagingType: 'full_numbers',
      pageLength: 10,
      displayStart: lastPageLibro, // Last Selected Page
      search: { search: lastSearchTextLibro }, // Last Searched Text
      serverSide: true,
      processing: true,
      ajax: (dataTablesParameters: any, callback) => {
        lastPageLibro = dataTablesParameters.start; // Note :  dataTablesParameters.start = page count * table length
        lastSearchTextLibro = dataTablesParameters.search.value;
        this.api
          .getBooks({ queryFilter: dataTablesParameters })
          .then((data) => {
            this.books = <BookI[]>(<unknown>data.data);

            callback({
              recordsTotal: data.meta.totalCount,
              recordsFiltered: data.meta.totalCount,
              data: [],
            });
          });
      },
      autoWidth: false,
    };
    this.dtOptionsAutores = {
      language: LanguageApp.spanish_datatables,
      pagingType: 'full_numbers',
      pageLength: 10,
      displayStart: lastPageAutor, // Last Selected Page
      search: { search: lastSearchTextAutor }, // Last Searched Text
      serverSide: true,
      processing: true,
      ajax: (dataTablesParameters: any, callback) => {
        lastPageAutor = dataTablesParameters.start; // Note :  dataTablesParameters.start = page count * table length
        lastSearchTextAutor = dataTablesParameters.search.value;
        this.api
          .getAuthors({ queryFilter: dataTablesParameters })
          .then((data) => {
            this.authors = <AuthorI[]>(<unknown>data.data);
            callback({
              recordsTotal: data.meta.totalCount,
              recordsFiltered: data.meta.totalCount,
              data: [],
            });
          });
      },
      autoWidth: false,
    };
  }
  sincronizar() {
    this.api.getSicronizar().subscribe({
      next: (data) => {
        this.sincronizado = <boolean>(<unknown>data.data);
        if (this.sincronizado) {
          this.alert
            .open(AlertComponent, {
              width: '300px',
              data: 'Datos sincronizados con éxito',
            })
            .afterClosed()
            .subscribe();
          this.GetSyncrhonize();
        } else {
          this.alert
            .open(AlertComponent, {
              width: '300px',
              data: 'Error al sincronizar los dotas',
            })
            .afterClosed()
            .subscribe();
        }
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
