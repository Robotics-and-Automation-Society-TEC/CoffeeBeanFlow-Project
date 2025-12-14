import { inject, Injectable } from '@angular/core';
import { Observable, forkJoin, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { TrazabilidadCompleta, EtapaProceso, MetricasTrazabilidad } from '../../models/trazabilidad.model';
import { AreaAcopioService } from './area-acopio.service';
import { CaracterizacionService } from './caracterizacion.service';
import { SecadoService } from './secado.service';
import { BodegaService } from './bodega.service';
import { TrillaService } from './trilla.service';
import { CatacionService } from './catacion.service';

@Injectable({
  providedIn: 'root'
})
export class TrazabilidadService {
  private acopioService = inject(AreaAcopioService);
  private caracterizacionService = inject(CaracterizacionService);
  private secadoService = inject(SecadoService);
  private bodegaService = inject(BodegaService);
  private trillaService = inject(TrillaService);
  private catacionService = inject(CatacionService);

  /**
   * Obtiene la trazabilidad completa de un lote
   * Recopila datos de todas las etapas del proceso
   */
  obtenerTrazabilidadCompleta(nlote: string): Observable<TrazabilidadCompleta> {
    return forkJoin({
      acopio: this.acopioService.obtenerPorLote(nlote).pipe(
        catchError(() => of(null))
      ),
      caracterizaciones: this.caracterizacionService.obtenerPorLote(nlote).pipe(
        map(data => Array.isArray(data) ? data : (data ? [data] : [])),
        catchError(() => of([]))
      ),
      secados: this.secadoService.obtenerPorLote(nlote).pipe(
        map(data => Array.isArray(data) ? data : (data ? [data] : [])),
        catchError(() => of([]))
      ),
      bodegas: this.bodegaService.obtenerPorLote(nlote).pipe(
        map(data => Array.isArray(data) ? data : (data ? [data] : [])),
        catchError(() => of([]))
      ),
      trillas: this.trillaService.obtenerPorLote(nlote).pipe(
        map(data => Array.isArray(data) ? data : (data ? [data] : [])),
        catchError(() => of([]))
      ),
      cataciones: this.catacionService.obtenerTodos().pipe(
        map(data => {
          const cataciones = Array.isArray(data) ? data : [];
          // Filtrar cataciones relacionadas con las trillas de este lote
          return cataciones;
        }),
        catchError(() => of([]))
      )
    }).pipe(
      map(datos => this.construirTrazabilidad(nlote, datos))
    );
  }

  /**
   * Construye el objeto de trazabilidad completa a partir de los datos
   */
  private construirTrazabilidad(nlote: string, datos: any): TrazabilidadCompleta {
    const etapaActual = this.determinarEtapaActual(datos);
    const metricas = this.calcularMetricas(datos);

    return {
      nlote,
      etapaActual,
      fechaInicio: datos.acopio?.facopio || new Date().toISOString(),
      fechaUltimaActualizacion: new Date().toISOString(),
      acopio: datos.acopio,
      caracterizaciones: datos.caracterizaciones,
      secados: datos.secados,
      bodegas: datos.bodegas,
      trillas: datos.trillas,
      cataciones: datos.cataciones,
      metricas
    };
  }

  /**
   * Determina en qué etapa del proceso se encuentra actualmente el lote
   */
  private determinarEtapaActual(datos: any): EtapaProceso {
    if (datos.cataciones && datos.cataciones.length > 0) {
      return 'Finalizado';
    }
    if (datos.trillas && datos.trillas.length > 0) {
      return 'Catación';
    }
    if (datos.bodegas && datos.bodegas.length > 0) {
      return 'Trilla';
    }
    if (datos.secados && datos.secados.length > 0) {
      return 'Bodega';
    }
    if (datos.caracterizaciones && datos.caracterizaciones.length > 0) {
      return 'Secado';
    }
    if (datos.acopio) {
      return 'Caracterización';
    }
    return 'Acopio';
  }

  /**
   * Calcula métricas agregadas del proceso
   */
  private calcularMetricas(datos: any): MetricasTrazabilidad {
    const metricas: MetricasTrazabilidad = {
      duracionTotalDias: 0,
      duracionSecado: 0,
      duracionReposo: 0
    };

    // Calcular duración total
    if (datos.acopio?.facopio) {
      const fechaInicio = new Date(datos.acopio.facopio);
      const fechaFin = new Date();
      metricas.duracionTotalDias = Math.floor(
        (fechaFin.getTime() - fechaInicio.getTime()) / (1000 * 60 * 60 * 24)
      );
    }

    // Duración de secado
    if (datos.secados && datos.secados.length > 0) {
      metricas.duracionSecado = datos.secados.reduce((total: number, secado: any) => {
        return total + (secado.dsecado || 0);
      }, 0);
    }

    // Humedad final
    if (datos.bodegas && datos.bodegas.length > 0) {
      const ultimaBodega = datos.bodegas[datos.bodegas.length - 1];
      metricas.humedadFinal = ultimaBodega.hfinal;
    }

    // Rendimiento final
    if (datos.trillas && datos.trillas.length > 0) {
      const ultimaTrilla = datos.trillas[datos.trillas.length - 1];
      metricas.rendimientoFinal = ultimaTrilla.rfinalSeleccion;
      metricas.pesoTotal = ultimaTrilla.wverdeFinal;
    }

    // Calidad y puntaje de catación
    if (datos.cataciones && datos.cataciones.length > 0) {
      const ultimaCatacion = datos.cataciones[datos.cataciones.length - 1];
      metricas.calidadFinal = ultimaCatacion.calidad;
      metricas.puntajeCatacion = ultimaCatacion.pfinales;
    }

    // Total de sacos
    if (datos.bodegas && datos.bodegas.length > 0) {
      metricas.totalSacos = datos.bodegas.reduce((total: number, bodega: any) => {
        return total + (bodega.cantidadSacos || 0);
      }, 0);
    }

    return metricas;
  }

  /**
   * Verifica si un lote existe en el sistema
   */
  verificarLoteExiste(nlote: string): Observable<boolean> {
    return this.acopioService.obtenerPorLote(nlote).pipe(
      map(acopio => acopio !== null && acopio !== undefined),
      catchError(() => of(false))
    );
  }

  /**
   * Obtiene el progreso del lote en porcentaje (0-100)
   */
  obtenerProgreso(etapaActual: EtapaProceso): number {
    const etapas: EtapaProceso[] = [
      'Acopio',
      'Caracterización',
      'Secado',
      'Bodega',
      'Trilla',
      'Catación',
      'Finalizado'
    ];
    
    const index = etapas.indexOf(etapaActual);
    return index >= 0 ? Math.round((index / (etapas.length - 1)) * 100) : 0;
  }
}
