import { LocationSummaryModel } from "../../dashboard/models/sensors.model";

export interface ModuleModel {
  id?: string;
  name: string;
  description?: string;
  locations?: LocationSummaryModel[];
  accessTokens?: accessTokenModel[];
}

export interface accessTokenModel {
  token: string;
  isActive: boolean;
  expiresAt: Date;
}
