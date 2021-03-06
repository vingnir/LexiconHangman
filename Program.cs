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
        private string secretWord;
        public string SecretWord
        {
            get => secretWord;
            private set => secretWord = value;
        }



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
            AddToWordList("lexicon"); // Create some testwords for wordList.txt TODO delete
            HandleUserInput();
        }

        public void PlayAgain()
        {
            Console.WriteLine("\n\nPlay Again? y/n ");
            string usrInput = Console.ReadLine();
            if (usrInput == "y" || usrInput == "Y")
            {
                RunGame();
            }
            else
            {
                Environment.Exit(0);
            }

            Console.WriteLine("Press enter to exit...");
        }

        public void HandleUserInput()
        {
            string usrInput;
            char guess;
            int guesses = 10;
            int ltrsRevealed = 0;
            bool winnner = false;
            
            //setup the secret word 
            SecretWord = GetRandomWord();
            StringBuilder incorrectLetters = new StringBuilder(guesses);
            char[] correctLetters = new char[SecretWord.Length];

            // Initialize correctLetters with underscore char '_'
            SetCorrectLtrs();

            while (!winnner && guesses > 0)
            {
                Console.WriteLine("\nGuess a letter...");
                usrInput = GetInput();
                guess = usrInput[0];
                if (correctLetters.Contains(guess) && usrInput != SecretWord)
                {
                    Console.WriteLine($"{guess} is already an accepted answer! Guess again..., you have {guesses} guesses left");
                    continue;
                }
                else if (incorrectLetters.ToString().Contains(guess))
                {
                    Console.WriteLine($"You have already guessed {guess} and it's still the wrong answer ;) Guess again...you have {guesses} guesses left");
                    continue;
                }

                if (SecretWord.Contains(guess))
                {
                    for (int i = 0; i < SecretWord.Length; i++)
                    {
                        if (SecretWord[i] == guess)
                        {
                            correctLetters[i] = SecretWord[i];
                            ltrsRevealed++;
                        }
                    }

                    if (ltrsRevealed == SecretWord.Length || usrInput == SecretWord)
                    {
                        winnner = true;
                    }
                    Console.WriteLine("\nCorrect");
                }
                else
                {   // Add the incorrect guesses to stringbuilder Obj incorrectLetters and informs user 
                    incorrectLetters.Append(guess);
                    Console.WriteLine($"Sorry, the secret word doesn't contain {guess}, try again...");
                    guesses--;
                    Console.WriteLine($"you have {guesses} guesses left");
                }

                // Inform user of the previous guessed letters
                Console.WriteLine("\nIncorrect guesses: " + incorrectLetters.ToString());

                // Writes the correct guesses to the screen, the rest is hidden by underscore: '_'
                foreach (char ltr in correctLetters)
                {
                    Console.Write(ltr);
                }

                // Prints the hanging man to the screen
                Console.WriteLine("\n" + GetHangningMan(guesses));

            }// While ends 

            // If winnner is true the game is won
            if (winnner)
            {
                Console.WriteLine($"\nAnd the correct word was ");

                // Replace the remaining underscores '_' with the correct letters
                for (int i = 0; i < SecretWord.Length; i++)
                {
                    correctLetters[i] = SecretWord[i];
                    Console.Write(correctLetters[i]);
                }
                Console.WriteLine("\n Awesome! You won the game...");
            }
            else
            {
                Console.WriteLine($"\nBummer, You lost the game...The word was {SecretWord}");
            }
            Console.WriteLine("\n Press enter to continue...");
            Console.ReadKey();
            PlayAgain();


            // Local method to initialize array with underscore char '_'
            void SetCorrectLtrs()
            {
                for (int i = 0; i < SecretWord.Length; i++)
                {
                    correctLetters[i] = '_';
                    Console.Write(correctLetters[i]);
                }
            }
        }// HandleUserInputs method end

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
            words += ",cat,dog,mouse,wolf,giraffe,wessel,elephant,bird";
            string txtToSave = words; //string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            string docPath = AppDomain.CurrentDomain.BaseDirectory;
            string combinedPath = Path.Combine(docPath, "wordList.txt");
            using (StreamWriter outputFile = new StreamWriter(combinedPath, true))
            {
                outputFile.WriteLine(txtToSave);
            }
           // Console.WriteLine("Sparat!");
            
        }

    }// Class end
} // Namespace end