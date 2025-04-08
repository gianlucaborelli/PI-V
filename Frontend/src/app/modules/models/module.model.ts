import { SensorsModel } from "../../dashboard/models/sensors.model";

export interface ModuleModel {
  id: string;
  tag: string;
  sensors?: SensorsModel[];
}
