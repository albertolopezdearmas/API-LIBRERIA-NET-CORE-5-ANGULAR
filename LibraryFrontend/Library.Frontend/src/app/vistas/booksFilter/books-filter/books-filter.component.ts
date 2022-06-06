import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';

import { ApiService } from 'src/app/servicios/api/api.service';
import { Router } from '@angular/router';
import { MAT_DATE_FORMATS } from '@angular/material/core';

import { FormControl, FormGroup } from '@angular/forms';
import { Subject } from 'rxjs';
import { DataTableDirective } from 'angular-datatables';

import { AuthorI } from 'src/app/modelos/author.interface';
import { LanguageApp, MY_DATE_FORMATS } from 'src/app/modelos/config.interface';
import { DataI } from 'src/app/modelos/data.interface';
import { BookI } from 'src/app/modelos/book.interface';

declare const $: any;
@Component({
  selector: 'app-books-filter',
  templateUrl: './books-filter.component.html',
  styleUrls: ['./books-filter.component.css'],
  providers: [{ provide: MAT_DATE_FORMATS, useValue: MY_DATE_FORMATS }],
})
export class BooksFilterComponent implements OnInit, AfterViewInit, OnDestroy {
  dateRange = new FormGroup({
    start: new FormControl(),
    end: new FormControl(),
  });
  constructor(private api: ApiService, private router: Router) {}
  @ViewChild(DataTableDirective, { static: false })
  dtElement: DataTableDirective | any;
  selectedValue: string = '';
  dtOptionsLibros: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  dataSource: DataI[] = [];
  books: BookI[] = [];
  authors: AuthorI[] = [];
  myGroup: FormGroup | any;
  idBook: string | any;
  fechaInicial: Date | any;
  fechaFinal: Date | any;

  ngAfterViewInit(): void {
    this.dtTrigger.next(null);
  }

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

  rerender(): void {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      // Destroy the table first
      dtInstance.destroy();
      // Call the dtTrigger to rerender again
      this.dtTrigger.next(null);
    });
  }
  ngOnInit(): void {
    this.GetCargarDatos();
  }

  GetCargarDatos() {
    let lastPageLibro = 0;
    let lastSearchTextLibro = '';

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
        dataTablesParameters.idBook = this.idBook;
        dataTablesParameters.fechaInicial = this.fechaInicial;
        dataTablesParameters.fechaFinal = this.fechaFinal;

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
    this.api.getAuthorsAll().subscribe({
      next: (data) => {
        this.authors = <AuthorI[]>(<unknown>data.data);
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
  range = new FormGroup({
    start: new FormControl(),

    end: new FormControl(),
  });
  Filtrar() {
    if (this.selectedValue.length != 0) {
      this.idBook = this.selectedValue;
    }

    if (this.dateRange.get('start')?.value) {
      this.fechaInicial = new Date(this.dateRange.get('start')?.value)
        .toISOString()
        .slice(0, 10);
      this.fechaFinal = new Date(this.dateRange.get('end')?.value)
        .toISOString()
        .slice(0, 10);
    } else {
      this.fechaInicial = null;
      this.fechaFinal = null;
    }
    this.rerender();
  }
}
