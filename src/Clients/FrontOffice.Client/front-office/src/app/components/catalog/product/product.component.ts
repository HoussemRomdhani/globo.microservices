import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Params } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { ProductModel } from 'src/app/models/product.model';
import { EventCatalogService } from 'src/app/services/eventCatalog.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  product: ProductModel;

  constructor(private route: ActivatedRoute,
              private eventCatalogService: EventCatalogService,
              private sanitizer:DomSanitizer) {

    this.route.params.pipe(
      switchMap((params: Params) =>  this.eventCatalogService.getProduct(params['id']))
    ).subscribe(
      (response) => {
         this.product = response;
      }
  );
  }

  ngOnInit(): void {
  }

  displayImg(image: string){
    return this.sanitizer.bypassSecurityTrustResourceUrl('data:image/png;base64,'+ image);
  }

}
