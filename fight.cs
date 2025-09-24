using System;
class Program
{
    static void CheckExit(string input)
    {
        if (input != null && (input.ToLower() == "exit" || input.ToLower() == "выход" || input.ToLower() == "q"))
        {
            Console.WriteLine("Exiting the game. Goodbye!");
            Environment.Exit(0);
        }
    }
    static Random rand = new Random();
    enum WeaponType {Sword, Spear, Axe, Mage};
    enum ArmorType {Shield, LeatherArmor, DefensifeRing, LeatherArmorPro, CheaterArmor};
    enum EnemyName {Bandit, Warrior, Lord};
    class Character
    {
        public string Name;
        public double Health = 100;        
        public double HealthMax;
        public int DamageMin;
        public int DamageMax;
        public double DefenseMax;
        public double DefenseMin;
        public WeaponType Weapon;
        public ArmorType Armor;
    }

    static void Main()
    {
        Console.WriteLine("Welcome to the Mini RPG Fight!");
        
        Character player = new Character();
        Character enemy = new Character();
        
        // Initialize player
        Console.WriteLine("Enter your character's name:");
        string nameInput = Console.ReadLine();
        CheckExit(nameInput);
        player.Name = nameInput;

        Console.WriteLine("Choose your weapon: (1)Sword, (2)Spear, (2)Axe");
        string weaponInput = Console.ReadLine();
        CheckExit(weaponInput);
        if (weaponInput == "123")
        {
            Console.WriteLine("Fucking cheater! You get the Mage weapon!");
            player.Weapon = WeaponType.Mage;
            player.Health = (double)(player.Health * 2);
            player.HealthMax = player.Health;
            player.DamageMin = 60;
            player.DamageMax = 100; 
        }
        else
        {
            int weaponChoice = int.Parse(Console.ReadLine());
            switch (weaponChoice)
            {
                case 1:
                    player.Weapon = WeaponType.Sword;
                    player.HealthMax = 100;
                    player.DamageMin = 20;
                    player.DamageMax = 40;
                    break;
                case 2:
                    player.Weapon = WeaponType.Spear;
                    player.HealthMax = 100;
                    player.DamageMin = 25;
                    player.DamageMax = 35;
                    break;
                case 3:
                    player.Weapon = WeaponType.Axe;
                    player.HealthMax = 100;
                    player.DamageMin = 15;
                    player.DamageMax = 55;
                    break;
                /*default:
                    Console.WriteLine("Invalid choice, defaulting to Sword.");
                    player.Weapon = WeaponType.Sword;
                    player.HealthMax = 100;
                    player.DamageMin = 20;
                    player.DamageMax = 40;
                    break;*/
            }
        }
        Console.WriteLine("Choose your armor: (1)Shield, (2)Leather Armor, (3)Defensive Ring");
        string armorInput = Console.ReadLine();
        CheckExit(armorInput);
        if (armorInput=="456")
        {
            Console.WriteLine("Nice, you found the Cheater Armor!");
            player.Armor = ArmorType.CheaterArmor;
            player.DefenseMin = 50;
            player.DefenseMax = 70;
        }
        else
        {
            int armorChoice = int.Parse(Console.ReadLine());
            switch (armorChoice)
            {
                case 1:
                if (player.Weapon == WeaponType.Spear)
                {
                    Console.WriteLine("Spear and Shield are incompatible! You get the Leather Armor-Pro instead.");
                    player.Armor = ArmorType.LeatherArmorPro;
                    player.DefenseMin = 25;
                    player.DefenseMax = 30;
                    break;
                }
                    player.Armor = ArmorType.Shield;
                    player.DefenseMin = 10;
                    player.DefenseMax = 30;
                    break;
                case 2:
                    player.Armor = ArmorType.LeatherArmor;
                    player.DefenseMin = 20;
                    player.DefenseMax = 25;
                    break;
                case 3:
                    player.Armor = ArmorType.DefensifeRing;
                    player.DefenseMin = 15;
                    player.DefenseMax = 35;
                    break;
                /*default:
                    Console.WriteLine("Invalid choice, defaulting to Shield.");
                    player.Armor = ArmorType.Shield;
                    player.DefenseMin = 10;
                    player.DefenseMax = 30;
                    break;*/
            }
        }

        enemy.Name = ((EnemyName)rand.Next(0,3)).ToString();
        enemy.weapon = (WeaponType)rand.Next(0,3);
        switch (enemy.weapon)
        {
            case WeaponType.Sword:
                enemy.HealthMax = 100;
                enemy.DamageMin = 20;
                enemy.DamageMax = 40;
                break;
            case WeaponType.Spear:
                enemy.HealthMax = 100;
                enemy.DamageMin = 25;
                enemy.DamageMax = 35;
                break;
            case WeaponType.Axe:
                enemy.HealthMax = 100;
                enemy.DamageMin = 15;
                enemy.DamageMax = 55;
                break;
        }
        enemy.armor = (ArmorType)rand.Next(0,3);
        switch (enemy.armor)
        {
            case ArmorType.Shield:
                if (enemy.weapon == WeaponType.Spear)
                {
                    enemy.Armor = ArmorType.LeatherArmorPro;
                    enemy.DefenseMin = 25;
                    enemy.DefenseMax = 30;
                    break;
                }
                enemy.Armor = ArmorType.Shield;
                enemy.DefenseMin = 10;
                enemy.DefenseMax = 30;
                break;
            case ArmorType.LeatherArmor:
                enemy.Armor = ArmorType.LeatherArmor;
                enemy.DefenseMin = 20;
                enemy.DefenseMax = 25;
                break;
            case ArmorType.DefensifeRing:
                enemy.Armor = ArmorType.DefensifeRing;
                enemy.DefenseMin = 15;
                enemy.DefenseMax = 35;
                break;
        }
    }
}