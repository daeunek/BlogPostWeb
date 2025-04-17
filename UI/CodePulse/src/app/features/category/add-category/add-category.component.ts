import { Component, OnDestroy } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { CategoryService } from '../services/category.service';
import { HttpClientModule } from '@angular/common/http';
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-add-category',
  standalone: true,  
  imports: [FormsModule, HttpClientModule],
  providers: [CategoryService],
  templateUrl: './add-category.component.html',
  styleUrl: './add-category.component.css'
})

export class AddCategoryComponent implements OnDestroy {
  model: AddCategoryRequest;

  // This subscription is used to unsubscribe from the observable when the component is destroyed
  private addCategorySubscription?: Subscription

  constructor(private categoryService: CategoryService) {
    this.model = {
      name: '',
      urlHandle : '',
    }
  }
  onFormSubmit() {
    this.addCategorySubscription = this.categoryService.addCategory(this.model)
    .subscribe ({
      next: (response) => {
        console.log('Category added successfull!')
      }
    })
  }

  ngOnDestroy() {
    this.addCategorySubscription?.unsubscribe();
  }
}
