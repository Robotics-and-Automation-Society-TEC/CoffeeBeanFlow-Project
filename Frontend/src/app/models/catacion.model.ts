export interface Rondas {
  idRondas: number;
  idCatacion: number;
  valorCalidad: number | null;
}

export interface Catacion {
  idCatacion: number;
  nlote: string | null;
  
  // Atributos simples
  limpio: number | null;
  defectuoso: number | null;
  ffreposo: number | null;
  overde: number | null;
  quaker: number | null;
  ccverde: number | null;
  rtostado: number | null;
  dfueste: number | null;
  cccalidad: number | null;
  
  // Atributos C1 (Categoría 1)
  c1agrio: number | null;
  c1hongos: number | null;
  c1cerezaseca: number | null;
  c1negro: number | null;
  c1insectos: number | null;
  c1negrop: number | null;
  c1agriop: number | null;
  c1me: number | null;
  
  // Atributos C2 (Categoría 2)
  c2flotador: number | null;
  c2averanado: number | null;
  c2pergamino: number | null;
  c2inmaduro: number | null;
  c2concha: number | null;
  c2insectos: number | null;
  
  // Atributo compuesto C2CP
  c2cascara: number | null;
  c2pulpa: number | null;
  
  // Atributo compuesto C2PCM
  c2partido: number | null;
  c2cortado: number | null;
  c2mordido: number | null;
  
  // Atributo compuesto Zaranda
  tresSobreDieciseis: number | null;
  veinte: number | null;
  diecinueve: number | null;
  dieciocho: number | null;
  diecisiete: number | null;
  dieciseis: number | null;
  quince: number | null;
  catorce: number | null;
  trece: number | null;
  
  // Atributo compuesto TonAgton
  tonagton25: number | null;
  tonagton35: number | null;
  tonagton45: number | null;
  tonagton55: number | null;
  tonagton65: number | null;
  tonagton75: number | null;
  tonagton85: number | null;
  tonagton95: number | null;
  
  // Atributo derivado
  pfinales: number | null;
  
  // Relación 1:N con Rondas
  rondas: Rondas[];
}

export interface CreateCatacionDto {
  nlote: string | null;
  limpio: number | null;
  defectuoso: number | null;
  ffreposo: number | null;
  overde: number | null;
  quaker: number | null;
  ccverde: number | null;
  rtostado: number | null;
  dfueste: number | null;
  cccalidad: number | null;
  c1agrio: number | null;
  c1hongos: number | null;
  c1cerezaseca: number | null;
  c1negro: number | null;
  c1insectos: number | null;
  c1negrop: number | null;
  c1agriop: number | null;
  c1me: number | null;
  c2flotador: number | null;
  c2averanado: number | null;
  c2pergamino: number | null;
  c2inmaduro: number | null;
  c2concha: number | null;
  c2insectos: number | null;
  c2cascara: number | null;
  c2pulpa: number | null;
  c2partido: number | null;
  c2cortado: number | null;
  c2mordido: number | null;
  tresSobreDieciseis: number | null;
  veinte: number | null;
  diecinueve: number | null;
  dieciocho: number | null;
  diecisiete: number | null;
  dieciseis: number | null;
  quince: number | null;
  catorce: number | null;
  trece: number | null;
  tonagton25: number | null;
  tonagton35: number | null;
  tonagton45: number | null;
  tonagton55: number | null;
  tonagton65: number | null;
  tonagton75: number | null;
  tonagton85: number | null;
  tonagton95: number | null;
  pfinales: number | null;
  rondas: Omit<Rondas, 'idRondas' | 'idCatacion'>[];
}

export interface UpdateCatacionDto {
  idCatacion: number;
  nlote: string | null;
  limpio: number | null;
  defectuoso: number | null;
  ffreposo: number | null;
  overde: number | null;
  quaker: number | null;
  ccverde: number | null;
  rtostado: number | null;
  dfueste: number | null;
  cccalidad: number | null;
  c1agrio: number | null;
  c1hongos: number | null;
  c1cerezaseca: number | null;
  c1negro: number | null;
  c1insectos: number | null;
  c1negrop: number | null;
  c1agriop: number | null;
  c1me: number | null;
  c2flotador: number | null;
  c2averanado: number | null;
  c2pergamino: number | null;
  c2inmaduro: number | null;
  c2concha: number | null;
  c2insectos: number | null;
  c2cascara: number | null;
  c2pulpa: number | null;
  c2partido: number | null;
  c2cortado: number | null;
  c2mordido: number | null;
  tresSobreDieciseis: number | null;
  veinte: number | null;
  diecinueve: number | null;
  dieciocho: number | null;
  diecisiete: number | null;
  dieciseis: number | null;
  quince: number | null;
  catorce: number | null;
  trece: number | null;
  tonagton25: number | null;
  tonagton35: number | null;
  tonagton45: number | null;
  tonagton55: number | null;
  tonagton65: number | null;
  tonagton75: number | null;
  tonagton85: number | null;
  tonagton95: number | null;
  pfinales: number | null;
  rondas: Rondas[];
}
