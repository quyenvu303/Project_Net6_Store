import { AuthService, PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ProductDto, ProductInlistDto, ProductsService } from '@proxy/products';
import { OAuthService } from 'angular-oauth2-oidc';
import { Subject, take, takeUntil } from 'rxjs';
import { Message, MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ConfirmationService } from 'primeng/api';
import { CategoriesService, CategoryInlistDto } from '@proxy/categories';
import { WarehouseDto, WarehouseService } from '@proxy/warehouses';
import { ProductDetailComponent } from './product-detail.component';
import { Categories } from '@proxy';
import { ProductViewComponent } from './product-view.component';
import { trigger } from '@angular/animations';

@Component({
  providers: [MessageService],
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss'],
})
export class ProductComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  items: ProductInlistDto[] = [];

  //paging variabls
  public skipCount: number = 0;
  public maxResultCount: number = 10;
  public totalCount: number;
  msgs1: Message[];
  public selectedItems: ProductInlistDto[] = [];
  //filter
  keyword: string = '';
  ListCategory: any[] = [];
  categoryId: string = '';
  ListWarehouse: any[] = [];
  WarehouseGuid: string = '';
  ListStatus: any[] = [];
  status: boolean = true;

  constructor(
    private ProductService: ProductsService, 
    private CategoryService: CategoriesService,
    private WarehouseService: WarehouseService,
    private dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService
    ) { }
  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  ngOnInit(): void {
    this.loadCategory();
    this.loadWarehouse();
    this.loadStatus();
    this.loadData();
  }

  loadData() {
    this.toggleBlockUI(true);
    this.ProductService.getListFilter({
      keyword: this.keyword,
      categoryId: this.categoryId,
      warehouseGuid: this.WarehouseGuid,
      status: this.status,
      maxResultCount: this.maxResultCount,
      skipCount: this.skipCount
    })
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (Response: PagedResultDto<ProductInlistDto>) => {
          this.items = Response.items;
          this.totalCount = Response.totalCount;
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
         },
      });
  }
  loadStatus() {
    this.ProductService.getListAll()
      .subscribe((response: ProductInlistDto[]) => {
        this.ListStatus.push(
          {
            value: true,
            label: "Xuát bán",
          },
          {
            value: false,
            label: "Không xuát bán",
          });
      });
  };
  loadCategory() {
    this.CategoryService.getListAll()
    .subscribe((response: CategoryInlistDto[])=>{
      this.ListCategory.push({
        value: null,
        label: "Tất cả",
      });
      response.forEach(element=>{
          this.ListCategory.push({
            value: element.id,
            label: element.categoryName,
          });
      });
    });
  };

  loadWarehouse() {
    this.WarehouseService.getListAll()
    .subscribe((response: WarehouseDto[])=>{
      this.ListWarehouse.push({
        value: null,
        label: "Tất cả",
      });
      response.forEach(element=>{
          this.ListWarehouse.push({
            value: element.id,
            label: element.title,
          });
      });
    });
  };
  showAddModal() {
    const ref = this.dialogService.open(ProductDetailComponent,{
      header: 'Thêm mới loại sản phẩm',
      width: '80%',
      height: '100%'
    });
    ref.onClose.subscribe((data: ProductDto) => {
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
    const ref = this.dialogService.open(ProductDetailComponent, {
      data: {
        id: id,
      },
      header: 'Cập nhật',
      width: '80%',
      height: '100%'
    });

    ref.onClose.subscribe((data: ProductDto) => {
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
    const ref = this.dialogService.open(ProductViewComponent, {
      data: {
        id: id,
      },
      header: 'Xem',
      width: '80%',
      height: '100%'
    });
  }
  deleteItemsConfirmed(ids: string[]) {
    this.toggleBlockUI(true);
    this.ProductService
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

  getCategoryName(value: string){
      return Categories[value]

  }



  pageChanged(event: any): void {
    this.skipCount = (event.page - 1) * this.maxResultCount;
    this.maxResultCount = event.rows;
    this.loadData();
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
