import { Component } from '@angular/core';
import { CreateProductDto, ProductService } from '../../services/product.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { FileUploadModule, FileSelectEvent } from 'primeng/fileupload'; 
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common'; 

@Component({
  selector: 'app-create-product',
  standalone: true,
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.css'],
  imports: [  
    CommonModule, 
    ReactiveFormsModule,
    ToastModule,
    FileUploadModule,
    InputNumberModule,
    InputTextModule,
    ButtonModule,],
    providers: [MessageService],


})
export class CreateProductComponent {
  productForm: FormGroup;
  file: File | null = null;
  chooseLabelText: string = 'Select Image';
    constructor(
      private fb: FormBuilder,
      private productService: ProductService,
      private router: Router,
      private messageService: MessageService


    ) {
      this.productForm = this.fb.group({
        name: ['', [Validators.required, Validators.maxLength(100)]],
        description: ['', [Validators.required, Validators.maxLength(500)]],
        price: [null, [Validators.required, Validators.min(0)]],
      });
    }   
    onFileChange(event: FileSelectEvent) {
      this.file = event.files[0];
      this.chooseLabelText = this.file.name;
      this.messageService.add({
        severity: 'info',
        summary: 'File Selected',
        detail: 'File has been selected successfully.',
      });
    }

  onSubmit() {
    if (this.productForm.invalid) {
      this.messageService.add({
        severity: 'error',
        summary: 'Error',
        detail: 'Please fill out the form correctly.',
      });
      return;
    }
    const formValue = this.productForm.value;
    

      if (this.file) {

        const reader = new FileReader();
        reader.onload = () => {
          const base64Image = reader.result as string;
          formValue.image = base64Image;
          this.createProduct(formValue);

   };
        reader.readAsDataURL(this.file); 
      }
       else {
        this.createProduct(formValue);

      }
    
  }
  createProduct(productData: CreateProductDto) {
    this.productService.createProduct(productData).subscribe(
      () => {
        this.messageService.add({
          severity: 'success',
          summary: 'Success',
          detail: 'Product created successfully!',
        });
        setTimeout(() => {
          this.router.navigate(['/']);
        }, 2000);      },
      (error) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Failed to create product. Please try again later.',
        });
      }
    );
  }

}