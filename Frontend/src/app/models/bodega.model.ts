// ========================================
// INTERFACES PARA ENTIDAD BODEGA
// ========================================

/**
 * Entidad principal: Bodega
 */
export interface Bodega {
  idBodega: number;
  nlote: string;
  wBellota: number | null;
  wPergamino: number | null;
  hfinal: number | null;
  hinicial: number | null;
  dPergamino: number | null;
  dBellota: number | null;
  finicioReposo: string | null;  // DateTime en formato ISO
  cantidadSacos: number | null;
  pmhRelativa: number | null;
  pmtInterna: number | null;
  pmtExterna: number | null;
  
  // Relación con AreaAcopio (opcional para visualización)
  areaAcopio?: {
    nlote: string;
    nproductor: string;
    nfinca: string;
  };
}

// ========================================
// DTOs PARA OPERACIONES CREATE/UPDATE
// ========================================

/**
 * DTO para crear una nueva Bodega
 */
export interface CreateBodegaDto {
  nlote: string;
  wBellota?: number | null;
  wPergamino?: number | null;
  hfinal?: number | null;
  hinicial?: number | null;
  dPergamino?: number | null;
  dBellota?: number | null;
  finicioReposo?: string | null;
  cantidadSacos?: number | null;
  pmhRelativa?: number | null;
  pmtInterna?: number | null;
  pmtExterna?: number | null;
}

/**
 * DTO para actualizar una Bodega existente
 */
export interface UpdateBodegaDto {
  idBodega: number;
  nlote: string;
  wBellota?: number | null;
  wPergamino?: number | null;
  hfinal?: number | null;
  hinicial?: number | null;
  dPergamino?: number | null;
  dBellota?: number | null;
  finicioReposo?: string | null;
  cantidadSacos?: number | null;
  pmhRelativa?: number | null;
  pmtInterna?: number | null;
  pmtExterna?: number | null;
}
