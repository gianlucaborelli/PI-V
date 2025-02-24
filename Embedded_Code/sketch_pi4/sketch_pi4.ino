#include <Arduino.h>
#include <ESPSupabase.h>
#include <ArduinoJson.h>
#include "DHT.h"

#if defined(ESP8266)
#include <ESP8266WiFi.h>
#else
#include <WiFi.h>
#endif

#define DHTPIN 23
#define DHTTYPE DHT22

DHT dht(DHTPIN, DHTTYPE);

Supabase db;

// Put your supabase URL and Anon key here...
String supabase_url = "Link publico Supabase"; 
String anon_key = "Chave anonima Supabase";

// put your WiFi credentials (SSID and Password) here
const char *ssid = "Nome da Rede";
const char *psswd = "Senha da Rede";

// Put your target table here
String table = "Sensors";

bool upsert = false;

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

  db.begin(supabase_url, anon_key);  
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

  JsonDocument doc;
  
  doc["Temperature"] = t;
  doc["Humidity"] = h;

  String JSON = "";

  serializeJson(doc, JSON);

  http.addHeader("Content-Type", "application/json");             //Specify content-type header
  
  int httpResponseCode = http.POST(JSON);   //Send the actual POST request
  
  Serial.println(httpResponseCode);   //Print return code
  Serial.println(response);   
 

  delay(600000);
}