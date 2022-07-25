using RPG_Extreme;

var rpg = new RPGExtremeProblem();

Console.Write(rpg.GetOutput());

namespace RPG_Extreme
{
    public class RPGExtremeProblem : BaekJoonProblem
    {
        private int _monsterCount;
        private int _itemCount;
        private Position _initialPosition;

        public RPGExtremeProblem() : base()
        {
        }

        public RPGExtremeProblem(List<string> input) : base(input)
        {
            var rowCount = int.Parse(input.First().Split().First());

            var monsterCount = 0;
            var itemCount = 0;

            for (int i = 1; i < rowCount + 1; i++)
            {
                var row = input.ElementAt(i);

                monsterCount += row.Count(chr => chr == '&' || chr == 'M');
                itemCount += row.Count(chr => chr == 'B');

                var characterColPosition = row.IndexOf('@');
                if (characterColPosition > -1)
                {
                    _initialPosition = new Position { Row = i - 1, Col = characterColPosition };
                }
            }

            _monsterCount = monsterCount;
            _itemCount = itemCount;
        }

        protected override List<string> ReadInput()
        {
            var inputs = new List<string>();
            var rowCol = Console.ReadLine();

            inputs.Add(rowCol);

            var rowCount = int.Parse(rowCol.Split().First());

            var monsterCount = 0;
            var itemCount = 0;

            for (int i = 0; i < rowCount; i++)
            {
                var row = Console.ReadLine();

                monsterCount += row.Count(chr => chr == '&' || chr == 'M');
                itemCount += row.Count(chr => chr == 'B');

                var characterColPosition = row.IndexOf('@');
                if (characterColPosition > -1)
                {
                    _initialPosition = new Position { Row = i, Col = characterColPosition };
                }

                inputs.Add(row);
            }

            _monsterCount = monsterCount;
            _itemCount = itemCount;

            inputs.Add(Console.ReadLine());

            for (int i = 0; i < monsterCount; i++)
            {
                inputs.Add(Console.ReadLine());
            }

            for (int i = 0; i < itemCount; i++)
            {
                inputs.Add(Console.ReadLine());
            }

            return inputs;
        }

        protected override string Solution()
        {
            var rowCol = Input.First().Split();

            var rowCount = int.Parse(rowCol.First());
            var colCount = int.Parse(rowCol.Skip(1).First());

            var grid = Input.Skip(1).Take(rowCount).ToList();
            var commands = Input.Skip(1 + rowCount).First();

            var monsterInfomations = Input.Skip(1 + rowCount + 1).Take(_monsterCount);
            var monsters = monsterInfomations.Select(monsterInfomation =>
            {
                var splittedInfomation = monsterInfomation.Split();

                var monsterRow = int.Parse(splittedInfomation.ElementAt(0)) - 1;
                var monsterCol = int.Parse(splittedInfomation.ElementAt(1)) - 1;

                var monsterName = splittedInfomation.ElementAt(2);
                var monsterAttack = int.Parse(splittedInfomation.ElementAt(3));
                var monsterDefense = int.Parse(splittedInfomation.ElementAt(4));
                var monsterHP = int.Parse(splittedInfomation.ElementAt(5));
                var monsterExperience = int.Parse(splittedInfomation.ElementAt(6));

                if (grid.ElementAt(monsterRow).ElementAt(monsterCol) == '&')
                {
                    return new Monster()
                    {
                        Position = new Position { Row = monsterRow, Col = monsterCol },
                        Name = monsterName,
                        Attack = monsterAttack,
                        Defence = monsterDefense,
                        MaxHP = monsterHP,
                        HP = monsterHP,
                        Experience = monsterExperience,
                    };
                }
                else
                {
                    return new BossMonster()
                    {
                        Position = new Position { Row = monsterRow, Col = monsterCol },
                        Name = monsterName,
                        Attack = monsterAttack,
                        Defence = monsterDefense,
                        MaxHP = monsterHP,
                        HP = monsterHP,
                        Experience = monsterExperience,
                    };
                }
            });

            var itemInfomations = Input.Skip(1 + rowCount + 1 + _monsterCount).Take(_itemCount);
            var items = itemInfomations.Select(itemInfomation =>
            {
                var splittedInfomation = itemInfomation.Split();

                var itemRow = int.Parse(splittedInfomation.ElementAt(0)) - 1;
                var itemCol = int.Parse(splittedInfomation.ElementAt(1)) - 1; 
                var itemType = splittedInfomation.ElementAt(2);
                var itemMetadata = splittedInfomation.ElementAt(3);
                
                if (itemType == "W")
                {
                    return new Weapon
                    {
                        Position = new Position { Row = itemRow, Col = itemCol },
                        Attack = int.Parse(itemMetadata),
                    } as Item;
                }
                else if (itemType == "A")
                {
                    return new Armor
                    {
                        Position = new Position { Row = itemRow, Col = itemCol },
                        Defence = int.Parse(itemMetadata),
                    } as Item;
                }
                else
                {
                    return new Accessory 
                    { 
                        Position = new Position { Row = itemRow, Col = itemCol },
                        Name = itemMetadata, 
                    } as Item;
                }
            });

            var character = new Character
            {
                CurrentPosition = _initialPosition,
                InitialPosition = _initialPosition,
            };

            var turn = 0;
            var prevSymbol = '.';

            for (var i = 0; i < commands.Length; i++)
            {
                var command = commands[i];

                turn++;

                var currentPosition = character.CurrentPosition;
                var nextPosition = new Position { Row = character.CurrentPosition.Row, Col = character.CurrentPosition.Col };
                var isGameEnd = false;
                var killer = string.Empty;

                switch (command)
                {
                    case 'L':
                        nextPosition.Col -= 1;
                        break;
                    case 'R':
                        nextPosition.Col += 1;
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

                if (nextPosition.Row >= rowCount
                    || nextPosition.Row < 0
                    || nextPosition.Col >= colCount
                    || nextPosition.Col < 0
                    || grid.ElementAt(nextPosition.Row).ElementAt(nextPosition.Col) == '#')
                {
                    nextPosition = currentPosition;
                }

                var nextSymbol = grid.ElementAt(nextPosition.Row).ElementAt(nextPosition.Col); 
                if (nextSymbol == '^' || (nextSymbol == '@' && prevSymbol == '^'))
                {
                    var revived = false;

                    character.DamagedBySpikeTrap();
                    (isGameEnd, revived) = character.CheckGameIsEnd();

                    if (isGameEnd)
                    {
                        killer = "SPIKE TRAP";
                    }
                    else
                    {
                        if (revived)
                        {
                            nextPosition = character.InitialPosition;
                        }
                    }
                }
                else if (nextSymbol == '&')
                {
                    var monster = monsters.First(x => x.Position.Row == nextPosition.Row && x.Position.Col == nextPosition.Col);
                    var result = character.FightWith(monster);

                    if (result == FightResult.Lose)
                    {
                        (isGameEnd, _) = character.CheckGameIsEnd(monster);

                        if (isGameEnd)
                        {
                            killer = monster.Name;
                        }
                        else
                        {
                            nextPosition = character.CurrentPosition;
                        }
                    }
                    else
                    {
                        grid[nextPosition.Row] = grid[nextPosition.Row].Remove(nextPosition.Col, 1).Insert(nextPosition.Col, ".");
                    }
                }
                else if (nextSymbol == 'M')
                {
                    var monster = monsters.First(x => x.Position.Row == nextPosition.Row && x.Position.Col == nextPosition.Col);
                    var result = character.FightWith(monster);

                    if (result == FightResult.Lose)
                    {
                        (isGameEnd, _) = character.CheckGameIsEnd(monster);

                        if (isGameEnd)
                        {
                            killer = monster.Name;
                        }
                        else
                        {
                            nextPosition = character.CurrentPosition;
                        }
                    }
                    else
                    {
                        grid[nextPosition.Row] = grid[nextPosition.Row].Remove(nextPosition.Col, 1).Insert(nextPosition.Col, ".");

                        isGameEnd = true;
                    }
                }
                else if (nextSymbol == 'B')
                {
                    var item = items.First(x => x.Position.Row == nextPosition.Row && x.Position.Col == nextPosition.Col);

                    if (item is Weapon)
                    {
                        character.Weapon = item as Weapon;
                    }
                    else if (item is Armor)
                    {
                        character.Armor = item as Armor;
                    }
                    else
                    {
                        if (character.isAbleToEquipAccessory(item as Accessory))
                        {
                            character.Accessories.Add(item as Accessory);
                        }
                    }

                    grid[nextPosition.Row] = grid[nextPosition.Row].Remove(nextPosition.Col, 1).Insert(nextPosition.Col, ".");
                }

                grid[currentPosition.Row] = grid[currentPosition.Row].Replace('@', prevSymbol);

                if (string.IsNullOrEmpty(killer))
                {
                    prevSymbol = grid[nextPosition.Row][nextPosition.Col];
                    grid[nextPosition.Row] = grid[nextPosition.Row].Remove(nextPosition.Col, 1).Insert(nextPosition.Col, "@");
                    character.CurrentPosition = nextPosition;
                }

                if (isGameEnd)
                {
                    return string.IsNullOrEmpty(killer) ? GetResult(character, grid, turn) + "YOU WIN!" : GetResult(character, grid, turn) + $"YOU HAVE BEEN KILLED BY {killer}..";
                }
            }

            return GetResult(character, grid, turn) + "Press any key to continue.";
        }

        private string GetResult(Character character, List<string> grid, int turn)
        {
            var remainingHP = character.CurrentHP < 0 ? 0 : character.CurrentHP;

            return string.Join(Environment.NewLine, grid) + Environment.NewLine +
                $"Passed Turns : {turn}" + Environment.NewLine +
                $"LV : {character.Level}" + Environment.NewLine +
                $"HP : {remainingHP}/{character.MaxHP}" + Environment.NewLine +
                $"ATT : {character.BaseAttack}+{character.Weapon?.Attack ?? 0}" + Environment.NewLine +
                $"DEF : {character.BaseDefence}+{character.Armor?.Defence ?? 0}" + Environment.NewLine +
                $"EXP : {character.Experience}/{character.Level * 5}" + Environment.NewLine;
        }

        public class Item
        {
            public Position Position { get; set; }
        }

        public class Weapon : Item
        {
            public int Attack { get; set; }
        }

        public class Armor : Item
        {
            public int Defence { get; set; }
        }

        public class Accessory : Item
        {
            public string Name { get; set; }
        }
 
        public enum FightResult
        {
            Win,
            Lose,
        }

        public class Character
        {
            private const int _maxAccessoriesCount = 4;

            public int BaseAttack { get; set; } = 2;
            public int BaseDefence { get; set; } = 2;

            public Position CurrentPosition { get; set; }
            public Position InitialPosition { get; set; }

            public int Level { get; set; } = 1;

            public int MaxHP { get; set; } = 20;
            public int CurrentHP { get; set; } = 20;
            
            public (bool IsEnd, bool Revived) CheckGameIsEnd(Monster killer = null)
            {
                if (CurrentHP > 0)
                {
                    return (false, false);
                }

                if (HasAccessory("RE"))
                {
                    CurrentHP = MaxHP;
                    CurrentPosition = InitialPosition;

                    var index = Accessories.FindIndex(x => x.Name == "RE");
                    Accessories.RemoveAt(index);

                    if (killer is not null)
                    {
                        killer.HP = killer.MaxHP;
                    }

                    return (false, true);
                }
                else
                {
                    return (true, false);
                }
            }

            public FightResult FightWith(Monster monster)
            {
                if (monster is BossMonster && HasAccessory("HU"))
                {
                    CurrentHP = MaxHP;
                }

                var isFirstTurn = true;

                while (CurrentHP > 0 && monster.HP > 0)
                {
                    monster.HP -= Math.Max(1, GetAttackDamage(isFirstTurn) - monster.Defence);

                    if (monster.HP <= 0)
                    {
                        GetExperience(monster.Experience);

                        if (HasAccessory("HR"))
                        {
                            CurrentHP = Math.Min(MaxHP, CurrentHP + 3);
                        }

                        return FightResult.Win;
                    }

                    DamagedByMonster(monster, isFirstTurn);

                    if (CurrentHP <= 0)
                    {
                        return FightResult.Lose;
                    }

                    isFirstTurn = false;
                }

                throw new Exception();
            }

            private void DamagedByMonster(Monster monster, bool isFirstTurn)
            {
                if (monster is BossMonster && isFirstTurn && HasAccessory("HU"))
                {
                    CurrentHP -= 0;
                }
                else
                {
                    CurrentHP -= Math.Max(1, monster.Attack - GetDefence());
                }
            }

            public void DamagedBySpikeTrap()
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

            public int GetAttackDamage(bool isFirstAttack = false)
            {
                var weaponAttack = Weapon?.Attack ?? 0;
                var attackDamage = BaseAttack + weaponAttack;

                return isFirstAttack ? attackDamage * GetAccessoriesAttackEffect() : attackDamage;
            }

            public int GetAccessoriesAttackEffect()
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

            private bool HasAccessory(string name)
            {
                return Accessories.Any(x => x.Name == name);
            }

            public int GetDefence()
            {
                var armorDefence = Armor?.Defence ?? 0;

                return BaseDefence + armorDefence;
            }

            public int Experience { get; set; } = 0;

            public Weapon Weapon { get; set; }
            public Armor Armor { get; set; }
            public List<Accessory> Accessories { get; set; } = new List<Accessory>();

            public void GetExperience(int experience)
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

            public void LevelUp()
            {
                Level++;
                Experience = 0;
                MaxHP += 5;
                CurrentHP = MaxHP;

                BaseAttack += 2;
                BaseDefence += 2;
            }

            public bool isAbleToEquipAccessory(Accessory accessory)
            {
                return Accessories.Count() < _maxAccessoriesCount && !Accessories.Any(x => x.Name == accessory.Name);
            }
        }

        public class Position
        {
            public int Row { get; set; }
            public int Col { get; set; }
        }

        public class Monster
        {
            public Position Position { get; set; }
            public string Name { get; set; }
            public int Attack { get; set; }
            public int Defence { get; set; }
            public int MaxHP { get; set; }
            public int HP { get; set; }
            public int Experience { get; set; }
        }

        public class BossMonster : Monster
        {
        }
    }
}

namespace RPG_Extreme
{
    public abstract class BaekJoonProblem : Problem<List<string>, string>
    {
        protected BaekJoonProblem() : base()
        {
        }

        protected BaekJoonProblem(List<string> input) : base(input)
        {
        }
    }
}
namespace RPG_Extreme
{
    public abstract class Problem<TInput, TOutput>
    {
        protected TInput? Input { get; init; }

        protected Problem()
        {
            Input = ReadInput();
        }

        protected Problem(TInput input)
        {
            Input = input;
        }

        public TOutput GetOutput()
        {
            return Solution();
        }

        protected abstract TInput ReadInput();
        protected abstract TOutput Solution();
    }
}