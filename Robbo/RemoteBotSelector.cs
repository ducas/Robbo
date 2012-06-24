using System.Threading;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace Robbo
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
            transceiver.Send("AWAITINGBOT\r\n");
            transceiver.MessageReceived += TransceiverMessageReceived;
            handle.Reset();
            Debug.Print("Awaiting Bot.");
            handle.WaitOne();
            transceiver.MessageReceived -= TransceiverMessageReceived;
            return selectedBot;
        }

        private void TransceiverMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var bot = botDiscovery; // e.Message; // overriden because xbee having issues
            switch (bot)
            {
                case botVacuum:
                    selectedBot = new VacuumBot(CreateMotorDriver(), CreateDistanceSensor());
                    break;
                case botMotorDriverTest:
                    selectedBot = new MotorDriverTestBot(CreateMotorDriver());
                    break;
                case botUltrasonicDistanceSensorTest:
                    selectedBot = new UltrasonicDistanceSensorTestBot(CreateDistanceSensor(), CreatePiezo());
                    break;
                case botMotorTest:
                    selectedBot = new MotorTestBot(
                        new Motor(PWM.Pin.PWM1, (Cpu.Pin)FEZ_Pin.Digital.Di9, (Cpu.Pin)FEZ_Pin.Digital.Di8),
                        new Motor(PWM.Pin.PWM6, (Cpu.Pin)FEZ_Pin.Digital.Di5, (Cpu.Pin)FEZ_Pin.Digital.Di4),
                        new OutputPort((Cpu.Pin)FEZ_Pin.Digital.Di7, false)
                        );
                    break;
                case botDiscovery:
                    selectedBot = new DiscoveryBot(CreateMotorDriver(), CreateDistanceSensor(), CreatePiezo());
                    break;
                case botPiezoTest:
                    new PiezoTestBot(CreatePiezo()).Go();
                    break;
                default:
                    transceiver.Send("CMD_UNRECOGNIZED");
                    return;
            }
            transceiver.Send("BOTSELECTED\r\n");
            handle.Set();
        }

        private static Piezo CreatePiezo()
        {
            return new Piezo(PWM.Pin.PWM2);
        }

        private static UltrasonicDistanceSensor CreateDistanceSensor()
        {
            return new UltrasonicDistanceSensor(AnalogIn.Pin.Ain5, (Cpu.Pin)FEZ_Pin.Digital.Di2);
        }

        private static MotorDriver CreateMotorDriver()
        {
            return new MotorDriver(
                PWM.Pin.PWM1, (Cpu.Pin)FEZ_Pin.Digital.Di9, (Cpu.Pin)FEZ_Pin.Digital.Di8,
                PWM.Pin.PWM6, (Cpu.Pin)FEZ_Pin.Digital.Di5, (Cpu.Pin)FEZ_Pin.Digital.Di4,
                (Cpu.Pin)FEZ_Pin.Digital.Di7
                );
        }
    }
}
