import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PermissionGuard } from '@abp/ng.core';
import { CategoryComponent } from './category/category.component';
import { ProductComponent } from './product/product.component';
import { ShippingComponent } from './shipping/shipping.component';
import { OrderComponent } from './order/order.component';
import { WarehouseComponent } from './warehouse/warehouse.component';
import { ContactComponent } from './contact/contact.component';
import { BlogComponent } from './blog/blog.component';
import { BannerComponent } from './banner/banner.component';

/* const routes: Routes = [{ path: '', component: CategoryComponent }]; */

const routes: Routes = [
  {
    path: 'category',
    component: CategoryComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPolicy: 'StoreAdminCatalog.Category',
    },
  },
  {
    path: 'product',
    component: ProductComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPolicy: 'StoreAdminCatalog.Product',
    },
  },
  {
    path: 'shipping',
    component: ShippingComponent,
  },
  {
    path: 'order',
    component: OrderComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPolicy: 'StoreAdminCatalog.Order',
    },
  },
  {
    path: 'warehouse',
    component: WarehouseComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPolicy: 'StoreAdminCatalog.Warehouse',
    },
  },
  {
    path: 'blog',
    component: BlogComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPolicy: 'StoreAdminCatalog.Blog',
    },
  },
  {
    path: 'contact',
    component: ContactComponent,
   
  },
  {
    path: 'banner',
    component: BannerComponent,
   
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CatalogRoutingModule { }
