import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { Bodega, CreateBodegaDto, UpdateBodegaDto } from '../../models/bodega.model';

@Injectable({
  providedIn: 'root'
})
export class BodegaService {
  private apiService = inject(ApiService);
  private endpoint = 'Bodega';

  /**
   * Obtener todos los registros de bodega
   */
  obtenerTodos(): Observable<Bodega[]> {
    return this.apiService.obtenerTodos<Bodega>(this.endpoint);
  }

  /**
   * Obtener una bodega por ID
   */
  obtenerPorId(idBodega: number): Observable<Bodega> {
    return this.apiService.obtenerPorId<Bodega>(this.endpoint, idBodega);
  }

  /**
   * Obtener todas las bodegas por número de lote
   */
  obtenerPorLote(nlote: string): Observable<Bodega[]> {
    return this.apiService.obtenerTodos<Bodega>(`${this.endpoint}/lote/${nlote}`);
  }

  /**
   * Crear una nueva bodega
   */
  crear(bodega: CreateBodegaDto): Observable<Bodega> {
    return this.apiService.crear<Bodega>(this.endpoint, bodega as any);
  }

  /**
   * Actualizar una bodega existente
   */
  actualizar(idBodega: number, bodega: UpdateBodegaDto): Observable<Bodega> {
    return this.apiService.actualizar<Bodega>(this.endpoint, idBodega, bodega as any);
  }

  /**
   * Eliminar una bodega por ID
   */
  eliminar(idBodega: number): Observable<void> {
    return this.apiService.eliminar(this.endpoint, idBodega);
  }
}
