import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private http = inject(HttpClient);
  private baseUrl = 'http://localhost:5253/api';

  /**
   * Crear un nuevo registro
   */
  crear<T>(endpoint: string, data: T): Observable<T> {
    return this.http.post<T>(`${this.baseUrl}/${endpoint}`, data).pipe(
      retry(1),
      catchError(this.handleError)
    );
  }

  /**
   * Obtener todos los registros
   */
  obtenerTodos<T>(endpoint: string): Observable<T[]> {
    const url = `${this.baseUrl}/${endpoint}`;
    console.log('ApiService: Llamando a', url);
    return this.http.get<T[]>(url).pipe(
      retry(1),
      catchError(this.handleError)
    );
  }

  /**
   * Obtener un registro por ID
   */
  obtenerPorId<T>(endpoint: string, id: string | number): Observable<T> {
    return this.http.get<T>(`${this.baseUrl}/${endpoint}/${id}`).pipe(
      retry(1),
      catchError(this.handleError)
    );
  }

  /**
   * Actualizar un registro
   */
  actualizar<T>(endpoint: string, id: string | number, data: T): Observable<T> {
    return this.http.put<T>(`${this.baseUrl}/${endpoint}/${id}`, data).pipe(
      retry(1),
      catchError(this.handleError)
    );
  }

  /**
   * Eliminar un registro
   */
  eliminar(endpoint: string, id: string | number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${endpoint}/${id}`).pipe(
      retry(1),
      catchError(this.handleError)
    );
  }

  /**
   * GET genérico
   */
  get<T>(url: string): Observable<T> {
    return this.http.get<T>(`${this.baseUrl}/${url}`).pipe(
      retry(1),
      catchError(this.handleError)
    );
  }

  /**
   * POST genérico
   */
  post<T>(url: string, data: any): Observable<T> {
    return this.http.post<T>(`${this.baseUrl}/${url}`, data).pipe(
      retry(1),
      catchError(this.handleError)
    );
  }

  /**
   * PUT genérico
   */
  put<T>(url: string, data: any): Observable<T> {
    return this.http.put<T>(`${this.baseUrl}/${url}`, data).pipe(
      retry(1),
      catchError(this.handleError)
    );
  }

  /**
   * DELETE genérico
   */
  delete<T>(url: string): Observable<T> {
    return this.http.delete<T>(`${this.baseUrl}/${url}`).pipe(
      retry(1),
      catchError(this.handleError)
    );
  }

  /**
   * Manejo de errores HTTP
   */
  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'Ocurrió un error desconocido';
    
    if (error.error instanceof ErrorEvent) {
      // Error del lado del cliente
      errorMessage = `Error del cliente: ${error.error.message}`;
    } else {
      // Error del lado del servidor
      errorMessage = `Error del servidor - Código: ${error.status}, Mensaje: ${error.message}`;
      
      // Log adicional para debugging
      console.error('Error completo:', error);
      console.error('URL:', error.url);
      console.error('Status:', error.status);
      console.error('StatusText:', error.statusText);
    }
    
    console.error('ApiService Error:', errorMessage);
    return throwError(() => new Error(errorMessage));
  }
}
