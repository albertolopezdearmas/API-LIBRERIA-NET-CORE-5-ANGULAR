export interface ResponseI {
  status: string;
  response: string;
}
export interface ResponseLoginI {
  token: string;
  success: boolean;
  errors: string;
}

export interface DataTablesResponseI {
  data: any[];
  draw: number;
  recordsFiltered: number;
  recordsTotal: number;
}
