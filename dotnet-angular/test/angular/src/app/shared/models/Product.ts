import { ProductDetail } from './ProductDetail';
import { Supplier } from './Supplier';

export class Product {
  public id: string;
  public name: string;
  public code: string;
  public quantity: int;
  public price: double;
  public details: ProductDetail[];
  public suppliers: Supplier[];
}
