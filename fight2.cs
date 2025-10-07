using System;
using System.Collections.Generic;
class Program
{
    //Check Exit function
    static void CheckExit(string input)
    {
        if (input != null && (input.ToLower() == "exit" || input.ToLower() == "q"))
        {
            Console.WriteLine("Exiting the game. Goodbye!");
            Environment.Exit(0);
        }
    }

    static Random rand = new Random();

    enum CharacterType { Warrior, Archer, Mage, SWAT };
    enum WeaponType { Assassins_Blade, Axe, Sword_Of_Justice, Sling, Bow, Crossbow, Wand, Magic_Staff, Book_Of_Death, AK_47, M4_A1, HK_MP5 };
    enum ArmorType { Shield, Helmet, Hunter_Armor, Chain_Armor, Mantel_Of_Fracture, Moonwhisper_Robe, SWAT_Armor };
    enum EnemyName { Lord, Bandit, Solder, Scelet };

    static double GetWeaponDamage(WeaponType weapon)
    {
        switch (weapon)
        {
            // Warrior weapons
            case WeaponType.Assassins_Blade:
                return 25.00;
            case WeaponType.Axe:
                return 23.60;
            case WeaponType.Sword_Of_Justice:
                return 24.30;

            // Arcer weapons
            case WeaponType.Sling:
                return 18.40;
            case WeaponType.Bow:
                return 20.30;
            case WeaponType.Crossbow:
                return 27.50;

            // Mage weapons
            case WeaponType.Wand:
                return 25.40;
            case WeaponType.Magic_Staff:
                return 27.50;
            case WeaponType.Book_Of_Death:
                return 35.00;

            // SWAT weapons
            case WeaponType.AK_47:
                return 87.00;
            case WeaponType.M4_A1:
                return 87.00;
            case WeaponType.HK_MP5:
                return 87.00;
            default:
                return 10.00;
        }
    }
    // Class for Attack types
    class Attack
    {
        public string Name;
        public double DamageMultiplier;
        public int ManaCost;
        public string Description;

        public Attack(string name, double damageMultiplier, int manaCost, string description)
        {
            Name = name;
            DamageMultiplier = damageMultiplier;
            ManaCost = manaCost;
            Description = description;
        }
    }

    static List<Attack> GetWeaponAttacks(WeaponType weapon)
    {
        switch (weapon)
        {
            // Warriors Weapons
            case WeaponType.Assassins_Blade:
                return new List<Attack>
            {
                new Attack("Quick Stab", 0.8, 0, "Fast but weak attack"),
                new Attack("Deep Cut", 1.5, 0, "Slower but deadly attack")
            };
            case WeaponType.Axe:
                return new List<Attack>
            {
                new Attack("Light Attack", 1.0, 0, "Regular swing"),
                new Attack("Heavy Attack", 2.0, 0, "Powerful crushing blow")
            };
            case WeaponType.Sword_Of_Justice:
                return new List<Attack>
            {
                new Attack("Slash", 1.0, 0, "Standard sword slash"),
                new Attack("Divine Strike", 1.8, 0, "Blessed powerful strike")
            };

            // Archer Weapons
            case WeaponType.Sling:
                return new List<Attack>
            {
                new Attack("Stone Shot", 1.0, 0, "Basic stone shot"),
                new Attack("Double Shot", 1.6, 0, "Throw two stones quickly")
            };
            case WeaponType.Bow:
                return new List<Attack>
            {
                new Attack("Quick Shot", 0.9, 0, "Fast arrow"),
                new Attack("Aimed Shot", 1.8, 0, "Carefully aimed arrow")
            };
            case WeaponType.Crossbow:
                return new List<Attack>
            {
                new Attack("Bolt Shot", 1.2, 0, "Heavy bolt"),
                new Attack("Piercing Bolt", 2.0, 0, "Armor-piercing bolt")
            };

            // Mage Weapons
            case WeaponType.Wand:
                return new List<Attack>
            {
                new Attack("Standart Spell", 1.0, 1, "Standart attack spell"),
                new Attack("Fire Spell", 1.3, 15, "Burning flame"),
                new Attack("Lightning Spell", 1.7, 20, "Electric shock")
            };
            case WeaponType.Magic_Staff:
                return new List<Attack>
            {
                new Attack("Standart Spell", 1.0, 1, "Standart attack spell"),
                new Attack("Fireball", 1.5, 20, "Explosive fire blast"),
                new Attack("Chain Lightning", 1.8, 25, "Lightning that hits multiple times")
            };
            case WeaponType.Book_Of_Death:
                return new List<Attack>
            {
                new Attack("Spel of Cursed", 1.0, 2, "Standart cursed spell"),
                new Attack("Fire of Darkness", 2.0, 30, "Cursed flame from the Abyss"),
                new Attack("Thunder Storm", 2.3, 35, "Devastating Lightning Storm")  // ← ИСПРАВЛЕНО!
            };
            default:
                return new List<Attack>
            {
                new Attack("Basic Attack", 1.0, 0, "Simple attack"),
                new Attack("Strong Attack", 1.2, 0, "Stronger attack")
            };
        }
    }

    // Function to show available attacks and let player choose
    static Attack ChooseAttack(WeaponType weapon, Character attacker)
    {
        List<Attack> availableAttacks = GetWeaponAttacks(weapon);
        Console.WriteLine("\nChoose your attack:");
        for (int i = 0; i < availableAttacks.Count; i++)
        {
            Attack atk = availableAttacks[i];
            string manaInfo = atk.ManaCost > 0 ? $"(Mana: {atk.ManaCost})" : "";
            Console.WriteLine($"({i + 1}) {atk.Name} {manaInfo} - {atk.Description}");
        }
        while (true)
        {
            string input = Console.ReadLine();
            CheckExit(input);

            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= availableAttacks.Count)
            {
                Attack selectedAttack = availableAttacks[choice - 1];

                //Check if Mage has enough mana
                if (selectedAttack.ManaCost > 0 && attacker.isMage)
                {
                    if (attacker.mana >= selectedAttack.ManaCost)
                    {
                        return selectedAttack;
                    }
                    else
                    {
                        Console.WriteLine("You did`t have enough mana");
                        Console.WriteLine("\nPlease choose another attack");
                        continue;
                    }
                }
                return selectedAttack;
            }
            else
            {
                Console.WriteLine($"Please write number between 1 and {availableAttacks.Count}");
            }
        }
    }

    // Function to perform attack
    static void PerformAttack(Character attacker, Attack attack)
    {
        // Calculate Damage
        double totalDamage = attacker.damage * attack.DamageMultiplier;

        //Use Mana if needed
        if (attack.ManaCost > 0 && attacker.isMage)
        {
            attacker.mana -= attack.ManaCost;
        }
        Console.WriteLine($">>>> {attacker.Name} used {attack.Name}!");
        Console.WriteLine($">>>> Damage: {totalDamage:F1}!");
        if (attack.ManaCost > 0)
        {
            Console.WriteLine($"Mana remaining:{attacker.mana}");
        }
    }

    // Function to get Armor DefenseBonus
    static double GetArmorDefense(ArmorType armor)
    {
        switch (armor)
        {
            case ArmorType.Shield:
                return 15;
            case ArmorType.Helmet:
                return 8;
            case ArmorType.Hunter_Armor:
                return 13;
            case ArmorType.Chain_Armor:
                return 16;
            case ArmorType.Mantel_Of_Fracture:
                return 12;
            case ArmorType.Moonwhisper_Robe:
                return 15;
            case ArmorType.SWAT_Armor:
                return 55;
            default:
                return 5;
        }
    }

    //Create Random Enemy
    static Character CreateRandomEnemy()
    {
        Character enemy = new Character();

        //1. Randomm class (not a SWAT)
        CharacterType[] normalClasses = { CharacterType.Warrior, CharacterType.Archer, CharacterType.Mage };
        CharacterType enemyClass = normalClasses[rand.Next(normalClasses.Length)];
        enemy.Hero = enemyClass.ToString();
        EnemyName[] nameClass = { EnemyName.Lord, EnemyName.Bandit, EnemyName.Solder, EnemyName.Scelet };
        EnemyName enemyName = nameClass[rand.Next(nameClass.Length)];
        enemy.isMage = (enemyClass = CharacterType.Mage);

        //2. Base characteristics 
        switch (enemyClass)
        {
            case CharacterType.Warrior:
                enemy.HP = enemy.maxHP = 150;
                enemy.damage = 25;
                enemy.defense = 15;
                break;
            case CharacterType.Archer:
                enemy.HP = enemy.maxHP = 100;
                enemy.damage = 30;
                enemy.defense = 8;
                break;
            case CharacterType.Mage:
                enemy.HP = enemy.maxHP = 150;
                enemy.damage = 20;
                enemy.defense = 5;
                enemy.mana = 100;
                break;
        }

        //3. Random weapon for a class
        List<WeaponType> availableWeapons = GetAvailableWeapons(enemyClass);
        enemy.weapon = availableWeapons[rand.Next(availableWeapons.Count)];

        // + Damage from weapon
        double weaponDamage = GetWeaponDamage(enemy.weapon.Value);
        enemy.damage += weaponDamage;

        //4. Random armor for class
        List<ArmorType> availableArmor = GetAvailableArmor(enemyClass);
        enemy.armor = availableArmor[rand.Next(availableArmor.Count)];
        //+ Defense frome armor
        double armorDefense = GetArmorDefense(enemy.armor.Value);
        enemy.defense += armorDefense;

        return enemy;
    }

    class Character
    {
        public string Hero;
        public string Name;
        public double HP;
        public double maxHP;
        public double damage;
        public double defense;
        public bool isMage;
        public double mana;

        public WeaponType? weapon;
        public ArmorType? armor;

        public void ShowInfo()
        {
            Console.WriteLine($"{Hero}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Hill Points: {HP}");
            if (isMage)
            {
                Console.WriteLine($"Mana:{mana}");
            }
            Console.WriteLine($"Damage: {damage}");
            Console.WriteLine($"Defense: {defense}");
            if (weapon != null)
            {
                Console.WriteLine($"{weapon}");
            }
        }
    }

    // Return of Available Weapons Functon
    static List<WeaponType> GetAvailableWeapons(CharacterType characterType)
    {
        switch (characterType)
        {
            case CharacterType.Warrior:
                return new List<WeaponType>
                {
                    WeaponType.Assassins_Blade,
                    WeaponType.Axe,
                    WeaponType.Sword_Of_Justice
                };

            case CharacterType.Archer:
                return new List<WeaponType>
                {
                    WeaponType.Sling,
                    WeaponType.Bow,
                    WeaponType.Crossbow
                };

            case CharacterType.Mage:
                return new List<WeaponType>
                {
                    WeaponType.Wand,
                    WeaponType.Magic_Staff,
                    WeaponType.Book_Of_Death
                };

            case CharacterType.SWAT:
                return new List<WeaponType>
                {
                    WeaponType.AK_47,
                    WeaponType.M4_A1,
                    WeaponType.HK_MP5
                };

            default:
                return new List<WeaponType>();
        }
    }

    static WeaponType ChooseWeapon(CharacterType character)
    {
        List<WeaponType> availableWeapons = GetAvailableWeapons(character);
        Console.WriteLine("\nChoose your weapon!");
        for (int i = 0; i < availableWeapons.Count; i++)
        {
            Console.WriteLine($"{i + 1} {availableWeapons[i]}");
        }

        while (true)
        {
            string input = Console.ReadLine();
            CheckExit(input);

            if (int.TryParse(input, out int choise) && choise >= 1 && choise <= availableWeapons.Count)
            {
                return availableWeapons[choise - 1];
            }
            else
            {
                Console.WriteLine($"Please whrite number between 1 and {availableWeapons.Count}");
            }
        }
    }

    // Return of Available Armor Functon
    static List<ArmorType> GetAvailableArmor(CharacterType character)
    {
        switch (character)
        {
            case CharacterType.Warrior:
                return new List<ArmorType>
            {
                ArmorType.Shield,
                ArmorType.Helmet,
                ArmorType.Chain_Armor
            };
            case CharacterType.Archer:
                return new List<ArmorType>
                {
                    ArmorType.Hunter_Armor,
                    ArmorType.Helmet,
                    ArmorType.Chain_Armor,
                };

            case CharacterType.Mage:
                return new List<ArmorType>
                {
                    ArmorType.Mantel_Of_Fracture,
                    ArmorType.Moonwhisper_Robe
                };

            case CharacterType.SWAT:
                return new List<ArmorType>
                {
                    ArmorType.SWAT_Armor
                };

            default:
                return new List<ArmorType>();
        }

    }

    static ArmorType ChooseArmor(CharacterType character)
    {
        List<ArmorType> availableArmors = GetAvailableArmor(character);
        Console.WriteLine("\nPlease choose your armor!");
        for (int i = 0; i < availableArmors.Count; i++)
        {
            Console.WriteLine($"{i + 1} {availableArmors[i]}");
        }
        while (true)
        {
            string input = Console.ReadLine();
            CheckExit(input);
            if (int.TryParse(input, out int choise) && choise >= 1 && choise <= availableArmors.Count)
            {
                return availableArmors[choise - 1];
            }
            else
            {
                Console.WriteLine("Please write accepted answer!");
            }
        }
    }

    static void Main()
    {
        Console.WriteLine("\n===Welcome to Mini RPG Fight===");
        Console.WriteLine("(You can write q or exit anytime for quit)");
        Console.WriteLine("\nEnter your name:");
        string nameInput = Console.ReadLine();
        CheckExit(nameInput);
        Console.WriteLine("\nPlease choose your Character:");
        Console.WriteLine("(1) Warrior\t(2) Archer\t(3) Mage");
        CharacterType chosenCharacter = CharacterType.Warrior;
        while (true)
        {
            string characterInput = Console.ReadLine();
            CheckExit(characterInput);
            if (characterInput == "911")
            {
                chosenCharacter = CharacterType.SWAT;
                Console.WriteLine("\nYou get a cheat solder!");
                break;
            }
            else if (characterInput == "1")
            {
                chosenCharacter = CharacterType.Warrior;
                Console.WriteLine("\nNow you`re a Warrior");
                break;
            }
            else if (characterInput == "2")
            {
                chosenCharacter = CharacterType.Archer;
                Console.WriteLine("\nNow you`re a Archer");
                break;
            }
            else if (characterInput == "3")
            {
                chosenCharacter = CharacterType.Mage;
                Console.WriteLine("\nNow you`re a Mage");
                break;
            }
            else
            {
                Console.WriteLine("\nPlease enter 1, 2 or 3");
            }
        }

        Character player = new Character();
        player.Hero = chosenCharacter.ToString();
        player.Name = (nameInput);
        player.isMage = (chosenCharacter == CharacterType.Mage);

        switch (chosenCharacter)
        {
            case CharacterType.Warrior:
                player.HP = player.maxHP = 150;
                player.damage = 25;
                player.defense = 15;
                break;
            case CharacterType.Archer:
                player.HP = player.maxHP = 100;
                player.damage = 30;
                player.defense = 8;
                break;
            case CharacterType.Mage:
                player.HP = 90;
                player.maxHP = 150;
                player.damage = 9;
                player.mana = 100;
                player.defense = 5;
                break;
            case CharacterType.SWAT:
                player.HP = player.maxHP = 250;
                player.damage = 10;
                player.defense = 10;
                break;
        }

        //Choose Weapon
        player.weapon = ChooseWeapon(chosenCharacter);
        double weaponDamage = GetWeaponDamage(player.weapon.Value);
        player.damage += weaponDamage; //Add weapon damage to base damage

        //Choose Armor
        player.armor = ChooseArmor(chosenCharacter);
        double armorDefense = GetArmorDefense(player.armor.Value);
        player.defense += armorDefense;

        //Show final character
        Console.WriteLine("\n✓Character created sucsessfully!");
        player.ShowInfo();

        //Initialize Enemy
        Console.WriteLine(">>>Your Opponent<<<");
        Character enemy = CreateRandomEnemy();
        enemy.ShowInfo();

        Console.WriteLine(">>>Press any key to start the battle!");
        Console.ReadLine();
    }
}