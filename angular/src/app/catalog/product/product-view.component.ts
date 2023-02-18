import { AuthService, PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ProductDto, ProductInlistDto, ProductsService } from '@proxy/products';
import { Subject, takeUntil } from 'rxjs';
import { MessageService } from 'primeng/api';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { CategoriesService, CategoryInlistDto } from '@proxy/categories';
import { WarehouseDto, WarehouseInlistDto, WarehouseService } from '@proxy/warehouses';
import { forkJoin } from 'rxjs';

@Component({
  providers: [MessageService],
  selector: 'app-product-view',
  templateUrl: './product-view.component.html',
})
export class ProductViewComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  public form: FormGroup;
  selectedEntity = {} as ProductDto;

  //dropdown
  ListCategory: any[] = [];
  categoryId: string = '';
  ListWarehouse: any[] = [];
  WarehouseGuid: string = '';


  constructor(
    private ProductService: ProductsService, 
    private CategoryService: CategoriesService,
    private WarehouseService: WarehouseService,
    private fb: FormBuilder,
    private config: DynamicDialogConfig,
    private ref: DynamicDialogRef,
    private notificationSerivce: NotificationService,
    private utilService: UtilityService,
    ) { }
  ngOnDestroy(): void {
    if (this.ref) {
      this.ref.close();
    }
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  ngOnInit(): void {
    this.buiLdForm();
    this.loadCategory();
    this.loadWarehouse();
    this.initFormData();
  }
  initFormData() {
    //Load data to form
    var categoryService = this.CategoryService.getListAll();
    var warehouseService = this.WarehouseService.getListAll();
    this.toggleBlockUI(true);
    forkJoin({
      categoryService,
      warehouseService,
    })
    .pipe(takeUntil(this.ngUnsubscribe))
    .subscribe({
      next: (response: any) => {
        var categoryService = response.categoryService as CategoryInlistDto[];
        var warehouseService = response.warehouseService as WarehouseInlistDto[];
        categoryService.forEach(element => {
          this.ListCategory.push({
            value: element.id,
            label: element.categoryName,
          });
        });
        warehouseService.forEach(element => {
          this.ListWarehouse.push({
            value: element.id,
            label: element.title,
          });
        });
        //Load edit data to form
        if (this.utilService.isEmpty(this.config.data?.id) == true) {
          //this.getNewSuggestionCode();
          this.toggleBlockUI(false);
        } else {
          this.loadFormDetail(this.config.data?.id);
        }
      }
    });
  }
  loadFormDetail(id: string) {
    this.toggleBlockUI(true);
    this.ProductService.get(id).pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: ProductDto) => {
          this.selectedEntity = response;
          this.buiLdForm();
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        }
      });
  }

  loadCategory() {
    this.CategoryService.getListAll()
    .subscribe((response: CategoryInlistDto[])=>{
      this.ListCategory.push({
        value: null,
        label: "Tất cả",
      });
      response.forEach(element=>{
          this.ListCategory.push({
            value: element.id,
            label: element.categoryName,
          });
      });
    });
  };

  loadWarehouse() {
    this.WarehouseService.getListAll()
    .subscribe((response: WarehouseDto[])=>{
      this.ListWarehouse.push({
        value: null,
        label: "Tất cả",
      });
      response.forEach(element=>{
          this.ListWarehouse.push({
            value: element.id,
            label: element.title,
          });
      });
    });
  };

  private buiLdForm() {
    this.form = this.fb.group({
      productId: new FormControl(this.selectedEntity.productId || null,
        Validators.compose([Validators.required, Validators.maxLength(250)])
      ),
      productName: new FormControl(this.selectedEntity.productName || null, Validators.required),
      categoryId: new FormControl(this.selectedEntity.categoryId || null, Validators.required),
      warehouseGuid: new FormControl(this.selectedEntity.warehouseGuid || null, Validators.required),
      origin: new FormControl(this.selectedEntity.origin || null, Validators.required),
      slug: new FormControl(this.selectedEntity.slug || null, Validators.required),
      quantity: new FormControl(this.selectedEntity.quantity || null, Validators.required),
      price: new FormControl(this.selectedEntity.price || null, Validators.required),
      priceSale: new FormControl(this.selectedEntity.priceSale || null, Validators.required),
      parameter: new FormControl(this.selectedEntity.parameter || null, Validators.required),
      description: new FormControl(this.selectedEntity.description || null, Validators.compose([Validators.required, Validators.maxLength(250)])),
      isActive: new FormControl(this.selectedEntity.isActive ),
      status: new FormControl(this.selectedEntity.status ),
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
