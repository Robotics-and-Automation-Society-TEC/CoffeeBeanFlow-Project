import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { TrazabilidadCompleta, EtapaProceso } from '../../../models/trazabilidad.model';
import { TrazabilidadService } from '../../../core/services/trazabilidad.service';
import { TimelineProcesoComponent } from '../timeline-proceso/timeline-proceso.component';

@Component({
  selector: 'app-trazabilidad-lote',
  standalone: true,
  imports: [CommonModule, FormsModule, TimelineProcesoComponent],
  templateUrl: './trazabilidad-lote.component.html',
  styleUrl: './trazabilidad-lote.component.css'
})
export class TrazabilidadLoteComponent implements OnInit {
  nlote: string = '';
  trazabilidad?: TrazabilidadCompleta;
  loading: boolean = false;
  error: string = '';

  // Estado de expansión de secciones
  seccionesExpandidas = {
    acopio: true,
    caracterizacion: false,
    secado: false,
    bodega: false,
    trilla: false,
    catacion: false,
    metricas: true
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private trazabilidadService: TrazabilidadService
  ) {}

  ngOnInit(): void {
    // Obtener nlote de los query params
    this.route.queryParams.subscribe(params => {
      this.nlote = params['nlote'] || '';
      if (this.nlote) {
        this.cargarTrazabilidad();
      }
    });
  }

  cargarTrazabilidad(): void {
    if (!this.nlote) {
      this.error = 'Debe especificar un número de lote';
      return;
    }

    this.loading = true;
    this.error = '';

    this.trazabilidadService.obtenerTrazabilidadCompleta(this.nlote)
      .subscribe({
        next: (data) => {
          this.trazabilidad = data;
          this.loading = false;

          // Si no hay datos, mostrar error
          if (!data.acopio) {
            this.error = `No se encontró información para el lote ${this.nlote}`;
          }
        },
        error: (err) => {
          this.error = `Error al cargar la trazabilidad del lote ${this.nlote}`;
          this.loading = false;
          console.error('Error:', err);
        }
      });
  }

  toggleSeccion(seccion: keyof typeof this.seccionesExpandidas): void {
    this.seccionesExpandidas[seccion] = !this.seccionesExpandidas[seccion];
  }

  expandirTodas(): void {
    Object.keys(this.seccionesExpandidas).forEach(key => {
      this.seccionesExpandidas[key as keyof typeof this.seccionesExpandidas] = true;
    });
  }

  colapsarTodas(): void {
    Object.keys(this.seccionesExpandidas).forEach(key => {
      this.seccionesExpandidas[key as keyof typeof this.seccionesExpandidas] = false;
    });
  }

  getEstadoEtapa(etapa: EtapaProceso): 'completada' | 'actual' | 'pendiente' {
    if (!this.trazabilidad) return 'pendiente';

    const orden: EtapaProceso[] = [
      'Acopio',
      'Caracterización',
      'Secado',
      'Bodega',
      'Trilla',
      'Catación',
      'Finalizado'
    ];

    const indexEtapa = orden.indexOf(etapa);
    const indexActual = orden.indexOf(this.trazabilidad.etapaActual);

    if (indexEtapa < indexActual) return 'completada';
    if (indexEtapa === indexActual) return 'actual';
    return 'pendiente';
  }

  tieneInformacion(seccion: string): boolean {
    if (!this.trazabilidad) return false;

    switch (seccion) {
      case 'acopio':
        return !!this.trazabilidad.acopio;
      case 'caracterizacion':
        return !!this.trazabilidad.caracterizaciones && this.trazabilidad.caracterizaciones.length > 0;
      case 'secado':
        return !!this.trazabilidad.secados && this.trazabilidad.secados.length > 0;
      case 'bodega':
        return !!this.trazabilidad.bodegas && this.trazabilidad.bodegas.length > 0;
      case 'trilla':
        return !!this.trazabilidad.trillas && this.trazabilidad.trillas.length > 0;
      case 'catacion':
        return !!this.trazabilidad.cataciones && this.trazabilidad.cataciones.length > 0;
      default:
        return false;
    }
  }

  volver(): void {
    this.router.navigate(['/historial']);
  }

  buscarNuevoLote(): void {
    if (this.nlote) {
      this.cargarTrazabilidad();
    }
  }

  exportarPDF(): void {
    alert('Funcionalidad de exportación a PDF en desarrollo');
  }

  imprimirTrazabilidad(): void {
    window.print();
  }
}
