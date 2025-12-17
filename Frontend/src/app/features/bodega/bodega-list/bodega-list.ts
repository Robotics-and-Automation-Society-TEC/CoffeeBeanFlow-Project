import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { BodegaService } from '../../../core/services/bodega.service';
import { Bodega } from '../../../models/bodega.model';

@Component({
  selector: 'app-bodega-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './bodega-list.html',
  styleUrl: './bodega-list.css'
})
export class BodegaListComponent implements OnInit {
  private bodegaService = inject(BodegaService);
  private router = inject(Router);
  private cdr = inject(ChangeDetectorRef);

  bodegas: Bodega[] = [];
  isLoading = false;
  errorMessage = '';

  ngOnInit(): void {
    this.cargarRegistros();
  }

  cargarRegistros(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.bodegaService.obtenerTodos().subscribe({
      next: (data) => {
        this.bodegas = data;
        this.isLoading = false;
        this.cdr.detectChanges();
      },
      error: (error) => {
        this.errorMessage = 'Error al cargar los registros: ' + error.message;
        this.isLoading = false;
        this.cdr.detectChanges();
      }
    });
  }

  editarRegistro(idBodega: number): void {
    this.router.navigate(['/bodega/editar', idBodega]);
  }

  eliminarRegistro(idBodega: number): void {
    if (!confirm('¿Estás seguro de que deseas eliminar este registro de bodega? Esta acción no se puede deshacer.')) {
      return;
    }

    this.isLoading = true;
    this.bodegaService.eliminar(idBodega).subscribe({
      next: () => {
        this.cargarRegistros();
      },
      error: (error) => {
        this.errorMessage = 'Error al eliminar el registro: ' + error.message;
        this.isLoading = false;
      }
    });
  }

  formatearFecha(fecha: string | null): string {
    if (!fecha) return '-';
    const date = new Date(fecha);
    return date.toLocaleDateString('es-ES', {
      year: 'numeric',
      month: '2-digit',
      day: '2-digit',
      hour: '2-digit',
      minute: '2-digit'
    });
  }

  formatearNumero(valor: number | null | undefined): string {
    if (valor === null || valor === undefined) return '-';
    return valor.toFixed(2);
  }

  getEstadoHumedad(bodega: Bodega): string {
    if (bodega.hfinal === null) return 'Sin medición';
    if (bodega.hfinal <= 12) return 'Óptima';
    if (bodega.hfinal <= 14) return 'Aceptable';
    return 'Alta';
  }

  getClaseEstadoHumedad(bodega: Bodega): string {
    if (bodega.hfinal === null) return 'badge-neutral';
    if (bodega.hfinal <= 12) return 'badge-success';
    if (bodega.hfinal <= 14) return 'badge-warning';
    return 'badge-danger';
  }
}
