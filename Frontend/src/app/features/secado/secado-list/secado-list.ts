import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { SecadoService } from '../../../core/services/secado.service';
import { Secado } from '../../../models/secado.model';

@Component({
  selector: 'app-secado-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './secado-list.html',
  styleUrl: './secado-list.css'
})
export class SecadoListComponent implements OnInit {
  private secadoService = inject(SecadoService);
  private router = inject(Router);
  private cdr = inject(ChangeDetectorRef);

  secados: Secado[] = [];
  isLoading = false;
  errorMessage = '';

  ngOnInit(): void {
    this.cargarRegistros();
  }

  cargarRegistros(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.secadoService.obtenerTodos().subscribe({
      next: (data) => {
        this.secados = data;
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

  editarRegistro(idSecado: number): void {
    this.router.navigate(['/secado/editar', idSecado]);
  }

  eliminarRegistro(idSecado: number): void {
    if (!confirm('¿Estás seguro de que deseas eliminar este registro de secado? Esta acción no se puede deshacer.')) {
      return;
    }

    this.isLoading = true;
    this.secadoService.eliminar(idSecado).subscribe({
      next: () => {
        this.cargarRegistros();
      },
      error: (error) => {
        this.errorMessage = 'Error al eliminar el registro: ' + error.message;
        this.isLoading = false;
      }
    });
  }

  formatearFecha(fecha: string): string {
    const date = new Date(fecha);
    return date.toLocaleDateString('es-ES', {
      year: 'numeric',
      month: '2-digit',
      day: '2-digit',
      hour: '2-digit',
      minute: '2-digit'
    });
  }

  contarEntidadesDebiles(secado: Secado): string {
    const counts = [];
    if (secado.humedades?.length) counts.push(`${secado.humedades.length} Humedades`);
    if (secado.termoHigrometrias?.length) counts.push(`${secado.termoHigrometrias.length} Termo-Hig.`);
    if (secado.temperaturasSecado?.length) counts.push(`${secado.temperaturasSecado.length} Temps.`);
    if (secado.ncamas?.length) counts.push(`${secado.ncamas.length} Camas`);
    return counts.length > 0 ? counts.join(', ') : 'Sin mediciones';
  }
}
