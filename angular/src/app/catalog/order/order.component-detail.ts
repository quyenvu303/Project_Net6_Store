import { AuthService, PagedResultDto } from '@abp/ng.core';
import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { OrderItemService } from '@proxy/order-items';
import { OrderItemDto } from '@proxy/orders';
import { Subject, take, takeUntil } from 'rxjs';
import { Message, MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { ConfirmationService } from 'primeng/api';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  providers: [MessageService],
  selector: 'app-order-detail',
  templateUrl: './order.component-detail.html',
})
export class OrderDetailComponent implements OnInit, OnDestroy{
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  public form: FormGroup;
  selectedEntity:any[] = [];
  imageEntity = {} as OrderItemDto;
  items: OrderItemDto[] = [];
   // Image ảnh
   public productImage;

  constructor(
    private OrderItemService: OrderItemService,
    private fb: FormBuilder,
    private config: DynamicDialogConfig,
    private ref: DynamicDialogRef,
    private dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService,
    private utilService: UtilityService,
    private cd: ChangeDetectorRef,
    private sanitizer: DomSanitizer
    ){}
  ngOnDestroy(): void {
    if (this.ref) {
      this.ref.close();
    }
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  ngOnInit(): void {
    this.initFormData();
  }

  initFormData() {
    //Load data to form
    if (this.utilService.isEmpty(this.config.data?.id) == true) {
      this.toggleBlockUI(false);
    } else {
      this.loadFormDetail(this.config.data?.id);
    }
  }
  loadFormDetail(id: string) {
    this.toggleBlockUI(true);
    this.OrderItemService.getOrderListItems(id).pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: OrderItemDto[]) => {
          this.items = response;
          for (let i = 0; i < response.length; i++) {
            this.loadThumbnail(this.items[0].productImage);
          }
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      })
  }

  loadThumbnail(fileName: string) {
    this.OrderItemService
      .getImage(fileName)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: string) => {
          for (let i = 0; i < this.items.length; i++) {
            var fileExt = this.items[i].productImage?.split('.').pop();
          }
         
          this.productImage = this.sanitizer.bypassSecurityTrustResourceUrl(
            `data:image/${fileExt};base64, ${response}`
          );
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
