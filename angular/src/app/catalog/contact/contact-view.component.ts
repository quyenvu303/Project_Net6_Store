import { PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ContactDto, ContactInlistDto, ContactsService } from '@proxy/contacts';
import { Subject, takeUntil } from 'rxjs';
import { MessageService } from 'primeng/api';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { UtilityService } from 'src/app/shared/services/utility.service';
@Component({
  providers: [MessageService],
  selector: 'app-contact-view',
  templateUrl: './contact-view.component.html'
})
export class ContactViewComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  public form: FormGroup;
  selectedEntity = {} as ContactDto;
  constructor(
    private ContactService: ContactsService,
    private fb: FormBuilder,
    private config: DynamicDialogConfig,
    private ref: DynamicDialogRef,
    private notificationSerivce: NotificationService,
    private utilService: UtilityService,
  ) { }
  validationMessages = {
    title: [{ type: 'required', message: 'Không được để trống' }],
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
    this.ContactService.get(id).pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: ContactDto) => {
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
      this.form.controls['title'].disable();
      this.form.controls['status'].disable();
      this.form.controls['description'].disable();
    } 
  }
  private buiLdForm() {
    this.form = this.fb.group({
      title: new FormControl(this.selectedEntity.title || null,
        Validators.compose([Validators.required, Validators.maxLength(250)])
      ),
      description: new FormControl(this.selectedEntity.description || null,
        Validators.compose([Validators.required, Validators.maxLength(250)])
      ),
      status: new FormControl(this.selectedEntity.status ),
    });
  }
  saveChange() {
    this.toggleBlockUI(true);

    if (this.utilService.isEmpty(this.config.data?.id) == true) {
      this.ContactService
        .create(this.form.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe({
          next: () => {
            this.toggleBlockUI(false);

            this.ref.close(this.form.value);
          },
          error: err => {
            this.notificationSerivce.showError(err.error.error.message);

            this.toggleBlockUI(false);
          },
        });
    } else {
      this.ContactService
        .update(this.config.data?.id, this.form.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe({
          next: () => {
            this.toggleBlockUI(false);
            this.ref.close(this.form.value);
          },
          error: err => {
            this.notificationSerivce.showError(err.error.error.message);
            this.toggleBlockUI(false);
          },
        });
    }
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
