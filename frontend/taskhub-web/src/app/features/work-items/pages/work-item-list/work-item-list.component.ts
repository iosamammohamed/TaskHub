import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { WorkItemsService, WorkItemListItemDtoPagedResult, WorkItemStatus, Priority, ChangeWorkItemStatusCommand, WorkItemListItemDto } from '../../../../api/generated';

// PrimeNG Imports
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { SelectModule } from 'primeng/select';
import { TagModule } from 'primeng/tag';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { PrimeTemplate, ConfirmationService, MessageService } from 'primeng/api';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';

@Component({
    selector: 'app-work-item-list',
    standalone: true,
    imports: [
        CommonModule,
        RouterModule,
        FormsModule,
        TableModule,
        ButtonModule,
        InputTextModule,
        SelectModule,
        TagModule,
        IconFieldModule,
        InputIconModule,
        PrimeTemplate,
        ConfirmDialogModule,
        ToastModule
    ],
    templateUrl: './work-item-list.component.html',
    styleUrls: ['./work-item-list.component.css']
})
export class WorkItemListComponent implements OnInit {
    private readonly service = inject(WorkItemsService);
    private readonly confirmationService = inject(ConfirmationService);
    private readonly messageService = inject(MessageService);

    data?: WorkItemListItemDtoPagedResult;
    searchTerm: string = '';
    statusFilter: WorkItemStatus | null = null;
    loading: boolean = false;
    readonly WorkItemStatus = WorkItemStatus;
    readonly Math = Math;

    ngOnInit() {
        this.loadData();
    }

    loadData(pageIdx: number = 1) {
        this.loading = true;
        const status = this.statusFilter === null ? undefined : this.statusFilter;

        this.service.getList(pageIdx, 10, this.searchTerm, status).subscribe({
            next: (res) => {
                this.data = res;
                this.loading = false;
            },
            error: () => {
                this.loading = false;
            }
        });
    }

    onLazyLoad(event: any) {
        const page = (event.first / event.rows) + 1;
        this.loadData(page);
    }

    resetFilters() {
        this.searchTerm = '';
        this.statusFilter = null;
        this.loadData(1);
    }



    deleteItem(id: number) {
        this.confirmationService.confirm({
            message: 'Are you sure you want to delete this task?',
            header: 'Confirm Deletion',
            icon: 'pi pi-exclamation-triangle',
            acceptLabel: 'Delete',
            rejectLabel: 'Cancel',
            acceptButtonStyleClass: 'p-button-danger',
            rejectButtonStyleClass: 'p-button-text',
            accept: () => {
                this.service._delete(id).subscribe({
                    next: () => {
                        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Task deleted successfully' });
                        this.loadData(this.data?.pageNumber);
                    },
                    error: () => {
                        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to delete task' });
                    }
                });
            }
        });
    }

    getStatusName(status?: WorkItemStatus): string {
        if (!status) return 'Unknown';
        switch (status) {
            case WorkItemStatus.New: return 'New';
            case WorkItemStatus.InProgress: return 'In Progress';
            case WorkItemStatus.Done: return 'Done';
            default: return 'Unknown';
        }
    }

    getStatusClass(status?: WorkItemStatus): string {
        if (!status) return '';
        switch (status) {
            case WorkItemStatus.New: return 'status-new';
            case WorkItemStatus.InProgress: return 'status-inprogress';
            case WorkItemStatus.Done: return 'status-done';
            default: return '';
        }
    }

    getPriorityName(prio?: Priority): string {
        if (!prio) return 'Unknown';
        switch (prio) {
            case Priority.Low: return 'Low';
            case Priority.Medium: return 'Medium';
            case Priority.High: return 'High';
            default: return 'Unknown';
        }
    }

    getPriorityClass(prio?: Priority): string {
        if (!prio) return '';
        switch (prio) {
            case Priority.Low: return 'prio-low';
            case Priority.Medium: return 'prio-medium';
            case Priority.High: return 'prio-high';
            default: return '';
        }
    }

    getPrimeSeverity(status?: WorkItemStatus): 'success' | 'info' | 'warn' | 'danger' | 'secondary' | undefined {
        if (!status) return 'secondary';
        switch (status) {
            case WorkItemStatus.New: return 'info';
            case WorkItemStatus.InProgress: return 'warn';
            case WorkItemStatus.Done: return 'success';
            default: return 'secondary';
        }
    }
}
