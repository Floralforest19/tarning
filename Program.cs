using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleDiceRandomizer
{   
    class Program
    {  
       
        /* -------------------------- CONFIGURATION --------------------------*/
        public static class ConfigClass
        {
            public const Int32 minDices = 1;
            public const Int32 maxDices = 5;
        }
        /* ------------------------------------------------------------------ */
        /* ------------------- DO NOT EDIT BELOW THIS LINE -------------------*/
        /* ------------------------------------------------------------------ */


        /* ---------------------- Our custom functions ---------------------- */

        //This is our Randomizer function. It returns a number between 1-6
        static int RandomDice(Random RandomizerObj)
        {
            return RandomizerObj.Next(1, 7); //Randomize a number between 1-6
        }

        //Average value of rolls stored in dicehistory
        static int AverageValue(List<int> dicehistory)
        {
            int total = 0;
            //Loop list
            for (int i = 0; i < dicehistory.Count; i++)
            {   
                //Get total amount
                total = total + dicehistory[i];   
            }

            //Get average amount by deviding total with rolls.
            return total / dicehistory.Count;
        }

        //Wait for Enter key to be clicked before returning.
        static int WaitForEntry(){
            System.Console.WriteLine("\n\t------Tryck på '[Enter]' för att återgå------");
            Console.ReadLine();
            Console.Clear();
            return 1;
        }

        //This function either roll x dices or print out history from the last run
        static List<int> RollDices(int rolls, Random slump, List<int> dicehistory){
            int totalvalue = 0; //Create total value
            //If rolls equals -1. Print history else roll dices
            if (rolls == -1){
               Console.WriteLine("\n\tRullade tärningar: "); //Print dice history by iterating the results stored in dice history
               foreach (int roll in dicehistory)
               {
                    Console.WriteLine("\t" + roll); //Print dice roll
                    totalvalue = totalvalue + roll; //Save current dice val to total var 
               }
            } else { // Else RollDices! :)
                //Clear history list.
                dicehistory = new List<int>();
                //Run as many times as we want to roll the dice (rolls)
                for (int i = 0; i < rolls; i++)
                {
                    //call on our randomization function. Roll the dice!
                    int roll = RandomDice(slump);
                    //dice is just to create a better visual experiance for the user. Eg. dice 1 instead of Dice 0.
                    //This could also be done by starting iterating the for loop at i = 1 and increase rolls by one.
                    //but this felt more readable.
                    int dice = i + 1;

                    //If the RandomDice returns the value 6. We will ignore the results from the current roll and roll 2 new dices.
                    if (roll == 6)
                    {
                        Console.WriteLine("\tT" + dice + ": (" + roll + ") Grattis du fick en 6a! \n\t\tVi rullar därför två tärningar till. Detta slag exkluderas");
                        //Roll 2 new dices by increasing amount of rolls.
                        rolls = rolls + 2;
                    }
                    else
                    {
                        Console.WriteLine("\tT" + dice + ": " + roll); //print the actual result from rolling dice with number $dice
                        dicehistory.Add(roll); //Add to history list
                        totalvalue = totalvalue + roll; //Save current dice val to total var 
                    }
                }

 
            }

            //print results
            Console.WriteLine("\n\tTotalt värde: " + totalvalue); // Print the total value of all dices
            Console.WriteLine("\n\tAntal rull: " + dicehistory.Count); // Print the total value of all dices
            Console.WriteLine("\tMedelvärdet på alla tärningsrull: " + AverageValue(dicehistory)); // Print average value (Just for fun :)

            //Return new dicehistory list
            return dicehistory;

        }

        /* -------------------------- Main program ---------------------------*/

        static void Main()
        {
            Random randomclass = new Random(); // Create a instance of the random function. We will use this for randomization
            List<int> dicehistory = new List<int>(); // Store our latest dices for history purposes.
            Console.WriteLine("\n\t<------ Mikaela Fryklund 19/5/2019 ------>");
            Console.WriteLine("\n\tVälkommen till 'Obegränsad 6-sidig tärning'!");

            //Our main while loop that prints the menu and runs our options.
            bool run = true;
            while (run)
            {
                Console.WriteLine("\n\t[1] Rulla tärning\n" +
                    "\t[2] Kolla vad du rullade\n" +
                    "\t[3] Avsluta");
                Console.Write("\tVälj: ");
                int val;
                int.TryParse(Console.ReadLine(), out val);

                Console.Clear();

                switch (val)
                {
                    case 1:
                        //Get the amount of dices that the user want's to roll
                        Console.Write("\n\t[1-5] Hur många tärningar vill du rulla: ");
                        //Make sure that we are able to Parse rolls as a int
                        bool inmatning = int.TryParse(Console.ReadLine(), out int rolls);
                        if (!inmatning){
                            Console.WriteLine("\n\tEndast siffror mellan " + ConfigClass.minDices + "-" + ConfigClass.maxDices + " är tillåtet");
                        } else if (rolls > ConfigClass.maxDices || rolls < ConfigClass.minDices){
                        //Check that the user has entered a valid amount of rolls    
                            Console.WriteLine("\n\tFel antal tärningar! ;( \n\t "+ConfigClass.minDices+"-"+ConfigClass.maxDices+" är tillåtet");
                        } else {
                            //call our RollDices function
                            dicehistory = RollDices(rolls, randomclass, dicehistory);
                        }
                        WaitForEntry();
                        break;
                    case 2:
                        if (dicehistory.Count <= 0)
                        {
                            Console.WriteLine("\n\tDet finns inga sparade tärningsrull! ");
                        } else {
                            //Call RollDices with -1 as rolls. This will print history instead (If possible)
                            RollDices(-1, randomclass, dicehistory);
                        }    

                        WaitForEntry();
                        break;
                    case 3:
                        //Kill program by ending our main while loop. (run = false)
                        Console.Clear();
                        Console.WriteLine("\n\tTack för att du rullade tärning!");
                        Thread.Sleep(1000);
                        run = false;
                        break;
                    default:
                        //If invalid number. Reprint menu
                        Console.WriteLine("\n\tOgiltit val. Välj 1-3 från menyn.");
                        WaitForEntry();
                        break;
                }
            }
        }
    }
}
