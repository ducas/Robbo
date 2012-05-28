using System;
using System.Threading;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT.Hardware;

namespace Robbo
{
    public class Robot : IDisposable
    {
        private const int interruptDistance = 30;
        private const int fullSpeed = 50;
        private const int forwardDuration = 500;
        private const int stopDuration = 500;
        private const int backUpDuration = 1000;
        private const int turnSpeed = 10;
        private const int turnDuration = 3000;

        private readonly Motor leftMotor;
        private readonly Motor rightMotor;
        private readonly InfraredDistanceSensor infraredDistanceSensor;

        public Robot()
        {
            leftMotor = new Motor(PWM.Pin.PWM4, Cpu.Pin.GPIO_Pin5);
            rightMotor = new Motor(PWM.Pin.PWM6, Cpu.Pin.GPIO_Pin7, true);
            infraredDistanceSensor = new InfraredDistanceSensor(AnalogIn.Pin.Ain10);
        }

        public void Go()
        {
            GoForward();
            while (true)
            {
                if (infraredDistanceSensor.Distance < interruptDistance)
                {
                    Stop();
                    BackUp();
                    TurnLeft();
                    GoForward();
                }

                Thread.Sleep(forwardDuration);
            }
// ReSharper disable FunctionNeverReturns
        }
// ReSharper restore FunctionNeverReturns

        private void GoForward()
        {
            leftMotor.Forward(fullSpeed);
            rightMotor.Forward(fullSpeed); 
        }

        private void Stop()
        {
            leftMotor.Stop();
            rightMotor.Stop();
            Thread.Sleep(stopDuration);
        }

        private void BackUp()
        {
            leftMotor.Reverse(fullSpeed);
            rightMotor.Reverse(fullSpeed);
            Thread.Sleep(backUpDuration);
            Stop();
        }

        private void TurnLeft()
        {
            leftMotor.Forward(turnSpeed);
            rightMotor.Reverse(turnSpeed);
            Thread.Sleep(turnDuration);
            Stop();
        }

        public void Dispose()
        {
            leftMotor.Dispose();
            rightMotor.Dispose();
            infraredDistanceSensor.Dispose();
        }
    }
}
