import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Category } from '../models/category.model';
import { CategoryService } from '../services/category.service';
import { OnInit } from '@angular/core';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';




@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [RouterModule, CommonModule, HttpClientModule],
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.css'
})

export class CategoryListComponent implements OnInit {

  
  categories$? : Observable<Category[]>;

  constructor(private categoryService: CategoryService) {

  }

  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();
  }
}
