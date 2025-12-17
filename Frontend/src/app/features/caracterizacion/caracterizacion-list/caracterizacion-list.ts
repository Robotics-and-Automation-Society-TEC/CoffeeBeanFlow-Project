import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { CaracterizacionService } from '../../../core/services/caracterizacion.service';
import { Caracterizacion } from '../../../models/caracterizacion.model';

@Component({
  selector: 'app-caracterizacion-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './caracterizacion-list.html',
  styleUrl: './caracterizacion-list.css'
})
export class CaracterizacionListComponent implements OnInit {
  private caracterizacionService = inject(CaracterizacionService);
  private router = inject(Router);
  private cdr = inject(ChangeDetectorRef);

  caracterizaciones: Caracterizacion[] = [];
  cargando = true;

  ngOnInit(): void {
    this.cargarRegistros();
  }

  cargarRegistros(): void {
    this.cargando = true;
    this.caracterizacionService.obtenerTodos().subscribe({
      next: (data: any) => {
        this.caracterizaciones = Array.isArray(data) ? data : [data];
        this.cargando = false;
        this.cdr.detectChanges();
      },
      error: (error) => {
        console.error('Error al cargar caracterizaciones:', error);
        this.caracterizaciones = [];
        this.cargando = false;
        this.cdr.detectChanges();
      }
    });
  }

  nuevoRegistro(): void {
    this.router.navigate(['/caracterizacion/nuevo']);
  }

  editarRegistro(tiempo: string): void {
    this.router.navigate(['/caracterizacion/editar', encodeURIComponent(tiempo)]);
  }

  eliminarRegistro(tiempo: string): void {
    if (confirm('¿Está seguro de eliminar esta caracterización?')) {
      this.caracterizacionService.eliminar(tiempo).subscribe({
        next: () => this.cargarRegistros(),
        error: (error) => console.error('Error al eliminar:', error)
      });
    }
  }

  formatearFecha(fecha: string | null): string {
    if (!fecha) return '-';
    return new Date(fecha).toLocaleString('es-ES', {
      year: 'numeric',
      month: '2-digit',
      day: '2-digit',
      hour: '2-digit',
      minute: '2-digit'
    });
  }

  calcularCalidadGeneral(caract: Caracterizacion): number | null {
    if (caract.emaduracion === null) return null;
    return caract.emaduracion;
  }

  getEstadoCalidad(caract: Caracterizacion): string {
    const calidad = this.calcularCalidadGeneral(caract);
    if (calidad === null) return 'Sin Datos';
    if (calidad >= 85) return 'Excelente';
    if (calidad >= 75) return 'Bueno';
    if (calidad >= 65) return 'Regular';
    return 'Bajo';
  }

  tieneRC(caract: Caracterizacion): boolean {
    return !!(caract.rCsobremaduras || caract.rCinmaduras || caract.rCmaduras);
  }

  contarRC(caract: Caracterizacion): number {
    let count = 0;
    if (caract.rCsobremaduras) count++;
    if (caract.rCinmaduras) count++;
    if (caract.rCmaduras) count++;
    return count;
  }
}
