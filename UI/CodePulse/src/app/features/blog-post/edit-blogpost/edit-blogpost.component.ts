import { Component, OnDestroy, OnInit, AfterViewChecked } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { BlogPostService } from '../services/blog-post.service';
import { BlogPost } from '../models/blogpost.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MarkdownModule } from 'ngx-markdown';
import { Category } from '../../category/models/category.model';
import { CategoryService } from '../../category/services/category.service';
import { Observable } from 'rxjs';
import { UpdateBlogPost } from '../models/update-blogpost.model';

@Component({
  selector: 'app-edit-blogpost',
  standalone: true, // Add standalone: true since you're using imports array
  imports: [FormsModule, CommonModule, MarkdownModule],
  templateUrl: './edit-blogpost.component.html',
  styleUrl: './edit-blogpost.component.css'
})
export class EditBlogpostComponent implements OnInit, OnDestroy {
  id: string | null = null;
  model?: BlogPost;
  categories$?: Observable<Category[]>;
  selectedCategories? : string[];     // Blogpost's response selected categories will be here 

  routeSubscription?: Subscription;
  updateBlogPostSubscription?: Subscription;
  getBlogPostSubscription?: Subscription;


  constructor(
    private route: ActivatedRoute, 
    private blogPostService: BlogPostService,
    private router: Router,
    private categoryService: CategoryService
  ) {}

  ngOnInit(): void {

    this.categories$ = this.categoryService.getAllCategories();

    this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');
        
        if (this.id) {
          this.getBlogPostSubscription = this.blogPostService.getBlogPostbyId(this.id).subscribe({
            next: (response) => {
              this.model = response;

              // Map the selected categories to the response blogpost's categories
              this.selectedCategories = response.categories.map(x=> x.id);

            }
          });
        }
      }
    });
  }

  onFormSubmit(): void {
    // Convert this.model to request object compare with api response 
    // API response does not have id, and also categories is an array of ids, so we need to create model
    if (this.model && this.id) {
      var updateBlogpost : UpdateBlogPost = {
        author: this.model.author,
        content: this.model.content,
        featuredImageUrl: this.model.featuredImageUrl,
        isVisible: this.model.isVisible,
        publishedDate: this.model.publishedDate,
        shortDescription: this.model.shortDescription,
        title: this.model.title,
        urlHandle: this.model.urlHandle,
        categories: this.selectedCategories ?? []
      };

      this.updateBlogPostSubscription = this.blogPostService.updateBlogPost(this.id, updateBlogpost).subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/blogposts');
        }
      });
    }

  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.updateBlogPostSubscription?.unsubscribe();
    this.getBlogPostSubscription?.unsubscribe();
  }
}