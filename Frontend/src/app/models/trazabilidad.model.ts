// ========================================
// MODELO: TRAZABILIDAD COMPLETA
// ========================================
// Interfaces para la trazabilidad de un lote desde Acopio hasta Catación

import { AreaAcopio } from './area-acopio.model';
import { Caracterizacion } from './caracterizacion.model';
import { Secado } from './secado.model';
import { Bodega } from './bodega.model';
import { Trilla } from './trilla.model';
import { Catacion } from './catacion.model';

export interface TrazabilidadCompleta {
  nlote: string;
  etapaActual: EtapaProceso;
  fechaInicio: string;
  fechaUltimaActualizacion: string;
  
  // Datos de cada etapa
  acopio?: AreaAcopio;
  caracterizaciones?: Caracterizacion[];
  secados?: Secado[];
  bodegas?: Bodega[];
  trillas?: Trilla[];
  cataciones?: Catacion[];
  
  // Relaciones N:N
  guardarCafe?: RelacionGuardarCafe[];
  enviarMuestras?: RelacionEnviarMuestras[];
  suministra?: RelacionSuministra[];
  
  // Métricas calculadas
  metricas?: MetricasTrazabilidad;
}

export type EtapaProceso = 
  | 'Acopio'
  | 'Caracterización'
  | 'Secado'
  | 'Bodega'
  | 'Trilla'
  | 'Catación'
  | 'Finalizado';

export interface RelacionGuardarCafe {
  idSecado: number;
  idBodega: number;
  cantidadSacos: number;
  fechaGuardado?: string;
}

export interface RelacionEnviarMuestras {
  idTrilla: number;
  idCatacion: number;
  ffinalReposo?: number;
  fechaEnvio?: string;
}

export interface RelacionSuministra {
  idBodega: number;
  idTrilla: number;
  fechaSuministro?: string;
}

export interface MetricasTrazabilidad {
  duracionTotalDias: number;
  duracionSecado: number;
  duracionReposo: number;
  rendimientoFinal?: number;
  humedadFinal?: number;
  calidadFinal?: string;
  puntajeCatacion?: number;
  totalSacos?: number;
  pesoTotal?: number;
}

export interface EtapaInfo {
  nombre: EtapaProceso;
  estado: EstadoEtapa;
  fecha?: string;
  completada: boolean;
  icono: string;
  color: string;
}

export type EstadoEtapa = 'completada' | 'actual' | 'pendiente';

export interface TimelineData {
  etapas: EtapaInfo[];
  etapaActual: EtapaProceso;
  progreso: number; // 0-100
}
