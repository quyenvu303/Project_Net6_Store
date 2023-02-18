import { AuthService, PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { OrderInlistDto, OrderService } from '@proxy/orders';
import { Subject, take, takeUntil } from 'rxjs';
import { Message, MessageService } from 'primeng/api';

@Component({
  providers: [MessageService],
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss'],
})
export class OrderComponent implements OnInit, OnDestroy{
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  items: OrderInlistDto[] = [];

  //paging variabls
  public skipCount: number = 0;
  public maxResultCount: number = 10;
  public totalCount: number;
  msgs1: Message[];

  
  // filter
  keyword: string = '';

  constructor(private OrderService: OrderService){}
  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  ngOnInit(): void {
    this.loadData();
  }

  loadData(){
    this.toggleBlockUI(true);
    this.OrderService.getListFilter({
      keyword: this.keyword,
      maxResultCount: this.maxResultCount,
      skipCount: this.skipCount
    })
    .pipe(takeUntil(this.ngUnsubscribe))
    .subscribe({
        next:(Response:PagedResultDto<OrderInlistDto>) => {
            this.items = Response.items; 
            this.totalCount = Response.totalCount;
            this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
    })
  }
  private toggleBlockUI(enabled: boolean){
    if(enabled == true){
      this.blockedPanel = true
    }else{
      setTimeout(() => {
        this.blockedPanel = false;  
      }, 1000);
      
    }
  }

}
