import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Product {
  id: string;
  name: string;
  description: string;
  price: number;
  createdDate: Date;
  imageUrl: string;
}

export interface CreateProductDto {
  name: string;
  description: string;
  price: number;
  image?: string; 
}
export interface UpdateProductDto extends Partial<CreateProductDto> {
  id: string;
}
@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private apiUrl = 'https://localhost:7035/products';

  constructor(private http: HttpClient) {}

  getProducts(search: string = '', minPrice?: number, maxPrice?: number): Observable<Product[]> {
    let params = new HttpParams().set('search', search);

    if (minPrice !== undefined && minPrice !== null) {
      params = params.set('minPrice', minPrice.toString());
    }

    if (maxPrice !== undefined && maxPrice !== null) {
      params = params.set('maxPrice', maxPrice.toString());
    }
    return this.http.get<Product[]>(this.apiUrl, { params });
  }

  createProduct(product: CreateProductDto): Observable<void> {

    return this.http.post<void>(this.apiUrl, product);
}

  updateProduct(product: UpdateProductDto): Observable<void> {
   console.log(product);
    return this.http.put<void>(this.apiUrl, product);
}

  deleteProduct(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
