// defines the pin locations
const int potPin1 = A1; //potentiometer pins, player1 = analog 1, player2 = analog 2
const int potPin2 = A2;
#define butPin1 2 // buttons = digital pins, D2/D3 = P1/P2
#define butPin2 3

void setup() {
  Serial.begin(115200); // opens serial port to send readigns to, this is main method of communication with unity
  pinMode(potPin1, INPUT); //defines all of them as input
  pinMode(potPin2, INPUT);
  pinMode(butPin1, INPUT);
  pinMode(butPin2, INPUT);
}

void loop() {
  int readValue1 = analogRead(potPin1); //retreives values and prints
  int readValue2 = analogRead(potPin2);
  Serial.println("1:" + String(readValue1)); //i use 1: and 2: etc, so when reading checks the first integer to see what it is. e.g 1 means player 1 potentiometer, etc
  Serial.println("2:" + String(readValue2));
  int readValue3 = digitalRead(butPin1);
  int readValue4 = digitalRead(butPin2);
  Serial.println("3:" + String(readValue3));
  Serial.println("4:" + String(readValue4));
  delay(20); // small delay to prevent lag with overflow
  }
