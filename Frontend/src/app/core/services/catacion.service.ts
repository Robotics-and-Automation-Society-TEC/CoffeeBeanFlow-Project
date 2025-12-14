import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { Catacion, CreateCatacionDto, UpdateCatacionDto, Rondas } from '../../models/catacion.model';

@Injectable({
  providedIn: 'root'
})
export class CatacionService {
  private apiService = inject(ApiService);
  private readonly endpoint = 'Catacion';

  obtenerTodos(): Observable<Catacion[]> {
    return this.apiService.get<Catacion[]>(this.endpoint);
  }

  obtenerPorId(id: number): Observable<Catacion> {
    return this.apiService.get<Catacion>(`${this.endpoint}/${id}`);
  }

  obtenerPorLote(nlote: string): Observable<Catacion[]> {
    return this.apiService.get<Catacion[]>(`${this.endpoint}/lote/${nlote}`);
  }

  crear(catacion: CreateCatacionDto): Observable<Catacion> {
    return this.apiService.post<Catacion>(this.endpoint, catacion);
  }

  actualizar(id: number, catacion: UpdateCatacionDto): Observable<void> {
    return this.apiService.put<void>(`${this.endpoint}/${id}`, catacion);
  }

  eliminar(id: number): Observable<void> {
    return this.apiService.delete<void>(`${this.endpoint}/${id}`);
  }

  // Métodos para gestión individual de Rondas
  agregarRonda(idCatacion: number, ronda: Omit<Rondas, 'idRondas' | 'idCatacion'>): Observable<Rondas> {
    return this.apiService.post<Rondas>(`${this.endpoint}/${idCatacion}/rondas`, ronda);
  }

  obtenerRondas(idCatacion: number): Observable<Rondas[]> {
    return this.apiService.get<Rondas[]>(`${this.endpoint}/${idCatacion}/rondas`);
  }

  obtenerRonda(idCatacion: number, idRonda: number): Observable<Rondas> {
    return this.apiService.get<Rondas>(`${this.endpoint}/${idCatacion}/rondas/${idRonda}`);
  }

  actualizarRonda(idCatacion: number, idRonda: number, ronda: Rondas): Observable<void> {
    return this.apiService.put<void>(`${this.endpoint}/${idCatacion}/rondas/${idRonda}`, ronda);
  }

  eliminarRonda(idCatacion: number, idRonda: number): Observable<void> {
    return this.apiService.delete<void>(`${this.endpoint}/${idCatacion}/rondas/${idRonda}`);
  }
}
