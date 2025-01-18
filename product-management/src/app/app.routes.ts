import { RouterModule, Routes } from '@angular/router';
import { ProductListComponent } from './components/product-list/product-list.component';
import { CreateProductComponent } from './components/create-product/create-product.component';
import { EditProductComponent } from './components/edit-product/edit-product.component';
import { NgModule } from '@angular/core';

export const routes: Routes = [

    { path: '', component: ProductListComponent }, 
    { path: 'create', component: CreateProductComponent },
    { path: 'edit/:id', component: EditProductComponent },  
    { path: '**', redirectTo: '', pathMatch: 'full' },
];
@NgModule({
    imports: [RouterModule.forRoot(routes, {})],
    exports: [RouterModule],
  })
  export class AppRoutingModule {}