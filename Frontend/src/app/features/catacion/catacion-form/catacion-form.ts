import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormArray, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CatacionService } from '../../../core/services/catacion.service';
import { AreaAcopioService } from '../../../core/services/area-acopio.service';
import { AreaAcopio } from '../../../models/area-acopio.model';

@Component({
  selector: 'app-catacion-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './catacion-form.html',
  styleUrl: './catacion-form.css'
})
export class CatacionFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private catacionService = inject(CatacionService);
  private acopioService = inject(AreaAcopioService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private cdr = inject(ChangeDetectorRef);

  catacionForm!: FormGroup;
  modoEdicion = false;
  idCatacion: number | null = null;
  cargando = true; // CAMBIADO: Empezar en true mientras se cargan los datos
  lotesDisponibles: AreaAcopio[] = [];
  errorMessage: string | null = null;

  ngOnInit(): void {
    console.log('=== CatacionFormComponent ngOnInit ===');
    console.log('Inicializando componente de catación...');
    
    this.inicializarFormulario();
    console.log('Formulario inicializado');
    
    const id = this.route.snapshot.paramMap.get('id');
    console.log('ID de ruta:', id);
    
    if (id) {
      this.modoEdicion = true;
      this.idCatacion = parseInt(id, 10);
      console.log('Modo edición activado. ID:', this.idCatacion);
      // En modo edición: cargar lotes Y catación en paralelo
      this.cargarDatosEdicion();
    } else {
      console.log('Modo creación. Solo cargar lotes.');
      // En modo creación: solo cargar lotes
      this.cargarLotesDisponibles();
    }
  }

  cargarDatosEdicion(): void {
    console.log('=== CARGAR DATOS EDICIÓN ===');
    console.log('Estado inicial - cargando:', this.cargando);
    this.cargando = true;
    this.errorMessage = null;
    console.log('Estado después de setear true - cargando:', this.cargando);
    
    let lotesCompletos = false;
    let catacionCompleta = false;
    console.log('Banderas inicializadas - lotesCompletos:', lotesCompletos, 'catacionCompleta:', catacionCompleta);
    
    // Cargar lotes
    this.acopioService.obtenerTodos().subscribe({
      next: (lotes) => {
        console.log('Lotes cargados:', lotes);
        this.lotesDisponibles = lotes;
        lotesCompletos = true;
        // Solo quitar loading si ambos terminaron
        if (lotesCompletos && catacionCompleta) {
          console.log('✅ AMBOS DATOS CARGADOS');
          console.log('Estado ANTES de cambiar - cargando:', this.cargando);
          console.log('lotesCompletos:', lotesCompletos, 'catacionCompleta:', catacionCompleta);
          
          // Usar setTimeout para asegurar que Angular detecte el cambio
          setTimeout(() => {
            this.cargando = false;
            console.log('✅ LOADING CAMBIADO A FALSE');
            console.log('Estado DESPUÉS de cambiar - cargando:', this.cargando);
            this.cdr.detectChanges();
            console.log('✅ detectChanges() ejecutado');
          }, 0);
        }
      },
      error: (error) => {
        console.error('Error al cargar lotes:', error);
        this.errorMessage = 'Error al cargar los lotes disponibles.';
        this.cargando = false;
        this.cdr.detectChanges();
        alert(this.errorMessage);
      }
    });
    
    // Cargar catación
    if (this.idCatacion) {
      this.catacionService.obtenerPorId(this.idCatacion).subscribe({
        next: (catacion) => {
          console.log('Catación cargada:', catacion);
          
          // Formatear fFreposo para el input datetime-local
          const catacionFormateada = {
            ...catacion,
            fFreposo: catacion.fFreposo ? this.formatDateForInput(catacion.fFreposo) : null
          };
          
          this.catacionForm.patchValue(catacionFormateada);
          
          // Cargar rondas existentes
          if (catacion.rondas && catacion.rondas.length > 0) {
            catacion.rondas.forEach(ronda => {
              const rondaGroup = this.crearRondaFormGroup();
              rondaGroup.patchValue(ronda);
              this.rondas.push(rondaGroup);
            });
          }
          
          catacionCompleta = true;
          // Solo quitar loading si ambos terminaron
          if (lotesCompletos && catacionCompleta) {
            console.log('✅ AMBOS DATOS CARGADOS (desde catación callback)');
            console.log('Estado ANTES de cambiar - cargando:', this.cargando);
            console.log('lotesCompletos:', lotesCompletos, 'catacionCompleta:', catacionCompleta);
            
            // Usar setTimeout para asegurar que Angular detecte el cambio
            setTimeout(() => {
              this.cargando = false;
              console.log('✅ LOADING CAMBIADO A FALSE');
              console.log('Estado DESPUÉS de cambiar - cargando:', this.cargando);
              this.cdr.detectChanges();
              console.log('✅ detectChanges() ejecutado');
            }, 0);
          }
        },
        error: (error) => {
          console.error('Error al cargar catación:', error);
          this.errorMessage = `Error al cargar la catación: ${error.message}`;
          this.cargando = false;
          this.cdr.detectChanges();
          alert(this.errorMessage);
        }
      });
    }
  }

  cargarLotesDisponibles(): void {
    console.log('Cargando lotes disponibles...');
    this.cargando = true;
    this.errorMessage = null;
    
    this.acopioService.obtenerTodos().subscribe({
      next: (lotes) => {
        console.log('Lotes cargados:', lotes);
        this.lotesDisponibles = lotes;
        console.log('Terminando loading (modo creación)');
        this.cargando = false;
        this.cdr.detectChanges(); // FORZAR DETECCIÓN DE CAMBIOS
        console.log('Change detection ejecutado, cargando =', this.cargando);
      },
      error: (error) => {
        console.error('Error al cargar lotes:', error);
        this.errorMessage = 'Error al cargar los lotes disponibles. Por favor, verifica que el servidor esté funcionando.';
        this.cargando = false;
        this.cdr.detectChanges();
        alert(this.errorMessage);
      }
    });
  }

  inicializarFormulario(): void {
    this.catacionForm = this.fb.group({
      nlote: ['', Validators.required],
      
      // Atributos simples
      limpio: [null, [Validators.min(0), Validators.max(100)]],
      defectuoso: [null, [Validators.min(0), Validators.max(100)]],
      fFreposo: [null],
      overde: [null, [Validators.min(0)]],
      quaker: [null, [Validators.min(0)]],
      cCverde: [null, [Validators.min(0), Validators.max(100)]],
      rtostado: [null, [Validators.min(0), Validators.max(100)]],
      dfueste: [null, [Validators.min(0)]],
      cCcalidad: [null, [Validators.min(0), Validators.max(100)]],
      
      // Atributos C1
      c1agrio: [null, [Validators.min(0)]],
      c1hongos: [null, [Validators.min(0)]],
      c1cerezaseca: [null, [Validators.min(0)]],
      c1negro: [null, [Validators.min(0)]],
      c1insectos: [null, [Validators.min(0)]],
      c1ME: [null, [Validators.min(0)]],
      
      // Atributos C2
      c2flotador: [null, [Validators.min(0)]],
      c2averanado: [null, [Validators.min(0)]],
      c2pergamino: [null, [Validators.min(0)]],
      c2inmaduro: [null, [Validators.min(0)]],
      c2concha: [null, [Validators.min(0)]],
      c2insectos: [null, [Validators.min(0)]],
      
      // C2CP
      c2cascara: [null, [Validators.min(0)]],
      c2pulpa: [null, [Validators.min(0)]],
      
      // C2PCM
      c2partido: [null, [Validators.min(0)]],
      c2mordido: [null, [Validators.min(0)]],
      c2negroP: [null, [Validators.min(0)]],
      c2agrioP: [null, [Validators.min(0)]],
      
      // Zaranda
      tresSobreDieciseis: [null, [Validators.min(0), Validators.max(100)]],
      veinte: [null, [Validators.min(0), Validators.max(100)]],
      diecinueve: [null, [Validators.min(0), Validators.max(100)]],
      dieciocho: [null, [Validators.min(0), Validators.max(100)]],
      diecisiete: [null, [Validators.min(0), Validators.max(100)]],
      dieciseis: [null, [Validators.min(0), Validators.max(100)]],
      quince: [null, [Validators.min(0), Validators.max(100)]],
      catorce: [null, [Validators.min(0), Validators.max(100)]],
      trece: [null, [Validators.min(0), Validators.max(100)]],
      
      // TonAgton
      tonagton25: [null, [Validators.min(0), Validators.max(100)]],
      tonagton35: [null, [Validators.min(0), Validators.max(100)]],
      tonagton45: [null, [Validators.min(0), Validators.max(100)]],
      tonagton55: [null, [Validators.min(0), Validators.max(100)]],
      tonagton65: [null, [Validators.min(0), Validators.max(100)]],
      tonagton75: [null, [Validators.min(0), Validators.max(100)]],
      tonagton85: [null, [Validators.min(0), Validators.max(100)]],
      tonagton95: [null, [Validators.min(0), Validators.max(100)]],
      
      // FormArray para Rondas (1:N)
      rondas: this.fb.array([])
    });
  }

  get rondas(): FormArray {
    return this.catacionForm.get('rondas') as FormArray;
  }

  crearRondaFormGroup(): FormGroup {
    return this.fb.group({
      idRondas: [0],
      idCatacion: [0],
      valorCalidad: [null, [Validators.min(0), Validators.max(100)]]
    });
  }

  agregarRonda(): void {
    this.rondas.push(this.crearRondaFormGroup());
  }

  eliminarRonda(index: number): void {
    this.rondas.removeAt(index);
  }

  guardar(): void {
    if (this.catacionForm.invalid) {
      alert('Por favor complete todos los campos requeridos correctamente');
      return;
    }

    this.cargando = true;
    const datos = this.catacionForm.value;

    console.log('=== GUARDAR CATACIÓN ===');
    console.log('Datos del formulario antes de conversión:', datos);
    console.log('fFreposo antes:', datos.fFreposo);
    console.log('cCverde:', datos.cCverde);
    console.log('cCcalidad:', datos.cCcalidad);
    console.log('c1ME:', datos.c1ME);
    console.log('c2negroP:', datos.c2negroP);
    console.log('c2agrioP:', datos.c2agrioP);

    // Convertir fFreposo a ISO string si existe
    if (datos.fFreposo) {
      datos.fFreposo = new Date(datos.fFreposo).toISOString();
      console.log('fFreposo después de conversión:', datos.fFreposo);
    }

    console.log('Datos completos a enviar:', JSON.stringify(datos, null, 2));

    if (this.modoEdicion && this.idCatacion) {
      datos.idCatacion = this.idCatacion;
      this.catacionService.actualizar(this.idCatacion, datos).subscribe({
        next: () => {
          this.cargando = false;
          alert('Catación actualizada exitosamente');
          this.router.navigate(['/catacion']);
        },
        error: (error) => {
          console.error('Error al actualizar:', error);
          this.cargando = false;
          alert('Error al actualizar la catación');
        }
      });
    } else {
      this.catacionService.crear(datos).subscribe({
        next: () => {
          this.cargando = false;
          alert('Catación creada exitosamente');
          this.router.navigate(['/catacion']);
        },
        error: (error) => {
          console.error('Error al crear:', error);
          this.cargando = false;
          alert('Error al crear la catación');
        }
      });
    }
  }

  cancelar(): void {
    this.router.navigate(['/catacion']);
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
}
