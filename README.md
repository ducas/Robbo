# Hello Robbo!

## A .Net Micro Framework Tank Bot

This project aims to build an automated tank bot using the .Net Micro Framework.

## Parts

### Electronics

* FEZ Panda II
* Dual Motor Driver
* Triple Axis Accelerometer
* Ultrasonic Distance Finder - MaxBotix LV-EZ0
* 4xAAA Battery Holder
* 1x9V Battery Holder

### Moving Stuff
* Tamiya Plate Set
* Tamiya Track and Wheel Set
* Tamiya Dual Gearbox
* Low Current Replacement Motors

## Hooking Things Up

The Fritzing diagram is included in the Fritzing directory (both as a Fritzing file and PNG).

### Power

* 4xAAA -> Motor Driver
	* Red -> Vin
	* Black -> GND
* 9V -> FEZ Panda
	* Red -> Vin
	* Black -> GND
* Accelerometer -> FEZ Panda
	* SCL -> SCL
	* SDA -> SDA
	* SDO -> GND
	* CS -> 3.3V
	* VCC -> 3.3V
	* GND -> GND
* Motor Driver -> FEZ Panda
	* PWMA
	* AIN0
	* AIN1
	* STBY
	* PWMB
	* BIN0
	* BIN1
* Distance Sensor -> FEZ Panda
	* GND -> GND
	* +5 -> 5V
	* RX -> D11
	* AN -> A0