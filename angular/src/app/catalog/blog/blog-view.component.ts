import { PagedResultDto } from '@abp/ng.core';
import { ChangeDetectorRef,Component, OnDestroy, OnInit } from '@angular/core';
import { BlogDto, BlogInlistDto, BlogsService } from '@proxy/blogs';
import { Subject, takeUntil } from 'rxjs';
import { MessageService } from 'primeng/api';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { DomSanitizer } from '@angular/platform-browser';
@Component({
  providers: [MessageService],
  selector: 'app-blog-view',
  templateUrl: './blog-view.component.html'
})
export class BlogViewComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  public form: FormGroup;
  selectedEntity = {} as BlogDto;
      // Image ảnh
      public image;
  constructor(
    private BlogService: BlogsService,
    private fb: FormBuilder,
    private config: DynamicDialogConfig,
    private ref: DynamicDialogRef,
    private notificationSerivce: NotificationService,
    private utilService: UtilityService,
    private cd: ChangeDetectorRef,
    private sanitizer: DomSanitizer
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
          this.loadThumbnail(this.selectedEntity.image);
          this.buiLdForm();
          this.setMode('open');
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        }
      });
  }
  loadThumbnail(fileName: string) {
    this.BlogService
      .getImage(fileName)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: string) => {
          var fileExt = this.selectedEntity.image?.split('.').pop();
          this.image = this.sanitizer.bypassSecurityTrustResourceUrl(
            `data:image/${fileExt};base64, ${response}`
          );
        },
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
  setMode(mode: string) {
    if (mode == 'open') {
      this.form.controls['title'].disable();
      this.form.controls['description'].disable();
      this.form.controls['status'].disable();
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
