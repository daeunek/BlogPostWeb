import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { BlogPostService } from '../services/blog-post.service';
import { Observable } from 'rxjs';
import { BlogPost } from '../models/blogpost.model';

@Component({
  selector: 'app-blogpost-list',
  imports: [CommonModule, RouterModule],
  standalone: true,
  templateUrl: './blogpost-list.component.html',
  styleUrl: './blogpost-list.component.css'
})
export class BlogpostListComponent implements OnInit {

  blogPosts$?: Observable<BlogPost[]>;

  constructor(private blogPostService : BlogPostService) {

   }
  
  ngOnInit(): void {
      // get all bloh posts from api
      this.blogPosts$ = this.blogPostService.getAllBlogPosts();
  }

}
