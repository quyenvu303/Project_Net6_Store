import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';

import { PanelModule } from 'primeng/panel';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { BlockUIModule } from 'primeng/blockui';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import {ProgressSpinnerModule} from 'primeng/progressspinner';
import {DynamicDialogModule} from 'primeng/dynamicdialog';
import { CatalogRoutingModule } from './catalog-routing.module';

import { CategoryComponent } from './category/category.component';
import { CategoryDetailComponent } from '../catalog/category/category-detail.component';
import { CategoryViewComponent } from '../catalog/category/category-view.component';
import { ShippingComponent } from './shipping/shipping.component';
import { ShippingDetailComponent } from './shipping/shipping-detail.component';
import { ShippingViewComponent } from './shipping/shipping-view.component';
import { WarehouseComponent } from './warehouse/warehouse.component';
import { WarehouseDetailComponent } from './warehouse/warehouse-detail.component';
import { WarehouseViewComponent } from './warehouse/warehouse-view.component';
import { ProductComponent } from './product/product.component';
import { ProductDetailComponent } from './product/product-detail.component';
import { ProductViewComponent } from './product/product-view.component';
import { OrderComponent } from './order/order.component';
import { ContactComponent } from './contact/contact.component';
import { ContactDetailComponent } from './contact/contact-detail.component';
import { ContactViewComponent } from './contact/contact-view.component';
import { BlogComponent } from './blog/blog.component';
import { BlogDetailComponent } from './blog/blog-detail.component';
import { BlogViewComponent } from './blog/blog-view.component';

import { InputNumberModule } from 'primeng/inputnumber';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { EditorModule } from 'primeng/editor';
import { StoreSharedModule } from '../shared/modules/Store-shared.module';
import { BadgeModule } from 'primeng/badge';
import { ImageModule } from 'primeng/image';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { CalendarModule } from 'primeng/calendar';

@NgModule({
  declarations: [
    CategoryComponent, 
    CategoryDetailComponent,
    CategoryViewComponent,
    ShippingComponent,
    ShippingDetailComponent,
    ShippingViewComponent,
    OrderComponent,
    ProductComponent,
    ProductDetailComponent,
    ProductViewComponent,
    WarehouseComponent,
    WarehouseDetailComponent,
    WarehouseViewComponent,
    ContactComponent,
    ContactDetailComponent,
    ContactViewComponent,
    BlogComponent,
    BlogDetailComponent,
    BlogViewComponent
  ],
  imports: [
    SharedModule,
    CatalogRoutingModule,
    PanelModule,
    TableModule,
    PaginatorModule,
    BlockUIModule,
    ButtonModule,
    DropdownModule,
    InputTextModule,
    ProgressSpinnerModule,
    DynamicDialogModule,
    InputNumberModule,
    CheckboxModule,
    InputTextareaModule,
    EditorModule,
    StoreSharedModule,
    BadgeModule,
    ImageModule,
    ConfirmDialogModule,
    CalendarModule,
    
  ],
  entryComponents: [
    CategoryDetailComponent,
    CategoryViewComponent,
    ShippingDetailComponent,
    ShippingViewComponent,
    WarehouseDetailComponent,
    WarehouseViewComponent,
    ProductDetailComponent,
    ProductViewComponent,
    ContactDetailComponent,
    ContactViewComponent,
    BlogDetailComponent,
    BlogViewComponent
  ]
})
export class CatalogModule { }
