import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { TrillaService } from '../../../core/services/trilla.service';
import { AreaAcopioService } from '../../../core/services/area-acopio.service';
import { AreaAcopio } from '../../../models/area-acopio.model';

@Component({
  selector: 'app-trilla-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './trilla-form.html',
  styleUrl: './trilla-form.css'
})
export class TrillaFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private trillaService = inject(TrillaService);
  private acopioService = inject(AreaAcopioService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  formulario: FormGroup;
  modoEdicion = false;
  idTrilla: number | null = null;
  incluyePesoVerde = false;
  lotesDisponibles: AreaAcopio[] = [];

  constructor() {
    this.formulario = this.fb.group({
      nlote: ['', Validators.required],
      hinicial: [null, [Validators.min(0), Validators.max(100)]],
      hfinal: [null, [Validators.min(0), Validators.max(100)]],
      rFinalPelado: [null, [Validators.min(0), Validators.max(100)]],
      rFinalSeleccion: [null, [Validators.min(0), Validators.max(100)]],
      wverdeFinal: [null, Validators.min(0)],
      rteoricoPelado: [null, [Validators.min(0), Validators.max(100)]],
      wverdeTeorico: [null, Validators.min(0)],
      rTeoricoSeleccion: [null, [Validators.min(0), Validators.max(100)]],
      ffinalReposo: [null],
      psegundas: [null, [Validators.min(0), Validators.max(100)]],
      pcataduras: [null, [Validators.min(0), Validators.max(100)]],
      pbarreduras: [null, [Validators.min(0), Validators.max(100)]],
      pescogeduras: [null, [Validators.min(0), Validators.max(100)]],
      pcaracolillo: [null, [Validators.min(0), Validators.max(100)]],
      pprimera: [null, [Validators.min(0), Validators.max(100)]],
      pmadres: [null, [Validators.min(0), Validators.max(100)]],
      pmenudos: [null, [Validators.min(0), Validators.max(100)]],
      pinferiores: [null, [Validators.min(0), Validators.max(100)]],
      pesoVerde: this.fb.group({
        winferiores: [null, Validators.min(0)],
        wfinal: [null, Validators.min(0)],
        wFinalInferiores: [null, Validators.min(0)]
      })
    });
  }

  ngOnInit(): void {
    this.cargarLotesDisponibles();
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.modoEdicion = true;
      this.idTrilla = +id;
      this.cargarTrilla(this.idTrilla);
    }
  }

  cargarLotesDisponibles(): void {
    this.acopioService.obtenerTodos().subscribe({
      next: (lotes) => this.lotesDisponibles = lotes,
      error: (error) => console.error('Error al cargar lotes:', error)
    });
  }

  cargarTrilla(id: number): void {
    this.trillaService.obtenerPorId(id).subscribe({
      next: (trilla) => {
        this.formulario.patchValue({
          nlote: trilla.nlote,
          hinicial: trilla.hinicial,
          hfinal: trilla.hfinal,
          rFinalPelado: trilla.rFinalPelado,
          rFinalSeleccion: trilla.rFinalSeleccion,
          wverdeFinal: trilla.wverdeFinal,
          rteoricoPelado: trilla.rteoricoPelado,
          wverdeTeorico: trilla.wverdeTeorico,
          rTeoricoSeleccion: trilla.rTeoricoSeleccion,
          ffinalReposo: trilla.ffinalReposo ? trilla.ffinalReposo.substring(0, 16) : null,
          psegundas: trilla.psegundas,
          pcataduras: trilla.pcataduras,
          pbarreduras: trilla.pbarreduras,
          pescogeduras: trilla.pescogeduras,
          pcaracolillo: trilla.pcaracolillo,
          pprimera: trilla.pprimera,
          pmadres: trilla.pmadres,
          pmenudos: trilla.pmenudos,
          pinferiores: trilla.pinferiores
        });

        if (trilla.pesoVerde) {
          this.incluyePesoVerde = true;
          this.formulario.patchValue({
            pesoVerde: {
              winferiores: trilla.pesoVerde.winferiores,
              wfinal: trilla.pesoVerde.wfinal,
              wFinalInferiores: trilla.pesoVerde.wFinalInferiores
            }
          });
        }
      },
      error: (error) => console.error('Error al cargar trilla:', error)
    });
  }

  togglePesoVerde(): void {
    this.incluyePesoVerde = !this.incluyePesoVerde;
    if (!this.incluyePesoVerde) {
      this.formulario.get('pesoVerde')?.reset();
    }
  }

  guardar(): void {
    if (this.formulario.invalid) {
      this.formulario.markAllAsTouched();
      return;
    }

    const datos = this.formulario.value;
    
    // Si no se incluye PesoVerde, eliminarlo del payload
    if (!this.incluyePesoVerde) {
      delete datos.pesoVerde;
    }

    if (this.modoEdicion && this.idTrilla) {
      this.trillaService.actualizar(this.idTrilla, datos).subscribe({
        next: () => this.router.navigate(['/trilla']),
        error: (error) => console.error('Error al actualizar:', error)
      });
    } else {
      this.trillaService.crear(datos).subscribe({
        next: () => this.router.navigate(['/trilla']),
        error: (error) => console.error('Error al crear:', error)
      });
    }
  }

  cancelar(): void {
    this.router.navigate(['/trilla']);
  }
}
