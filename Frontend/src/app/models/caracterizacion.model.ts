export interface RCsobremaduras {
  idSobremaduras: number;
  tiempo: string;
  gbx: number | null;
  promedio: number | null;
  observaciones: string | null;
}

export interface RCinmaduras {
  idInmaduras: number;
  tiempo: string;
  gbx: number | null;
  promedio: number | null;
  observaciones: string | null;
}

export interface RCmaduras {
  idMaduras: number;
  tiempo: string;
  gbx: number | null;
  promedio: number | null;
  observaciones: string | null;
}

export interface Caracterizacion {
  tiempo: string;
  nloteAreaAcopio: string;
  dRmaduras: number | null;  // Backend serializa como dRmaduras
  pCdebajo: number | null;   // Backend serializa como pCdebajo
  proceso: string | null;
  lAsignado: string | null;
  cverdes: number | null;    // Backend serializa como cverdes
  cobjetivo: number | null;  // Backend serializa como cobjetivo
  cinmaduras: number | null; // Backend serializa como cinmaduras
  csobremaduras: number | null; // Backend serializa como csobremaduras
  csecas: number | null;     // Backend serializa como csecas
  mtabla: number | null;     // Backend serializa como mtabla
  pCverdes: number | null;   // Backend serializa como pCverdes
  pCsecas: number | null;    // Backend serializa como pCsecas
  pCencima: number | null;   // Backend serializa como pCencima
  emaduracion: number | null; // Backend serializa como emaduracion
  broca: number | null;
  densidad: number | null;
  vanos: number | null;
  secos: number | null;
  pCobjetivo: number | null; // Backend serializa como pCobjetivo
  rCsobremaduras: RCsobremaduras | null; // Backend serializa como rCsobremaduras
  rCinmaduras: RCinmaduras | null;       // Backend serializa como rCinmaduras
  rCmaduras: RCmaduras | null;           // Backend serializa como rCmaduras
}

export interface CreateCaracterizacionDto {
  tiempo: string;
  nloteAreaAcopio: string;
  drMaduras?: number | null;
  pcDebajo?: number | null;
  proceso?: string | null;
  lAsignado?: string | null;
  cVerdes?: number | null;
  cObjetivo?: number | null;
  cInmaduras?: number | null;
  cSobremaduras?: number | null;
  cSecas?: number | null;
  mTabla?: number | null;
  pcVerdes?: number | null;
  pcSecas?: number | null;
  pcEncima?: number | null;
  eMaduracion?: number | null;
  broca?: number | null;
  densidad?: number | null;
  vanos?: number | null;
  secos?: number | null;
  pcObjetivo?: number | null;
  rcSobremaduras?: {
    gbx?: number | null;
    promedio?: number | null;
    observaciones?: string | null;
  } | null;
  rcInmaduras?: {
    gbx?: number | null;
    promedio?: number | null;
    observaciones?: string | null;
  } | null;
  rcMaduras?: {
    gbx?: number | null;
    promedio?: number | null;
    observaciones?: string | null;
  } | null;
}

export interface UpdateCaracterizacionDto {
  tiempo: string;
  nloteAreaAcopio: string;
  drMaduras?: number | null;
  pcDebajo?: number | null;
  proceso?: string | null;
  lAsignado?: string | null;
  cVerdes?: number | null;
  cObjetivo?: number | null;
  cInmaduras?: number | null;
  cSobremaduras?: number | null;
  cSecas?: number | null;
  mTabla?: number | null;
  pcVerdes?: number | null;
  pcSecas?: number | null;
  pcEncima?: number | null;
  eMaduracion?: number | null;
  broca?: number | null;
  densidad?: number | null;
  vanos?: number | null;
  secos?: number | null;
  pcObjetivo?: number | null;
  rcSobremaduras?: {
    gbx?: number | null;
    promedio?: number | null;
    observaciones?: string | null;
  } | null;
  rcInmaduras?: {
    gbx?: number | null;
    promedio?: number | null;
    observaciones?: string | null;
  } | null;
  rcMaduras?: {
    gbx?: number | null;
    promedio?: number | null;
    observaciones?: string | null;
  } | null;
}
