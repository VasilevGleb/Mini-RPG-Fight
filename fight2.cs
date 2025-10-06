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
    enum WeaponType { AssassinsBlade, Axe, SwordOfJustice, Sling, Bow, Crossbow, Wand, MagicStaff, BookOfDeath, AK47, M4A1, HKMP5 };
    enum ArmorType { Shield, Helmet, HunterArmor, ChainArmor, MantelOfFracture, MoonwhisperRobe, SWATArmor };
    enum EnemyName { };

    static double GetWeaponDamage(WeaponType weapon)
    {
        switch (weapon)
        {
            // Warrior weapons
            case WeaponType.AssassinsBlade:
                return 25.00;
            case WeaponType.Axe:
                return 23.60;
            case WeaponType.SwordOfJustice:
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
            case WeaponType.MagicStaff:
                return 27.50;
            case WeaponType.BookOfDeath:
                return 35.00;

            // SWAT weapons
            case WeaponType.AK47:
                return 87.00;
            case WeaponType.M4A1:
                return 87.00;
            case WeaponType.HKMP5:
                return 87.00;
            default:
                return 10.00;
        }
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
                    WeaponType.AssassinsBlade,
                    WeaponType.Axe,
                    WeaponType.SwordOfJustice
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
                    WeaponType.MagicStaff,
                    WeaponType.BookOfDeath
                };

            case CharacterType.SWAT:
                return new List<WeaponType>
                {
                    WeaponType.AK47,
                    WeaponType.M4A1,
                    WeaponType.HKMP5
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
                ArmorType.ChainArmor
            };
            case CharacterType.Archer:
                return new List<ArmorType>
                {
                    ArmorType.HunterArmor,
                    ArmorType.Helmet,
                    ArmorType.ChainArmor,
                };

            case CharacterType.Mage:
                return new List<ArmorType>
                {
                    ArmorType.MantelOfFracture,
                    ArmorType.MoonwhisperRobe
                };

            case CharacterType.SWAT:
                return new List<ArmorType>
                {
                    ArmorType.SWATArmor
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
                Console.WriteLine("You get a cheat solder!");
                break;
            }
            else if (characterInput == "1")
            {
                chosenCharacter = CharacterType.Warrior;
                break;
            }
            else if (characterInput == "2")
            {
                chosenCharacter = CharacterType.Archer;
                break;
            }
            else if (characterInput == "3")
            {
                chosenCharacter = CharacterType.Mage;
                break;
            }
            else
            {
                Console.WriteLine("Please enter 1, 2 or 3");
            }
        }

        Character player = new Character();
        player.Hero = chosenCharacter.ToString();
        player.Name = (nameInput);
        player.isMage = (chosenCharacter == CharacterType.Mage);


    }
}