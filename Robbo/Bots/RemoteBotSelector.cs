using System.Threading;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Robbo.Devices;

namespace Robbo.Bots
{
    public class RemoteBotSelector
    {
        const string botAwaiting = "BOT:AWAITING";
        const string botUnrecognized = "BOT:UNRECOGNIZED";
        const string botSelected = "BOT:SELECTED";

        private const string botUltrasonicDistanceSensorTest = "BOT:UDSTEST";
        private const string botVacuum = "BOT:VACUUM";
        private const string botMotorDriverTest = "BOT:MDRTEST";
        private const string botMotorTest = "BOT:MOTTEST";
        private const string botDiscovery = "BOT:DISCOVERY";
        private const string botSafeDiscovery = "BOT:SAFEDISCO";
        private const string botPiezoTest = "BOT:PZOTEST";
        private const string botAccelerometerTest = "BOT:ACCTEST";

        private readonly Transceiver transceiver;
        private readonly AutoResetEvent handle;
        private IBot selectedBot;

        public RemoteBotSelector(Transceiver transceiver)
        {
            this.transceiver = transceiver;
            handle = new AutoResetEvent(false);
        }

        public IBot AwaitBot()
        {
            Thread.Sleep(5000);
            transceiver.Send(botAwaiting);
            transceiver.MessageReceived += TransceiverMessageReceived;
            handle.Reset();
            Debug.Print("Awaiting Bot.");
            handle.WaitOne();
            transceiver.MessageReceived -= TransceiverMessageReceived;
            return selectedBot;
        }

        private void TransceiverMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var bot = e.Message;
            switch (bot)
            {
                case botVacuum:
                    selectedBot = new VacuumBot(DeviceInitializer.MotorDriver(), DeviceInitializer.DistanceSensor());
                    break;
                case botMotorDriverTest:
                    selectedBot = new MotorDriverTestBot(DeviceInitializer.MotorDriver());
                    break;
                case botUltrasonicDistanceSensorTest:
                    selectedBot = new UltrasonicDistanceSensorTestBot(DeviceInitializer.DistanceSensor(), DeviceInitializer.Piezo());
                    break;
                case botMotorTest:
                    selectedBot = new MotorTestBot(
                        new Motor(PWM.Pin.PWM1, (Cpu.Pin)FEZ_Pin.Digital.Di9, (Cpu.Pin)FEZ_Pin.Digital.Di8),
                        new Motor(PWM.Pin.PWM6, (Cpu.Pin)FEZ_Pin.Digital.Di5, (Cpu.Pin)FEZ_Pin.Digital.Di4),
                        new OutputPort((Cpu.Pin)FEZ_Pin.Digital.Di7, false)
                        );
                    break;
                case botDiscovery:
                    selectedBot = new DiscoveryBot(DeviceInitializer.MotorDriver(), DeviceInitializer.DistanceSensor());
                    break;
                case botSafeDiscovery:
                    selectedBot = new SafeDiscoveryBot(DeviceInitializer.MotorDriver(), DeviceInitializer.DistanceSensor(), DeviceInitializer.Accelerometer());
                    break;
                case botPiezoTest:
                    new PiezoTestBot(DeviceInitializer.Piezo()).Go();
                    break;
                case botAccelerometerTest:
                    new AccelerometerTestBot(DeviceInitializer.Accelerometer()).Go();
                    break;
                default:
                    transceiver.Send(botUnrecognized);
                    return;
            }
            transceiver.Send(botSelected);
            handle.Set();
        }
    }
}
