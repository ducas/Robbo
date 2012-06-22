using System.Threading;
using Microsoft.SPOT;

namespace Robbo
{
    public class MotorDriverTestBot
    {
        private readonly MotorDriverBridge driver;

        public MotorDriverTestBot(MotorDriverBridge driver)
        {
            this.driver = driver;
        }

        public void Go()
        {
            while (true)
            {
                TestA();
                TestB();
            }
            // ReSharper disable FunctionNeverReturns
        }
        // ReSharper restore FunctionNeverReturns

        private void TestA()
        {
            Debug.Print("AIN0: 0, AIN1: 0, STBY: 1 => A OFF");
            driver.PwmA = 100;
            driver.AIn0 = false;
            driver.AIn1 = false;
            driver.Stby = true;
            Thread.Sleep(1000);

            Debug.Print("AIN0: 1, AIN1: 0, STBY: 1 => A CCW");
            driver.PwmA = 100;
            driver.AIn0 = true;
            driver.AIn1 = false;
            driver.Stby = true;
            Thread.Sleep(1000);

            Debug.Print("AIN0: 1, AIN1: 1, STBY: 1 => A OFF");
            driver.PwmA = 100;
            driver.AIn0 = true;
            driver.AIn1 = true;
            driver.Stby = true;
            Thread.Sleep(1000);

            Debug.Print("AIN0: 0, AIN1: 1, STBY: 1 => A OFF");
            driver.PwmA = 100;
            driver.AIn0 = false;
            driver.AIn1 = true;
            driver.Stby = true;
            Thread.Sleep(1000);
        }

        private void TestB()
        {
            Debug.Print("BIN0: 0, BIN1: 0, STBY: 1 => B OFF");
            driver.PwmB = 100;
            driver.BIn0 = false;
            driver.BIn1 = false;
            driver.Stby = true;
            Thread.Sleep(1000);

            Debug.Print("BIN0: 1, BIN1: 0, STBY: 1 => B CCW");
            driver.PwmB = 100;
            driver.BIn0 = true;
            driver.BIn1 = false;
            driver.Stby = true;
            Thread.Sleep(1000);

            Debug.Print("BIN0: 1, BIN1: 1, STBY: 1 => B OFF");
            driver.PwmB = 100;
            driver.BIn0 = true;
            driver.BIn1 = true;
            driver.Stby = true;
            Thread.Sleep(1000);

            Debug.Print("BIN0: 0, BIN1: 1, STBY: 1 => B OFF");
            driver.PwmB = 100;
            driver.BIn0 = false;
            driver.BIn1 = true;
            driver.Stby = true;
            Thread.Sleep(1000);
        }
    }
}
