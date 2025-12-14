import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { Secado, CreateSecadoDto, UpdateSecadoDto } from '../../models/secado.model';

@Injectable({
  providedIn: 'root'
})
export class SecadoService {
  private apiService = inject(ApiService);
  private endpoint = 'Secado';

  /**
   * Obtener todos los registros de secado
   */
  obtenerTodos(): Observable<Secado[]> {
    return this.apiService.obtenerTodos<Secado>(this.endpoint);
  }

  /**
   * Obtener un secado por ID
   */
  obtenerPorId(idSecado: number): Observable<Secado> {
    return this.apiService.obtenerPorId<Secado>(this.endpoint, idSecado);
  }

  /**
   * Obtener todos los secados por número de lote
   */
  obtenerPorLote(nlote: string): Observable<Secado[]> {
    return this.apiService.obtenerTodos<Secado>(`${this.endpoint}/lote/${nlote}`);
  }

  /**
   * Crear un nuevo secado con entidades débiles
   */
  crear(secado: CreateSecadoDto): Observable<Secado> {
    return this.apiService.crear<Secado>(this.endpoint, secado as any);
  }

  /**
   * Actualizar un secado existente con entidades débiles
   */
  actualizar(idSecado: number, secado: UpdateSecadoDto): Observable<Secado> {
    return this.apiService.actualizar<Secado>(this.endpoint, idSecado, secado as any);
  }

  /**
   * Eliminar un secado por ID
   */
  eliminar(idSecado: number): Observable<void> {
    return this.apiService.eliminar(this.endpoint, idSecado);
  }
}
