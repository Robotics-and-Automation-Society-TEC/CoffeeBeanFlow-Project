import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormArray, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { SecadoService } from '../../../core/services/secado.service';
import { AreaAcopioService } from '../../../core/services/area-acopio.service';
import { AreaAcopio } from '../../../models/area-acopio.model';
import { CreateSecadoDto, UpdateSecadoDto } from '../../../models/secado.model';

@Component({
  selector: 'app-secado-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './secado-form.html',
  styleUrl: './secado-form.css'
})
export class SecadoFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private secadoService = inject(SecadoService);
  private acopioService = inject(AreaAcopioService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  secadoForm!: FormGroup;
  isEditMode = false;
  idSecado: number | null = null;
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
    this.secadoForm = this.fb.group({
      nlote: ['', [Validators.required, Validators.maxLength(50)]],
      finicio: ['', Validators.required],
      dsecado: [{ value: null, disabled: true }],
      ffinal: [null],
      humedades: this.fb.array([]),
      termoHigrometrias: this.fb.array([]),
      temperaturasSecado: this.fb.array([]),
      ncamas: this.fb.array([])
    });

    // Escuchar cambios en las fechas para calcular automáticamente los días
    this.secadoForm.get('finicio')?.valueChanges.subscribe(() => this.calcularDiasSecado());
    this.secadoForm.get('ffinal')?.valueChanges.subscribe(() => this.calcularDiasSecado());
  }

  private calcularDiasSecado(): void {
    const finicio = this.secadoForm.get('finicio')?.value;
    const ffinal = this.secadoForm.get('ffinal')?.value;

    if (finicio && ffinal) {
      const fechaInicio = new Date(finicio);
      const fechaFinal = new Date(ffinal);
      
      if (fechaFinal >= fechaInicio) {
        const diferenciaMilisegundos = fechaFinal.getTime() - fechaInicio.getTime();
        const dias = Math.ceil(diferenciaMilisegundos / (1000 * 60 * 60 * 24));
        this.secadoForm.get('dsecado')?.setValue(dias, { emitEvent: false });
      } else {
        this.secadoForm.get('dsecado')?.setValue(null, { emitEvent: false });
      }
    } else {
      this.secadoForm.get('dsecado')?.setValue(null, { emitEvent: false });
    }
  }

  private checkEditMode(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.idSecado = parseInt(id, 10);
      this.cargarDatos();
    }
  }

  private cargarDatos(): void {
    if (this.idSecado === null) return;

    this.isLoading = true;
    this.secadoService.obtenerPorId(this.idSecado).subscribe({
      next: (secado) => {
        this.secadoForm.patchValue({
          nlote: secado.nlote,
          finicio: this.formatDateForInput(secado.finicio),
          dsecado: secado.dsecado,
          ffinal: secado.ffinal ? this.formatDateForInput(secado.ffinal) : null
        });

        // Cargar entidades débiles
        secado.humedades?.forEach(h => this.agregarHumedad(h));
        secado.termoHigrometrias?.forEach(t => this.agregarTermoHigrometria(t));
        secado.temperaturasSecado?.forEach(t => this.agregarTemperaturaSecado(t));
        secado.ncamas?.forEach(n => this.agregarNcama(n));

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

  // ============================================
  // GETTERS PARA FORMARRAYS
  // ============================================

  get humedades(): FormArray {
    return this.secadoForm.get('humedades') as FormArray;
  }

  get termoHigrometrias(): FormArray {
    return this.secadoForm.get('termoHigrometrias') as FormArray;
  }

  get temperaturasSecado(): FormArray {
    return this.secadoForm.get('temperaturasSecado') as FormArray;
  }

  get ncamas(): FormArray {
    return this.secadoForm.get('ncamas') as FormArray;
  }

  // ============================================
  // HUMEDAD - Agregar/Eliminar
  // ============================================

  agregarHumedad(humedad?: any): void {
    const humedadGroup = this.fb.group({
      idHumedad: [humedad?.idHumedad || null],
      fecha: [humedad?.fecha ? this.formatDateForInput(humedad.fecha) : '', Validators.required],
      humedad: [humedad?.humedad || null, [Validators.required, Validators.min(0), Validators.max(100)]]
    });
    this.humedades.push(humedadGroup);
  }

  eliminarHumedad(index: number): void {
    this.humedades.removeAt(index);
  }

  // ============================================
  // TERMO-HIGROMETRIA - Agregar/Eliminar
  // ============================================

  agregarTermoHigrometria(termo?: any): void {
    const termoGroup = this.fb.group({
      idTermoHigrometria: [termo?.idTermoHigrometria || null],
      fecha: [termo?.fecha ? this.formatDateForInput(termo.fecha) : '', Validators.required],
      temperaturaAmbiente: [termo?.temperaturaAmbiente || null, [Validators.required, Validators.min(-50), Validators.max(70)]],
      humedadAmbiente: [termo?.humedadAmbiente || null, [Validators.required, Validators.min(0), Validators.max(100)]]
    });
    this.termoHigrometrias.push(termoGroup);
  }

  eliminarTermoHigrometria(index: number): void {
    this.termoHigrometrias.removeAt(index);
  }

  // ============================================
  // TEMPERATURA SECADO - Agregar/Eliminar
  // ============================================

  agregarTemperaturaSecado(temp?: any): void {
    const tempGroup = this.fb.group({
      idTemperaturaSecado: [temp?.idTemperaturaSecado || null],
      fecha: [temp?.fecha ? this.formatDateForInput(temp.fecha) : '', Validators.required],
      temperatura: [temp?.temperatura || null, [Validators.required, Validators.min(0), Validators.max(100)]]
    });
    this.temperaturasSecado.push(tempGroup);
  }

  eliminarTemperaturaSecado(index: number): void {
    this.temperaturasSecado.removeAt(index);
  }

  // ============================================
  // NCAMA - Agregar/Eliminar
  // ============================================

  agregarNcama(ncama?: any): void {
    const ncamaGroup = this.fb.group({
      idNcama: [ncama?.idNcama || null],
      numeroCama: [ncama?.numeroCama || null, [Validators.required, Validators.min(1)]]
    });
    this.ncamas.push(ncamaGroup);
  }

  eliminarNcama(index: number): void {
    this.ncamas.removeAt(index);
  }

  // ============================================
  // SUBMIT
  // ============================================

  onSubmit(): void {
    console.log('=== INICIO onSubmit ===');
    console.log('Formulario válido:', !this.secadoForm.invalid);
    
    if (this.secadoForm.invalid) {
      console.log('Formulario inválido - mostrando errores');
      this.secadoForm.markAllAsTouched();
      this.errorMessage = 'Por favor, completa todos los campos requeridos';
      
      // Mostrar qué campos están inválidos
      Object.keys(this.secadoForm.controls).forEach(key => {
        const control = this.secadoForm.get(key);
        if (control?.invalid) {
          console.log(`Campo inválido: ${key}`, control.errors);
        }
      });
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    // Habilitar dsecado temporalmente para obtener su valor
    this.secadoForm.get('dsecado')?.enable();
    const formValue = this.secadoForm.value;
    this.secadoForm.get('dsecado')?.disable();

    console.log('Valores del formulario:', formValue);

    if (this.isEditMode && this.idSecado !== null) {
      console.log('Modo edición - ID:', this.idSecado);
      
      const updateDto: UpdateSecadoDto = {
        idSecado: this.idSecado,
        nlote: formValue.nlote,
        finicio: new Date(formValue.finicio).toISOString(),
        dsecado: formValue.dsecado,
        ffinal: formValue.ffinal ? new Date(formValue.ffinal).toISOString() : null,
        humedades: formValue.humedades.map((h: any) => ({
          ...h,
          fecha: new Date(h.fecha).toISOString()
        })),
        termoHigrometrias: formValue.termoHigrometrias.map((t: any) => ({
          ...t,
          fecha: new Date(t.fecha).toISOString()
        })),
        temperaturasSecado: formValue.temperaturasSecado.map((t: any) => ({
          ...t,
          fecha: new Date(t.fecha).toISOString()
        })),
        ncamas: formValue.ncamas
      };

      console.log('DTO de actualización:', updateDto);

      this.secadoService.actualizar(this.idSecado, updateDto).subscribe({
        next: () => {
          console.log('Actualización exitosa');
          this.isLoading = false;
          this.router.navigate(['/secado']);
        },
        error: (error) => {
          console.error('Error al actualizar:', error);
          this.errorMessage = 'Error al actualizar: ' + error.message;
          this.isLoading = false;
          alert('Error al actualizar: ' + error.message);
        }
      });
    } else {
      console.log('Modo creación');
      
      const createDto: any = {
        nlote: formValue.nlote,
        finicio: new Date(formValue.finicio).toISOString(),
        dsecado: formValue.dsecado || null,
        ffinal: formValue.ffinal ? new Date(formValue.ffinal).toISOString() : null,
        humedades: formValue.humedades?.length > 0 ? formValue.humedades.map((h: any) => ({
          fecha: new Date(h.fecha).toISOString(),
          humedad: h.humedad
        })) : [],
        termoHigrometrias: formValue.termoHigrometrias?.length > 0 ? formValue.termoHigrometrias.map((t: any) => ({
          fecha: new Date(t.fecha).toISOString(),
          temperaturaAmbiente: t.temperaturaAmbiente,
          humedadAmbiente: t.humedadAmbiente
        })) : [],
        temperaturasSecado: formValue.temperaturasSecado?.length > 0 ? formValue.temperaturasSecado.map((t: any) => ({
          fecha: new Date(t.fecha).toISOString(),
          temperatura: t.temperatura
        })) : [],
        ncamas: formValue.ncamas?.length > 0 ? formValue.ncamas.map((n: any) => ({
          numeroCama: String(n.numeroCama)  // Convertir a string
        })) : []
      };

      console.log('DTO de creación:', JSON.stringify(createDto, null, 2));
      console.log('Enviando petición al backend...');

      this.secadoService.crear(createDto).subscribe({
        next: (response) => {
          console.log('Creación exitosa. Respuesta:', response);
          this.isLoading = false;
          this.router.navigate(['/secado']);
        },
        error: (error) => {
          console.error('Error completo:', error);
          console.error('Error status:', error.status);
          console.error('Error statusText:', error.statusText);
          console.error('Error error:', error.error);
          
          let mensajeError = 'Error al crear el secado';
          if (error.error?.message) {
            mensajeError = error.error.message;
          } else if (error.error?.errors) {
            mensajeError = JSON.stringify(error.error.errors);
          } else if (error.message) {
            mensajeError = error.message;
          }
          
          this.errorMessage = mensajeError;
          this.isLoading = false;
          alert('Error al crear: ' + mensajeError);
        }
      });
    }
    
    console.log('=== FIN onSubmit ===');
  }

  cancelar(): void {
    this.router.navigate(['/secado']);
  }
}
