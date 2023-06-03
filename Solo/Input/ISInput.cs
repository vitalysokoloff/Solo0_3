namespace Solo.Input
{
    public interface ISInput
    {
        public SKeyState IsPressed(string keyName);
        public SKeyState IsDown(string keyName);
        public void Add(string keyName, Key key);
    }

}