import { Component, OnDestroy } from '@angular/core';
import { AddBlogpost } from '../models/add-blogpost.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BlogPostService } from '../services/blog-post.service';
import { Router } from '@angular/router';
import { MarkdownModule } from 'ngx-markdown';
import { OnInit } from '@angular/core';
import { Category } from '../../category/models/category.model';
import { CategoryService } from '../../category/services/category.service';
import { Observable, Subscription } from 'rxjs';
import { ImageSelectorComponent } from '../../../shared/components/image-selector/image-selector.component';
import { ImageService } from '../../../shared/components/image-selector/image.service';



@Component({
  selector: 'app-add-blogpost',
  imports: [FormsModule, CommonModule,   MarkdownModule, ImageSelectorComponent],
  standalone: true,
  templateUrl: './add-blogpost.component.html',
  styleUrl: './add-blogpost.component.css'
})
export class AddBlogpostComponent implements OnInit, OnDestroy{
  model : AddBlogpost;
  categories$? : Observable<Category[]>;
  isImageSelectorVisible : boolean = false;

  imageSelectorSubscription?: Subscription;

  constructor (private blogPostService : BlogPostService, private router: Router, private categoryService : CategoryService,
    private imageService : ImageService
  ) {
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

  openImageSelector() : void {
    this.isImageSelectorVisible = true;
  }

  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();

    this.imageSelectorSubscription = this.imageService.onSelectImage().subscribe({
      next: (selectedImage) => {
        this.model.featuredImageUrl = selectedImage.url;
        this.closeImgSelector();
      }
    });
  }

  closeImgSelector() : void {
    this.isImageSelectorVisible = false;
  }

  ngOnDestroy(): void {
    this.imageSelectorSubscription?.unsubscribe();
  }



}
