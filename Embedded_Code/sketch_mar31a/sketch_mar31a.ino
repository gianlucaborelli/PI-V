#include <OneWire.h>
#include <DallasTemperature.h>
#include "DHT.h"
#include <HTTPClient.h>
#include <ArduinoJson.h>
#include <WiFi.h>

// put your WiFi credentials (SSID and Password) here
const char *ssid = "Insert your ssid";
const char *psswd = "insert your password";

const char* serverIP = "pi-v.onrender.com/"; 

const int oneWireBus = 23;
const int DHTPIN = 22;
const int DHTTYPE = DHT22;

String chipId;

const String dbtId = "0195acc7-3878-76e7-8fb0-5a89f0dbfdfe";
const String bgtId = "0195acc7-3878-7e27-baf1-65b3bd0238db";
const String rhtId = "0195acc7-386b-786f-a88d-d765eb9b9a75";

const String moduleId;

DHT dbt(DHTPIN, DHTTYPE); //dry bulb thermometer and relative Humidity
OneWire oneWire(oneWireBus);
DallasTemperature bgt(&oneWire); //black globe thermometer

void setup() {  
  dbt.begin();
  bgt.begin();

  Serial.begin(115200);
  Serial.print("Connecting to WiFi");
  WiFi.begin(ssid, psswd);
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(100);
    Serial.print(".");
  }
  Serial.println("\nConnected!");

  delay(1000); 
  
  String chipId = WiFi.macAddress();  
  Serial.println("MAC Address do ESP32: " + chipId);
}

void loop() {  
  bgt.requestTemperatures(); 
  float darkBulbTemp = bgt.getTempCByIndex(0);  
  float relativeHumidity = dbt.readHumidity();
  float dryBulbTemp = dbt.readTemperature();

  if (darkBulbTemp == DEVICE_DISCONNECTED_C) {
    Serial.println("Erro ao ler o sensor DS18B20!");
  }  

  if (isnan(relativeHumidity) || isnan(dryBulbTemp)) {
    Serial.println("Erro ao ler o sensor DHT22!");
  }

  PrintOnSerialMonitor(darkBulbTemp, relativeHumidity, dryBulbTemp);
  SendData(dbtId, dryBulbTemp);
  SendData(bgtId, darkBulbTemp);
  SendData(rhtId, relativeHumidity);

  delay(30000);
}

void SendData(String sensorId, float sensorValue){
  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;
    String url = "https://" + String(serverIP) + "sensor-data";
    
    JsonDocument doc;
    doc["SensorId"] = sensorId;
    doc["Value"] = sensorValue;

    String JSON;
    serializeJson(doc, JSON);
    
    http.begin(url);
    http.addHeader("Content-Type", "application/json");

    int httpResponseCode = http.POST(JSON);   

    Serial.print("HTTP Response Code: ");
    Serial.println(httpResponseCode);

    if (httpResponseCode > 0) {
      if (httpResponseCode == 200) {
        Serial.println("Dados enviados com sucesso!");
      } else if (httpResponseCode == 400) {
        Serial.println("Erro: Bad Request - Verifique o JSON enviado.");
      } else if (httpResponseCode == 401) {
        Serial.println("Erro: Unauthorized - Verifique as credenciais.");
      } else if (httpResponseCode == 403) {
        Serial.println("Erro: Forbidden - Acesso negado ao recurso.");
      } else if (httpResponseCode == 404) {
        Serial.println("Erro: Not Found - Endpoint inválido.");
      } else if (httpResponseCode >= 500) {
        Serial.println("Erro: Problema no servidor.");
      } else {
        Serial.println("Erro desconhecido.");
      }
    } else {
      Serial.println("Falha na conexão ou erro ao enviar dados!");
    }
    
    http.end();
  } else {
    Serial.println("WiFi desconectado! Não foi possível enviar os dados.");
  }
}

void PrintOnSerialMonitor(float darkBulbTemp, float relativeHumidity, float  dryBulbTemp){
  Serial.print("Bulbo escuro: ");
  Serial.print(darkBulbTemp);  
  Serial.print("ºC | Humidade: ");
  Serial.print(relativeHumidity);
  Serial.print("% | Temperatura: ");
  Serial.print(dryBulbTemp);
  Serial.println("ºC");
}
