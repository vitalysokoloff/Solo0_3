namespace Solo.Input
{
    /// <summary>
    /// Одно из восьми направлений верх, низ, право, лево, левоВерх, правоВерх, левоНиз, ПравоНиз + undefined, используется когда стик в "положении покоя"
    /// </summary>
    public enum StickDirections   
    {
        Undefined = 0,
        Up = 1,
        RightUp = 2,
        Right = 3,
        RightDown = 4,
        Down = 5,
        LeftDown = 6,
        Left = 7,        
        LeftUp = 8
    }
}