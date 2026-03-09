import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { WorkItemsService, WorkItemDto, WorkItemStatus, Priority, ChangeWorkItemStatusCommand } from '../../../../api/generated';

// PrimeNG Imports
import { ButtonModule } from 'primeng/button';
import { TagModule } from 'primeng/tag';

@Component({
    selector: 'app-work-item-details',
    standalone: true,
    imports: [CommonModule, RouterModule, ButtonModule, TagModule],
    templateUrl: './work-item-details.component.html',
    styleUrls: ['./work-item-details.component.css']
})
export class WorkItemDetailsComponent implements OnInit {
    private readonly route = inject(ActivatedRoute);
    private readonly service = inject(WorkItemsService);

    item?: WorkItemDto;
    readonly WorkItemStatus = WorkItemStatus;
    readonly Priority = Priority;

    ngOnInit() {
        const id = Number(this.route.snapshot.params['id']);
        if (id) {
            this.loadItem(id);
        }
    }

    private loadItem(id: number) {
        this.service.getById(id).subscribe(res => this.item = res);
    }

    markAsDone() {
        if (this.item?.id) {
            this.service.changeStatus(this.item.id, { id: this.item.id, status: WorkItemStatus.Done }).subscribe(() => {
                this.loadItem(this.item!.id!);
            });
        }
    }

    getStatusName(s?: WorkItemStatus) {
        if (!s) return 'Unknown';
        switch (s) {
            case WorkItemStatus.New: return 'New';
            case WorkItemStatus.InProgress: return 'In Progress';
            case WorkItemStatus.Done: return 'Done';
            default: return 'Unknown';
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

    getStatusClass(s?: WorkItemStatus) {
        if (!s) return '';
        switch (s) {
            case WorkItemStatus.New: return 'status-new';
            case WorkItemStatus.InProgress: return 'status-inprogress';
            case WorkItemStatus.Done: return 'status-done';
            default: return '';
        }
    }

    getPriorityName(p?: Priority) {
        if (!p) return 'Unknown';
        switch (p) {
            case Priority.Low: return 'Low';
            case Priority.Medium: return 'Medium';
            case Priority.High: return 'High';
            default: return 'Unknown';
        }
    }

    getPriorityClass(p?: Priority) {
        if (!p) return '';
        switch (p) {
            case Priority.Low: return 'prio-low';
            case Priority.Medium: return 'prio-medium';
            case Priority.High: return 'prio-high';
            default: return '';
        }
    }
}
