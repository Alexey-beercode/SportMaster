import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../../services/auth.service';
import { TokenService } from '../../../services/token.service';
import { UserRequestDTO } from '../../../models/dtos-request';
import { EnumService } from '../../../services/enum-service';
import { HeaderComponent } from '../../shared/header/header.component';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, HeaderComponent],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  errorMessage: string | null = null;
  genderOptions: { value: string; label: string }[] = [];
  activityLevels: { value: string; label: string }[] = [
    { value: '1.2', label: 'Не очень подвижный' },
    { value: '1.4', label: 'Малоподвижный' },
    { value: '1.7', label: 'Активный' },
    { value: '1.9', label: 'Очень подвижный' },
  ];

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private tokenService: TokenService,
    private router: Router,
    private enumService: EnumService
  ) {
    this.initForm();
  }

  ngOnInit() {
    this.genderOptions = this.enumService.getEnumValues('Gender');

  }

  private initForm(): void {
    this.registerForm = this.fb.group({
      username: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      age: [null, [Validators.required, Validators.min(10)]],
      height: [null, [Validators.required, Validators.min(50)]],
      weight: [null, [Validators.required, Validators.min(20)]],
      gender: ['', [Validators.required]],
      activityLevel: ['', [Validators.required]],
      dailyStepGoal: [null, [Validators.required, Validators.min(1000)]],
      dailyWaterGoal: [null, [Validators.required, Validators.min(1)]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.registerForm.get(fieldName);
    return field ? field.invalid && (field.dirty || field.touched) : false;
  }

  onSubmit(): void {
    if (this.registerForm.valid) {
      const registerDto: UserRequestDTO = {
        ...this.registerForm.value,
      };

      this.authService.register(registerDto).subscribe({
        next: () => {
          alert('Регистрация прошла успешно!');
          this.router.navigate(['/login']);
        },
        error: (err) => {
          this.errorMessage = err.message || 'Ошибка регистрации';
        },
      });
    } else {
      Object.keys(this.registerForm.controls).forEach((key) => {
        const control = this.registerForm.get(key);
        if (control) control.markAsTouched();
      });
    }
  }
}
