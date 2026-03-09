import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { WorkItemsService, WorkItemStatus, Priority, CreateWorkItemCommand, UpdateWorkItemCommand } from '../../../../api/generated';

// PrimeNG Imports
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { SelectModule } from 'primeng/select';
import { DatePickerModule } from 'primeng/datepicker';

@Component({
    selector: 'app-work-item-edit',
    standalone: true,
    imports: [
        CommonModule,
        RouterModule,
        ReactiveFormsModule,
        ButtonModule,
        InputTextModule,
        TextareaModule,
        SelectModule,
        DatePickerModule
    ],
    templateUrl: './work-item-edit.component.html',
    styleUrls: ['./work-item-edit.component.css']
})
export class WorkItemEditComponent implements OnInit {
    private readonly route = inject(ActivatedRoute);
    private readonly router = inject(Router);
    private readonly service = inject(WorkItemsService);
    private readonly fb = inject(FormBuilder);

    form: FormGroup = this.fb.group({
        title: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
        description: ['', [Validators.maxLength(500)]],
        priority: [Priority.Medium, [Validators.required]],
        status: [WorkItemStatus.New, [Validators.required]],
        dueDate: ['', [Validators.required]]
    });

    isEdit = false;
    itemId?: number;
    isSubmitting = false;

    priorityOptions = [
        { label: 'Low', value: Priority.Low },
        { label: 'Medium', value: Priority.Medium },
        { label: 'High', value: Priority.High }
    ];

    statusOptions = [
        { label: 'New', value: WorkItemStatus.New },
        { label: 'In Progress', value: WorkItemStatus.InProgress },
        { label: 'Done', value: WorkItemStatus.Done }
    ];

    ngOnInit() {
        const idParam = this.route.snapshot.params['id'];
        if (idParam) {
            this.itemId = Number(idParam);
            this.isEdit = true;
            this.service.getById(this.itemId).subscribe(res => {
                this.form.patchValue({
                    ...res,
                    dueDate: res.dueDate ? new Date(res.dueDate) : null
                });
            });
        }
    }

    save() {
        if (this.form.invalid) {
            this.form.markAllAsTouched();
            return;
        }

        this.isSubmitting = true;
        const model = this.form.value;

        if (this.isEdit) {
            const updateCommand = { ...model, id: this.itemId };
            this.service.update(this.itemId!, updateCommand as any).subscribe({
                next: () => this.router.navigate(['/work-items', this.itemId]),
                error: (err) => {
                    console.error(err);
                    this.isSubmitting = false;
                }
            });
        } else {
            const createCommand: CreateWorkItemCommand = model;
            this.service.create(createCommand).subscribe({
                next: () => this.router.navigate(['/work-items']),
                error: (err) => {
                    console.error(err);
                    this.isSubmitting = false;
                }
            });
        }
    }

    // Helper getters for easy validation access in template
    get f() { return this.form.controls; }
}
