import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EtapaProceso, EtapaInfo, TimelineData } from '../../../models/trazabilidad.model';

@Component({
  selector: 'app-timeline-proceso',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './timeline-proceso.component.html',
  styleUrl: './timeline-proceso.component.css'
})
export class TimelineProcesoComponent implements OnInit {
  @Input() etapaActual: EtapaProceso = 'Acopio';
  
  timelineData: TimelineData = {
    etapas: [],
    etapaActual: 'Acopio',
    progreso: 0
  };

  ngOnInit(): void {
    this.construirTimeline();
  }

  ngOnChanges(): void {
    this.construirTimeline();
  }

  private construirTimeline(): void {
    const etapasConfig: { nombre: EtapaProceso; icono: string; color: string }[] = [
      { nombre: 'Acopio', icono: '🏪', color: '#8FAD5A' },
      { nombre: 'Caracterización', icono: '🔬', color: '#C8956F' },
      { nombre: 'Secado', icono: '☀️', color: '#f39c12' },
      { nombre: 'Bodega', icono: '🏭', color: '#8B5A3C' },
      { nombre: 'Trilla', icono: '⚙️', color: '#4A5D2E' },
      { nombre: 'Catación', icono: '☕', color: '#A52A3D' },
      { nombre: 'Finalizado', icono: '✅', color: '#2e3d1a' }
    ];

    const etapas: EtapaInfo[] = etapasConfig.map(config => {
      const estado = this.determinarEstado(config.nombre);
      return {
        nombre: config.nombre,
        icono: config.icono,
        color: config.color,
        estado,
        completada: estado === 'completada',
        fecha: undefined
      };
    });

    const indexActual = etapas.findIndex(e => e.nombre === this.etapaActual);
    const progreso = indexActual >= 0 
      ? Math.round((indexActual / (etapas.length - 1)) * 100) 
      : 0;

    this.timelineData = {
      etapas,
      etapaActual: this.etapaActual,
      progreso
    };
  }

  private determinarEstado(nombreEtapa: EtapaProceso): 'completada' | 'actual' | 'pendiente' {
    const orden: EtapaProceso[] = [
      'Acopio',
      'Caracterización',
      'Secado',
      'Bodega',
      'Trilla',
      'Catación',
      'Finalizado'
    ];

    const indexEtapa = orden.indexOf(nombreEtapa);
    const indexActual = orden.indexOf(this.etapaActual);

    if (indexEtapa < indexActual) return 'completada';
    if (indexEtapa === indexActual) return 'actual';
    return 'pendiente';
  }

  getEstadoClass(etapa: EtapaInfo): string {
    return etapa.estado;
  }
}
