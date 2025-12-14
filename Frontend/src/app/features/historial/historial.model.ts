// ========================================
// MODELO: HISTORIAL GENERAL
// ========================================
// Interfaces para visualizar todos los registros del sistema

export interface RegistroGeneral {
  tipo: TipoEntidad;
  id: number | string;
  nlote?: string;
  fecha: string;
  estado: EstadoRegistro;
  resumen: string;
  datos: any; // Datos específicos según el tipo
}

export type TipoEntidad = 
  | 'AreaAcopio'
  | 'Secado'
  | 'Bodega'
  | 'Trilla'
  | 'Caracterizacion'
  | 'Catacion';

export type EstadoRegistro = 
  | 'Completado'
  | 'En Proceso'
  | 'Pendiente';

export interface FiltrosHistorial {
  tipo?: TipoEntidad;
  nlote?: string;
  fechaInicio?: string;
  fechaFin?: string;
  estado?: EstadoRegistro;
  busqueda?: string;
}

export interface ResumenEstadisticas {
  totalRegistros: number;
  registrosPorTipo: Map<TipoEntidad, number>;
  registrosPorEstado: Map<EstadoRegistro, number>;
  lotesActivos: number;
  ultimaActualizacion: Date;
}
