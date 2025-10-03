import { LocationSummaryModel } from "../../dashboard/models/sensors.model";
import { LocationModel } from "./location.model";

export interface ModuleModel {
  id?: string;
  name: string;
  description?: string;
  locations?: LocationModel[];
  accessToken?: accessTokenModel;
}

export interface accessTokenModel {
  token: string;
  isActive: boolean;
  expiresAt: Date;
}
