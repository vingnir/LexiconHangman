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
            SecretWord = GetRandomWord();           
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

            StringBuilder incorrectLetters = new StringBuilder(guesses);
            char[] correctLetters = new char[SecretWord.Length];


            for (int i = 0; i < SecretWord.Length; i++)
            {
                correctLetters[i] = '_';
                Console.Write(correctLetters[i]);
            }

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
                {   // Add the incorrect guesses to stringbuilder Obj incorrectLetters and inform user 
                    incorrectLetters.Append(guess);
                    Console.WriteLine($"Sorry, the secret word doesn't contain {guess}, try again...");
                    guesses--;
                    Console.WriteLine($"you have {guesses} guesses left");
                }

                // Inform user of the previous guessed letters
                Console.WriteLine("\nIncorrect guesses: " + incorrectLetters.ToString());

                // Write the correct guesses to the screen, the rest is hidden by underscore: '_'
                foreach (char ltr in correctLetters)
                {
                    Console.Write(ltr);
                }
                // Prints the hanging man to the screen
                Console.WriteLine("\n" + HangHimHigh(guesses));
            }// While ends 

            // If winnner is true game is won
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
        }// HandleUserInputs() end

        // Get the input from user and check if empty
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
            string currentPath = Environment.CurrentDirectory;
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
        static string HangHimHigh(int x)
        {
            string[] hangingMan = {  @"
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
=========",@"
  +---+
  |   |
  O   |
 /|\  |
      |
      |
=========",@"
  +---+
  |   |
  O   |
 /|   |
      |
      |
=========",@"
  +---+
  |   |
  O   |
  |   |
      |
      |
=========",@"
  +---+
  |   |
  O   |
      |
      |
      |
=========",@"
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
      

      
      
=========" };
            return hangingMan[x];
        }

    }// Class end
} // Namespace end