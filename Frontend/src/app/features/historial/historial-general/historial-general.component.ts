import { Component, OnInit, ChangeDetectorRef, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { 
  RegistroGeneral, 
  TipoEntidad, 
  EstadoRegistro, 
  FiltrosHistorial,
  ResumenEstadisticas 
} from '../historial.model';
import { AreaAcopioService } from '../../../core/services/area-acopio.service';
import { SecadoService } from '../../../core/services/secado.service';
import { BodegaService } from '../../../core/services/bodega.service';
import { TrillaService } from '../../../core/services/trilla.service';
import { CaracterizacionService } from '../../../core/services/caracterizacion.service';
import { CatacionService } from '../../../core/services/catacion.service';
import { forkJoin, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Component({
  selector: 'app-historial-general',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './historial-general.component.html',
  styleUrl: './historial-general.component.css'
})
export class HistorialGeneralComponent implements OnInit {
  registros: RegistroGeneral[] = [];
  registrosFiltrados: RegistroGeneral[] = [];
  loading: boolean = true;
  error: string = '';
  
  // Filtros
  filtros: FiltrosHistorial = {};
  tiposEntidad: TipoEntidad[] = [
    'AreaAcopio',
    'Secado',
    'Bodega',
    'Trilla',
    'Caracterizacion',
    'Catacion'
  ];
  
  estadosRegistro: EstadoRegistro[] = [
    'Completado',
    'En Proceso',
    'Pendiente'
  ];

  // Estadísticas
  estadisticas?: ResumenEstadisticas;

  // Paginación
  paginaActual: number = 1;
  registrosPorPagina: number = 20;
  totalPaginas: number = 1;

  // Vista
  vistaActual: 'tabla' | 'cards' = 'cards';
  registroSeleccionado?: RegistroGeneral;
  mostrarModal: boolean = false;

  constructor(
    private router: Router,
    private acopioService: AreaAcopioService,
    private secadoService: SecadoService,
    private bodegaService: BodegaService,
    private trillaService: TrillaService,
    private caracterizacionService: CaracterizacionService,
    private catacionService: CatacionService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.cargarTodosLosRegistros();
  }

  cargarTodosLosRegistros(): void {
    this.loading = true;
    this.error = '';

    forkJoin({
      acopios: this.acopioService.obtenerTodos().pipe(
        map(data => Array.isArray(data) ? data : []),
        catchError(() => of([]))
      ),
      secados: this.secadoService.obtenerTodos().pipe(
        map(data => Array.isArray(data) ? data : []),
        catchError(() => of([]))
      ),
      bodegas: this.bodegaService.obtenerTodos().pipe(
        map(data => Array.isArray(data) ? data : []),
        catchError(() => of([]))
      ),
      trillas: this.trillaService.obtenerTodos().pipe(
        map(data => Array.isArray(data) ? data : [data]),
        catchError(() => of([]))
      ),
      caracterizaciones: this.caracterizacionService.obtenerTodos().pipe(
        map(data => Array.isArray(data) ? data : [data]),
        catchError(() => of([]))
      ),
      cataciones: this.catacionService.obtenerTodos().pipe(
        map(data => Array.isArray(data) ? data : []),
        catchError(() => of([]))
      )
    }).subscribe({
      next: (data) => {
        this.registros = [
          ...this.procesarAcopios(data.acopios),
          ...this.procesarSecados(data.secados),
          ...this.procesarBodegas(data.bodegas),
          ...this.procesarTrilla(data.trillas),
          ...this.procesarCaracterizaciones(data.caracterizaciones),
          ...this.procesarCataciones(data.cataciones)
        ];

        // Ordenar por fecha descendente
        this.registros.sort((a, b) => 
          new Date(b.fecha).getTime() - new Date(a.fecha).getTime()
        );

        this.registrosFiltrados = [...this.registros];
        this.calcularEstadisticas();
        this.calcularPaginacion();
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Error al cargar los registros del historial';
        this.loading = false;
        this.cdr.detectChanges();
        console.error('Error cargando historial:', err);
      }
    });
  }

  private procesarAcopios(acopios: any[]): RegistroGeneral[] {
    return acopios.map(a => ({
      tipo: 'AreaAcopio',
      id: a.nlote,
      nlote: a.nlote,
      fecha: a.facopio || new Date().toISOString(),
      estado: 'Completado',
      resumen: `${a.nproductor} - ${a.nfinca} - ${a.zona}`,
      datos: a
    }));
  }

  private procesarSecados(secados: any[]): RegistroGeneral[] {
    return secados.map(s => ({
      tipo: 'Secado',
      id: s.idSecado,
      nlote: s.nlote,
      fecha: s.finicio || new Date().toISOString(),
      estado: s.ffinal ? 'Completado' : 'En Proceso',
      resumen: `Secado ${s.dsecado || 'Natural'} - ${s.ncamas?.length || 0} camas`,
      datos: s
    }));
  }

  private procesarBodegas(bodegas: any[]): RegistroGeneral[] {
    return bodegas.map(b => ({
      tipo: 'Bodega',
      id: b.idBodega,
      nlote: b.nlote,
      fecha: b.finicioReposo || new Date().toISOString(),
      estado: 'Completado',
      resumen: `${b.cantidadSacos || 0} sacos - H: ${b.hfinal || 0}%`,
      datos: b
    }));
  }

  private procesarTrilla(trillas: any[]): RegistroGeneral[] {
    return trillas.map(t => ({
      tipo: 'Trilla',
      id: t.idTrilla,
      nlote: t.nlote,
      fecha: t.ftrilla || new Date().toISOString(),
      estado: 'Completado',
      resumen: `R.Final: ${t.rfinalSeleccion || 0}% - Verde: ${t.wverdeFinal || 0}kg`,
      datos: t
    }));
  }

  private procesarCaracterizaciones(caracterizaciones: any[]): RegistroGeneral[] {
    return caracterizaciones.map(c => ({
      tipo: 'Caracterizacion',
      id: `${c.fecha}_${c.hora}`,
      nlote: c.nlote,
      fecha: c.fecha || new Date().toISOString(),
      estado: 'Completado',
      resumen: `${c.proceso || ''} - Lote Asignado: ${c.lasignado || ''}`,
      datos: c
    }));
  }

  private procesarCataciones(cataciones: any[]): RegistroGeneral[] {
    return cataciones.map(c => ({
      tipo: 'Catacion',
      id: c.idCatacion,
      nlote: 'N/A', // Catación puede no estar vinculada directamente a nlote
      fecha: c.fcatacion || new Date().toISOString(),
      estado: 'Completado',
      resumen: `Catador: ${c.ncatador || ''} - Calidad: ${c.calidad || ''}`,
      datos: c
    }));
  }

  aplicarFiltros(): void {
    this.registrosFiltrados = this.registros.filter(registro => {
      let cumple = true;

      // Filtro por tipo
      if (this.filtros.tipo && registro.tipo !== this.filtros.tipo) {
        cumple = false;
      }

      // Filtro por lote
      if (this.filtros.nlote && 
          (!registro.nlote || !registro.nlote.toLowerCase().includes(this.filtros.nlote.toLowerCase()))) {
        cumple = false;
      }

      // Filtro por estado
      if (this.filtros.estado && registro.estado !== this.filtros.estado) {
        cumple = false;
      }

      // Filtro por fecha inicio
      if (this.filtros.fechaInicio) {
        const fechaRegistro = new Date(registro.fecha);
        const fechaInicio = new Date(this.filtros.fechaInicio);
        if (fechaRegistro < fechaInicio) {
          cumple = false;
        }
      }

      // Filtro por fecha fin
      if (this.filtros.fechaFin) {
        const fechaRegistro = new Date(registro.fecha);
        const fechaFin = new Date(this.filtros.fechaFin);
        if (fechaRegistro > fechaFin) {
          cumple = false;
        }
      }

      // Filtro por búsqueda general
      if (this.filtros.busqueda) {
        const busqueda = this.filtros.busqueda.toLowerCase();
        const resumen = registro.resumen.toLowerCase();
        const nlote = (registro.nlote || '').toLowerCase();
        
        if (!resumen.includes(busqueda) && !nlote.includes(busqueda)) {
          cumple = false;
        }
      }

      return cumple;
    });

    this.paginaActual = 1;
    this.calcularPaginacion();
  }

  limpiarFiltros(): void {
    this.filtros = {};
    this.registrosFiltrados = [...this.registros];
    this.paginaActual = 1;
    this.calcularPaginacion();
  }

  calcularEstadisticas(): void {
    const registrosPorTipo = new Map<TipoEntidad, number>();
    const registrosPorEstado = new Map<EstadoRegistro, number>();
    const lotesUnicos = new Set<string>();

    this.tiposEntidad.forEach(tipo => registrosPorTipo.set(tipo, 0));
    this.estadosRegistro.forEach(estado => registrosPorEstado.set(estado, 0));

    this.registros.forEach(registro => {
      // Contar por tipo
      const countTipo = registrosPorTipo.get(registro.tipo) || 0;
      registrosPorTipo.set(registro.tipo, countTipo + 1);

      // Contar por estado
      const countEstado = registrosPorEstado.get(registro.estado) || 0;
      registrosPorEstado.set(registro.estado, countEstado + 1);

      // Lotes únicos
      if (registro.nlote) {
        lotesUnicos.add(registro.nlote);
      }
    });

    this.estadisticas = {
      totalRegistros: this.registros.length,
      registrosPorTipo,
      registrosPorEstado,
      lotesActivos: lotesUnicos.size,
      ultimaActualizacion: new Date()
    };
  }

  calcularPaginacion(): void {
    this.totalPaginas = Math.ceil(this.registrosFiltrados.length / this.registrosPorPagina);
    if (this.paginaActual > this.totalPaginas) {
      this.paginaActual = this.totalPaginas || 1;
    }
  }

  get registrosPaginados(): RegistroGeneral[] {
    const inicio = (this.paginaActual - 1) * this.registrosPorPagina;
    const fin = inicio + this.registrosPorPagina;
    return this.registrosFiltrados.slice(inicio, fin);
  }

  cambiarPagina(pagina: number): void {
    if (pagina >= 1 && pagina <= this.totalPaginas) {
      this.paginaActual = pagina;
    }
  }

  get paginasArray(): number[] {
    return Array.from({ length: this.totalPaginas }, (_, i) => i + 1);
  }

  cambiarVista(vista: 'tabla' | 'cards'): void {
    this.vistaActual = vista;
  }

  verDetalle(registro: RegistroGeneral): void {
    this.registroSeleccionado = registro;
    this.mostrarModal = true;
  }

  cerrarModal(): void {
    this.mostrarModal = false;
    this.registroSeleccionado = undefined;
  }

  editarRegistro(registro: RegistroGeneral): void {
    const rutas: Record<TipoEntidad, string> = {
      'AreaAcopio': '/acopio/edit',
      'Secado': '/secado/edit',
      'Bodega': '/bodega/edit',
      'Trilla': '/trilla/edit',
      'Caracterizacion': '/caracterizacion/edit',
      'Catacion': '/catacion/edit'
    };

    const ruta = rutas[registro.tipo];
    if (ruta) {
      this.router.navigate([ruta, registro.id]);
    }
  }

  eliminarRegistro(registro: RegistroGeneral): void {
    if (!confirm(`¿Está seguro de eliminar este registro de ${registro.tipo}?`)) {
      return;
    }

    let servicio: any;
    switch (registro.tipo) {
      case 'AreaAcopio':
        servicio = this.acopioService;
        break;
      case 'Secado':
        servicio = this.secadoService;
        break;
      case 'Bodega':
        servicio = this.bodegaService;
        break;
      case 'Trilla':
        servicio = this.trillaService;
        break;
      case 'Caracterizacion':
        servicio = this.caracterizacionService;
        break;
      case 'Catacion':
        servicio = this.catacionService;
        break;
    }

    if (servicio) {
      servicio.eliminar(registro.id).subscribe({
        next: () => {
          this.cargarTodosLosRegistros();
        },
        error: (err: any) => {
          alert(`Error al eliminar: ${err.message || 'Error desconocido'}`);
        }
      });
    }
  }

  getIconoTipo(tipo: TipoEntidad): string {
    const iconos: Record<TipoEntidad, string> = {
      'AreaAcopio': '🏪',
      'Secado': '☀️',
      'Bodega': '🏭',
      'Trilla': '⚙️',
      'Caracterizacion': '🔬',
      'Catacion': '☕'
    };
    return iconos[tipo] || '📝';
  }

  getColorEstado(estado: EstadoRegistro): string {
    const colores: Record<EstadoRegistro, string> = {
      'Completado': '#8FAD5A',
      'En Proceso': '#f39c12',
      'Pendiente': '#A52A3D'
    };
    return colores[estado] || '#999';
  }

  exportarCSV(): void {
    const headers = ['Tipo', 'ID', 'Lote', 'Fecha', 'Estado', 'Resumen'];
    const rows = this.registrosFiltrados.map(r => [
      r.tipo,
      r.id,
      r.nlote || 'N/A',
      new Date(r.fecha).toLocaleDateString(),
      r.estado,
      r.resumen
    ]);

    let csv = headers.join(',') + '\n';
    rows.forEach(row => {
      csv += row.map(cell => `"${cell}"`).join(',') + '\n';
    });

    const blob = new Blob([csv], { type: 'text/csv;charset=utf-8;' });
    const link = document.createElement('a');
    const url = URL.createObjectURL(blob);
    
    link.setAttribute('href', url);
    link.setAttribute('download', `historial_${new Date().getTime()}.csv`);
    link.style.visibility = 'hidden';
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }
}
