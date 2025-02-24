#include <Arduino.h>
#include <ESPSupabase.h>
#include <ArduinoJson.h>
#include <HTTPClient.h>
#include "DHT.h"

#if defined(ESP8266)
#include <ESP8266WiFi.h>
#else
#include <WiFi.h>
#endif

#define DHTPIN 23
#define DHTTYPE DHT22
const char* serverIP = "192.168.0.18"; // IP do seu computador onde a API está rodando
const int serverPort = 5273;  
const int sensorId = 407770;

DHT dht(DHTPIN, DHTTYPE);

// put your WiFi credentials (SSID and Password) here
const char *ssid = "Nome da Rede";
const char *psswd = "Senha da Rede";

int count = 0;

void setup()
{
  Serial.begin(9600);
  dht.begin();

  Serial.print("Connecting to WiFi");
  WiFi.begin(ssid, psswd);
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(100);
    Serial.print(".");
  }
  Serial.println("\nConnected!");
}

void loop()
{
  float h = dht.readHumidity();
  float t = dht.readTemperature();
  float f = dht.readTemperature(true);

  if (isnan(h) || isnan(t) || isnan(f)) {
    Serial.println(F("Failed reception"));
    return;
  }

  Serial.print("Humidite: ");
  Serial.print(h);
  Serial.print("%  Temperature: ");
  Serial.print(t);
  Serial.print("°C, ");
  Serial.print(f);
  Serial.println("°F");
  
  if (WiFi.status() == WL_CONNECTED && count == 2) {
    HTTPClient http;
    String url = "http://" + String(serverIP) + ":" + String(serverPort) + "/api/Sensor";
    
    JsonDocument doc;
  
    doc["SensorId"] = sensorId;
    doc["Temperature"] = t;
    doc["Humidity"] = h;

    String JSON = "";

    serializeJson(doc, JSON);
    
    http.begin(url);
    http.addHeader("Content-Type", "application/json"); //Specify content-type header
    
    int httpResponseCode = http.POST(JSON);   

    if (httpResponseCode > 0) {
      String response = http.getString();
      Serial.println("Resposta: " + response);
    } else {
      Serial.println("Erro na requisição: " + String(httpResponseCode));
    }
    http.end();
    count = 0;
  } 
  count = (count + 1);
  delay(10000);
}