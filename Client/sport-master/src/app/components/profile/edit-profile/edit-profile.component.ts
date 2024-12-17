import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../../services/user-service';
import { UpdateUserRequestDTO } from '../../../models/dtos-request';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { HeaderComponent } from '../../shared/header/header.component';
import { UserDto } from '../../../models/dtos-response';
import { TokenService } from '../../../services/token.service';

@Component({
  selector: 'app-edit-user-modal',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, HeaderComponent],
  templateUrl: 'edit-profile.component.html',
  styleUrls: ['edit-profile.component.css'],
})
export class EditUserModalComponent implements OnInit {
  @Input() userData!: UserDto; // Текущая информация о пользователе
  @Output() close = new EventEmitter<void>();
  @Output() userUpdated = new EventEmitter<void>();

  editUserForm!: FormGroup;
  errorMessage: string | null = null;
  userId: string | null = null;

  constructor(private fb: FormBuilder, private userService: UserService, private tokenService: TokenService) {}

  ngOnInit(): void {
    this.initForm();
    this.userId = this.tokenService.getUserId();
  }

  private initForm(): void {
    this.editUserForm = this.fb.group({
      age: [this.userData.age, [Validators.required, Validators.min(10)]],
      height: [this.userData.height, [Validators.required, Validators.min(50)]],
      weight: [this.userData.weight, [Validators.required, Validators.min(20)]],
      dailyStepGoal: [this.userData.dailyStepGoal || 0, [Validators.required, Validators.min(1)]],
      dailyWaterGoal: [this.userData.dailyWaterGoal || 0, [Validators.required, Validators.min(1)]],
    });
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.editUserForm.get(fieldName);
    return field ? field.invalid && (field.dirty || field.touched) : false;
  }

  onSubmit(): void {
    if (this.editUserForm.valid) {
      const updateRequest: UpdateUserRequestDTO = {
        ...this.editUserForm.value,
      };

      this.userService.updateUser(this.userId!, updateRequest).subscribe({
        next: () => {
          this.userUpdated.emit();
          this.onCancel();
        },
        error: (err) => {
          this.errorMessage = err.message || 'Ошибка при обновлении данных';
        },
      });
    } else {
      Object.keys(this.editUserForm.controls).forEach((key) => {
        const control = this.editUserForm.get(key);
        if (control) control.markAsTouched();
      });
    }
  }

  onCancel(): void {
    this.close.emit();
  }
}
