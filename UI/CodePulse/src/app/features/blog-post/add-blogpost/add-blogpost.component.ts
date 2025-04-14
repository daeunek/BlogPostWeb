import { Component } from '@angular/core';
import { AddBlogpost } from '../models/add-blogpost.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BlogPostService } from '../services/blog-post.service';
import { Router } from '@angular/router';
import { MarkdownModule } from 'ngx-markdown';
import { OnInit } from '@angular/core';
import { Category } from '../../category/models/category.model';
import { CategoryService } from '../../category/services/category.service';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-add-blogpost',
  imports: [FormsModule, CommonModule,   MarkdownModule],
  standalone: true,
  templateUrl: './add-blogpost.component.html',
  styleUrl: './add-blogpost.component.css'
})
export class AddBlogpostComponent implements OnInit{
  model : AddBlogpost;
  categories$? : Observable<Category[]>;

  constructor (private blogPostService : BlogPostService, private router: Router, private categoryService : CategoryService) {
    this.model = {
      title : '',
      shortDescription : '',
      content: '',
      featuredImageUrl: '',
      urlHandle : '',
      author: '',
      publishedDate : new Date(),
      isVisible : true,
      categories : []
    }
  }

  onFormSubmit() : void{
    console.log(this.model);
    this.blogPostService.createBlogPost(this.model).subscribe({
      next:(response) => {
        this.router.navigateByUrl('/admin/blogposts');
      }
    })

  }

  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();
  }

}
