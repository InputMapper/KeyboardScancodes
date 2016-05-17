using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODIF;
using System.Windows.Media.Imaging;

namespace KeyboardScancode
{
    [PluginInfo(
        PluginName = "Keyboard Scancode",
        PluginDescription = "",
        PluginID = 53,
        PluginAuthorName = "InputMapper",
        PluginAuthorEmail = "jhebbel@gmail.com",
        PluginAuthorURL = "http://inputmapper.com",
        PluginIconPath = @""
    )]
    public class KeyboardScancode:OutputDevicePlugin
    {
        public KeyboardScancode()
        {
            KeyboardScancodeDevice Keyboard = new KeyboardScancodeDevice();
            Devices.Add(Keyboard);
        }
    }

    public class KeyboardScancodeDevice : OutputDevice
    {
        public KeyboardScancodeDevice()
        {
            this.DeviceName = "Keyboard Scancode Simulator";
            this.StatusIcon = new BitmapImage(new Uri("pack://application:,,,/KeyboardScancode;component/Resources/Blank_White_Normal.png"));
            foreach (var key in Enum.GetNames(typeof(statics.ScanCodeShort)))
            {
                if (key == null || key == "")
                    continue;

                statics.ScanCodeShort keyEnum;
                if (Enum.TryParse(key, out keyEnum))
                {
                    InputChannelTypes.Button keyChannel = new InputChannelTypes.Button(key, ((short)keyEnum).ToString());
                    keyChannel.PropertyChanged += (s, e) =>
                    {
                        if (keyChannel.Value)
                        {
                            statics.SendKeyDown(keyEnum);
                        }
                        else
                        {
                            statics.SendKeyUp(keyEnum);
                        }
                    };
                    InputChannels.Add(keyChannel);
                }
            }
        }
    }
}
