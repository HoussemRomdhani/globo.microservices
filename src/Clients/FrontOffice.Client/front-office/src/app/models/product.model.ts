import { Guid } from 'guid-ts';

export class ProductModel {
  id: Guid;
  name: string;
  price: number;
  description: string;
  image: string;
  categoryId: Guid;
}


