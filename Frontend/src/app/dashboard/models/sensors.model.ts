export interface LocationSummaryModel {
  name?: string;
  type?: string;
  description?: string;
  ibutgEstimation?: number;
  ibutgReferenceLimit?: number;
  humidityAverage?: number;
  temperatureAverage?: number;
  maxTemperature?: number;
  minTemperature?: number;
  series?: SeriesDataModel[] | null;
}

export interface SeriesDataModel {
  createdAt?: Date;
  humidity?: number | null;
  dryBulbTemperature?: number | null;
  darkBulbTemperature?: number | null;
}
