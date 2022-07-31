namespace Baekjoon
{
    public class Program
    {
        static void Main(string[] args)
        {
            var rpgExtreme = new RPGExtreme();
            rpgExtreme.Initialize();
            Console.WriteLine(rpgExtreme.StartPlay());
        }
    }

    public class RPGExtreme
    {
        private Position _start;
        private int _monsterCount;
        private int _itemCount;
        private string _commands;
        private List<string> _gridInfo = new List<string>();
        private List<string> _monsterInfo = new List<string>();
        private List<string> _itemInfo = new List<string>();
        private IEnumerable<Monster> _monsters;
        private IEnumerable<Item> _items;

        public RPGExtreme()
        {
        }

        public void Initialize()
        {
            var gridSize = Console.ReadLine().Split();
            var gridRow = int.Parse(gridSize.First());
            var gridColumn = int.Parse(gridSize.Skip(1).First());

            var monsterCount = 0;
            var itemCount = 0;
            
            for (var i = 0; i < gridRow; i++)
            {
                var grid = Console.ReadLine();
                monsterCount += grid.Count(info => info == '&' || info == 'M');
                itemCount += grid.Count(info => info == 'B');
                
                var start = grid.IndexOf('@');
                if (start > -1)
                {
                    _start = new Position { Row = i, Column = start };
                }
                
                _gridInfo.Add(grid);
            }

            _monsterCount = monsterCount;
            _itemCount = itemCount;

            _commands = Console.ReadLine();

            for (int i = 0; i < monsterCount; i++)
            {
                var monster = Console.ReadLine();
                _monsterInfo.Add(monster);
            }

            for (int i = 0; i < itemCount; i++)
            {
                var item = Console.ReadLine();
                _itemInfo.Add(item);
            }

            Console.WriteLine($"전체 몬스터 수 : {_monsterCount}");
            Console.WriteLine($"전체 아이템 수 : {_itemCount}");

            foreach (var grid in _gridInfo)
                Console.WriteLine($"그리드 정보 : {grid}");

            Console.WriteLine($"명령어 정보 : {_commands}");

            foreach (var monster in _monsterInfo)
                Console.WriteLine($"몬스터 정보 : {monster}");
            foreach (var item in _itemInfo)
                Console.WriteLine($"아이템 정보 : {item}");
        }

        public string StartPlay()
        {
            _monsters = _monsterInfo.Select(info =>
            {
                var splittedInfo = info.Split();
                var row = int.Parse(splittedInfo.ElementAt(0)) - 1;
                var col = int.Parse(splittedInfo.ElementAt(1)) - 1;
                var name = splittedInfo.ElementAt(2);
                var attack = int.Parse(splittedInfo.ElementAt(3));
                var defence = int.Parse(splittedInfo.ElementAt(4));
                var hp = int.Parse(splittedInfo.ElementAt(5));
                var exp = int.Parse(splittedInfo.ElementAt(6));

                if (_gridInfo.ElementAt(row).ElementAt(col) == '&')
                {
                    return new Monster()
                    {
                        Position = new Position { Row = row, Column = col },
                        Name = name,
                        Attack = attack,
                        Defence = defence,
                        MaxHP = hp,
                        CurrentHP = hp,
                        Experience = exp,
                    };
                }
                else
                {
                    return new BossMonster()
                    {
                        Position = new Position { Row = row, Column = col },
                        Name = name,
                        Attack = attack,
                        Defence = defence,
                        MaxHP = hp,
                        CurrentHP = hp,
                        Experience = exp,
                    };
                }
            });
            foreach (var monster in _monsters)
                Console.WriteLine($"monster : Row {monster.Position.Row}, Column {monster.Position.Column}");
            Console.WriteLine();

            _items = _itemInfo.Select(info =>
            {
                var splittedInfo = info.Split();
                var row = int.Parse(splittedInfo.ElementAt(0)) - 1;
                var col = int.Parse(splittedInfo.ElementAt(1)) - 1;
                var type = splittedInfo.ElementAt(2);
                var data = splittedInfo.ElementAt(3);

                if (type == "W")
                {
                    return new Weapon()
                    {
                        Position = new Position { Row = row, Column = col },
                        Attack = int.Parse(data)
                    } as Item;
                }
                else if (type == "A")
                {
                    return new Armor()
                    {
                        Position = new Position { Row = row, Column = col },
                        Defence = int.Parse(data)
                    } as Item;
                }
                else
                {
                    return new Accessory()
                    {
                        Position = new Position { Row = row, Column = col },
                        Name = data
                    } as Item;
                }
            });
            foreach (var item in _items)
                Console.WriteLine($"item : Row {item.Position.Row}, Column {item.Position.Column}");
            Console.WriteLine();

            var hero = new Hero { Position = _start };
            Console.WriteLine($"hero : Row {hero.Position.Row}, Column {hero.Position.Column}");

            return GetGameResult(hero);
        }

        public string GetGameResult(Hero hero)
        {
            var turn = 0;
            var mark = '@';
            
            for (var i = 0; i < _commands.Length; i++)
            {
                turn += 1;
                var command = _commands[i];
                var currentPosition = hero.Position;
                var nextPosition = new Position { Row = currentPosition.Row, Column = currentPosition.Column };
                var isGameEnd = false;
                var killer = string.Empty;

                switch (command)
                {
                    case 'L':
                        nextPosition.Column -= 1;
                        break;
                    case 'R':
                        nextPosition.Column += 1;
                        break;
                    case 'U':
                        nextPosition.Row -= 1;
                        break;
                    case 'D':
                        nextPosition.Row += 1;
                        break;
                    default:
                        throw new Exception();
                }

                if (nextPosition.Row >= _gridInfo.Count()
                    || nextPosition.Row < 0
                    || nextPosition.Column >= _gridInfo[0].Length
                    || nextPosition.Column < 0
                    || _gridInfo.ElementAt(nextPosition.Row).ElementAt(nextPosition.Column) == '#')
                {
                    nextPosition = currentPosition;
                }

                var nextMark = _gridInfo.ElementAt(nextPosition.Row).ElementAt(nextPosition.Column);

                if (nextMark == '^' || (mark == '^'))
                {
                    hero.GetDamageBySpike();
                    isGameEnd = hero.IsGameEnd();
                    if (isGameEnd)
                    {
                        killer = "SPIKE TRAP";
                    }
                    else
                    {
                        nextPosition = _start;
                    }
                }

                else if (nextMark == '&' || nextMark == 'M')
                {
                    var monster = _monsters.First(monster => monster.Position.Row == nextPosition.Row && monster.Position.Column == nextPosition.Column);
                    var result = hero.GetDamageByMonster(monster);

                    if (result == FightResult.Lose)
                    {
                        isGameEnd = hero.IsGameEnd();
                        if (isGameEnd)
                        {
                            killer = monster.Name;
                        }
                        else
                        {
                            nextPosition = _start;
                            monster.CurrentHP = monster.MaxHP;
                        }
                    }
                    else
                    {
                        if (nextMark == 'M')
                        {
                            isGameEnd = true;
                        }
                        _gridInfo[nextPosition.Row] = _gridInfo[nextPosition.Row].Remove(nextPosition.Column, 1).Insert(nextPosition.Column, ".");
                    }
                }

                else if (nextMark == 'B')
                {
                    var item = _items.First(item => item.Position.Row == nextPosition.Row && item.Position.Column == nextPosition.Column);

                    if (item is Weapon)
                    {
                        hero.Weapon = item as Weapon;
                    }
                    else if (item is Armor)
                    {
                        hero.Armor = item as Armor;
                    }
                    else
                    {
                        if (hero.isAbleToEquipAccessory(item as Accessory))
                        {
                            hero.Accessories.Add(item as Accessory);
                        }
                    }
                    _gridInfo[nextPosition.Row] = _gridInfo[nextPosition.Row].Remove(nextPosition.Column, 1).Insert(nextPosition.Column, ".");
                }

                if (string.IsNullOrEmpty(killer))
                {
                    mark = nextMark;
                    if (mark != '^' || nextMark != '^')
                    {
                        _gridInfo[nextPosition.Row] = _gridInfo[nextPosition.Row].Remove(nextPosition.Column, 1).Insert(nextPosition.Column, "@");
                        _gridInfo[currentPosition.Row] = _gridInfo[currentPosition.Row].Replace('@', '.');
                    }
                    hero.Position = nextPosition;
                }

                if (isGameEnd)
                {
                    return string.IsNullOrEmpty(killer) ? ResultPrint(hero, _gridInfo, turn) + " YOU WIN!" : ResultPrint(hero, _gridInfo, turn) + $"YOU HAVE BEEN KILLED BY {killer}..";
                }
            }
            return ResultPrint(hero, _gridInfo, turn) + "Press any key to continue.";
        }

        private string ResultPrint(Hero hero, List<string> gridInfo, int turn)
        {
            return string.Join(Environment.NewLine, gridInfo) + Environment.NewLine +
                $"Passed Turns : {turn}" + Environment.NewLine +
                $"LV : {hero.Level}" + Environment.NewLine +
                $"HP : {hero.CurrentHP}/{hero.MaxHP}" + Environment.NewLine +
                $"ATT : {hero.Attack}+{hero.Weapon?.Attack ?? 0}" + Environment.NewLine +
                $"DEF : {hero.Defence}+{hero.Armor?.Defence ?? 0}" + Environment.NewLine +
                $"EXP : {hero.Experience}/{hero.Level * 5}" + Environment.NewLine;
        }
    }

    public class GameObject
    {
        public Position Position { get; set; }
    }

    public class Monster: GameObject
    {
        public string? Name { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public int Experience { get; set; }
    }
    public class BossMonster : Monster
    {

    }

    public class Hero : GameObject
    {
        public int MaxHP { get; set; } = 20;
        public int CurrentHP { get; set; } = 20;

        public int Attack { get; set; } = 2;
        public int Defence { get; set; } = 2;
        public int Level { get; set; } = 1;
        public int Experience { get; set; } = 0;
        public Weapon Weapon { get; set; }
        public Armor Armor { get; set; }
        public List<Accessory> Accessories { get; set; } = new List<Accessory>();

        public bool HasAccessory(string name)
        {
            return Accessories.Any(accessory => accessory.Name == name);
        }
        public void GetDamageBySpike()
        {
            if (HasAccessory("DX"))
            {
                CurrentHP -= 1;
            }
            else
            {
                CurrentHP -= 5;
            }
        }


        public bool IsGameEnd()
        {
            if (CurrentHP > 0)
            {
                return false;
            }

            if (HasAccessory("RE"))
            {
                CurrentHP = MaxHP;
                var index = Accessories.FindIndex(accessory => accessory.Name == "RE");
                Accessories.RemoveAt(index);
                return false;
            }

            return true;
        }

        public FightResult GetDamageByMonster(Monster monster)
        {
            if (monster is BossMonster && HasAccessory("HU"))
            {
                CurrentHP = MaxHP;
            }

            var isFirstTurn = true;

            while (true)
            {
                monster.CurrentHP -= Math.Max(1, totalAttack(isFirstTurn) - monster.Defence);

                if (monster.CurrentHP <= 0)
                {
                    GetExperience(monster.Experience);
                    
                    if (HasAccessory("HR"))
                    {
                        CurrentHP = Math.Min(MaxHP, CurrentHP + 3);
                    }
                    return FightResult.Win;
                }

                if (monster is BossMonster && isFirstTurn && HasAccessory("HU"))
                {
                    CurrentHP -= 0;
                }
                else
                {
                    CurrentHP -= Math.Max(1, monster.Attack - GetDefence());
                }
                if (CurrentHP <= 0)
                {
                    return FightResult.Lose;
                }
                isFirstTurn= false;
            }            
        }

        private int GetDefence()
        {
            var armorDefence = Armor?.Defence ?? 0;

            return Defence + armorDefence;
        }

        private void GetExperience(int experience)
        {
            if (HasAccessory("EX"))
            {
                Experience += (int)(experience * 1.2);
            }
            else
            {
                Experience += experience;
            }

            if (Level * 5 <= Experience)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level++;
            Experience = 0;
            MaxHP += 5;
            CurrentHP = MaxHP;

            Attack += 2;
            Defence += 2;
        }

        public int totalAttack(bool isFirstTurn)
        {
            var weaponAttack = Weapon?.Attack ?? 0;
            var attackDamage = Attack + weaponAttack;

            return isFirstTurn ? attackDamage * GetAccessoriesAttackEffect() : attackDamage;
        }

        private int GetAccessoriesAttackEffect()
        {
            if (HasAccessory("CO"))
            {
                if (HasAccessory("DX"))
                {
                    return 3;
                }
                return 2;
            }
            return 1;
        }

        internal bool isAbleToEquipAccessory(Accessory? accessory)
        {
            return Accessories.Count() < 4 && !Accessories.Any(x => x.Name == accessory.Name);
        }
    }

    public enum FightResult
    {
        Win,
        Lose
    }

    public class Spike: GameObject
    {

    }

    public class Item: GameObject
    {

    }

    public class Weapon: Item
    {
        public int Attack { get; set; }
    }

    public class Armor: Item
    {
        public int Defence { get; set; }
    }
    
    public class Accessory: Item
    {
        public string Name { get; set;}
    }

    public class Position
    {
        public int Row { get; internal set; }
        public int Column { get; internal set; }
    }

}
