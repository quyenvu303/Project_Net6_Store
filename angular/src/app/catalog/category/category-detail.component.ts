import { Component, OnDestroy, OnInit } from '@angular/core';
import { CategoriesService, CategoryDto, CategoryInlistDto } from '@proxy/categories';
import { forkJoin, Subject, take, takeUntil } from 'rxjs';
import { Message, MessageService } from 'primeng/api';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
@Component({
  providers: [MessageService],
  selector: 'app-category-detail',
  templateUrl: './category-detail.component.html',
})
export class CategoryDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  public form: FormGroup;

  selectedEntity = {} as CategoryDto;
  parentId: string = '';
  //dropdown
  ListCategory: any[] = [];

  constructor(
    private CategoryService: CategoriesService,
    private fb: FormBuilder,
    private config: DynamicDialogConfig,
    private ref: DynamicDialogRef,
    private notificationSerivce: NotificationService,
    private utilService: UtilityService,
    ) { }

  validationMessages = {
    categoryId: [{ type: 'required', message: 'Bạn phải nhập mã duy nhất' }],
    categoryName: [
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
    this.buildForm();
    this.loadCategory();
    this.initFormData();
  }
  loadCategory() {
    this.CategoryService.getListAll()
    .subscribe((response: CategoryInlistDto[])=>{
      this.ListCategory.push({
        value: null,
        label: "Bỏ chọn",
      });
      response.forEach(element=>{
          this.ListCategory.push({
            value: element.id,
            label: element.categoryName,
          });
      });
    });
  };

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
    this.CategoryService.get(id).pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: CategoryDto) => {
          this.selectedEntity = response;
          this.buildForm();
          this.setMode('open');
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        }
      });
  }

  private buildForm() {
    this.form = this.fb.group({
      categoryId: new FormControl(this.selectedEntity.categoryId || null,
        Validators.compose([Validators.required, Validators.maxLength(250)])
      ),
      categoryName: new FormControl(this.selectedEntity.categoryName || null,
        Validators.compose([Validators.required, Validators.maxLength(250)])
      ),
      parentId: new FormControl(this.selectedEntity.parentId),
      sortOrder: new FormControl(this.selectedEntity.sortOrder || null, Validators.required),
      description: new FormControl(this.selectedEntity.description || null, Validators.compose([Validators.required, Validators.maxLength(250)])),
      isActive: new FormControl(this.selectedEntity.isActive ),
    });
  }

  setMode(mode: string) {
    if (mode == 'open') {
      this.form.controls['categoryId'].disable();
      this.form.controls['categoryName'].disable();
      this.form.controls['parentId'].disable();
      this.form.controls['sortOrder'].disable();
      this.form.controls['isActive'].disable();
      this.form.controls['description'].disable();
    } 
  }


  saveChange() {
    this.toggleBlockUI(true);

    if (this.utilService.isEmpty(this.config.data?.id) == true) {
      this.CategoryService
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
      this.CategoryService
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
      }, 500);

    }
  }

}
