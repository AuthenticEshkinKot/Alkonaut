using System;
using System.Drawing;

using OpenTK;

namespace Alkonaut
{
    class Program
    {
        const double updatesPerSecond = 30, framesPerSecond = 30;

        static void Main(string[] args)
        {
            DisplayDevice display = DetermineDisplay();

            if (display != null)
            {
                using (Game game = new Game(display))
                {
                    game.Run(updatesPerSecond, framesPerSecond);
                }
            }
        }

        static DisplayDevice DetermineDisplay()
        {
            DisplayDevice ret = null;

            foreach (DisplayDevice device in DisplayDevice.AvailableDisplays)
            {
                if (ret == null || device.IsPrimary)
                {
                    ret = device;
                }
            }

            return ret;
        }
    }
}
