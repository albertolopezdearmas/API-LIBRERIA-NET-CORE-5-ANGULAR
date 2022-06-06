import { BookI } from './book.interface';

export interface AuthorI {
  id: number;
  idBook: number;
  idBookNavigation: BookI;
  title: string;
  firstName: string;
  lastName: string;
}
