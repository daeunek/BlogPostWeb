import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BlogPostService } from '../../blog-post/services/blog-post.service';
import { BlogPost } from '../../blog-post/models/blogpost.model';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { MarkdownModule } from 'ngx-markdown';

@Component({
  selector: 'app-blog-details',
  imports: [CommonModule, MarkdownModule],
  standalone: true,
  templateUrl: './blog-details.component.html',
  styleUrl: './blog-details.component.css'
})
export class BlogDetailsComponent implements OnInit {

  url: string | null = null;
  blogPost$?: Observable<BlogPost>;

  constructor(private route: ActivatedRoute, private blogPostService: BlogPostService) {
    // Initialize any properties or services needed for the component 

  }

  // Fetch Blog details from url
  ngOnInit(): void {
    this.route.paramMap
    .subscribe({
      next: (params) => {
        this.url = params.get('url');
        if (this.url) {
          this.blogPost$ = this.blogPostService.getBlogPostbyUrlHandle(this.url);
        }
      }
    });
  }
}
