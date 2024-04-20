namespace OverlayKit.Keymap
{
    public class KeyEvent
    {
        public uint Key;
        public VKeyCodes VKey;

        public KeyEvent()
        {
            Key = 0;
            VKey = 0;
        }

        public KeyEvent(uint key, VKeyCodes vKey)
        {
            Key = key;
            VKey = vKey;
        }

        public override bool Equals(object obj)
        {
            return obj is KeyEvent @event &&
                   Key == @event.Key &&
                   VKey == @event.VKey;
        }

        public override int GetHashCode()
        {
            int hashCode = 31668044;
            hashCode = hashCode * -1521134295 + Key.GetHashCode();
            hashCode = hashCode * -1521134295 + VKey.GetHashCode();
            return hashCode;
        }
    }
}