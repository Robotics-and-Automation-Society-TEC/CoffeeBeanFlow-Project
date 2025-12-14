import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { BodegaService } from '../../../core/services/bodega.service';
import { AreaAcopioService } from '../../../core/services/area-acopio.service';
import { AreaAcopio } from '../../../models/area-acopio.model';
import { CreateBodegaDto, UpdateBodegaDto } from '../../../models/bodega.model';

@Component({
  selector: 'app-bodega-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './bodega-form.html',
  styleUrl: './bodega-form.css'
})
export class BodegaFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private bodegaService = inject(BodegaService);
  private acopioService = inject(AreaAcopioService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  bodegaForm!: FormGroup;
  isEditMode = false;
  idBodega: number | null = null;
  isLoading = false;
  errorMessage = '';
  lotesDisponibles: AreaAcopio[] = [];

  ngOnInit(): void {
    this.initForm();
    this.cargarLotesDisponibles();
    this.checkEditMode();
  }

  cargarLotesDisponibles(): void {
    this.acopioService.obtenerTodos().subscribe({
      next: (lotes) => this.lotesDisponibles = lotes,
      error: (error) => console.error('Error al cargar lotes:', error)
    });
  }

  private initForm(): void {
    this.bodegaForm = this.fb.group({
      nlote: ['', [Validators.required, Validators.maxLength(50)]],
      wBellota: [null, [Validators.min(0)]],
      wPergamino: [null, [Validators.min(0)]],
      hfinal: [null, [Validators.min(0), Validators.max(100)]],
      hinicial: [null, [Validators.min(0), Validators.max(100)]],
      dPergamino: [null, [Validators.min(0), Validators.max(1)]],
      dBellota: [null, [Validators.min(0), Validators.max(1)]],
      finicio_reposo: [null],
      cantidadSacos: [null, [Validators.min(0)]],
      pmhRelativa: [null, [Validators.min(0), Validators.max(1)]],
      pmtInterna: [null],
      pmtExterna: [null]
    });
  }

  private checkEditMode(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.idBodega = parseInt(id, 10);
      this.cargarDatos();
    }
  }

  private cargarDatos(): void {
    if (this.idBodega === null) return;

    this.isLoading = true;
    this.bodegaService.obtenerPorId(this.idBodega).subscribe({
      next: (bodega) => {
        this.bodegaForm.patchValue({
          nlote: bodega.nlote,
          wBellota: bodega.wBellota,
          wPergamino: bodega.wPergamino,
          hfinal: bodega.hfinal,
          hinicial: bodega.hinicial,
          dPergamino: bodega.dPergamino,
          dBellota: bodega.dBellota,
          finicio_reposo: bodega.finicio_reposo ? this.formatDateForInput(bodega.finicio_reposo) : null,
          cantidadSacos: bodega.cantidadSacos,
          pmhRelativa: bodega.pmhRelativa,
          pmtInterna: bodega.pmtInterna,
          pmtExterna: bodega.pmtExterna
        });
        this.isLoading = false;
      },
      error: (error) => {
        this.errorMessage = 'Error al cargar los datos: ' + error.message;
        this.isLoading = false;
      }
    });
  }

  private formatDateForInput(dateString: string): string {
    const date = new Date(dateString);
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    const hours = String(date.getHours()).padStart(2, '0');
    const minutes = String(date.getMinutes()).padStart(2, '0');
    return `${year}-${month}-${day}T${hours}:${minutes}`;
  }

  onSubmit(): void {
    if (this.bodegaForm.invalid) {
      this.bodegaForm.markAllAsTouched();
      this.errorMessage = 'Por favor, completa todos los campos requeridos';
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    const formValue = this.bodegaForm.value;

    if (this.isEditMode && this.idBodega !== null) {
      const updateDto: UpdateBodegaDto = {
        idBodega: this.idBodega,
        nlote: formValue.nlote,
        wBellota: formValue.wBellota,
        wPergamino: formValue.wPergamino,
        hfinal: formValue.hfinal,
        hinicial: formValue.hinicial,
        dPergamino: formValue.dPergamino,
        dBellota: formValue.dBellota,
        finicio_reposo: formValue.finicio_reposo ? new Date(formValue.finicio_reposo).toISOString() : null,
        cantidadSacos: formValue.cantidadSacos,
        pmhRelativa: formValue.pmhRelativa,
        pmtInterna: formValue.pmtInterna,
        pmtExterna: formValue.pmtExterna
      };

      this.bodegaService.actualizar(this.idBodega, updateDto).subscribe({
        next: () => {
          this.router.navigate(['/bodega']);
        },
        error: (error) => {
          this.errorMessage = error.message;
          this.isLoading = false;
        }
      });
    } else {
      const createDto: CreateBodegaDto = {
        nlote: formValue.nlote,
        wBellota: formValue.wBellota,
        wPergamino: formValue.wPergamino,
        hfinal: formValue.hfinal,
        hinicial: formValue.hinicial,
        dPergamino: formValue.dPergamino,
        dBellota: formValue.dBellota,
        finicio_reposo: formValue.finicio_reposo ? new Date(formValue.finicio_reposo).toISOString() : null,
        cantidadSacos: formValue.cantidadSacos,
        pmhRelativa: formValue.pmhRelativa,
        pmtInterna: formValue.pmtInterna,
        pmtExterna: formValue.pmtExterna
      };

      this.bodegaService.crear(createDto).subscribe({
        next: () => {
          this.router.navigate(['/bodega']);
        },
        error: (error) => {
          this.errorMessage = error.message;
          this.isLoading = false;
        }
      });
    }
  }

  cancelar(): void {
    this.router.navigate(['/bodega']);
  }
}
