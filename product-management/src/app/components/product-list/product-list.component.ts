import { Component, OnInit } from '@angular/core';
import { Product, ProductService } from '../../services/product.service';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { Router } from '@angular/router';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TooltipModule } from 'primeng/tooltip';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { SkeletonModule } from 'primeng/skeleton';
import { ToolbarModule } from 'primeng/toolbar';
import { ToastModule } from 'primeng/toast';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ConfirmDialogModule } from 'primeng/confirmdialog';

@Component({
  selector: 'app-product-list',
  standalone: true,
  templateUrl: './product-list.component.html',
  providers: [MessageService, ConfirmationService],
  imports: [
    CommonModule,
    TableModule,
    ButtonModule,
    InputTextModule,
    FormsModule,
    TooltipModule,
    ProgressSpinnerModule,
    SkeletonModule,
    ToolbarModule ,
    ToastModule,
    ConfirmDialogModule
  ],
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  loading = true;
  searchQuery: string = '';  
  minPrice?: number;
  maxPrice?: number;

  constructor(private productService: ProductService,
        private router: Router,
        private messageService: MessageService,
        private confirmationService: ConfirmationService
  ) {}

  ngOnInit() {
    this.loadProducts();

  }


  loadProducts(search: string = '', minPrice?: number, maxPrice?: number) {
    this.loading = true;

    this.productService.getProducts(search, minPrice, maxPrice).subscribe(
      (data) => {

      this.products = data;
      this.loading = false;

    },
    (error) => {
      this.loading = false;
      this.messageService.add({
        severity: 'error',
        summary: 'Error',
        detail: 'Failed to load products. Please try again later.',
      });
    });
  }
  searchProducts() {
    this.loadProducts(this.searchQuery, this.minPrice, this.maxPrice);
  }
  editProduct(id: any) {
    this.router.navigate(['/edit/',id]);
  }
  addProduct() {
    this.router.navigate(['/create']);
  }
  deleteProduct(id: string) {
    this.confirmationService.confirm({
      
      message: 'Are you sure you want to delete this product?',
      header: 'Confirm Deletion',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.productService.deleteProduct(id).subscribe(
          () => {
            this.loadProducts();
            this.messageService.add({
              severity: 'success',
              summary: 'Success',
              detail: 'Product deleted successfully.',
            });
          },
          (error) => {
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: 'Failed to delete product. Please try again later.',
            });
          }
        );
      },
    });
  }
}
