import { Component, inject, OnInit } from '@angular/core';
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

  catacionForm!: FormGroup;
  modoEdicion = false;
  idCatacion: number | null = null;
  cargando = false;
  lotesDisponibles: AreaAcopio[] = [];

  ngOnInit(): void {
    this.inicializarFormulario();
    this.cargarLotesDisponibles();
    
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.modoEdicion = true;
      this.idCatacion = parseInt(id, 10);
      this.cargarCatacion(this.idCatacion);
    }
  }

  cargarLotesDisponibles(): void {
    this.acopioService.obtenerTodos().subscribe({
      next: (lotes) => this.lotesDisponibles = lotes,
      error: (error) => console.error('Error al cargar lotes:', error)
    });
  }

  inicializarFormulario(): void {
    this.catacionForm = this.fb.group({
      nlote: ['', Validators.required],
      
      // Atributos simples
      limpio: [null, [Validators.min(0), Validators.max(100)]],
      defectuoso: [null, [Validators.min(0), Validators.max(100)]],
      ffreposo: [null, [Validators.min(0)]],
      overde: [null, [Validators.min(0)]],
      quaker: [null, [Validators.min(0)]],
      ccverde: [null, [Validators.min(0), Validators.max(100)]],
      rtostado: [null, [Validators.min(0), Validators.max(100)]],
      dfueste: [null, [Validators.min(0)]],
      cccalidad: [null, [Validators.min(0), Validators.max(100)]],
      
      // Atributos C1
      c1agrio: [null, [Validators.min(0)]],
      c1hongos: [null, [Validators.min(0)]],
      c1cerezaseca: [null, [Validators.min(0)]],
      c1negro: [null, [Validators.min(0)]],
      c1insectos: [null, [Validators.min(0)]],
      c1negrop: [null, [Validators.min(0)]],
      c1agriop: [null, [Validators.min(0)]],
      c1me: [null, [Validators.min(0)]],
      
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
      c2cortado: [null, [Validators.min(0)]],
      c2mordido: [null, [Validators.min(0)]],
      
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
      
      // Atributo derivado
      pfinales: [null, [Validators.min(0), Validators.max(100)]],
      
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

  cargarCatacion(id: number): void {
    this.cargando = true;
    this.catacionService.obtenerPorId(id).subscribe({
      next: (catacion) => {
        this.catacionForm.patchValue(catacion);
        
        // Cargar rondas existentes
        if (catacion.rondas && catacion.rondas.length > 0) {
          catacion.rondas.forEach(ronda => {
            const rondaGroup = this.crearRondaFormGroup();
            rondaGroup.patchValue(ronda);
            this.rondas.push(rondaGroup);
          });
        }
        
        this.cargando = false;
      },
      error: (error) => {
        console.error('Error al cargar catación:', error);
        this.cargando = false;
        alert('Error al cargar la catación');
      }
    });
  }

  guardar(): void {
    if (this.catacionForm.invalid) {
      alert('Por favor complete todos los campos requeridos correctamente');
      return;
    }

    this.cargando = true;
    const datos = this.catacionForm.value;

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
}
