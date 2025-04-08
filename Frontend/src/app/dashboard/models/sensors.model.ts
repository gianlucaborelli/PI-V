export interface SensorsModel {
  id?: string;
  name?: string;
  type?: string;
  description?: string;
  series?: SensorDataModel[] | null;
}

export interface SensorDataModel {
  value: number;
  name: string;
}
