

export interface LocationModel {
  id?: string;
  name: string;
  description?: string;
  moduleId?: string;
  riskLimits: RiskLimitsModel[];
}

export interface RiskLimitsModel {
  id?: string;
  category?: string;
  subcategory?: string;
  activity?: string;
  metabolicRate?: number;
  type?: string;

  riskId?: string;
}
