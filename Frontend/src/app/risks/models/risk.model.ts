export interface RiskModel {
  category: string;
  subCategory: string;
  activity: string;
  metabolicRate: number;
  id: string;




}

export interface Categoria {
  nome: string;
  subcategorias: {
    nome: string;
    itens: {
      id: string;
      nome: string;
      metabolicRate: number;
    }[];
  }[];
}
