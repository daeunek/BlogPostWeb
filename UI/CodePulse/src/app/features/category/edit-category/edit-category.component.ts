import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { UpdateCategoryRequest } from '../models/update-category-request.model';
import { Router } from '@angular/router';



@Component({
  selector: 'app-edit-category',
  imports: [FormsModule, CommonModule],
  templateUrl: './edit-category.component.html',
  styleUrl: './edit-category.component.css'
})
export class EditCategoryComponent implements OnInit, OnDestroy{

  id : string | null = null;
  paramsSubscription? : Subscription;
  editCategorySubscription? : Subscription;
  category?: Category;

  constructor(private route : ActivatedRoute,
    private categoryService: CategoryService, private router: Router) {   

  }

  ngOnInit(): void {
       this.paramsSubscription = this.route.params.subscribe({
        next: (params) => {
          this.id = params['id'];

          if (this.id){
            this.categoryService.getCategory(this.id).subscribe({
              next: (response) => {
                this.category = response;
              }
            })
          }
        },
 
      });
  }

  onFormSubmit(){
    const UpdateCategoryRequest: UpdateCategoryRequest ={
      name : this.category?.name ?? '',
      urlHandle: this.category?.urlHandle ?? ''
    };

    //pass this object to service
    if (this.id) {
      this.editCategorySubscription = this.categoryService.updateCategory(this.id, UpdateCategoryRequest)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('admin/categories');    //if success then go to another page here list page, so add router to constructor
        }
      })

    }
  }

  onDelete(): void {
    if (this.id){
      this.categoryService.deleteCategory(this.id)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('admin/categories');   
        }
      })

    }
  }

  ngOnDestroy() : void {
    this.paramsSubscription?.unsubscribe();
    this.editCategorySubscription?.unsubscribe();
  }


}
