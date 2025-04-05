import { SensorsModel } from "./sensors.model";

export interface DashboardModel {
  humidityAverage?: number;
  temperatureAverage?: number;
  maxTemperature?: number;
  ibtgEstimation?: number;
  series?: SensorsModel[];
}

