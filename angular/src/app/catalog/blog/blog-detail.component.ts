import { PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { BlogDto, BlogInlistDto, BlogsService } from '@proxy/blogs';
import { Subject, takeUntil } from 'rxjs';
import { MessageService } from 'primeng/api';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { UtilityService } from 'src/app/shared/services/utility.service';
@Component({
  providers: [MessageService],
  selector: 'app-blog-detail',
  templateUrl: './blog-detail.component.html'
})
export class BlogDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  public form: FormGroup;
  selectedEntity = {} as BlogDto;
  constructor(
    private BlogService: BlogsService,
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
    this.BlogService.get(id).pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: BlogDto) => {
          this.selectedEntity = response;
          this.buiLdForm();
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        }
      });
  }

  private buiLdForm() {
    this.form = this.fb.group({
      title: new FormControl(this.selectedEntity.title || null,
        Validators.compose([Validators.required, Validators.maxLength(250)])
      ),
      description: new FormControl(this.selectedEntity.description || null,
        Validators.compose([Validators.required, Validators.maxLength(250)])
      ),
      status: new FormControl(this.selectedEntity.status),
    });
  }
  saveChange() {
    this.toggleBlockUI(true);

    if (this.utilService.isEmpty(this.config.data?.id) == true) {
      this.BlogService
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
      this.BlogService
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
