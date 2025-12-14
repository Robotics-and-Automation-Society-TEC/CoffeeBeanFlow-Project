import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CaracterizacionService } from '../../../core/services/caracterizacion.service';
import { AreaAcopioService } from '../../../core/services/area-acopio.service';
import { AreaAcopio } from '../../../models/area-acopio.model';

@Component({
  selector: 'app-caracterizacion-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './caracterizacion-form.html',
  styleUrl: './caracterizacion-form.css'
})
export class CaracterizacionFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private caracterizacionService = inject(CaracterizacionService);
  private acopioService = inject(AreaAcopioService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  formulario: FormGroup;
  modoEdicion = false;
  tiempoOriginal: string | null = null;
  incluyeSobremaduras = false;
  incluyeInmaduras = false;
  incluyeMaduras = false;
  lotesDisponibles: AreaAcopio[] = [];

  constructor() {
    this.formulario = this.fb.group({
      tiempo: ['', Validators.required],
      nloteAreaAcopio: ['', Validators.required],
      drMaduras: [null, [Validators.min(0), Validators.max(100)]],
      pcDebajo: [null, [Validators.min(0), Validators.max(100)]],
      proceso: [null],
      cVerdes: [null, Validators.min(0)],
      cObjetivo: [null, Validators.min(0)],
      cInmaduras: [null, Validators.min(0)],
      cSobremaduras: [null, Validators.min(0)],
      cSecas: [null, Validators.min(0)],
      mTabla: [null, Validators.min(0)],
      pcVerdes: [null, [Validators.min(0), Validators.max(100)]],
      pcSecas: [null, [Validators.min(0), Validators.max(100)]],
      pcEncima: [null, [Validators.min(0), Validators.max(100)]],
      eMaduracion: [null, [Validators.min(0), Validators.max(100)]],
      broca: [null, [Validators.min(0), Validators.max(100)]],
      densidad: [null, Validators.min(0)],
      vanos: [null, Validators.min(0)],
      secos: [null, Validators.min(0)],
      pcObjetivo: [null, [Validators.min(0), Validators.max(100)]],
      rcSobremaduras: this.fb.group({
        gbx: [null, Validators.min(0)],
        promedio: [null, Validators.min(0)],
        observaciones: [null]
      }),
      rcInmaduras: this.fb.group({
        gbx: [null, Validators.min(0)],
        promedio: [null, Validators.min(0)],
        observaciones: [null]
      }),
      rcMaduras: this.fb.group({
        gbx: [null, Validators.min(0)],
        promedio: [null, Validators.min(0)],
        observaciones: [null]
      })
    });
  }

  ngOnInit(): void {
    this.cargarLotesDisponibles();
    const tiempo = this.route.snapshot.paramMap.get('tiempo');
    if (tiempo) {
      this.modoEdicion = true;
      this.tiempoOriginal = decodeURIComponent(tiempo);
      this.cargarCaracterizacion(this.tiempoOriginal);
    }
  }

  cargarLotesDisponibles(): void {
    this.acopioService.obtenerTodos().subscribe({
      next: (lotes) => this.lotesDisponibles = lotes,
      error: (error) => console.error('Error al cargar lotes:', error)
    });
  }

  cargarCaracterizacion(tiempo: string): void {
    this.caracterizacionService.obtenerPorTiempo(tiempo).subscribe({
      next: (caracterizacion) => {
        this.formulario.patchValue({
          tiempo: caracterizacion.tiempo ? caracterizacion.tiempo.substring(0, 16) : '',
          nloteAreaAcopio: caracterizacion.nloteAreaAcopio,
          drMaduras: caracterizacion.drMaduras,
          pcDebajo: caracterizacion.pcDebajo,
          proceso: caracterizacion.proceso,
          cVerdes: caracterizacion.cVerdes,
          cObjetivo: caracterizacion.cObjetivo,
          cInmaduras: caracterizacion.cInmaduras,
          cSobremaduras: caracterizacion.cSobremaduras,
          cSecas: caracterizacion.cSecas,
          mTabla: caracterizacion.mTabla,
          pcVerdes: caracterizacion.pcVerdes,
          pcSecas: caracterizacion.pcSecas,
          pcEncima: caracterizacion.pcEncima,
          eMaduracion: caracterizacion.eMaduracion,
          broca: caracterizacion.broca,
          densidad: caracterizacion.densidad,
          vanos: caracterizacion.vanos,
          secos: caracterizacion.secos,
          pcObjetivo: caracterizacion.pcObjetivo
        });

        if (caracterizacion.rcSobremaduras) {
          this.incluyeSobremaduras = true;
          this.formulario.patchValue({
            rcSobremaduras: {
              gbx: caracterizacion.rcSobremaduras.gbx,
              promedio: caracterizacion.rcSobremaduras.promedio,
              observaciones: caracterizacion.rcSobremaduras.observaciones
            }
          });
        }

        if (caracterizacion.rcInmaduras) {
          this.incluyeInmaduras = true;
          this.formulario.patchValue({
            rcInmaduras: {
              gbx: caracterizacion.rcInmaduras.gbx,
              promedio: caracterizacion.rcInmaduras.promedio,
              observaciones: caracterizacion.rcInmaduras.observaciones
            }
          });
        }

        if (caracterizacion.rcMaduras) {
          this.incluyeMaduras = true;
          this.formulario.patchValue({
            rcMaduras: {
              gbx: caracterizacion.rcMaduras.gbx,
              promedio: caracterizacion.rcMaduras.promedio,
              observaciones: caracterizacion.rcMaduras.observaciones
            }
          });
        }
      },
      error: (error) => console.error('Error al cargar caracterización:', error)
    });
  }

  toggleSobremaduras(): void {
    this.incluyeSobremaduras = !this.incluyeSobremaduras;
    if (!this.incluyeSobremaduras) {
      this.formulario.get('rcSobremaduras')?.reset();
    }
  }

  toggleInmaduras(): void {
    this.incluyeInmaduras = !this.incluyeInmaduras;
    if (!this.incluyeInmaduras) {
      this.formulario.get('rcInmaduras')?.reset();
    }
  }

  toggleMaduras(): void {
    this.incluyeMaduras = !this.incluyeMaduras;
    if (!this.incluyeMaduras) {
      this.formulario.get('rcMaduras')?.reset();
    }
  }

  guardar(): void {
    if (this.formulario.invalid) {
      this.formulario.markAllAsTouched();
      return;
    }

    const datos = { ...this.formulario.value };
    
    // Convertir tiempo local a ISO 8601 UTC
    if (datos.tiempo) {
      datos.tiempo = new Date(datos.tiempo).toISOString();
    }

    // Eliminar RC que no están incluidos
    if (!this.incluyeSobremaduras) {
      delete datos.rcSobremaduras;
    }
    if (!this.incluyeInmaduras) {
      delete datos.rcInmaduras;
    }
    if (!this.incluyeMaduras) {
      delete datos.rcMaduras;
    }

    if (this.modoEdicion && this.tiempoOriginal) {
      this.caracterizacionService.actualizar(this.tiempoOriginal, datos).subscribe({
        next: () => this.router.navigate(['/caracterizacion']),
        error: (error) => console.error('Error al actualizar:', error)
      });
    } else {
      this.caracterizacionService.crear(datos).subscribe({
        next: () => this.router.navigate(['/caracterizacion']),
        error: (error) => console.error('Error al crear:', error)
      });
    }
  }

  cancelar(): void {
    this.router.navigate(['/caracterizacion']);
  }
}
