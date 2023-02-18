import { PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { CategoryInlistDto, CategoriesService, CategoryDto } from '@proxy/categories';
import { Subject, takeUntil } from 'rxjs';
import { Message, MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { CategoryDetailComponent } from './category-detail.component';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ConfirmationService } from 'primeng/api';
import { CategoryViewComponent } from './category-view.component';
@Component({
  providers: [MessageService],
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss'],
})
export class CategoryComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  items: CategoryInlistDto[] = [];
  public selectedItems: CategoryInlistDto[] = [];
  //paging variabls
  public skipCount: number = 0;
  public maxResultCount: number = 10;
  public totalCount: number;
  msgs1: Message[];

  // filter
  keyword: string = '';
  ListStatus: any[] = [];
  status: boolean = true;

  public form: FormGroup;

  constructor(
    private CategoryService: CategoriesService,
    private dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService
    ) { }
  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
  
  ngOnInit(): void {
    this.loadStatus();
    this.loadData();
  }

  loadData() {
    this.toggleBlockUI(true);
    this.CategoryService.getListFilter({
      keyword: this.keyword,
      status: this.status,
      maxResultCount: this.maxResultCount,
      skipCount: this.skipCount
    })
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (Response: PagedResultDto<CategoryInlistDto>) => {
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
    this.CategoryService.getListAll()
      .subscribe((response: CategoryInlistDto[]) => {
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
    const ref = this.dialogService.open(CategoryDetailComponent,{
      header: 'Thêm mới loại sản phẩm',
      width: '80%',
      height: '70%'
    });
    ref.onClose.subscribe((data: CategoryDto) => {
      if(data){
          this.loadData();
          this.notificationService.showSuccess("Thêm loại sản phẩm thành công");
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
    const ref = this.dialogService.open(CategoryDetailComponent, {
      data: {
        id: id,
      },
      header: 'Cập nhật sản phẩm',
      width: '80%',
      height: '70%'
    });

    ref.onClose.subscribe((data: CategoryDto) => {
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
    const ref = this.dialogService.open(CategoryViewComponent, {
      data: {
        id: id,
      },
      header: 'Xem',
      width: '80%',
      height: '70%'
    });
  }
  deleteItemsConfirmed(ids: string[]) {
    this.toggleBlockUI(true);
    this.CategoryService
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
  pageChanged(event: any): void {
    this.skipCount = (event.page - 1) * this.maxResultCount;
    this.maxResultCount = event.rows;
    this.loadData();
  }
  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.blockedPanel = true
    } else {
      setTimeout(() => {
        this.blockedPanel = false;
      }, 1000);

    }
  }

}
