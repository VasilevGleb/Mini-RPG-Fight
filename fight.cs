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
    enum WeaponType { Sword, Spear, Axe, Mage };
    enum ArmorType { Shield, LeatherArmor, DefensifeRing, LegendLeatherArmor, CheaterArmor };
    enum EnemyName { Bandit, Warrior, Lord };
    class Character
    {
        public string Name;
        public int Health = 100;
        public int HealthMax;
        public int Damage;
        public double Defense;
        public WeaponType Weapon;
        public ArmorType Armor;
        public bool isMage = false;
    }

    static void Main()
    {
        Console.WriteLine("\nWelcome to the Mini RPG Fight!");

        Character player = new Character();
        Character enemy = new Character();

        // Initialize player
        Console.WriteLine("Enter your character's name:");
        string nameInput = Console.ReadLine();
        CheckExit(nameInput);
        player.Name = nameInput;

        // Choose weapon
        Console.WriteLine("Choose your weapon: (1)Sword, (2)Spear, (3)Axe");
        string weaponInput = Console.ReadLine();
        CheckExit(weaponInput);
        if (weaponInput == "123")
        {
            Console.WriteLine("Fucking cheater! You get the Mage weapon!");
            player.Weapon = WeaponType.Mage;
            player.Health = player.Health * 2;
            player.Damage = rand.Next(60, 101);
            player.isMage = true;
        }
        else
        {
            switch (weaponInput)
            {
                case "1":
                    player.Weapon = WeaponType.Sword;
                    player.Health = 100;
                    player.Damage = rand.Next(20, 41);
                    break;
                case "2":
                    player.Weapon = WeaponType.Spear;
                    player.Health = 100;
                    player.Damage = rand.Next(25, 36);
                    break;
                case "3":
                    player.Weapon = WeaponType.Axe;
                    player.Health = 100;
                    player.Damage = rand.Next(15, 56);
                    break;
            }
        }

        // Choose armor
        Console.WriteLine("Choose your armor: (1)Shield, (2)Leather Armor, (3)Defensive Ring");
        string armorInput = Console.ReadLine();
        CheckExit(armorInput);
        if (armorInput == "456")
        {
            Console.WriteLine("Nice, you found the Cheater Armor!");
            player.Armor = ArmorType.CheaterArmor;
            player.Defense = rand.Next(50, 71);
        }
        else
        {
            switch (armorInput)
            {
                case "1":
                    if (player.Weapon == WeaponType.Spear)
                    {
                        Console.WriteLine("Spear and Shield are incompatible! You get the Leather Armor-Pro instead.");
                        player.Armor = ArmorType.LegendLeatherArmor;
                        player.Health = 114;
                        player.Defense = rand.Next(25, 30);
                        break;
                    }
                    player.Armor = ArmorType.Shield;
                    player.Health = 110;
                    player.Defense = rand.Next(10, 30);
                    break;
                case "2":
                    player.Armor = ArmorType.LeatherArmor;
                    player.Health = 105;
                    player.Defense = rand.Next(20, 25);
                    break;
                case "3":
                    player.Armor = ArmorType.DefensifeRing;
                    player.Health = 100;
                    player.Defense = rand.Next(15, 35);
                    break;


            }
        }

        // Initialize enemy
        enemy.Name = ((EnemyName)rand.Next(0, 3)).ToString();
        enemy.Weapon = (WeaponType)rand.Next(0, 3);
        switch (enemy.Weapon)
        {
            case WeaponType.Sword:
                enemy.HealthMax = 110;
                enemy.Health = enemy.HealthMax;
                enemy.Damage = rand.Next(20, 41);
                break;
            case WeaponType.Spear:
                enemy.HealthMax = 113;
                enemy.Health = enemy.HealthMax;
                enemy.Damage = rand.Next(25, 36);
                break;
            case WeaponType.Axe:
                enemy.HealthMax = 93;
                enemy.Health = enemy.HealthMax;
                enemy.Damage = rand.Next(15, 56);
                break;
        }
        enemy.Armor = (ArmorType)rand.Next(0, 3);
        switch (enemy.Armor)
        {
            case ArmorType.Shield:
                if (enemy.Weapon == WeaponType.Spear)
                {
                    enemy.Armor = ArmorType.LegendLeatherArmor;
                    enemy.Defense = rand.Next(25, 30);
                    break;
                }
                enemy.Armor = ArmorType.Shield;
                enemy.Defense = rand.Next(10, 30);
                break;
            case ArmorType.LeatherArmor:
                enemy.Armor = ArmorType.LeatherArmor;
                enemy.Defense = rand.Next(20, 25);
                break;
            case ArmorType.DefensifeRing:
                enemy.Armor = ArmorType.DefensifeRing;
                enemy.Defense = rand.Next(15, 35);
                break;
        }

        while (player.Health > 0 && enemy.Health > 0)
        {
            Console.WriteLine($"\n{player.Name}`s Health:{player.Health} Armor:{player.Defense} Weapon:{player.Weapon}");
            Console.WriteLine($"{enemy.Name}`s Health:{enemy.Health} Armor:{enemy.Defense} Weapon:{enemy.Weapon} ");

            // Combat logic here
            // Player turn
            Console.WriteLine("Your turn! (1) Attack (2)Defense (3) Heal");
            string actionInput = Console.ReadLine();
            CheckExit(actionInput);

            //Cheat code "666" for infinite health
            if (actionInput == "666")
            {
                player.Health = 999;
                player.Defense = 999;
                Console.WriteLine("It`s unlimited health time!");
                Console.WriteLine($"Your health is now {player.Health}");
                continue;
            }
            switch (actionInput)
            {
                case "1": //Attack
                    int damageToEnemy = Math.Max(1, player.Damage - (int)(enemy.Defense));
                    enemy.Health -= damageToEnemy;
                    Console.WriteLine($"You attack the {enemy.Name} with {damageToEnemy} damage");
                    break;
                case "2": //Defense
                    player.Defense += 5;
                    Console.WriteLine("Your defense increases by 5 for this turn.");
                    break;

                case "3": //Heal
                    int healAmount = rand.Next(9, 21);
                    player.Health = Math.Min(player.Health + healAmount, player.HealthMax);
                    Console.WriteLine($"You heal yourserlf for {healAmount} points.");
                    break;
            }

            // Check if enemy is defeated
            if (enemy.Health <= 0)
            {
                Console.WriteLine($"You have defeated the {enemy.Name}! Congratulations!");
                Console.WriteLine("Thank you for playing!");
                break;
            }

            // Enemy turn
            Console.WriteLine($"\n{enemy.Name}`s turn!");
            int enemyAction = rand.Next(1, 3); // 1 for attack, 2 for defend, 3 for heal 
            switch (enemyAction)
            {
                case 1: //Attack
                    int damageToPlayer = Math.Max(1, enemy.Damage - (int)(player.Defense));
                    player.Health -= damageToPlayer;
                    Console.WriteLine($"The {enemy.Name} attacks you with {damageToPlayer} damage!");
                    break;
                case 2: //Defensive
                    enemy.Defense += 5;
                    Console.WriteLine($"The {enemy.Name} defends and increases it`s defense by 5 for this turn.");
                    break;
                case 3: //Heal
                    int enemyHealAmount = rand.Next(9, 21);
                    enemy.Health = Math.Min(enemy.Health + enemyHealAmount, enemy.HealthMax);
                    Console.WriteLine($"The {enemy.Name} heals itself for {enemyHealAmount} points.");
                    break;
            }

            // Check if player is defeated
            if (player.Health <= 0)
            {
                if (player.isMage == true)
                {
                    Console.WriteLine("What a shame! The mighty Mage has fallen!");
                    Console.WriteLine("Thank you for playing!");
                    break;
                }

                else
                    Console.WriteLine("You have been defeated! Game Over.");
                Console.WriteLine("Thank you for playing!");
                break;

            }

            // Reset temporary defense boosts
            player.Defense = Math.Max(0, player.Defense - 5);
            enemy.Defense = Math.Max(0, enemy.Defense - 5);
        }
    }
}