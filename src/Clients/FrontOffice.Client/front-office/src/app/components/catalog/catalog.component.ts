import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Guid } from 'guid-ts';
import { EventModel } from 'src/app/models/event.model';
import { combineLatest, BehaviorSubject, EMPTY, Subject, Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { EventCatalogService } from 'src/app/services/eventCatalog.service';
import { DomSanitizer } from '@angular/platform-browser';


@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent implements OnInit {


  constructor(private sanitizer:DomSanitizer,
              private eventCatalogService: EventCatalogService) {
  }

  ngOnInit() {

  }

  // onPurchaseHandler($event) {
  //  console.log('event', $event);
  // }

  displayImg(image: string){
    return this.sanitizer.bypassSecurityTrustResourceUrl('data:image/png;base64,'+ image);
  }

  vm$ = combineLatest([
    this.eventCatalogService.getProducts(),
    this.eventCatalogService.getCategories()
  ])
  .pipe(
    map(([products, categories]) =>
      ({ products, categories }))
  );
}




