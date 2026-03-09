import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { providePrimeNG } from 'primeng/config';
import Aura from '@primeng/themes/aura';
import { routes } from './app.routes';
import { BASE_PATH } from './api/generated/variables';
import { environment } from '../environments/environment';
import { ConfirmationService, MessageService } from 'primeng/api';
import { errorInterceptor } from './core/interceptors/error.interceptor';
import { provideOAuthClient } from 'angular-oauth2-oidc';
import { APP_INITIALIZER } from '@angular/core';
import { AuthService } from './core/auth/auth.service';
import { tokenInterceptor } from './core/interceptors/token.interceptor';





function initializeApp(authService: AuthService) {
  return () => authService.runInitialLoginSequence();
}


export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(
      withInterceptors([
        tokenInterceptor,
        errorInterceptor
      ])
    ),



    provideOAuthClient(),


    provideAnimationsAsync(),
    providePrimeNG({
      theme: {
        preset: Aura
      }
    }),
    { provide: BASE_PATH, useValue: environment.apiBaseUrl },
    ConfirmationService,
    MessageService,
    {
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      deps: [AuthService],
      multi: true
    }
  ]
};

