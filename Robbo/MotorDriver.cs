using System;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT.Hardware;

namespace Robbo
{
    public class MotorDriverBridge : IDisposable
    {
        private readonly Motor leftMotor;
        private readonly Motor rightMotor;
        private readonly OutputPort standbyPort;

        /// <summary>
        /// Represents a H Bridge Motor Driver
        /// </summary>
        /// <param name="leftPwm">The pin connected to the left motor channel's PWM pin on the driver (PWMA)</param>
        /// <param name="leftForwardPin">The pin connected to the left motor channel's forward direction pin on the driver (AIN0)</param>
        /// <param name="leftReversePin">The pin connected to the left motor channel's reverse direction pin on the driver (AIN1)</param>
        /// <param name="rightPwm">The pin connected to the righ motor channel's PWM pin on the driver (PWMB)</param>
        /// <param name="rightForwardPin">The pin connected to the righ motor channel's forward direction pin on the driver (BIN0)</param>
        /// <param name="rightReversePin">The pin connected to the righ motor channel's reverse direction pin on the driver (BIN1)</param>
        /// <param name="standbyPin">The pin connected to the motor driver's standby pin (STBY) </param>
        public MotorDriverBridge(
            PWM.Pin leftPwm, Cpu.Pin leftForwardPin, Cpu.Pin leftReversePin,
            PWM.Pin rightPwm, Cpu.Pin rightForwardPin, Cpu.Pin rightReversePin,
            Cpu.Pin standbyPin
            )
        {
            leftMotor = new Motor(leftPwm, leftForwardPin, leftReversePin);
            rightMotor = new Motor(rightPwm, rightForwardPin, rightReversePin);
            standbyPort = new OutputPort(standbyPin, false);
        }

        public int PwmA { set { leftMotor.Pwm = value; } }
        public bool AIn0 { set { leftMotor.In0 = value; } }
        public bool AIn1 { set { leftMotor.In1 = value; } }
        public bool Stby { set { standbyPort.Write(value); } }
        public int PwmB { set { rightMotor.Pwm = value; } }
        public bool BIn0 { set { rightMotor.In0 = value; } }
        public bool BIn1 { set { rightMotor.In1 = value; } }

        public void Forward(int speed)
        {
            standbyPort.Write(true);
            leftMotor.Forward(speed);
            rightMotor.Forward(speed);
        }

        public void TurnLeft(int speed)
        {
            standbyPort.Write(true);
            leftMotor.Reverse(speed);
            rightMotor.Forward(speed);
        }

        public void TurnRight(int speed)
        {
            standbyPort.Write(true);
            leftMotor.Forward(speed);
            rightMotor.Reverse(speed);
        }

        public void Reverse(int speed)
        {
            standbyPort.Write(true);
            leftMotor.Reverse(speed);
            rightMotor.Reverse(speed);
        }

        public void Stop()
        {
            standbyPort.Write(false);
        }

        public void Dispose()
        {
            leftMotor.Dispose();
            rightMotor.Dispose();
            standbyPort.Dispose();
        }
    }
}