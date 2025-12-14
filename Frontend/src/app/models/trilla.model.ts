export interface PesoVerde {
  idPesoVerde: number;
  winferiores: number | null;
  wfinal: number | null;
  wFinalInferiores: number | null;
}

export interface Trilla {
  idTrilla: number;
  nlote: string;
  hinicial: number | null;
  hfinal: number | null;
  rFinalPelado: number | null;
  rFinalSeleccion: number | null;
  wverdeFinal: number | null;
  rteoricoPelado: number | null;
  wverdeTeorico: number | null;
  rTeoricoSeleccion: number | null;
  ffinalReposo: string | null;
  psegundas: number | null;
  pcataduras: number | null;
  pbarreduras: number | null;
  pescogeduras: number | null;
  pcaracolillo: number | null;
  pprimera: number | null;
  pmadres: number | null;
  pmenudos: number | null;
  pinferiores: number | null;
  pesoVerde: PesoVerde | null;
}

export interface CreateTrillaDto {
  nlote: string;
  hinicial?: number | null;
  hfinal?: number | null;
  rFinalPelado?: number | null;
  rFinalSeleccion?: number | null;
  wverdeFinal?: number | null;
  rteoricoPelado?: number | null;
  wverdeTeorico?: number | null;
  rTeoricoSeleccion?: number | null;
  ffinalReposo?: string | null;
  psegundas?: number | null;
  pcataduras?: number | null;
  pbarreduras?: number | null;
  pescogeduras?: number | null;
  pcaracolillo?: number | null;
  pprimera?: number | null;
  pmadres?: number | null;
  pmenudos?: number | null;
  pinferiores?: number | null;
  pesoVerde?: {
    winferiores?: number | null;
    wfinal?: number | null;
    wFinalInferiores?: number | null;
  } | null;
}

export interface UpdateTrillaDto {
  nlote: string;
  hinicial?: number | null;
  hfinal?: number | null;
  rFinalPelado?: number | null;
  rFinalSeleccion?: number | null;
  wverdeFinal?: number | null;
  rteoricoPelado?: number | null;
  wverdeTeorico?: number | null;
  rTeoricoSeleccion?: number | null;
  ffinalReposo?: string | null;
  psegundas?: number | null;
  pcataduras?: number | null;
  pbarreduras?: number | null;
  pescogeduras?: number | null;
  pcaracolillo?: number | null;
  pprimera?: number | null;
  pmadres?: number | null;
  pmenudos?: number | null;
  pinferiores?: number | null;
  pesoVerde?: {
    winferiores?: number | null;
    wfinal?: number | null;
    wFinalInferiores?: number | null;
  } | null;
}
