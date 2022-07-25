namespace RpgExtreme
{
    public class Program
    {
        static void Main(string[] args)
        {
            var gameManager = new GameManager();
            gameManager.GameInitialize();
            gameManager.Process();
        }
    }

    public class GameManager
    {
        public int Turn { get; set; }
        public World World { get; set; }
        public User User { get; set; }
        public bool IsEnd() => !string.IsNullOrEmpty(EndResult);
        public string EndResult { get; set; }
        private string _userInput;
        public GameManager()
        {
        }

        public void GameInitialize()
        {
            var sizes = Console.ReadLine()?.Trim().Split() ?? new string[2];
            int.TryParse(sizes[0], out var height);
            int.TryParse(sizes[1], out var width);
            var mapStrings = new List<string>();
            User user = null;
            var monsterCount = 1;
            var boxCount = 0;
            for (var i = 0; i < height; i++)
            {
                mapStrings.Add(Console.ReadLine()?.Trim() ?? "");
                monsterCount += mapStrings[i].Count(c => c == '&');
                boxCount += mapStrings[i].Count(c => c == 'B');
                var userIndex = mapStrings[i].IndexOf('@');
                if (userIndex != -1)
                {
                    user = new User(new Point(userIndex, i));
                    var newRow = mapStrings[i].ToCharArray();
                    newRow[userIndex] = '.';
                    mapStrings[i] = new string(newRow);
                }
            }
            var userInputs = Console.ReadLine()?.Trim();
            var monsterDict = new Dictionary<Point, Monster>();
            for (var i = 0; i < monsterCount; i++)
            {
                var monsterInfoStr = Console.ReadLine()?.Trim() ?? "";
                var monster = new Monster(monsterInfoStr);
                monsterDict[monster.CurrentPoint] = monster;
            }
            var itemDict = new Dictionary<Point, ItemInfo>();
            for (var i = 0; i < boxCount; i++)
            {
                var itemInfoStr = Console.ReadLine()?.Trim() ?? "";
                var itemInfo = new ItemInfo(itemInfoStr);
                itemDict[itemInfo.CurrentPoint] = itemInfo;
            }

            World = new World()
            {
                Map = new Map(mapStrings, height, width),
                Monsters = monsterDict,
                ItemInfos = itemDict,
            };
            _userInput = userInputs;
            User = user;

        }

        public void Process()
        {
            var inputArr = _userInput.ToCharArray();
            for (int i = 0; i < inputArr.Length; i++)
            {
                Point direction = Point.ToDirection(inputArr[i]);
                ProcessTurn(direction);
                if (IsEnd())
                    break;
            }

            EndResult ??= "Press any key to continue.";
            ProcessEnd();
        }

        public void ProcessEnd()
        {
            World.Map.Dump(User);
            Console.WriteLine($"Passed Turns : {Turn}");
            User.Dump();
            Console.WriteLine(EndResult);
        }

        public void ProcessTurn(Point direction)
        {
            ++Turn;
            UserMove(direction);
            UserAction();
        }

        public void UserMove(Point direction)
        {
            var userCurrentPoint = User.CurrentPoint;
            var arrivalPoint = new Point(userCurrentPoint.X + direction.X, userCurrentPoint.Y + direction.Y);
            var locationInfo = World.Map.GetLocationInfo(arrivalPoint);
            if (locationInfo != '#')
            {
                User.CurrentPoint = arrivalPoint;
            }
        }

        public void UserAction()
        {
            var userCurrentPoint = User.CurrentPoint;
            var locationInfo = World.Map.GetLocationInfo(userCurrentPoint);
            switch (locationInfo)
            {
                case 'B':
                    BoxAction(userCurrentPoint);
                    break;
                case '^':
                    SpikeAction();
                    break;
                case '&':
                    MonsterAction(userCurrentPoint, false);
                    break;
                case 'M':
                    MonsterAction(userCurrentPoint, true);
                    break;
            }
        }

        private void BoxAction(Point point)
        {
            if (!World.ItemInfos.TryGetValue(point, out var itemInfo))
                return;
            User.AcquireItem(itemInfo);
            World.Map.SetLocationEmpty(point);
        }

        private void SpikeAction()
        {
            User.TakeSpikeDamage();

            if (!User.IsAlive())
            {
                EndResult = "YOU HAVE BEEN KILLED BY SPIKE TRAP..";
            }
        }

        private void MonsterAction(Point point, bool isBoss)
        {
            if (!World.Monsters.TryGetValue(point, out var monster))
                return;

            // fight
            User.StartCombat();
            while (true)
            {
                // user => monster
                monster.TakeDamage(User.GetAttackDamage());
                if (!monster.IsAlive())
                    break;

                // monster => user
                User.TakeDamage(monster.GetAttackDamage(), isBoss);
                if (!User.IsAlive())
                    break;
            }
            User.EndCombat(monster);

            if (!User.IsAlive())
            {
                EndResult = $"YOU HAVE BEEN KILLED BY {monster.Name}..";
            }
            if (!monster.IsAlive() && isBoss)
            {
                EndResult = "YOU WIN!";
            }
            if (!monster.IsAlive())
            {
                World.Map.SetLocationEmpty(point);
            }
        }
    }

    public class World
    {
        public Map Map { get; set; }
        public Dictionary<Point, Monster> Monsters { get; set; }
        public Dictionary<Point, ItemInfo> ItemInfos { get; set; }
    }

    public struct Point
    {
        public int X;
        public int Y;
        
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Point ToDirection(char c)
        {
            switch (c)
            {
                case 'L':
                    return new Point(-1, 0);
                case 'R':
                    return new Point(1, 0);
                case 'U':
                    return new Point(0, -1);
                case 'D':
                    return new Point(0, 1);
                default:
                    return new Point(0, 0);
            }
        }
    }

    public class Map
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string[] Location { get; set; }

        public Map(List<string> locations, int height, int width)
        {
            Location = locations.ToArray();
            Height = height;
            Width = width;
        }

        public char GetLocationInfo(Point point)
        {
            if (point.Y >= Height || point.Y < 0 || point.X >= Width || point.X < 0)
                return '#';
            return Location[point.Y][point.X];
        }

        public void SetLocationEmpty(Point point)
        {
            //var sb = new StringBuilder(Location[point.Y]);
            //sb[point.X] = '.';
            //Location[point.Y] = sb.ToString();

            var newRow = Location[point.Y].ToCharArray();
            newRow[point.X] = '.';
            Location[point.Y] = new string(newRow);
        }

        public void Dump(User user)
        {
            if (user.IsAlive())
            {
                var newRow = Location[user.CurrentPoint.Y].ToCharArray();
                newRow[user.CurrentPoint.X] = '@';
                Location[user.CurrentPoint.Y] = new string(newRow);
            }

            Array.ForEach(Location, row =>
            {
                Console.WriteLine(row);
            });
        }
    }

    public abstract class Creature
    {
        public int MaxHp { get; set; }
        public int HP { get; set; }
        public int ATT { get; set; }
        public int DEF { get; set; }
        public Point CurrentPoint { get; set; }
        public bool IsAlive() => HP > 0;
        public virtual void TakeDamage(int damage, bool isBoss = false)
        {
            damage = Math.Max(1, damage - GetDefence());
            HP -= damage;
        }
        public virtual int GetAttackDamage()
        {
            return ATT;
        }
        public virtual int GetDefence()
        {
            return DEF;
        }
    }

    public class User : Creature
    {
        public int Level { get; set; }
        public int Exp { get; set; }
        public int Weapon { get; set; }
        public int Armor { get; set; }
        public HashSet<AccessoryType> Accessory { get; set; }

        public Point StartPoint { get; set; }
        private bool _isFirstCombat { get; set; }

        public User()
        {

        }

        public User(Point startPoint)
        {
            Level = 1;
            MaxHp = 20;
            HP = 20;
            ATT = 2;
            DEF = 2;
            Accessory = new HashSet<AccessoryType>();
            StartPoint = startPoint;
            CurrentPoint = startPoint;
        }

        public void Dump()
        {
            Console.WriteLine($"LV : {Level}");
            Console.WriteLine($"HP : {(HP < 0 ? 0 : HP)}/{MaxHp}");
            Console.WriteLine($"ATT : {ATT}+{Weapon}");
            Console.WriteLine($"DEF : {DEF}+{Armor}");
            Console.WriteLine($"EXP : {Exp}/{Level * 5}");
        }

        public void AcquireItem(ItemInfo itemInfo)
        {
            switch (itemInfo.ItemType)
            {
                case ItemType.Weapon:
                    Weapon = itemInfo.Value;
                    break;
                case ItemType.Armor:
                    Armor = itemInfo.Value;
                    break;
                case ItemType.Accessory:
                    if (Accessory.Count >= 4)
                        break;
                    Accessory.Add(itemInfo.AccessoryType);
                    break;
            }
        }

        public override int GetAttackDamage()
        {
            var attackDamage = ATT + Weapon;

            if (_isFirstCombat)
            {
                if (Accessory.Contains(AccessoryType.CO) && Accessory.Contains(AccessoryType.DX))
                    return attackDamage * 3;

                if (Accessory.Contains(AccessoryType.CO))
                    return attackDamage * 2;
            }

            return attackDamage;
        }

        public override int GetDefence() => DEF + Armor;

        public void TakeSpikeDamage()
        {
            if (HasAccessory(AccessoryType.DX))
            {
                HP -= 1;
            }
            else
            {
                HP -= 5;
            }

            if (!IsAlive() && HasAccessory(AccessoryType.RE))
            {
                Accessory.Remove(AccessoryType.RE);
                HP = MaxHp;
                CurrentPoint = StartPoint;
            }
        }

        public override void TakeDamage(int damage, bool isBoss = false)
        {
            if (_isFirstCombat && isBoss && HasAccessory(AccessoryType.HU))
            {
                HP = MaxHp;
            }
            else
            {
                damage = Math.Max(1, damage - GetDefence());
                HP -= damage;
            }

            _isFirstCombat = false;
        }

        public void Heal(int amount)
        {
            HP = Math.Min(MaxHp, HP + amount);
        }

        public void StartCombat()
        {
            _isFirstCombat = true;
        }

        public void EndCombat(Monster monster)
        {
            if (!IsAlive() && HasAccessory(AccessoryType.RE))
            {
                Accessory.Remove(AccessoryType.RE);
                HP = MaxHp;
                CurrentPoint = StartPoint;
                monster.HP = monster.MaxHp;
            }
            else if (IsAlive())
            {
                AddExp(monster.RewardExp);
                if (HasAccessory(AccessoryType.HR))
                {
                    Heal(3);
                }
            }
        }

        private void AddExp(int exp)
        {
            var maxExp = Level * 5;
            if (HasAccessory(AccessoryType.EX))
            {
                exp = exp * 6 / 5;
            }

            Exp += exp;
            if (Exp >= maxExp)
            {
                ProcessLevelUp();
            }
        }

        public bool HasAccessory(AccessoryType accessoryType)
        {
            return Accessory.Contains(accessoryType);
        }

        private void ProcessLevelUp()
        {
            Level += 1;
            Exp = 0;
            MaxHp += 5;
            ATT += 2;
            DEF += 2;
            HP = MaxHp;
        }
    }

    public class Monster : Creature
    {
        public string? Name { get; set; }
        public int RewardExp { get; set; }

        public Monster()
        {

        }

        public Monster(string monsterInfoStr)
        {
            var input = monsterInfoStr.Split();
            int.TryParse(input[0], out var y);
            int.TryParse(input[1], out var x);
            int.TryParse(input[3], out var att);
            int.TryParse(input[4], out var def);
            int.TryParse(input[5], out var hp);
            int.TryParse(input[6], out var xp);
            var point = new Point(x - 1, y - 1);
            CurrentPoint = point;
            Name = input[2];
            ATT = att;
            DEF = def;
            HP = hp;
            MaxHp = hp;
            RewardExp = xp;
        }
    }

    public enum ItemType
    {
        Weapon, Armor, Accessory,
    }

    public enum AccessoryType
    {
        HR, RE, CO, EX, DX, HU, CU,
    }

    public class ItemInfo
    {
        public ItemType ItemType { get; set; }
        public AccessoryType AccessoryType { get; set; }
        public int Value { get; set; }
        public Point CurrentPoint { get; set; }

        public ItemInfo()
        {

        }

        public ItemInfo(string itemInfoStr)
        {
            var input = itemInfoStr.Split();
            int.TryParse(input[0], out var y);
            int.TryParse(input[1], out var x);
            CurrentPoint = new Point(x - 1, y - 1);
            int value;
            switch (input[2])
            {
                case "W":
                    ItemType = ItemType.Weapon;
                    int.TryParse(input[3], out value);
                    Value = value;
                    break;
                case "A":
                    ItemType = ItemType.Armor;
                    int.TryParse(input[3], out value);
                    Value = value;
                    break;
                case "O":
                    ItemType = ItemType.Accessory;
                    Enum.TryParse(typeof(AccessoryType), input[3], out var accessoryType);
                    AccessoryType = (AccessoryType)(accessoryType ?? AccessoryType.HR);
                    break;
            }
        }
    }
}