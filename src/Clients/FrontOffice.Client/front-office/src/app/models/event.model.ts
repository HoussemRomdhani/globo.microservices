import { Guid } from 'guid-ts';

export class EventModel {
  id: Guid;
  name: string;
  artist: string;
  price: number;
  description: string;
  date: Date;
  imageUrl: string;
  categoryId: Guid;
}
