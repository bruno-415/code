using System;
using System.Collections.Generic;
using System.Threading;

namespace code
{
    class Wizard
    {
        Random numberGen = new Random();
        public string name;
        public string favoriteSpell;
        public int health;
        public int mana;
        public int minDamage;
        public int maxDamage;
        public int damage;
        public int experience;
        public int level;
        public int maxLevel;
        public int expLeft;
        public int turnsDone;

        // Starting values
        public Wizard(string _name, string _favoriteSpell)
        {
            name = _name;
            favoriteSpell = _favoriteSpell;
            health = 100;
            mana = 100;
            minDamage = 10;
            maxDamage = 15;
            damage = 10;
            experience = 0;
            level = 1;
            maxLevel = 10;
            expLeft = 60;
            turnsDone = 0;
        }

        public void CastSpell()
        {
            Console.WriteLine("------------------------------------------------------------------------\n" + name + " casts " + favoriteSpell + ", dealing " + damage + " damage!");
            mana -= 35;
            experience += 20;
            expLeft -= 20;
        }

        public void Meditate()
        {
            health += 5;
            mana += 25;
            minDamage += 2;
            maxDamage += 3;
            damage = numberGen.Next(minDamage, maxDamage + 1);
            if (health > 100)
            {
                health = 100;
            }
            if (mana > 100)
            {
                mana = 100;
            }
            Console.WriteLine("------------------------------------------------------------------------\n" + name + " regains health and mana. \n" + name + " grew stronger and can now deal from " + minDamage + " to " + maxDamage + " damage.");
        }

        // When a player levels up, their level number will increase, their damage values will increase, and their damage value will update
        public void LevelUp()
        {
            level++;
            minDamage += 7;
            maxDamage += 8;
            expLeft = 60;
            Console.WriteLine (name + " reached level " + level + "! Their max damage is now " + maxDamage + ".");
        }

        // Checks if player is at a certain experience threshold, and if they are, they will level up
        public void CheckForLevelUp()
        {
            if (experience == 60 && level == 1){
                LevelUp();
            }
            else if (experience == 120 && level == 2){
                LevelUp();
            }
            else if (experience == 180 && level == 3){
                LevelUp();
            }
            else if (experience == 240 && level == 4){
                LevelUp();
            }
            else if (experience == 300 && level == 5){
                LevelUp();
            }
            else if (experience == 360 && level == 6){
                LevelUp();
            }
            else if (experience == 420 && level == 7){
                LevelUp();
            }
            else if (experience == 480 && level == 8){
                LevelUp();
            }
        }

        // Method to display a player's health, mana, level, damage, and exp left
        public void ShowStats()
        {
            Console.WriteLine(name + "'s stats:");
            Console.WriteLine("HP: " + health);
            Console.WriteLine("MP: " + mana);
            Console.WriteLine("LVL: " + level);
            Console.WriteLine("DMG: " + minDamage + "-" + maxDamage);
            Console.WriteLine("\nExp needed for next level: " + expLeft+ "\n------------------------------------------------------------------------");
        }

        // Activates the random number generator to determine player's damage
        public void SetDamage()
        {
            damage = numberGen.Next(minDamage, maxDamage + 1);
        }
        
    }

    class Program
    {
        static void Main(string[] args)
        {

            //Get Player 1's name and spell
            Console.WriteLine ("\n\nPlayer 1, enter your wizard's name: ");
            string name01 = Console.ReadLine();
            Console.WriteLine ("Enter your wizard's spell of choice: ");
            string spell01 = Console.ReadLine();
            Wizard wizard01 = new Wizard(name01, spell01);

            //Get Player 2's name and spell
            Console.WriteLine ("\nPlayer 2, enter your wizard's name: ");
            string name02 = Console.ReadLine();
            Console.WriteLine ("Enter your wizard's spell of choice: ");
            string spell02 = Console.ReadLine();
            Wizard wizard02 = new Wizard(name02, spell02);

            Console.WriteLine("\n------------------------------------------------------------------------\n" + wizard01.name + ", type \"Cast spell\" to cast a spell which \nuses 35 mana and deals " + wizard01.minDamage + " to " + wizard01.maxDamage + " damage to the enemy. \n\nType \"Meditate\" to restore 5 health and 25 mana \nwhile boosting your damage by 3. \n\nType \"End game\" if you wish to end the game. \n");
            // First, display Player 1's health, mana, level, remaining exp
            wizard01.ShowStats();
            
            // Setting random value between minDamage and maxDamage as the damage int
            wizard01.SetDamage();

            // Waiting for player input
            string action = Console.ReadLine();

            while (String.Compare(action, "End game", true) != 0)
            {
                // Player 1's turn:

                // Trigger the CastSpell method if Player 1 types "Cast spell"
                if (String.Compare(action, "Cast spell", true) == 0)
                {
                    // Make sure they have enough mana
                    if (wizard01.mana >= 35)
                    {
                        wizard01.CastSpell();
                        wizard02.health -= wizard01.damage;
                        
                        if (wizard02.health <= 0)
                        {   
                            wizard02.health = 0;
                            Console.WriteLine (wizard02.name + " has " + wizard02.health + " health remaining!");
                            Thread.Sleep(2000);
                            Console.WriteLine(wizard02.name + " has no health left. " + wizard02.name + " is vanquished by " + wizard01.name + "!");
                            break;
                        }
                        else
                        {
                            Console.WriteLine (wizard02.name + " has " + wizard02.health + " health remaining!");
                        }
                    }
                    // If they are out of mana, it won't work
                    else 
                    {
                        Console.WriteLine("------------------------------------------------------------------------\n" + wizard01.name + " is too weak to cast a spell, they don't have enough mana.");
                    }

                // Trigger the Meditate method if Player 1 types "Meditate"
                }
                else if (String.Compare(action, "Meditate", true) == 0)
                {
                    wizard01.Meditate();
                }
                // If Player 1 does not type "Cast spell," "Meditate," or "End game," then they will receive this message
                else
                {
                    Console.WriteLine("------------------------------------------------------------------------\nInvalid action...");
                }
                
                // Check if Player 1 leveled up
                wizard01.CheckForLevelUp();

                // Increase turn count by 1
                wizard01.turnsDone++;

                // Wait a few seconds
                Thread.Sleep(5000);


                // Player 2's turn:

                // Before the first turn, buff Player 2's damage to compensate for going second
                if (wizard02.turnsDone == 0)
                {
                    wizard02.minDamage += 2;
                    wizard02.maxDamage += 3;
                }

                Console.WriteLine("------------------------------------------------------------------------\n" + wizard02.name + ", type \"Cast spell\" to cast a spell which \nuses 35 mana and deals " + wizard02.minDamage + " to " + wizard02.maxDamage + " damage to the enemy. \n\nType \"Meditate\" to restore 5 health and 25 mana \nwhile boosting your damage by 3. \n\nType \"End game\" if you wish to end the game. \n");
                // Display Player 2's health, mana, level, remaining exp
                wizard02.ShowStats();

                // Setting random value between minDamage and maxDamage as the damage int
                wizard02.SetDamage();

                // Waiting for player input
                action = Console.ReadLine();

                // Trigger the CastSpell method if Player 2 types "Cast spell"
                if (String.Compare(action, "Cast spell", true) == 0)
                {
                    // Make sure they have enough mana
                    if (wizard02.mana >= 35)
                    {
                        wizard02.CastSpell();
                        wizard01.health -= wizard02.damage;

                        if (wizard01.health <= 0)
                        {   
                            wizard01.health = 0;
                            Console.WriteLine (wizard01.name + " has " + wizard01.health + " health remaining!");
                            Thread.Sleep(2000);
                            Console.WriteLine(wizard01.name + " has no health left. " + wizard01.name + " is vanquished by " + wizard02.name + "!");
                            break;
                        }
                        else
                        {
                            Console.WriteLine (wizard01.name + " has " + wizard01.health + " health remaining!");
                        }
                    }
                    // If they are out of mana, it won't work
                    else 
                    {
                        Console.WriteLine("------------------------------------------------------------------------\n" + wizard02.name + " is too weak to cast a spell, they don't have enough mana.");
                    }

                    

                // Trigger the Meditate method if Player 2 types "Meditate"    
                }
                else if (String.Compare(action, "Meditate", true) == 0)
                {
                    wizard02.Meditate();
                }
                // Break the loop and end the game if Player 2 types "End game"
                else if (String.Compare(action, "End game", true) == 0)
                {
                    break;
                }
                // If Player 2 does not type "Cast spell," "Meditate," or "End game," then they will receive this message
                else
                {
                    Console.WriteLine("------------------------------------------------------------------------\nInvalid action...");
                }

                // Check if Player 2 leveled up
                wizard02.CheckForLevelUp();

                // Increase Player 2's turn count by 1
                wizard02.turnsDone++;

                // Wait a few seconds
                Thread.Sleep(5000);


                // Back to Player 1's turn
                Console.WriteLine("------------------------------------------------------------------------\n" + wizard01.name + ", type \"Cast spell\" to cast a spell which \nuses 35 mana and deals " + wizard01.minDamage + " to " + wizard01.maxDamage + " damage to the enemy. \n\nType \"Meditate\" to restore 5 health and 25 mana \nwhile boosting your damage by 3. \n\nType \"End game\" if you wish to end the game. \n");
                // Display Player 1's health, mana, level, remaining exp
                wizard01.ShowStats();
                
                // Setting random value between minDamage and maxDamage as the damage int
                wizard01.SetDamage();
                
                // Waiting for player input
                action = Console.ReadLine();
            }
            
            // Game over
            Thread.Sleep(800);
            Console.WriteLine ("Game over! The game took " + wizard01.turnsDone + " turns to finish.");
            Console.ReadKey();
        }
    }
}