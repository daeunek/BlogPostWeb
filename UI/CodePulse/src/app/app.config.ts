import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http'; // 👈 import this
import { provideMarkdown } from 'ngx-markdown'; 


import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }),
               provideRouter(routes),
               provideHttpClient(), // 👈 add this
               provideMarkdown()

              ]
};
