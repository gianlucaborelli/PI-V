export interface SensorsModel {
  sensorType?: string;
  series?: SensorDataModel[] | null;
}

export interface SensorDataModel {
  value: number;
  name: string;
}
