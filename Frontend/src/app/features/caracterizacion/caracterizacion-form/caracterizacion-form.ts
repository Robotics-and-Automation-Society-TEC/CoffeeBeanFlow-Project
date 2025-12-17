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
      next: (caracterizacion: any) => {
        this.formulario.patchValue({
          tiempo: caracterizacion.tiempo ? caracterizacion.tiempo.substring(0, 16) : '',
          nloteAreaAcopio: caracterizacion.nloteAreaAcopio,
          drMaduras: caracterizacion.dRmaduras,
          pcDebajo: caracterizacion.pCdebajo,
          proceso: caracterizacion.proceso,
          cVerdes: caracterizacion.cverdes,
          cObjetivo: caracterizacion.cobjetivo,
          cInmaduras: caracterizacion.cinmaduras,
          cSobremaduras: caracterizacion.csobremaduras,
          cSecas: caracterizacion.csecas,
          mTabla: caracterizacion.mtabla,
          pcVerdes: caracterizacion.pCverdes,
          pcSecas: caracterizacion.pCsecas,
          pcEncima: caracterizacion.pCencima,
          eMaduracion: caracterizacion.emaduracion,
          broca: caracterizacion.broca,
          densidad: caracterizacion.densidad,
          vanos: caracterizacion.vanos,
          secos: caracterizacion.secos,
          pcObjetivo: caracterizacion.pCobjetivo
        });

        if (caracterizacion.rcSobremaduras || caracterizacion.rCsobremaduras) {
          this.incluyeSobremaduras = true;
          const rcSobre = caracterizacion.rcSobremaduras || caracterizacion.rCsobremaduras;
          this.formulario.patchValue({
            rcSobremaduras: {
              gbx: rcSobre.gbx,
              promedio: rcSobre.promedio,
              observaciones: rcSobre.observaciones
            }
          });
        }

        if (caracterizacion.rcInmaduras || caracterizacion.rCinmaduras) {
          this.incluyeInmaduras = true;
          const rcInma = caracterizacion.rcInmaduras || caracterizacion.rCinmaduras;
          this.formulario.patchValue({
            rcInmaduras: {
              gbx: rcInma.gbx,
              promedio: rcInma.promedio,
              observaciones: rcInma.observaciones
            }
          });
        }

        if (caracterizacion.rcMaduras || caracterizacion.rCmaduras) {
          this.incluyeMaduras = true;
          const rcMadu = caracterizacion.rcMaduras || caracterizacion.rCmaduras;
          this.formulario.patchValue({
            rcMaduras: {
              gbx: rcMadu.gbx,
              promedio: rcMadu.promedio,
              observaciones: rcMadu.observaciones
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
      console.log('Formulario inválido:', this.formulario.errors);
      Object.keys(this.formulario.controls).forEach(key => {
        const control = this.formulario.get(key);
        if (control?.invalid) {
          console.log(`Campo inválido: ${key}`, control.errors);
        }
      });
      this.formulario.markAllAsTouched();
      return;
    }

    const formValue = this.formulario.value;
    
    // Transformar los nombres de propiedades del formulario al formato del backend
    const datos: any = {
      tiempo: formValue.tiempo ? new Date(formValue.tiempo).toISOString() : null,
      nloteAreaAcopio: formValue.nloteAreaAcopio,
      dRmaduras: formValue.drMaduras,
      pCdebajo: formValue.pcDebajo,
      proceso: formValue.proceso,
      cverdes: formValue.cVerdes,
      cobjetivo: formValue.cObjetivo,
      cinmaduras: formValue.cInmaduras,
      csobremaduras: formValue.cSobremaduras,
      csecas: formValue.cSecas,
      mtabla: formValue.mTabla,
      pCverdes: formValue.pcVerdes,
      pCsecas: formValue.pcSecas,
      pCencima: formValue.pcEncima,
      emaduracion: formValue.eMaduracion,
      broca: formValue.broca,
      densidad: formValue.densidad,
      vanos: formValue.vanos,
      secos: formValue.secos,
      pCobjetivo: formValue.pcObjetivo
    };

    // Agregar RC si están incluidos
    if (this.incluyeSobremaduras && formValue.rcSobremaduras) {
      datos.rCsobremaduras = formValue.rcSobremaduras;
    }
    if (this.incluyeInmaduras && formValue.rcInmaduras) {
      datos.rCinmaduras = formValue.rcInmaduras;
    }
    if (this.incluyeMaduras && formValue.rcMaduras) {
      datos.rCmaduras = formValue.rcMaduras;
    }

    console.log('Datos a enviar:', datos);

    if (this.modoEdicion && this.tiempoOriginal) {
      // El backend requiere que el tiempo en el cuerpo coincida exactamente con el de la URL
      // Usar el tiempo original sin modificaciones
      datos.tiempo = this.tiempoOriginal;
      console.log('Actualizando con tiempo:', this.tiempoOriginal);
      console.log('Datos completos:', JSON.stringify(datos, null, 2));
      
      this.caracterizacionService.actualizar(this.tiempoOriginal, datos).subscribe({
        next: () => {
          console.log('Actualización exitosa');
          this.router.navigate(['/caracterizacion']);
        },
        error: (error) => {
          console.error('Error al actualizar:', error);
          alert('Error al actualizar: ' + error.message);
        }
      });
    } else {
      console.log('Creando nuevo registro');
      this.caracterizacionService.crear(datos).subscribe({
        next: () => {
          console.log('Creación exitosa');
          this.router.navigate(['/caracterizacion']);
        },
        error: (error) => {
          console.error('Error al crear:', error);
          alert('Error al crear: ' + error.message);
        }
      });
    }
  }

  cancelar(): void {
    this.router.navigate(['/caracterizacion']);
  }
}
