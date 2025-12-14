import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { Trilla } from '../../models/trilla.model';

@Injectable({
  providedIn: 'root'
})
export class TrillaService {
  private apiService = inject(ApiService);
  private endpoint = 'Trilla';

  obtenerTodos(): Observable<Trilla> {
    return this.apiService.get<Trilla>(this.endpoint);
  }

  obtenerPorId(idTrilla: number): Observable<Trilla> {
    return this.apiService.get<Trilla>(`${this.endpoint}/${idTrilla}`);
  }

  obtenerPorLote(nlote: string): Observable<Trilla[]> {
    return this.apiService.obtenerTodos<Trilla>(`${this.endpoint}/lote/${nlote}`);
  }

  crear(trilla: any): Observable<Trilla> {
    return this.apiService.post<Trilla>(this.endpoint, trilla);
  }

  actualizar(idTrilla: number, trilla: any): Observable<Trilla> {
    return this.apiService.put<Trilla>(`${this.endpoint}/${idTrilla}`, trilla);
  }

  eliminar(idTrilla: number): Observable<void> {
    return this.apiService.delete<void>(`${this.endpoint}/${idTrilla}`);
  }

  agregarPesoVerde(idTrilla: number, pesoVerde: any): Observable<any> {
    return this.apiService.post<any>(`${this.endpoint}/${idTrilla}/peso-verde`, pesoVerde);
  }

  eliminarPesoVerde(idTrilla: number): Observable<void> {
    return this.apiService.delete<void>(`${this.endpoint}/${idTrilla}/peso-verde`);
  }
}
