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
        const string botUltrasonicDistanceSensorTest = "BOT:UDSTEST";
        const string botVacuum = "BOT:VACUUM";
        const string botMotorDriverTest = "BOT:MDRTEST";
        const string botMotorTest = "BOT:MOTTEST";
        const string botDiscovery = "BOT:DISCOVERY";
        const string botPiezoTest = "BOT:PZOTEST";

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
            transceiver.Send("BOT:AWAITING");
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
                case botPiezoTest:
                    new PiezoTestBot(DeviceInitializer.Piezo()).Go();
                    break;
                default:
                    transceiver.Send("BOT:UNRECOGNIZED");
                    return;
            }
            transceiver.Send("BOT:SELECTED");
            handle.Set();
        }
    }
}
