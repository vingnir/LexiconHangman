using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace LexiconHangman
{
    class Program
    {
        static void Main()
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
            Console.WriteLine("Lexicon Hangman");
            Console.WriteLine("Press enter to start game...");
            SecretWord = GetRandomWord();
           // SetRemainingLetters();
           // SetupWordToDisplay();
            Console.WriteLine("The word is " + SecretWord[0] + SecretWord[1]);
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
            bool win = false;
            
            StringBuilder incorrectLetters = new StringBuilder(guesses);
            char[] correctLetters = new char[SecretWord.Length];
            

            for (int i = 0; i < SecretWord.Length; i++)
            {
                correctLetters[i] = '_';
                Console.Write(correctLetters[i]);
            }

            while (!win && guesses > 0)
            {
                Console.WriteLine("\nGuess a letter...");
                usrInput = Console.ReadLine()?.ToLower();
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
                        win = true;
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
            }// While ends 

            // If win is true game is won
            if (win)
                {
                Console.WriteLine($"\nAnd the correct word was ");

                 // Replace the remaining underscores '_' with the correct letters
                for (int i = 0; i < SecretWord.Length; i++)
                {
                    correctLetters[i] = SecretWord[i];
                    Console.Write(correctLetters[i]);
                }
                Console.WriteLine("\n Awesome! You won the game...");
                    PlayAgain();
                }
                else 
                {
                    Console.WriteLine($"\nBummer, You lost the game...The word was {SecretWord}");
                    PlayAgain();
                }
            }// HandleUserInputs() end
        
        // Gets random word from a textfile and remove ',' then returns string
        public string GetRandomWord()
        {
            Random random = new Random();
            List<string> wordList = new List<string>();
            string currentPath = Environment.CurrentDirectory;
            string filePath = Path.Combine(currentPath, "wordList.txt"); // current path =  LexiconHangman\bin\Debug\netcoreapp3.1\wordlist.txt
            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    wordList = line.Split(',').ToList();
                }
            }
            int index = random.Next(wordList.Count);
            string randomWord =  wordList[index];

            return randomWord;
        }
        
    }// Class end
} // Namespace end