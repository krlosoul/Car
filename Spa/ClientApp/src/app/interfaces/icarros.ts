import { IProductos } from "./iproductos";
export interface ICarros {
  id: number,
  referenciaProducto: string,
  cantidad: number,
  total: number,
  estado: number
  productos: Array<IProductos>,
}


