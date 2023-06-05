namespace Solo.Input
{
    public interface ISInput
    {
        public bool IsPressed(string keyName);
        public bool IsDown(string keyName);
        public void Add(string keyName, Key key);
    }

}