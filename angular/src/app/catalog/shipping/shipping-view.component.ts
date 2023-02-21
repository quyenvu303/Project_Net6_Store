import { PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ShippingDto, ShippingInlistDto, ShippingService } from '@proxy/shippings';
import { Subject, takeUntil } from 'rxjs';
import { MessageService } from 'primeng/api';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { UtilityService } from 'src/app/shared/services/utility.service';
@Component({
  providers: [MessageService],
  selector: 'app-shipping-view',
  templateUrl: './shipping-view.component.html'
})
export class ShippingViewComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  public form: FormGroup;
  selectedEntity = {} as ShippingDto;
  constructor(
    private ShippingService: ShippingService,
    private fb: FormBuilder,
    private config: DynamicDialogConfig,
    private ref: DynamicDialogRef,
    private notificationSerivce: NotificationService,
    private utilService: UtilityService,
  ) { }
  validationMessages = {
    shippingName: [{ type: 'required', message: 'Không được để trống' }],
    shippingFee: [{ type: 'required', message: 'Không được để trống' }],
  };

  ngOnDestroy(): void {
    if (this.ref) {
      this.ref.close();
    }
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  ngOnInit(): void {
    this.buiLdForm();
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
    this.ShippingService.get(id).pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: ShippingDto) => {
          this.selectedEntity = response;
          this.buiLdForm();
          this.setMode('open');
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        }
      });
  }
  setMode(mode: string) {
    if (mode == 'open') {
      this.form.controls['shippingName'].disable();
      this.form.controls['shippingFee'].disable();
      this.form.controls['description'].disable();
      this.form.controls['status'].disable();
    } 
  }
  private buiLdForm() {
    this.form = this.fb.group({
      shippingName: new FormControl(this.selectedEntity.shippingName || null,
        Validators.compose([Validators.required, Validators.maxLength(250)])
      ),
      shippingFee: new FormControl(this.selectedEntity.shippingFee || null, Validators.required),
      description: new FormControl(this.selectedEntity.description || null, Validators.compose([Validators.required, Validators.maxLength(250)])),
      status: new FormControl(this.selectedEntity.status ),
    });
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
