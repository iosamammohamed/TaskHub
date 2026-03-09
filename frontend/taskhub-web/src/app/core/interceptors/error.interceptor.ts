import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { MessageService } from 'primeng/api';
import { catchError, throwError } from 'rxjs';

/**
 * Intercepts HTTP errors and displays detailed but user-friendly messages using PrimeNG Toast.
 */
export const errorInterceptor: HttpInterceptorFn = (req, next) => {
    const messageService = inject(MessageService);

    return next(req).pipe(
        catchError((error: HttpErrorResponse) => {
            let errorMsg = 'An unexpected error occurred';
            let summary = 'Error';
            let severity: 'error' | 'warn' | 'info' = 'error';

            if (error.error instanceof ErrorEvent) {
                // Client-side or network error
                errorMsg = `Error: ${error.error.message}`;
            } else {
                // Backend error
                switch (error.status) {
                    case 400:
                        summary = 'Bad Request';
                        errorMsg = 'Invalid request data. Please check your input.';
                        severity = 'warn';
                        break;
                    case 401:
                        summary = 'Unauthorized';
                        errorMsg = 'You are not authorized to perform this action.';
                        break;
                    case 403:
                        summary = 'Forbidden';
                        errorMsg = 'You do not have permission to access this resource.';
                        break;
                    case 404:
                        summary = 'Not Found';
                        errorMsg = 'The requested resource was not found.';
                        break;
                    case 500:
                        summary = 'Server Error';
                        errorMsg = 'Something went wrong on our end. Please try again later.';
                        break;
                    default:
                        summary = `Error ${error.status}`;
                        errorMsg = 'An error occurred while communicating with the server.';
                }
            }

            // Display Toast message
            messageService.add({
                severity: severity,
                summary: summary,
                detail: errorMsg,
                life: 5000
            });


            return throwError(() => error);
        })
    );
};
