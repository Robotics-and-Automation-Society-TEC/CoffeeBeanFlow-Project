/**
 * Modelo para Área de Acopio
 * Representa la entidad AreaAcopio con sus 22 atributos
 */
export interface AreaAcopio {
  // Clave primaria
  nlote: string;

  // Atributos simples
  altura: number;
  zona: string;
  nrecibo: number;
  nproductor: string;
  nfinca: string;
  robjetivo: number;
  rtotal: number;
  vendido: boolean;
  disponible: number;
  enproceso: string;

  // Atributo compuesto: Despulpado (6 sub-atributos)
  semilavado: boolean;
  natural: boolean;
  anaerobico: boolean;
  otro: string | null;
  miel: boolean;
  lavado: boolean;

  // Atributo compuesto: Pruebas_Fisicas_BH (6 sub-atributos)
  pF_Pulpa_Pergamino: number;
  pF_DMecanicos: number;
  pF_Segundas: number;
  pF_Pergamino_Pulpa: number;
  pDensidad_Fruta: number;
  pDensidad_Pergamino_Humedo: number;
}

/**
 * DTO para crear un nuevo registro de Área de Acopio
 */
export interface CreateAreaAcopioDto extends Omit<AreaAcopio, 'nlote'> {
  nlote?: string;
}

/**
 * DTO para actualizar un registro existente
 */
export interface UpdateAreaAcopioDto extends Partial<AreaAcopio> {
  nlote: string;
}
