using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LexiconHangman
{
    internal class Program
    {
        private static void Main()
        {
            Hangman player = new Hangman();
            player.RunGame();
        }
    }

    public class Hangman
    {
        // TODO fix encaps
        public StringBuilder incorrectLetters = new StringBuilder();
        public string SecretWord { get; private set; }
        public bool Winner { get; set; }
        public int Guesses { get; set; }


        public char[] CorrectLetters { get; set; }
        public int LtrsRevealed { get; set; }
        public char Guess { get; set; }

       

        // Starts the game
        public void RunGame()
        {
            string menuText = @"
            
                     __             _                    __ __                                 
                    / /  ___ __ __ (_)____ ___   ___    / // /___ _ ___  ___ _ __ _  ___ _ ___ 
                   / /__/ -_)\ \ // // __// _ \ / _ \  / _  // _ `// _ \/ _ `//  ' \/ _ `// _ \
                  /____/\__//_\_\/_/ \__/ \___//_//_/ /_//_/ \_,_//_//_/\_, //_/_/_/\_,_//_//_/
                                                                       /___/                                
            ";

            Console.Clear();
            Console.WriteLine(menuText);
            Console.WriteLine("Press enter to start game...");
            Console.ReadKey();
            InitializeSettings();
            Console.WriteLine(PrintCorrectLetters());
            HandleUserInput();
        }

        public void InitializeSettings()
        {
            //setup the secret word 
            SecretWord = GetRandomWord();

            //Initialize array to hold correct guesses
            SetupCorrectLtrs(SecretWord);

            // setup number of allowed guesses
            Guesses = 10;

            // set winner to false
            Winner = false;
        }

        public void PlayAgain()
        {
            Console.WriteLine("\n\nPlay Again? y/n ");
            string usrInput = Console.ReadLine();
            if (usrInput.ToLower() == "y")
            {
                RunGame();
            }
            else
            {
                Environment.Exit(0);
            }

            // Console.WriteLine("Press enter to exit...");
        }

        public void HandleUserInput()
        {
            string usrInput;

            while (!Winner && Guesses > 0)
            {
                Console.WriteLine("\nGuess a letter..." + $"\n you have {Guesses} guesses left");

                // Get user input
                usrInput = GetInput();
                Guess = usrInput[0];

                // Check if input is the correct word then winner is true
                Winner = LtrsRevealed == SecretWord.Length || usrInput == SecretWord;

                // Check if input has been used before 
                string duplicateWarning = CompareInput(usrInput);

                if (duplicateWarning != string.Empty)
                { Console.WriteLine(duplicateWarning); }

                // Prints the correct guesses to the screen, the rest is hidden by underscore: '_'
                Console.WriteLine(PrintCorrectLetters());

                // Prints the hanging man to the screen
                Console.WriteLine("\n" + GetHangningMan(Guesses));

                // Inform user of the previous guessed letters
                string userInfo = incorrectLetters.ToString() != string.Empty? "\nIncorrect guesses: " + incorrectLetters.ToString() : "\nIncorrect guesses: none";
                Console.WriteLine(userInfo);
            }// While ends 

            // If winnner is true the game is won
            if (Winner)
            {
                Console.WriteLine($"\nAnd the correct word was " + SecretWord + "\n Awesome! You won the game...");
            }
            else
            {
                Console.WriteLine($"\nBummer, You lost the game...The word was {SecretWord}");
            }
            Console.WriteLine("\n Press enter to continue...");
            Console.ReadKey();
            PlayAgain();

        }// HandleUserInputs method end


        public string CompareInput(string userInput)
        {
            string reply;

            if (CorrectLetters.Contains(Guess) && userInput != SecretWord)
            {
                reply = $"{Guess} is already an accepted answer! Guess again..., you have {Guesses} guesses left";
                // foreach(var ltr in CorrectLetters)
                //{ Console.WriteLine(ltr); }

            }
            else if (SecretWord.Contains(Guess))
            {
                UpdateCorrectLtrs();

                reply = "Correct";
            }
            else if (incorrectLetters.ToString().Contains(Guess))
            {
                reply = $"You have already guessed {Guess} and it's still the wrong answer ;) Guess again...you have {Guesses} guesses left";

            }
            else
            {   // Add the incorrect guesses to stringbuilder Obj incorrectLetters and informs user 
                incorrectLetters.Append(Guess);
                Guesses--;
                reply = $"Sorry, the secret word doesn't contain {Guess}, try again...";
            }

            return reply;
        }

        // Initialize correctLetters with underscore char '_'
        private void SetupCorrectLtrs(string secWord)
        {
            CorrectLetters = new char[SecretWord.Length];
            for (int i = 0; i < secWord.Length; i++)
            {
                CorrectLetters[i] = '_';
                // Console.Write(CorrectLetters[i]);
            }
            Console.WriteLine("\n\t The secret word has " + SecretWord.Length + " letters ");
        }
        // returns string with the correct guesses, the rest is hidden by underscore: '_'
        public string PrintCorrectLetters()
        {
            string visibleLtrs = string.Empty;
            foreach (char ltr in CorrectLetters)
            {
                visibleLtrs += ltr;
            }
            return visibleLtrs;
        }

        public void UpdateCorrectLtrs()
        {
            for (int i = 0; i < SecretWord.Length; i++)
            {
                if (SecretWord[i] == Guess)
                {
                    CorrectLetters[i] = SecretWord[i];
                    LtrsRevealed++;
                }
            }
        }

        // Method to get the input from user and check if empty
        private static string GetInput()
        {
            string Result;
            do
            {
                Result = Console.ReadLine().ToLower();
                if (string.IsNullOrEmpty(Result))
                {
                    Console.WriteLine("Empty input, please try again");
                }
            } while (string.IsNullOrEmpty(Result));
            return Result;
        }

        // Gets random word from a textfile and remove ',' then returns string
        public string GetRandomWord()
        {
            Random random = new Random();
            List<string> wordList = new List<string>();
            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(currentPath, "wordList.txt"); // current path =  LexiconHangman\bin\Debug\netcoreapp3.1\
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    wordList = line.Split(',').ToList();
                }
            }
            int index = random.Next(wordList.Count);
            string randomWord = wordList[index];

            return randomWord;
        }

        // Array of 10 hanged man strings,  starts backwards to match number of guesses 
        private static string GetHangningMan(int guessNum)
        {
            string[] hangingMan = { @"
  +---+
  |   |
  O   |
 /|\  |
 / \  |
      |
=========", @"
  +---+
  |   |
  O   |
 /|\  |
 /    |
      |
=========", @"
  +---+
  |   |
  O   |
 /|\  |
      |
      |
=========", @"
  +---+
  |   |
  O   |
 /|   |
      |
      |
=========", @"
  +---+
  |   |
  O   |
  |   |
      |
      |
=========", @"
  +---+
  |   |
  O   |
      |
      |
      |
=========", @"
  +---+
  |   |
      |
      |
      |
      |
=========", @"
   ---+
      |
      |
      |
      |
      |
=========", @"
      |
      |
      |
      |
=========", @"
      

      
      
=========", "" };
            return hangingMan[guessNum];
        }

        // Method to add new words to wordList if not exist
        public static void AddToWordList(string words)
        {
            words += ",cat,dog,mouse,wolf,giraffe,wessel,elephant,bird"; //TODO delete
            string txtToSave = words; //string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            string docPath = AppDomain.CurrentDomain.BaseDirectory;
            string combinedPath = Path.Combine(docPath, "wordList.txt");
            using StreamWriter outputFile = new StreamWriter(combinedPath, true);
            outputFile.WriteLine(txtToSave);
        }

    }// Class end
} // Namespace end