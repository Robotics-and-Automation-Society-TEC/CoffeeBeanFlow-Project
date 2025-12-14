// ========================================
// INTERFACES PARA ENTIDAD SECADO Y ENTIDADES DÉBILES
// ========================================

/**
 * Entidad principal: Secado
 */
export interface Secado {
  idSecado: number;
  nlote: string;
  finicio: string;  // DateTime en formato ISO
  dsecado: number | null;
  ffinal: string | null;  // DateTime en formato ISO
  
  // Relación con AreaAcopio (opcional para visualización)
  areaAcopio?: {
    nlote: string;
    nproductor: string;
    nfinca: string;
  };
  
  // Entidades débiles (colecciones)
  humedades: Humedad[];
  termoHigrometrias: TermoHigrometria[];
  temperaturasSecado: TemperaturaSecado[];
  ncamas: Ncama[];
}

/**
 * Entidad débil: Humedad
 */
export interface Humedad {
  idHumedad?: number;
  idSecado?: number;
  fecha: string;  // DateTime en formato ISO
  humedad: number;
}

/**
 * Entidad débil: TermoHigrometria
 */
export interface TermoHigrometria {
  idTermoHigrometria?: number;
  idSecado?: number;
  fecha: string;  // DateTime en formato ISO
  temperaturaAmbiente: number;
  humedadAmbiente: number;
}

/**
 * Entidad débil: TemperaturaSecado
 */
export interface TemperaturaSecado {
  idTemperaturaSecado?: number;
  idSecado?: number;
  fecha: string;  // DateTime en formato ISO
  temperatura: number;
}

/**
 * Entidad débil: Ncama (atributo multivaluado)
 */
export interface Ncama {
  idNcama?: number;
  idSecado?: number;
  numeroCama: number;
}

// ========================================
// DTOs PARA OPERACIONES CREATE/UPDATE
// ========================================

/**
 * DTO para crear un nuevo Secado
 */
export interface CreateSecadoDto {
  nlote: string;
  finicio: string;
  dsecado?: number | null;
  ffinal?: string | null;
  
  humedades?: Humedad[];
  termoHigrometrias?: TermoHigrometria[];
  temperaturasSecado?: TemperaturaSecado[];
  ncamas?: Ncama[];
}

/**
 * DTO para actualizar un Secado existente
 */
export interface UpdateSecadoDto {
  idSecado: number;
  nlote: string;
  finicio: string;
  dsecado?: number | null;
  ffinal?: string | null;
  
  humedades?: Humedad[];
  termoHigrometrias?: TermoHigrometria[];
  temperaturasSecado?: TemperaturaSecado[];
  ncamas?: Ncama[];
}
