using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
        private StringBuilder secretWord = new StringBuilder();
        public StringBuilder SecretWord
        {
            get => secretWord;
            private set => secretWord = value;
        }
        public void RunGame()
        {
            Console.WriteLine("Lexicon Hangman");
            Console.WriteLine("Press enter to start game...");
            SecretWord = GetRandomWord();
            SetRemainingLetters();
            Console.WriteLine("The word is " + SecretWord[0] + SecretWord[1]);
            HandleUserInput();
        }

        public void PlayAgain()
        {
            Console.WriteLine("Play Again? y/n ");
            Console.WriteLine("Press enter to exit...");
            string usrInput = Console.ReadLine();
            if (usrInput == "y" || usrInput == "Y")
            {
                RunGame();
            }
            else
            {
                Environment.Exit(0);
            }
        }


        public void HandleUserInput()
        {
            string input;
            //char inputToCheck = '\0'; // sets char to Unicode Null character
            //bool compare;
            int guesses = 10;
            bool? validatedInput;
            //string usrReply;
            //string correctWord = secretWord.ToString();

            for (int i = 0; i < 10; i++)
            {
                guesses--;
                Console.WriteLine("Guess a letter...");
                input = Console.ReadLine()?.ToLower();
                char parsedInput = char.Parse(input);
                

                switch (validatedInput)
                {
                    case false:
                        Console.WriteLine($"You have already guessed { input }");
                        i--;
                        break;
                    case true:
                        Console.WriteLine($"Correct!");
                        break;
                    default:
                        Console.WriteLine($"Incorrect!");
                        break;
                }              
            }
            Console.WriteLine("\nGame over...");
            PlayAgain();
        }

       
        public bool CompareInputLetters(char guess)
        {
            char guess;
            guess = char.Parse(str);
            bool? returnValue;

            if (guessedLetters.Contains(guess))
            {
                returnValue = false;
            }
            else if (remainingLetters.Contains(guess))
            {
                SetGuessedLetters(guess);
                RemoveRemainingLetters(guess);
                returnValue = true;
            }
            else
            {
                returnValue = null;
            }
           
            return returnValue;
        }

        public StringBuilder GetRandomWord()
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
            StringBuilder randomWord = new StringBuilder(wordList[index]);

            return randomWord;
        }

        //Storing correct guessed characters in a stringbuilder object
        private readonly List<char> guessedLetters = new List<char>();
        private void SetGuessedLetters(char ltr)
        {
            guessedLetters.Append(ltr);
        }
        // TODO delete 
        public List<char> GetGuessedLetters()
        {
            return guessedLetters;
        }

        //Storing remaining characters in a list
        private readonly List<char> remainingLetters = new List<char>();
        public void RemoveRemainingLetters(char ltr)
        {
            for (int i = 0; i < remainingLetters.Count; i++)
            {
                remainingLetters.Remove(ltr);
            }
        }

        private void SetRemainingLetters()
        {
            // Adds all char of secretWord to remainingLetters
            for (int i = 0; i < secretWord.Length; i++)
            {
                // get char at position i
                char ltr = secretWord[i];
                remainingLetters.Add(ltr);
            }


        }
    }
}
