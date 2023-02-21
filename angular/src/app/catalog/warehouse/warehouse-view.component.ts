import { AuthService, PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { WarehouseDto, WarehouseInlistDto, WarehouseService } from '@proxy/warehouses';
import { Subject, takeUntil } from 'rxjs';
import { MessageService } from 'primeng/api';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { UtilityService } from 'src/app/shared/services/utility.service';

@Component({
  providers: [MessageService],
  selector: 'app-warehouse-view',
  templateUrl: './warehouse-view.component.html',
})
export class WarehouseViewComponent implements OnInit, OnDestroy{
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  items: WarehouseInlistDto[] = [];
  selectedEntity = {} as WarehouseDto;
  public form: FormGroup;


  constructor(
    private WarehouseService: WarehouseService,
    private fb: FormBuilder,
    private config: DynamicDialogConfig,
    private ref: DynamicDialogRef,
    private notificationSerivce: NotificationService,
    private utilService: UtilityService,
    ){}
    validationMessages = {
      warehouseId: [{ type: 'required', message: 'Không được để trống' }],
      title: [
        { type: 'required', message: 'Bạn phải nhập tên' },
        { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' },
      ],
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
    this.WarehouseService.get(id).pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: WarehouseDto) => {
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

  private buiLdForm() {
    this.form = this.fb.group({
      warehouseId: new FormControl(this.selectedEntity.warehouseId || null,
        Validators.compose([Validators.required, Validators.maxLength(250)])
      ),
      title: new FormControl(this.selectedEntity.title || null, Validators.required),
      status: new FormControl(this.selectedEntity.status ),
    });
  }
 

  setMode(mode: string) {
    if (mode == 'open') {
      this.form.controls['warehouseId'].disable();
      this.form.controls['title'].disable();
      this.form.controls['status'].disable();
    } 
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
