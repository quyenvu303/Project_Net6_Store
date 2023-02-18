import { PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ContactDto, ContactInlistDto, ContactsService } from '@proxy/contacts';
import { Subject,  takeUntil } from 'rxjs';
import { Message, MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ConfirmationService } from 'primeng/api';
import { ContactDetailComponent } from './contact-detail.component';
import { ContactViewComponent } from './contact-view.component';
@Component({
  providers: [MessageService],
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss'],
})
export class ContactComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  items: ContactInlistDto[] = [];
  public selectedItems: ContactInlistDto[] = [];
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
    private ContactService: ContactsService,
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
    this.ContactService.getListFilter({
      keyword: this.keyword,
      status: this.status,
      maxResultCount: this.maxResultCount,
      skipCount: this.skipCount
    })
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (Response: PagedResultDto<ContactInlistDto>) => {
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
    this.ContactService.getListAll()
      .subscribe((response: ContactInlistDto[]) => {
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
    const ref = this.dialogService.open(ContactDetailComponent,{
      header: 'Thêm mới contact',
      width: '70%',
      height: '70%'
    });
    ref.onClose.subscribe((data: ContactDto) => {
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
    const ref = this.dialogService.open(ContactDetailComponent, {
      data: {
        id: id,
      },
      header: 'Cập nhật',
      width: '70%',
      height: '70%'
    });

    ref.onClose.subscribe((data: ContactDto) => {
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
    const ref = this.dialogService.open(ContactViewComponent, {
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
    this.ContactService
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
