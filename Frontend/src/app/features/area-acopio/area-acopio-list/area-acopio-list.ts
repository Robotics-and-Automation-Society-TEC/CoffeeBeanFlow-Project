import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { AreaAcopioService } from '../../../core/services/area-acopio.service';
import { AreaAcopio } from '../../../models/area-acopio.model';

@Component({
  selector: 'app-area-acopio-list',
  imports: [CommonModule, RouterModule],
  templateUrl: './area-acopio-list.html',
  styleUrl: './area-acopio-list.css',
})
export class AreaAcopioListComponent implements OnInit {
  private router = inject(Router);
  private areaAcopioService = inject(AreaAcopioService);
  private cdr = inject(ChangeDetectorRef);

  registros: AreaAcopio[] = [];
  isLoading = true;
  errorMessage = '';
  successMessage = '';

  ngOnInit(): void {
    this.cargarRegistros();
  }

  cargarRegistros(): void {
    this.isLoading = true;
    this.errorMessage = '';
    console.log('AreaAcopioList: Iniciando carga de registros...');
    
    this.areaAcopioService.obtenerTodos().subscribe({
      next: (registros) => {
        console.log('AreaAcopioList: Registros recibidos:', registros);
        this.registros = registros;
        this.isLoading = false;
        this.cdr.detectChanges();
      },
      error: (error) => {
        console.error('AreaAcopioList: Error recibido:', error);
        this.errorMessage = 'Error al cargar los registros: ' + error.message;
        this.isLoading = false;
        this.cdr.detectChanges();
      },
      complete: () => {
        console.log('AreaAcopioList: Petición completada');
      }
    });
  }

  nuevoRegistro(): void {
    this.router.navigate(['/area-acopio/nuevo']);
  }

  editarRegistro(nlote: string): void {
    this.router.navigate(['/area-acopio/editar', nlote]);
  }

  eliminarRegistro(nlote: string): void {
    if (!confirm(`¿Está seguro de eliminar el lote ${nlote}?`)) {
      return;
    }

    this.areaAcopioService.eliminar(nlote).subscribe({
      next: () => {
        this.successMessage = 'Registro eliminado exitosamente';
        this.cargarRegistros();
        setTimeout(() => this.successMessage = '', 3000);
      },
      error: (error) => {
        this.errorMessage = 'Error al eliminar: ' + error.message;
      }
    });
  }

  getTipoDespulpado(registro: AreaAcopio): string {
    const tipos: string[] = [];
    if (registro.semilavado) tipos.push('Semilavado');
    if (registro.natural) tipos.push('Natural');
    if (registro.anaerobico) tipos.push('Anaeróbico');
    if (registro.miel) tipos.push('Miel');
    if (registro.lavado) tipos.push('Lavado');
    if (registro.otro) tipos.push(registro.otro);
    return tipos.length > 0 ? tipos.join(', ') : 'N/A';
  }
}
