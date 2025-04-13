import { Component } from '@angular/core';
import { AddBlogpost } from '../models/add-blogpost.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BlogPostService } from '../services/blog-post.service';
import { Router } from '@angular/router';
import { MarkdownModule } from 'ngx-markdown';

@Component({
  selector: 'app-add-blogpost',
  imports: [FormsModule, CommonModule,   MarkdownModule],
  standalone: true,
  templateUrl: './add-blogpost.component.html',
  styleUrl: './add-blogpost.component.css'
})
export class AddBlogpostComponent {
  model : AddBlogpost;

  constructor (private blogPostService : BlogPostService, private router: Router) {
    this.model = {
      title : '',
      shortDescription : '',
      content: '',
      featuredImageUrl: '',
      urlHandle : '',
      author: '',
      publishedDate : new Date(),
      isVisible : true
    }
  }

  onFormSubmit() : void{
    this.blogPostService.createBlogPost(this.model).subscribe({
      next:(response) => {
        this.router.navigateByUrl('/admin/blogposts');
      }
    })

  }




}
