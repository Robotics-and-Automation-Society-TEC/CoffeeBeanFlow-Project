import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { AreaAcopioService } from '../../../core/services/area-acopio.service';
import { AreaAcopio } from '../../../models/area-acopio.model';

@Component({
  selector: 'app-area-acopio-form',
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './area-acopio-form.html',
  styleUrl: './area-acopio-form.css',
})
export class AreaAcopioFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private areaAcopioService = inject(AreaAcopioService);

  areaAcopioForm!: FormGroup;
  isEditMode = false;
  isLoading = false;
  errorMessage = '';
  successMessage = '';
  nloteOriginal = '';

  ngOnInit(): void {
    this.initForm();
    this.checkEditMode();
  }

  private initForm(): void {
    this.areaAcopioForm = this.fb.group({
      // Clave primaria
      nlote: ['', [Validators.required, Validators.maxLength(50)]],
      
      // Atributos simples
      altura: [0, [Validators.required, Validators.min(0)]],
      zona: ['', [Validators.required, Validators.maxLength(100)]],
      nrecibo: [0, [Validators.required, Validators.min(1)]],
      nproductor: ['', [Validators.required, Validators.maxLength(100)]],
      nfinca: ['', [Validators.required, Validators.maxLength(100)]],
      robjetivo: [0, [Validators.required, Validators.min(0)]],
      rtotal: [0, [Validators.min(0)]],
      vendido: [false],
      disponible: [0, [Validators.min(0)]],
      enproceso: ['No', Validators.required],
      
      // Atributo compuesto: Despulpado
      semilavado: [false],
      natural: [false],
      anaerobico: [false],
      otro: [null],
      miel: [false],
      lavado: [false],
      
      // Atributo compuesto: Pruebas_Fisicas_BH
      pF_Pulpa_Pergamino: [0, [Validators.min(0)]],
      pF_DMecanicos: [0, [Validators.min(0)]],
      pF_Segundas: [0, [Validators.min(0)]],
      pF_Pergamino_Pulpa: [0, [Validators.min(0)]],
      pDensidad_Fruta: [0, [Validators.min(0)]],
      pDensidad_Pergamino_Humedo: [0, [Validators.min(0)]]
    });
  }

  private checkEditMode(): void {
    const nlote = this.route.snapshot.paramMap.get('nlote');
    if (nlote) {
      this.isEditMode = true;
      this.nloteOriginal = nlote;
      this.loadAreaAcopio(nlote);
      this.areaAcopioForm.get('nlote')?.disable();
    }
  }

  private loadAreaAcopio(nlote: string): void {
    this.isLoading = true;
    this.areaAcopioService.obtenerPorLote(nlote).subscribe({
      next: (areaAcopio) => {
        this.areaAcopioForm.patchValue(areaAcopio);
        this.isLoading = false;
      },
      error: (error) => {
        this.errorMessage = 'Error al cargar el registro: ' + error.message;
        this.isLoading = false;
      }
    });
  }

  onSubmit(): void {
    if (this.areaAcopioForm.invalid) {
      this.markFormGroupTouched(this.areaAcopioForm);
      this.errorMessage = 'Por favor complete todos los campos requeridos';
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';
    this.successMessage = '';

    const formValue = this.areaAcopioForm.getRawValue();

    if (this.isEditMode) {
      this.areaAcopioService.actualizar(this.nloteOriginal, formValue).subscribe({
        next: () => {
          this.successMessage = 'Registro actualizado exitosamente';
          this.isLoading = false;
          setTimeout(() => this.router.navigate(['/area-acopio']), 1500);
        },
        error: (error) => {
          this.errorMessage = 'Error al actualizar: ' + error.message;
          this.isLoading = false;
        }
      });
    } else {
      this.areaAcopioService.crear(formValue).subscribe({
        next: () => {
          this.successMessage = 'Registro creado exitosamente';
          this.isLoading = false;
          setTimeout(() => this.router.navigate(['/area-acopio']), 1500);
        },
        error: (error) => {
          this.errorMessage = 'Error al crear: ' + error.message;
          this.isLoading = false;
        }
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/area-acopio']);
  }

  private markFormGroupTouched(formGroup: FormGroup): void {
    Object.keys(formGroup.controls).forEach(key => {
      const control = formGroup.get(key);
      control?.markAsTouched();
    });
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.areaAcopioForm.get(fieldName);
    return !!(field && field.invalid && field.touched);
  }
}
