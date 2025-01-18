import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProductService } from '../../services/product.service';
import { ToastModule } from 'primeng/toast';
import { FileUploadModule, FileSelectEvent } from 'primeng/fileupload';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-edit-product',
  standalone: true,
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css'],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ToastModule,
    FileUploadModule,
    InputNumberModule,
    InputTextModule,
    ButtonModule,
  ],
  providers: [MessageService],
})
export class EditProductComponent implements OnInit {
  productForm: FormGroup;
  productId: string = '';
  file: File | null = null;
  chooseLabelText: string = 'Select Image';
  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private router: Router,
    private route: ActivatedRoute,
    private messageService: MessageService
  ) {
    this.productForm = this.fb.group({
      id: [''],
      name: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(500)]],
      price: ['', [Validators.required, Validators.min(0)]],
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.productId = params['id'];
      this.loadProduct();
    });
  }

  loadProduct(): void {
    this.productService.getProducts().subscribe((products) => {
      const product = products.find((p) => p.id === this.productId);
      if (product) {
        this.productForm.patchValue(product);
      }
    });
  }

  onFileSelect(event: FileSelectEvent): void {
    this.file = event.files[0];
    this.chooseLabelText = this.file.name;
    this.messageService.add({
      severity: 'info',
      summary: 'File Selected',
      detail: 'File has been selected successfully.',
    });
  }

  onSubmit(): void {
    if (this.productForm.invalid) {
      this.messageService.add({
        severity: 'error',
        summary: 'Error',
        detail: 'Please fill out the form correctly.',
      });
      return;
    }

    const formValue = this.productForm.value;
    formValue.id = this.productId;

    if (this.file) {
      const reader = new FileReader();
      reader.onload = () => {
        const base64Image = reader.result as string;
        formValue.image = base64Image;
        this.updateProduct(formValue);
      };
      reader.readAsDataURL(this.file);
    } else {
      this.updateProduct(formValue);
    }
  }

  updateProduct(productData: any): void {
    this.productService.updateProduct(productData).subscribe(
      () => {
        this.messageService.add({
          severity: 'success',
          summary: 'Success',
          detail: 'Product updated successfully!',
        });
        setTimeout(() => {
          this.router.navigate(['/']);
        }, 2000); // Delay navigation to allow the message to display
      },
      (error) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Failed to update product. Please try again later.',
        });
      }
    );
  }
}