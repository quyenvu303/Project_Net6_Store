import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from './service/app.layout.service';

@Component({
  selector: 'app-menu',
  templateUrl: './app.menu.component.html',
})
export class AppMenuComponent implements OnInit {
  model: any[] = [];

  constructor(public layoutService: LayoutService) { }

  ngOnInit() {
    this.model = [
      {
        items: [
          {
            label: 'Danh sách sản phẩm',
            items: [
              {
                label: 'Danh sách loại sản phẩm',
                icon: 'pi pi-list',
                routerLink: ['/catalog/category'],
              },
              {
                label: 'Danh sách sản phẩm',
                icon: 'pi pi-microsoft',
                routerLink: ['/catalog/product'],
                //permission: 'Catalog.product',
              },
            ]
          },
          {
            label: 'Quản lý đơn hàng',
            items: [
              {
                label: 'Quản lý đơn hàng',
                icon: 'pi pi-shopping-cart',
                routerLink: ['/catalog/order']
              }
            ]
          },
          {
            label: 'Quản lý kho',
            items: [
              {
                label: 'Kho',
                icon: 'fa fa-warehouse',
                routerLink: ['/catalog/warehouse'],
                //permission: 'Catalog.warehouse',
              }
            ]
          },
          {
            label: 'Danh mục',
            items: [
              {
                label: 'Shiping',
                icon: 'fas fa-shipping-fast',
                routerLink: ['/catalog/shipping']
              },
              {
                label: 'Tin tức',
                icon: 'fa fa-newspaper-o',
                routerLink: ['/catalog/blog']
              },
              {
                label: 'Contact',
                icon: 'fa fa-address-card',
                routerLink: ['/catalog/contact']
              }
            ]
          },
          {
            label: 'Hệ thống',
            items: [
              {
                label: 'Phân quyền',
                icon: 'pi pi-sitemap',
                routerLink: ['/system/role'],
               // permission: 'AbpIdentity.Roles',
              },
              {
                label: 'Danh sách người dùng',
                icon: 'pi	pi-users',
                routerLink: ['/system/user'],
                //permission: 'AbpIdentity.Users',
              },
            ]
          },
        ]
      },
    ];
  }
}

