import { createRouter, createWebHistory } from 'vue-router'
import FormularioAcopio from '../components/FormularioAcopio.vue'
import HomeView from '@/components/HomeView.vue';
import FormularioCaracterizacion from '../components/FCaracterizacion.vue'
import FSecado from '@/components/FSecado.vue';
import FBodega from '../components/FBodega.vue';
import FTrilla from '@/components/FTrilla.vue';
import FCatacion from '@/components/FCatacion.vue';

const routes = [
  { path: "/", name: "HomeView", component: HomeView },
  { path: "/formulario-nuevo", name: "FormularioNuevo", component: FormularioAcopio},
  {path:"/caracterizacion",name:"FormularioCaracterizacion",component:FormularioCaracterizacion},
  { path: '/secado', name: 'FormularioSecado', component: FSecado },
  { path: '/bodega', name: 'FormularioBodega', component: FBodega },
  { path: '/trilla', name: 'FormularioTrilla', component: FTrilla },
  { path: '/catacion', name: 'FormularioCatación', component: FCatacion },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
