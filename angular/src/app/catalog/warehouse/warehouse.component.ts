import { AuthService, PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { WarehouseDto, WarehouseInlistDto, WarehouseService } from '@proxy/warehouses';
import { OAuthService } from 'angular-oauth2-oidc';
import { Subject, take, takeUntil } from 'rxjs';
import { Message, MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ConfirmationService } from 'primeng/api';
import { WarehouseDetailComponent } from './warehouse-detail.component';
import { WarehouseViewComponent } from './warehouse-view.component';

@Component({
  providers: [MessageService],
  selector: 'app-warehouse',
  templateUrl: './warehouse.component.html',
  styleUrls: ['./warehouse.component.scss'],
})
export class WarehouseComponent implements OnInit, OnDestroy{
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  items: WarehouseInlistDto[] = [];

  //paging variabls
  public skipCount: number = 0;
  public maxResultCount: number = 10;
  public totalCount: number;
 
  public selectedItems: WarehouseInlistDto[] = [];

  // filter
  keyword: string = '';
  ListStatus: any[] = [];
  status: boolean = true;

  constructor(
    private WarehouseService: WarehouseService,
    private dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService
    ){}
  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  ngOnInit(): void {
    this.loadStatus();
    this.loadData();
  }

  loadData(){
    this.toggleBlockUI(true);
    this.WarehouseService.getListFilter({
      keyword: this.keyword,
      status: this.status,
      maxResultCount: this.maxResultCount,
      skipCount: this.skipCount
    })
    .pipe(takeUntil(this.ngUnsubscribe))
    .subscribe({
        next:(Response:PagedResultDto<WarehouseInlistDto>) => {
            this.items = Response.items; 
            this.totalCount = Response.totalCount;
            this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
    })
  }
loadStatus() {
    this.WarehouseService.getListAll()
      .subscribe((response: WarehouseInlistDto[]) => {
        this.ListStatus.push(
          {
            value: true,
            label: "Sử dụng",
          },
          {
            value: false,
            label: "Không sử dụng",
          });
      });
  };
  showAddModal() {
    const ref = this.dialogService.open(WarehouseDetailComponent,{
      header: 'Thêm mới loại sản phẩm',
      width: '60%',
      height: '50%'
    });
    ref.onClose.subscribe((data: WarehouseDto) => {
      if(data){
          this.loadData();
          this.notificationService.showSuccess("Thêm mới thành công");
          this.selectedItems = [];
      }
    });
  }
  showEditModal() {
    if (this.selectedItems.length == 0) {
      this.notificationService.showError('Bạn phải chọn một bản ghi');
      return;
    }
    const id = this.selectedItems[0].id;
    const ref = this.dialogService.open(WarehouseDetailComponent, {
      data: {
        id: id,
      },
      header: 'Cập nhật',
      width: '60%',
      height: '50%'
    });

    ref.onClose.subscribe((data: WarehouseDto) => {
      if (data) {
        this.loadData();
        this.selectedItems = [];
        this.notificationService.showSuccess('Cập nhật thành công');
      }
    });
  }
  showOpenModal() {
    if (this.selectedItems.length == 0) {
      this.notificationService.showError('Bạn phải chọn một bản ghi');
      return;
    }
    const id = this.selectedItems[0].id;
    const ref = this.dialogService.open(WarehouseViewComponent, {
      data: {
        id: id,
      },
      header: 'Xem',
      width: '60%',
      height: '50%'
    });
  }
  deleteItemsConfirmed(ids: string[]) {
    this.toggleBlockUI(true);
    this.WarehouseService
      .deleteMultile(ids)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: () => {
          this.notificationService.showSuccess('Xóa thành công');
          this.loadData();
          this.selectedItems = [];
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }
  deleteItems() {
    if (this.selectedItems.length == 0) {
      this.notificationService.showError('Phải chọn ít nhất một bản ghi');
      return;
    }
    var ids = [];
    this.selectedItems.forEach(element => {
      ids.push(element.id);
    });
    this.confirmationService.confirm({
      message: 'Bạn có chắc muốn xóa bản ghi này?',
      accept: () => {
        this.deleteItemsConfirmed(ids);
      },
    });
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
