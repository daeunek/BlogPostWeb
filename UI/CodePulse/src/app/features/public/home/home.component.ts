import { Component, OnInit } from '@angular/core';
import { BlogPostService } from '../../blog-post/services/blog-post.service';
import { Observable } from 'rxjs';
import { BlogPost } from '../../blog-post/models/blogpost.model';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  imports: [RouterLink, CommonModule],
  standalone: true,
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{

  blogs$?: Observable<BlogPost[]>;

  constructor(private blogPostSeevice: BlogPostService) {
    // Initialize any properties or services needed for the component
    
  }

  ngOnInit(): void {
    this.blogs$ = this.blogPostSeevice.getAllBlogPosts();
  }


  // Other component methods and properties can go here
}
