import { IPersonas } from "./ipersonas";

export interface IUsuarios {
  id: number,
  idPersona:number,
  usuario: string,
  clave: string,
  persona: IPersonas;
}
