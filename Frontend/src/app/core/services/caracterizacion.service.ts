import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { Caracterizacion } from '../../models/caracterizacion.model';

@Injectable({
  providedIn: 'root'
})
export class CaracterizacionService {
  private apiService = inject(ApiService);
  private endpoint = 'Caracterizacion';

  obtenerTodos(): Observable<Caracterizacion> {
    return this.apiService.get<Caracterizacion>(this.endpoint);
  }

  obtenerPorTiempo(tiempo: string): Observable<Caracterizacion> {
    return this.apiService.get<Caracterizacion>(`${this.endpoint}/${tiempo}`);
  }

  obtenerPorLote(nlote: string): Observable<Caracterizacion[]> {
    return this.apiService.obtenerTodos<Caracterizacion>(`${this.endpoint}/lote/${nlote}`);
  }

  crear(caracterizacion: any): Observable<Caracterizacion> {
    return this.apiService.post<Caracterizacion>(this.endpoint, caracterizacion);
  }

  actualizar(tiempo: string, caracterizacion: any): Observable<Caracterizacion> {
    return this.apiService.put<Caracterizacion>(`${this.endpoint}/${tiempo}`, caracterizacion);
  }

  eliminar(tiempo: string): Observable<void> {
    return this.apiService.delete<void>(`${this.endpoint}/${tiempo}`);
  }

  agregarSobremaduras(tiempo: string, rcSobremaduras: any): Observable<any> {
    return this.apiService.post<any>(`${this.endpoint}/${tiempo}/sobremaduras`, rcSobremaduras);
  }

  eliminarSobremaduras(tiempo: string): Observable<void> {
    return this.apiService.delete<void>(`${this.endpoint}/${tiempo}/sobremaduras`);
  }

  agregarInmaduras(tiempo: string, rcInmaduras: any): Observable<any> {
    return this.apiService.post<any>(`${this.endpoint}/${tiempo}/inmaduras`, rcInmaduras);
  }

  eliminarInmaduras(tiempo: string): Observable<void> {
    return this.apiService.delete<void>(`${this.endpoint}/${tiempo}/inmaduras`);
  }

  agregarMaduras(tiempo: string, rcMaduras: any): Observable<any> {
    return this.apiService.post<any>(`${this.endpoint}/${tiempo}/maduras`, rcMaduras);
  }

  eliminarMaduras(tiempo: string): Observable<void> {
    return this.apiService.delete<void>(`${this.endpoint}/${tiempo}/maduras`);
  }
}
