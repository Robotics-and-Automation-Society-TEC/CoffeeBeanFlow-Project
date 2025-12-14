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
  drMaduras: number | null;
  pcDebajo: number | null;
  proceso: string | null;
  lAsignado: string | null;
  cVerdes: number | null;
  cObjetivo: number | null;
  cInmaduras: number | null;
  cSobremaduras: number | null;
  cSecas: number | null;
  mTabla: number | null;
  pcVerdes: number | null;
  pcSecas: number | null;
  pcEncima: number | null;
  eMaduracion: number | null;
  broca: number | null;
  densidad: number | null;
  vanos: number | null;
  secos: number | null;
  pcObjetivo: number | null;
  rcSobremaduras: RCsobremaduras | null;
  rcInmaduras: RCinmaduras | null;
  rcMaduras: RCmaduras | null;
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
