import { PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { BlogDto, BlogInlistDto, BlogsService } from '@proxy/blogs';
import { Subject,  takeUntil } from 'rxjs';
import { Message, MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ConfirmationService } from 'primeng/api';
import { BlogDetailComponent } from './blog-detail.component';
import { BlogViewComponent } from './blog-view.component';
@Component({
  providers: [MessageService],
  selector: 'app-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.scss'],
})
export class BlogComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  items: BlogInlistDto[] = [];
  public selectedItems: BlogInlistDto[] = [];
  //paging variabls
  public skipCount: number = 0;
  public maxResultCount: number = 10;
  public totalCount: number;
  msgs1: Message[];

  // filter
  keyword: string = '';
  ListStatus: any[] = [];
  status: boolean = true;

  constructor(
    private BlogService: BlogsService,
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
    this.BlogService.getListFilter({
      keyword: this.keyword,
      status: this.status,
      maxResultCount: this.maxResultCount,
      skipCount: this.skipCount
    })
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (Response: PagedResultDto<BlogInlistDto>) => {
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
    this.BlogService.getListAll()
      .subscribe((response: BlogInlistDto[]) => {
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
    const ref = this.dialogService.open(BlogDetailComponent,{
      header: 'Thêm mới tin tức',
      width: '70%',
      height: '70%'
    });
    ref.onClose.subscribe((data: BlogDto) => {
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
    const ref = this.dialogService.open(BlogDetailComponent, {
      data: {
        id: id,
      },
      header: 'Cập nhật',
      width: '70%',
      height: '70%'
    });

    ref.onClose.subscribe((data: BlogDto) => {
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
    const ref = this.dialogService.open(BlogViewComponent, {
      data: {
        id: id,
      },
      header: 'Xem',
      width: '70%',
      height: '70%'
    });
  }

  deleteItemsConfirmed(ids: string[]) {
    this.toggleBlockUI(true);
    this.BlogService
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
