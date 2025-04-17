import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { RouterModule } from '@angular/router';
import { Category } from '../models/category.model';
import { CategoryService } from '../services/category.service';
import { OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-category-list',
  imports: [RouterModule, CommonModule],
  standalone: true,
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.css'
})

export class CategoryListComponent implements OnInit {
  categories$?: Observable<Category[]>;

  constructor(private categoryService: CategoryService) {

  }

  ngOnInit(): void {
    this.categoryService.getAllCategories()
    .subscribe({
      next: (response) => {
        this.categories$ = this.categoryService.getAllCategories();
      }
    });
  }
}
