import { Input, Output, EventEmitter, Component, OnChanges } from '@angular/core';

@Component({
  selector: 'app-pagination',
  template: `
  <nav>
    <ul class="pagination">
      <li class="page-item" [class.disabled]="currentPage == 1">
        <a class="page-link" (click)="previous()">Previous</a>
      </li>
      <li *ngFor="let page of pages" class="page-item" [class.active]="currentPage == page">
        <a class="page-link" (click)="changePage(page)" >{{ page }}</a>
      </li>
      <li class="page-item" [class.disabled]="currentPage == pages.lenght">
        <a class="page-link" (click)="next()" >Next</a>
      </li>
    </ul>
  </nav>`,
  styles: ['page-link {color: #007bff;}']
})
export class PaginationComponent implements OnChanges {
  @Input('total-items') totalItems;
  @Input('page-size') pageSize = 10;
  @Output('page-changed') pageChanged = new EventEmitter();
  pages: any[];
  currentPage = 1;

  ngOnChanges() {
    this.currentPage = 1;
    var pagesCount = Math.ceil(this.totalItems / this.pageSize);
    this.pages = [];

    for (let i = 1; i <= pagesCount; i++) {
      this.pages.push(i);
    }
  }

  changePage(page) {
    this.currentPage = page;
    this.pageChanged.emit(this.currentPage);
  }

  next() {
    if (this.currentPage == this.pages.length)
      return;

    this.currentPage++;
    this.pageChanged.emit(this.currentPage);
  }

  previous() {
    if (this.currentPage == 1)
      return;

    this.currentPage--;
    this.pageChanged.emit(this.currentPage);
  }
}
