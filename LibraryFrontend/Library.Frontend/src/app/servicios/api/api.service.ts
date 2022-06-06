import { Injectable } from '@angular/core';
import { LoginI } from '../../modelos/login.interface';
import { ResponseLoginI } from '../../modelos/response.interface';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, retry } from 'rxjs';
import { DataI } from 'src/app/modelos/data.interface';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  url: string = 'http://localhost:43697';
  constructor(private http: HttpClient) {}

  loginByUser(form: LoginI): Observable<ResponseLoginI> {
    let direccion = this.url + '/api/Token';

    return this.http.post<ResponseLoginI>(direccion, form);
  }

  getBooks(queryFilter: any): Promise<DataI | any> {
    let auth_token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${auth_token}`,
    });

    let direccion = this.url + '/api/book/';

    let params = new HttpParams();
    params = params.append('PageSize', queryFilter.queryFilter.length);
    params = params.append(
      'PageNumber',
      queryFilter.queryFilter.start == 0
        ? 1
        : Math.floor(
            queryFilter.queryFilter.start / queryFilter.queryFilter.length
          ) + 1
    );

    params = params.append(
      'OrderColumn',
      queryFilter.queryFilter.order[0].column
    );

    params = params.append('Order', queryFilter.queryFilter.order[0].dir);
    params = params.append('SearchAll', queryFilter.queryFilter.search.value);
    if (queryFilter.queryFilter.idBook != null) {
      params = params.append('Id', queryFilter.queryFilter.idBook);
    }
    if (
      queryFilter.queryFilter.fechaFinal != null &&
      queryFilter.queryFilter.fechaInicial
    ) {
      params = params.append(
        'StrarPublishDate',
        queryFilter.queryFilter.fechaInicial
      );
      params = params.append(
        'EndPublishDate',
        queryFilter.queryFilter.fechaFinal
      );
    }
    const requestOptions = { headers: headers, params, withCredentials: true };
    return this.http
      .get<DataI>(direccion, requestOptions)
      .pipe(retry(1))
      .toPromise();
  }

  getAuthors(queryFilter: any): Promise<DataI | any> {
    let auth_token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${auth_token}`,
    });
    let direccion = this.url + '/api/Author';
    queryFilter.withCredentials = true;
    queryFilter.PageSize = queryFilter.queryFilter.length;
    let params = new HttpParams();
    params = params.append('PageSize', queryFilter.queryFilter.length);
    params = params.append(
      'PageNumber',
      queryFilter.queryFilter.start == 0
        ? 1
        : Math.floor(
            queryFilter.queryFilter.start / queryFilter.queryFilter.length
          ) + 1
    );

    params = params.append(
      'OrderColumn',
      queryFilter.queryFilter.order[0].column
    );

    params = params.append('Order', queryFilter.queryFilter.order[0].dir);
    params = params.append('SearchAll', queryFilter.queryFilter.search.value);
    const requestOptions = { headers: headers, params, withCredentials: true };
    return this.http
      .get<DataI>(direccion, requestOptions)
      .pipe(retry(1))
      .toPromise();
  }
  getSicronizar(): Observable<DataI> {
    let auth_token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${auth_token}`,
    });
    let direccion = this.url + '/api/Synchronize';
    const requestOptions = { headers: headers, withCredentials: true };
    return this.http.get<DataI>(direccion, requestOptions);
  }
  getAuthorsAll(): Observable<DataI> {
    let auth_token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${auth_token}`,
    });
    let direccion = this.url + '/api/Author';
    let params = new HttpParams();
    params = params.append('PageSize', 10000);
    const requestOptions = { headers: headers, params, withCredentials: true };
    return this.http.get<DataI>(direccion, requestOptions);
  }
}
