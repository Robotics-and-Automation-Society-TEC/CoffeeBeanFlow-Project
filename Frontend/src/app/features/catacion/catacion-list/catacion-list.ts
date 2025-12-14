import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { CatacionService } from '../../../core/services/catacion.service';
import { Catacion } from '../../../models/catacion.model';

@Component({
  selector: 'app-catacion-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './catacion-list.html',
  styleUrl: './catacion-list.css'
})
export class CatacionListComponent implements OnInit {
  private catacionService = inject(CatacionService);
  private router = inject(Router);
  private cdr = inject(ChangeDetectorRef);

  cataciones: Catacion[] = [];
  cargando = true;

  ngOnInit(): void {
    this.cargarRegistros();
  }

  cargarRegistros(): void {
    this.cargando = true;
    this.catacionService.obtenerTodos().subscribe({
      next: (data: any) => {
        this.cataciones = Array.isArray(data) ? data : [data];
        this.cargando = false;
        this.cdr.detectChanges();
      },
      error: (error) => {
        console.error('Error al cargar cataciones:', error);
        this.cataciones = [];
        this.cargando = false;
        this.cdr.detectChanges();
      }
    });
  }

  nuevoRegistro(): void {
    this.router.navigate(['/catacion/nuevo']);
  }

  editarRegistro(id: number): void {
    this.router.navigate(['/catacion/editar', id]);
  }

  eliminarRegistro(id: number): void {
    if (confirm('¿Está seguro de eliminar esta catación?')) {
      this.catacionService.eliminar(id).subscribe({
        next: () => this.cargarRegistros(),
        error: (error) => console.error('Error al eliminar:', error)
      });
    }
  }

  calcularPromedioRondas(catacion: Catacion): number | null {
    if (!catacion.rondas || catacion.rondas.length === 0) return null;
    
    const valores = catacion.rondas
      .filter(r => r.valorCalidad !== null)
      .map(r => r.valorCalidad!);
    
    if (valores.length === 0) return null;
    
    const suma = valores.reduce((acc, val) => acc + val, 0);
    return Math.round((suma / valores.length) * 100) / 100;
  }

  getEstadoCalidad(pfinales: number | null): string {
    if (pfinales === null) return 'Sin Datos';
    if (pfinales >= 85) return 'Excelente';
    if (pfinales >= 80) return 'Muy Bueno';
    if (pfinales >= 75) return 'Bueno';
    if (pfinales >= 70) return 'Regular';
    return 'Bajo';
  }

  contarDefectosCategoria1(catacion: Catacion): number {
    let total = 0;
    if (catacion.c1agrio) total += catacion.c1agrio;
    if (catacion.c1hongos) total += catacion.c1hongos;
    if (catacion.c1cerezaseca) total += catacion.c1cerezaseca;
    if (catacion.c1negro) total += catacion.c1negro;
    if (catacion.c1insectos) total += catacion.c1insectos;
    if (catacion.c1negrop) total += catacion.c1negrop;
    if (catacion.c1agriop) total += catacion.c1agriop;
    if (catacion.c1me) total += catacion.c1me;
    return Math.round(total * 100) / 100;
  }

  contarDefectosCategoria2(catacion: Catacion): number {
    let total = 0;
    if (catacion.c2flotador) total += catacion.c2flotador;
    if (catacion.c2averanado) total += catacion.c2averanado;
    if (catacion.c2pergamino) total += catacion.c2pergamino;
    if (catacion.c2inmaduro) total += catacion.c2inmaduro;
    if (catacion.c2concha) total += catacion.c2concha;
    if (catacion.c2insectos) total += catacion.c2insectos;
    if (catacion.c2cascara) total += catacion.c2cascara;
    if (catacion.c2pulpa) total += catacion.c2pulpa;
    if (catacion.c2partido) total += catacion.c2partido;
    if (catacion.c2cortado) total += catacion.c2cortado;
    if (catacion.c2mordido) total += catacion.c2mordido;
    return Math.round(total * 100) / 100;
  }
}
