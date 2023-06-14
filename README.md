# Solo 0.3
Игровой движок на базе MonoGame 3.8.1 | net6.0  
  
![Лого](https://github.com/vitalysokoloff/Solo0_3/blob/main/solo_engine_0_3.png)
## [Library's Tree]
```
Solo    
    [] Collections
        - Heap (class)    
    [] Entities
        /*[] Cameras
        [] Colliders
            - Collider (class)
            - ICollider (Interface) 
        [] GameObjects
            - GameObjectInfo (class)
            - IGameObject (Interface)
        - IEntity (Interface) // Интерфейс сущности которая имеет два метода: Update() и Draw().        
        - MoveDelegate(Vector2 position) (delegate)
        - RotateDelegate(float angle) (delegate)
        - SConsoleManager (class)
        [] Shapes
            - IShape (Interface)
            - Shape (Abstract class) : IShape
            - SRectangle (class) : Shape // Прямоугольник.
            - SRegularPolygon (class) : Shape // Правильный многоугольник (Отрзок, треугольник, ромб и т д.).
        [] Sprites
            - ISprite (Inerface)
            - Sprite (class)
    [] Input
        - Chars (static class)
        - ISInput (Interface)
        - Key (class)
        - KeyInput (class)
        - SKeyState (enum)
        - StickDirections (enum) // Одно из восьми направлений верх, низ, право, лево, левоВерх, правоВерх, левоНиз, ПравоНиз + undefined, используется когда стик в "положении покоя"
        - TextInput (class)        
    [] Physics        
        - CollisionInformation (static class) // Класс для получения информации о столкновении объектов.
        - GJK (static class) // Класс для обнаружения столкновений объектов.
    - SConsole (static class) // Консоль для воода / вывода текста.
    - Timer (class)
    - Tools (static class) // Класс содержащий различные вспомогательные методы.
```


### [Collections]
```
[Heap]
    public Heap()
    public void Add(string key, int value) // Добавляет в кучу новыую переменную с типом int, если такой ключ уже имеется, то переписывает значение на новое.
    public void Add(string key, float value) // Добавляет в кучу новыую переменную с типом float, если такой ключ уже имеется, то переписывает значение на новое.
    public void Add(string key, string value) // Добавляет в кучу новыую переменную с типом string, если такой ключ уже имеется, то переписывает значение на новое.
    public void Add(string key, bool value) // Добавляет в кучу новыую переменную с типом bool, если такой ключ уже имеется, то переписывает значение на новое.
    public void Add(string key, Point value) // Добавляет в кучу новыую переменную с типом Point, если такой ключ уже имеется, то переписывает значение на новое.
    public void Add(string key, Vector2 value) // Добавляет в кучу новыую переменную с типом Vector2, если такой ключ уже имеется, то переписывает значение на новое.
    public void Add(string key, Heap value) // Добавляет в кучу новыую переменную с типом Heap, если такой ключ уже имеется, то переписывает значение на новое.

    public int GetInt(string key) // Ищет int с данным ключём, если такого ключа нет то вернёт 0.
    public float GetFloat(string key) // Ищет float с данным ключём, если такого ключа нет то вернёт 0f.
    public string GetString(string key) // Ищет string с данным ключём, если такого ключа нет то вернёт строку "DON'T EXIST".
    public bool GetBool(string key) // Ищет bool с данным ключём, если такого ключа нет то вернёт false.
    public Point GetPoint(string key) // Ищет Point с данным ключём, если такого ключа нет то вернёт Point.Zero.
    public Vector2 GetVector2(string key) // Ищет Vector2 с данным ключём, если такого ключа нет то вернёт Vector2.Zero.
    public Heap GetHeap(string key) // Ищет Heap с данным ключём, если такого ключа нет то вернёт новую пустую кучу.

    public static Heap Open(string path) // Читает данные из файла и возвращает новую кучу с этими данными.
    public void Save(string path) // Пишет в файл содержимое кучи
```
#### Примеры создания кучи:
```
Heap heap = new Heap() // Новая пустая куча.
Heap heap = Heap.Open("test.heap") // Новая куча с данными из файла.
```        
#### Синтаксис файла:
```
h: 480
w: 600
testFloat: 12,12f
testFloatZero: 1f
title: "Test window"
object {
	age: 17
	name: "Vasya Pupkin"
	admin: True
	smerd: False
	pocket {
		range: 20
		pointTest: 0.0
		vectorTest: 12,12f.12f
		e1 {
			w: 1
		}
	}
	e2 {
		e2_asd: 20
		e2_asd2: 20
	}
}
```                   
#### Параметры:
```
<name>: <number> // ( test: 1) - int.
<name>: <number>f // ( test: 1f / test: 1,1f) - float.
<name>: "<string>" // ( test: "1") - string.
<name>: <+/true/True/on/On/-/false/False/off/Off> // ( test: +) - bool.
<name>: <number>.<number> // ( test: 1.1) - Point.
<name>: <number>f.<number>f // ( test: 1f.1f / test: 1,1f.1,1f) - Vector2.
<name> <{> // object {                 
           //} - куча. NB!!! Кучу нужно закрыть при помещи <}> на следующей строке.
```    
#### Примечания:
```
Повторное задание переменной с уже содержащимся ключём (именем) того же типа, что и уже заданная переменная
приведёт к переписыванию этой переменной.
    a: 1
    a: 2
В куче при загрузке из файла будет только одно значение "2" с ключом "a", которое можно будет получить используя .GetInt("a").

Повторное задание переменной другого типа с уже содержащимся ключём (именем) не приведёт к переписыванию этой переменной.
    a: 1
    a: 1,1f
В куче при загрузке из файла будет два значения с ключом "a". Первое "1", которое можно будет получить используя .GetInt("a"),
второе "1,1", которое можно будет получить используя .GetFloat("a").

Повторное задание переменной с уже содержащимся ключём (именем) того же типа, что и уже заданная переменная, но внутри вложенной
кучи не приведёт к переписыванию этой переменной.
a: 1
heap {
    a: 2
    heap {
        a: 3
    } 
}
Значение "1" можно будет получить вызвав .GetInt("a").
Значение "2" можно будет получить вызвав GetHeap("heap").GetInt("a").
Значение "3" можно будет получить вызвав GetHeap("heap").GetHeap("heap").GetInt("a").               
```

### [Entities]
```
[Colliders : ICollider]
    public IGameObject Parent 
    public Vector2 Position

    public Collider(Shape shape, Vector2 position, GraphicsDeviceManager graphics)
    public Collider(Shape shape, Color color, Vector2 position, GraphicsDeviceManager graphics)

    public static Collider Box(GraphicsDeviceManager graphics)
    public static Collider Box(Vector2 position, GraphicsDeviceManager graphics)
    public static Collider Box(int edgeSize, GraphicsDeviceManager graphics)
    public static Collider Box(int edgeSize, Vector2 position, GraphicsDeviceManager graphics)
    public static Collider Box(int edgeSize, Color color, Vector2 position, GraphicsDeviceManager graphics)

[GameObject : IGameObject]    

[GameObjectInfo] // Содержит информацию об игровом объекте, используется для получения инфы при столкновении игровых объектов
    public string Name
    public string Type
    public Vector2 Direction
    public Vector2 Position
    public Vector2 CollisionNormal

    public GameObjectInfo()
    public GameObjectInfo(string name)
    public GameObjectInfo(string name, string type)
    public GameObjectInfo(string name, string type, Vector2 direction)
    public GameObjectInfo(string name, string type, Vector2 direction, Vector2 position)
    public GameObjectInfo(string name, string type, Vector2 direction, Vector2 position, Vector2 normal)

[ICollider : IEntity]
    public void On()
    public void Off()
    public bool GetState()
    public void OnMove(Vector2 position)
    public void OnRotate(float angle)  
    public Shape GetShape() 

[IEntity]
    public void Update(GameTime gameTime);
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch);

[IGameObject : IEntity]  
    public event MoveDelegate MoveEvent
    public event RotateDelegate RotateEvent
    public Vector2 Postion
    public string Type 
    public string Name
    public Vector2 Direction
    public ICollider CheckCollision(IGameObject go)
    public void onCollision(GameObjectInfo GOInfo) 
    public void OnGUI(MouseState mauseState)

[IShape : IEntity]
    public Vector2 GetGlobalVertex(int number);
    public int GetVertiesQty();

[ISprite : IEntity]
    public void AnimationStart()
    public void AnimationStop()
    public void AnimationReset()
    public void On() // Включает отрисовку спрайта
    public void Off() // Выключает отрисовку спрайта
    public bool GetState() // Узнать включён спрайт или нет
    public void OnMove(Vector2 position) // Для подписывание на событие игрового объекта
    public void OnRotate(float angle) // Для подписывание на событие игрового объекта
    public void Resize(float multiplier)

[SConsoleManager]    
    public Texture2D Texture;
    public Rectangle SourceRectangle;
    public Rectangle DrawRectangle;
    
    public SConsoleManager(GraphicsDeviceManager graphics, SpriteFont font)
    public void Update(GameTime gameTime)
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)

[Sprite : ISprite]
    public float Layer // Слой на котором будет отрисовываться
    public IGameObject Parent
    public int FramesQty // Кол-во кадров 
    public int FrameNumber // Номер стартового кадра
        
    public Timer AnimationTimer 
    public Color SpriteColor

    public Sprite(Texture2D texture, Rectangle sourceRectangle, Vector2 position, Point size)
    public Sprite(Texture2D texture, Rectangle sourceRectangle, Vector2 position, Point size, int frameNumber, int framesQty, Timer animationTimer, bool startAnimationInitially)

```
#### Примечание
```
SConsole  можно использовать напрямую, его экземпляр создавать не нужно, так как он static.
Но стоит отметить что для отображения консоли следует вызывать SConsole.Draw(GameTime gameTime, SpriteBatch spriteBatch),
а для работы ввода текста в кносоль - SConsole.Update(GameTime gameTime).

Также можно использовать прослойку ввиде SConsoleManager, чей экземпляр нужно создавать.
Создав экземпляр SConsoleManager, следует вызывать методы Draw и Update у этого экземпляра,
а не у SConsole, так как вызовы соответствующих методов класса SConsole содержаться внутри
экземпляра SConsoleManager.

(Планируется автоматически создавать экземпляр SConsoleManager и вызывать необходимые методы
либо в классе  Scene либо SoloGame)

Также SConsoleManager позволяет включать и выключать отображение консоли по нажатии на клавишу "`"(консоль / тильда),
Включать и отключать ввод с клавиатуры на клавишу "F1",
Добавлять и менять в Config параметры читая последнюю строку консоли на клавишу "Enter".

<имя параметра><пробел><число> - int
<имя параметра><пробел><число>f / <имя параметра><пробел><число>,<число>f - float
<имя параметра><пробел>"<какие-то символы>" - string
<имя параметра><пробел><+|true|True|on|On-|false|False|off|Off"> - bool
```

```
[Shape : IShape]
    public Vector2 Position // Координаты центра фигуры.
    public float X // Координата X центра фигуры.
    public float Y // Координата Y центра фигуры.      
    public float Angle
    public Shape(int x, int y, int w, int z)

    public virtual void Update(GameTime gameTime)
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    public Vector2 GetGlobalVertex(int number) // возвращает глобальные координаты вершины с соответствующим номером.
    public int GetVertiesQty() // Возвращает количество вершин фигуры.
    public void SetColor(Color color) // Установит цвет фигуры.
    public void SetTexture(GraphicsDeviceManager graphics) // сгенирирует текстуру для фигуры, пока текстура не сгенирированна фигура не отрисовывается,
                                                                // но вызывать метод Draw() можно даже если текстура не сгенирированна - ошибки не будет.

[SRectangle : Shape]
    public SRectangle(int x, int y, int w, int z) // x - Координата X, y - Координата Y, w - Ширина, z - Высота.

[SRegularPolygon : Shape]
    public SRegularPolygon(int x, int y, int w, int z) // x - Координата X, y - Координата Y, w - Радиус, z - количество вершин.
```
### [Input]
```
[Chars]
    public static Heap GetDefaulChars() // Стандартная таблица символов
```
#### Таблица символов из GetDefaultChars():
```
"D1", "1"
"D2", "2"
"D3", "3"
"D4", "4"
"D5", "5"
"D6", "6"
"D7", "7"
"D8", "8"
"D9", "9"
"D0", "0"
"OemMinus", "-"
"OemPlus", "+"
"Q", "q"
"W", "w"
"E", "e"
"R", "r"
"T", "t"
"Y", "y"
"U", "u"
"I", "i"
"O", "o"
"P", "p"
"A", "a"
"S", "s"
"D", "d"
"F", "f"
"G", "g"
"H", "h"
"J", "j"
"K", "k"
"L", "l"
"OemSemicolon", ":"
"OemQuotes", "\""
"Z", "z"
"X", "x"
"C", "c"
"V", "v"
"B", "b"
"N", "n"
"M", "m"
"OemComma", ","
"OemPeriod", "."
"OemQuestion", "?"
"Tab", "  "
// "OemTilde", "~"
"Space", " "
"Enter", "\n"
"LeftControl", "^"
"RightControl", "^"
```
```
[ISInput]
    public bool IsPressed(string keyName)
    public bool IsDown(string keyName)
    public void Add(string keyName, Key key)

[Key]
    public Key(Keys key) // Для клавиатуры
    public Key(Buttons button, PlayerIndex index) // Для геймпада
    public SKeyState Listen() // Вернёт SKeyState.Up если кнопка не нажата, SKeyState.Pressed единичное нажатие, SKeyState.Down кнопка зажата

[KeysInput]
    public KeysInput()
    public KeysInput(PlayerIndex index)
    public KeysInput(Dictionary<string, List<Key>> keys)
    public KeysInput(Dictionary<string, List<Key>> keys, PlayerIndex index)
    public SKeyState IsPressed(string keyName)
    public SKeyState IsDown(string keyName)
    public StickDirections GetLeftStickDirections() // 8 позиций левого стика
    public StickDirections GetRightStickDirections() // 8 позиций правого стика    
    public void Add(string keyName, Key key)
```
#### Примечание:
```
KeysInput нужен для того чтобы отлавливать состояние кнопок, в отличии от Microsoft.Xna.Framework.Input
можно получить единичное нажатие кнопки IsPressed. Также KeysInput нужен для того чтобы привязать
кнопки к какому либо ключу, к одному ключу может быть привзяно больше одной кнопки.

Пример:
...
private KeysInput _input = new KeysInput();
_input.Add("console", new Key(Keys.OemTilde));
_input.Add("console", new Key(Keys.F1));
...

...
public void Update(GameTime gameTime)
{
    if (_input.IsPressed("console"))
    {
        if (!SConsole.GetState())
        {
            SConsole.On();
        }
        else
        {
            SConsole.Off();
        }
    }
}
...
```
```
[SKeyState]
    Up = 0,
    Down = 1,
    Pressed = 2

[StickDirections]   
    Undefined = 0,
    Up = 1,
    RightUp = 2,
    Right = 3,
    RightDown = 4,
    Down = 5,
    LeftDown = 6,
    Left = 7,        
    LeftUp = 8

[TextInput]
	public Heap CharTable // Таблица символов
	public TextInput(Heap charTable)
	public string Listen() // Слушает нажатые кнопки и возвращает соответствующий символ согласно таблице. 
			       // Если кнопка не нажата, то возвращает null.
```


### [Physics]
```
[CollisionInformation]
    public static Vector2 GetNormal(Shape s1, Shape s2) /// Возвращает нормаль ближайшей грани s2 к центру s1, если у двух и более граней растояние до центра равно, то суммирует нормали и 
                                                        /// возвращает суму векторов (нормалей) и приводит её (сумму) к единичному виду.

[GJK]
    public static bool CheckCollision(IShape s1, IShape s2) /// Проверяет столкнулись ли два выпуклых объекта или нет. Используя алгоритм GJK
```

### [SConsole] // Нужно для дебага
```
public Heap Configs
public static SpriteFont Font
public static Color FontColor
public static bool isTextInput // разрешён ли текст инпут
public static Vector2 Position // Позиция последней строки.

public static void Update(GameTime gameTime) // NB!!! По умолчанию там TextInput.Listen() для ввода символов в консоль.
public static void Draw(GameTime gameTime, SpriteBatch spriteBatch) // NB!!! Чтобы работали методы на вывод, нужно чтобы этот метод был вызван в основном Draw().

public static void On() // Включить консоль
public static void Off() // Выключить консоль
public static void GetState() // Узнать состояние консоли

public static void WriteLine(string str) // Вывод строки, то есть в конце переданной строки добавит '\n' (перенос строки).
public static void WriteLine(int str) // Вывод строки, то есть в конце переданной строки добавит '\n' (перенос строки).
public static void WriteLine(float str) // Вывод строки, то есть в конце переданной строки добавит '\n' (перенос строки).
public static void WriteLine(bool str) // Вывод строки, то есть в конце переданной строки добавит '\n' (перенос строки).
public static void WriteLine(Object str) // Вывод строки, то есть в конце переданной строки добавит '\n' (перенос строки).

public static void Write(string s) // Вывод без переноса строки
public static void Write(int i)
public static void Write(float f)
public static void Write(bool b)
public static void Write(object o)

public static string ReadLine() // Читает предпоследнюю строку (Length - 2). Последняя строка используется для ввода текста.

public static void Remove(int n) // Удаляет указанное количество символов начиная с конца.
public static void Clear() // Очистить Консоль.
```

### [Timer] 
```
public int Period // Период одного такта в миллисекундах.
public int Count // Счёт тактов. 
public Timer(int period)
public void Start() 
public void Stop()
public void Reset()
public bool Beap(GameTime gameTime) // Один такт таймера
static public Timer GetDefault() // Таймер с периодом в секунду.
```

### [Tools]
```
public static Vector2 VectorToNormal(Vector2 a) // Вернёт нормаль из вектора, подразумевается, что вектор исходит из начала координат.
public static Vector2 EdgeToNormal(Vector2 a, Vector2 b) // Вернёт нормаль из вектора, который зада отрезком. Смещает вектор в начало координат (Vector2 a стоновится равен (0, 0))
                                                         // Дальше вызывает VectorToNormal и возвращает полученный результат.```
public static Vector2 Basis(Vector2 a) // Вернёт единичный вектор (орт), длина которого равна 1. Служит для задания направления.
public static float DegreesToRadians(int angle) // Принимает градусы, возвращает радианы.
public static int RadiansToDegrees(float angle) // Принимает радианы, возвращает градусы.
public static float CalculateAngle(float sum) // Вернёт угол в радианах промежутке между 0 и 6.283. Нужен чтоб при вращении объекта (Angle++) не произошло переполнения.
public static float DistanceBetweenVertices(Vector2 a, Vector2 b) // Вернёт длину отрезка.

public static void DrawLine(Texture2D texture, Color color, Vector2 va, Vector2 vb)  // Рисует линию на текстуре между двумя точками.
public static Texture2D DrawTextureWithNormal(GraphicsDeviceManager graphics, Vector2 vb) // Создаёт текстуру 50 на 50 пикселей на которой рисует единичный вектор. 
                                                                                              // Начало координат находится по центру текстуры (25, 25).  
public static Texture2D MakeSolidColorTexture(GraphicsDeviceManager graphics, Point size, Color color) // Создаёт одноцветную текстуру.
```