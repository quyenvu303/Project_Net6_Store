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

/* const routes: Routes = [{ path: '', component: CategoryComponent }]; */

const routes: Routes = [
    {
      path: 'category',
      component: CategoryComponent,
     // canActivate: [PermissionGuard],
      data: {
        requiredPolicy: 'Catalog.category',
      },
    },
    {
      path: 'product',
      component: ProductComponent,
     // canActivate: [PermissionGuard],
      data: {
        requiredPolicy: 'Catalog.product',
      },
    },
    {
      path: 'shipping',
      component: ShippingComponent,
     // canActivate: [PermissionGuard],
      data: {
        requiredPolicy: 'Catalog.shipping',
      },
    },
    {
      path: 'order',
      component: OrderComponent,
     // canActivate: [PermissionGuard],
      data: {
        requiredPolicy: 'Catalog.order',
      },
    },
    {
      path: 'warehouse',
      component: WarehouseComponent,
      //canActivate: [PermissionGuard],
      data: {
        requiredPolicy: 'Catalog.warehouse',
      },
    },
    {
      path: 'blog',
      component: BlogComponent,
     // canActivate: [PermissionGuard],
      data: {
        requiredPolicy: 'Catalog.blog',
      },
    },
    {
      path: 'contact',
      component: ContactComponent,
    //  canActivate: [PermissionGuard],
      data: {
        requiredPolicy: 'Catalog.contact',
      },
    },
  ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CatalogRoutingModule {}
