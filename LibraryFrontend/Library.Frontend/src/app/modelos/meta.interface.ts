export interface MetaI {
  totalCount: number;
  pagesSize: number;
  currentPage: number;
  totalPage: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  nextPageUrl: string;
  previousPageUrl: string;
}
