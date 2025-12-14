import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { AreaAcopio, CreateAreaAcopioDto, UpdateAreaAcopioDto } from '../../models/area-acopio.model';

@Injectable({
  providedIn: 'root'
})
export class AreaAcopioService {
  private apiService = inject(ApiService);
  private readonly endpoint = 'AreaAcopio';

  /**
   * Obtener todos los registros de Área de Acopio
   */
  obtenerTodos(): Observable<AreaAcopio[]> {
    return this.apiService.obtenerTodos<AreaAcopio>(this.endpoint);
  }

  /**
   * Obtener un registro por número de lote
   */
  obtenerPorLote(nlote: string): Observable<AreaAcopio> {
    return this.apiService.obtenerPorId<AreaAcopio>(this.endpoint, nlote);
  }

  /**
   * Crear un nuevo registro de Área de Acopio
   */
  crear(areaAcopio: CreateAreaAcopioDto): Observable<AreaAcopio> {
    return this.apiService.crear<AreaAcopio>(this.endpoint, areaAcopio as AreaAcopio);
  }

  /**
   * Actualizar un registro existente
   */
  actualizar(nlote: string, areaAcopio: UpdateAreaAcopioDto): Observable<AreaAcopio> {
    return this.apiService.actualizar<AreaAcopio>(this.endpoint, nlote, areaAcopio as AreaAcopio);
  }

  /**
   * Eliminar un registro por número de lote
   */
  eliminar(nlote: string): Observable<void> {
    return this.apiService.eliminar(this.endpoint, nlote);
  }
}
