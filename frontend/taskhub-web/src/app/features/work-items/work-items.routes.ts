import { Routes } from '@angular/router';
import { WorkItemListComponent } from './pages/work-item-list/work-item-list.component';
import { WorkItemDetailsComponent } from './pages/work-item-details/work-item-details.component';
import { WorkItemEditComponent } from './pages/work-item-edit/work-item-edit.component';

export const WORK_ITEM_ROUTES: Routes = [
    { path: '', component: WorkItemListComponent },
    { path: 'new', component: WorkItemEditComponent },
    { path: ':id', component: WorkItemDetailsComponent },
    { path: ':id/edit', component: WorkItemEditComponent }
];

