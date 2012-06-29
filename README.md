# Hello Robbo!

## A .Net Micro Framework Tank Bot

This project aims to build an automated tank bot using the .Net Micro Framework.

## Parts

### Electronics

* FEZ Panda II [Datasheet](https://raw.github.com/ducas/Robbo/master/Datasheets/FEZ_Panda_II_UserManual.pdf)
* Motor Driver 1A Dual TB6612FNG (ROB-09457) [Datasheet](https://raw.github.com/ducas/Robbo/master/Datasheets/TB6612FNG.pdf)
* Triple Axis Accelerometer Breakout - ADXL345 (SEN-09836) [Datasheet](https://raw.github.com/ducas/Robbo/master/Datasheets/ADXL345.pdf)
* Ultrasonic Range Finder - MaxBotix LV-EZ0 (SEN-08502) [Datasheet](https://raw.github.com/ducas/Robbo/master/Datasheets/MB1000_Datasheet.pdf)
* 4xAAA Battery Holder
* 1x9V Battery Holder

### Moving Stuff
* Tamiya Plate Set 70157
* Tamiya Track and Wheel Set 70100
* Tamiya Dual Gearbox 70168
* Low Current Replacement Motors - Solarbotics RM3

## Hooking Things Up

The Fritzing diagram is included in the Fritzing directory (both as a Fritzing file and PNG).

![Wiring Diagram](https://raw.github.com/ducas/Robbo/master/Fritzing/robbo.PNG)

* 4xAAA -> Motor Driver
	* Red -> VM
	* Black -> GND
* 9V -> FEZ Panda
	* Red -> Vin
	* Black -> GND
* Accelerometer -> FEZ Panda
	* SCL -> D3 (SCL)
	* SDA -> D2 (SDA)
	* SDO -> GND
	* CS -> 3.3V
	* VCC -> 3.3V
	* GND -> GND
* Motor Driver -> FEZ Panda
	* PWMA -> D10 (PWM6)
	* AIN0 -> D9
	* AIN1 -> D8
	* STBY -> D7
	* PWMB -> D6 (PWM1)
	* BIN0 -> D5
	* BIN1 -> D4
	* VCC -> 5V
	* AOUT0/1 -> Left Motor
	* BOUT0/1 -> Right Motor
* Distance Sensor -> FEZ Panda
	* GND -> GND
	* +5 -> 5V
	* RX -> D11
	* AN -> A0