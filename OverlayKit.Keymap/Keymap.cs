using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace OverlayKit.Keymap
{
    public class Keymap
    {
        ConcurrentDictionary<uint, bool> prevBuff = new ConcurrentDictionary<uint, bool>();

        #region WinAPi

        [DllImport("User32.dll")]
        static extern bool GetAsyncKeyState(uint key);

        #endregion

        /// <summary>
        /// This event is triggered when a key is pressed, held or let go
        /// </summary>
        public event Action<KeyEvent> OnKeyPress;

        /// <summary>
        /// Constructor for keymap
        /// </summary>
        /// <param name="tick4me">if true it'll start a new thread for the keymap to tick on</param>
        public Keymap(bool tick4me = true)
        {
            if (tick4me)
            {
                Task.Factory.StartNew(() => {
                    while (true)
                    {
                        Thread.Sleep(15); // minimal sleep system allows

                        Tick(); // tick keymap
                    }
                });
            }

            // initialize the keymap with no values
            for (uint i = 0; i < 0xFF; ++i)
                prevBuff.TryAdd(i, false);
        }

        /// <summary>
        /// Attempt to get the down state from the concurrent keymap
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool GetDown(uint key)
        {
            bool down = false;

            prevBuff.TryGetValue(key, out down);

            return down;
        }

        /// <summary>
        /// Called when ticking a keymap
        /// </summary>
        public void Tick()
        {
            // keypress event is hooked so lets tick
            if (OnKeyPress != null)
            {
                // loop over every possible key
                for (uint i = 0; i < 0xFF; ++i)
                {
                    bool held = GetAsyncKeyState(i);

                    if (held && !prevBuff[i]) OnKeyPress.Invoke(new KeyEvent(i, VKeyCodes.KeyDown));
                    else if (held && prevBuff[i]) OnKeyPress.Invoke(new KeyEvent(i, VKeyCodes.KeyHeld));
                    else if (!held && prevBuff[i]) OnKeyPress.Invoke(new KeyEvent(i, VKeyCodes.KeyUp));

                    prevBuff[i] = held;
                }
            }
        }
    }
}