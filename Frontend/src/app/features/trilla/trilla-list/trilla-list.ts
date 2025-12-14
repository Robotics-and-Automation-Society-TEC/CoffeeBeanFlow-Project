import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { TrillaService } from '../../../core/services/trilla.service';
import { Trilla } from '../../../models/trilla.model';

@Component({
  selector: 'app-trilla-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './trilla-list.html',
  styleUrl: './trilla-list.css'
})
export class TrillaListComponent implements OnInit {
  private trillaService = inject(TrillaService);
  private router = inject(Router);
  private cdr = inject(ChangeDetectorRef);

  trillas: Trilla[] = [];
  cargando = true;

  ngOnInit(): void {
    this.cargarRegistros();
  }

  cargarRegistros(): void {
    this.cargando = true;
    this.trillaService.obtenerTodos().subscribe({
      next: (data: any) => {
        this.trillas = Array.isArray(data) ? data : [data];
        this.cargando = false;
        this.cdr.detectChanges();
      },
      error: (error) => {
        console.error('Error al cargar trillas:', error);
        this.trillas = [];
        this.cargando = false;
        this.cdr.detectChanges();
      }
    });
  }

  nuevoRegistro(): void {
    this.router.navigate(['/trilla/nuevo']);
  }

  editarRegistro(idTrilla: number): void {
    this.router.navigate(['/trilla/editar', idTrilla]);
  }

  eliminarRegistro(idTrilla: number): void {
    if (confirm('¿Está seguro de eliminar esta trilla?')) {
      this.trillaService.eliminar(idTrilla).subscribe({
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

  calcularRendimientoReal(trilla: Trilla): number | null {
    if (trilla.pprimera === null) return null;
    const secundarias = (trilla.psegundas || 0) + (trilla.pcaracolillo || 0) + 
                        (trilla.pmadres || 0) + (trilla.pmenudos || 0);
    return trilla.pprimera + secundarias;
  }

  getEstadoRendimiento(trilla: Trilla): string {
    const rendimiento = this.calcularRendimientoReal(trilla);
    if (rendimiento === null) return 'Sin Datos';
    if (rendimiento >= 85) return 'Excelente';
    if (rendimiento >= 75) return 'Bueno';
    if (rendimiento >= 65) return 'Regular';
    return 'Bajo';
  }
}
