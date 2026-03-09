import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';


export const routes: Routes = [
    { path: '', redirectTo: 'work-items', pathMatch: 'full' },
    {
        path: 'work-items',
        canActivate: [authGuard],
        loadChildren: () => import('./features/work-items/work-items.routes').then(m => m.WORK_ITEM_ROUTES)
    }
];


