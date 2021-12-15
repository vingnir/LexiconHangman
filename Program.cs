using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
        private StringBuilder secretWord = new StringBuilder();
        public StringBuilder SecretWord
        {
            get { return secretWord; }
            private set { secretWord = value; }
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
            char inputToCheck;
            bool compare;
            int guesses = 10;
            bool? validatedInput;
            string usrReply;

            for (int i=0; i < 10; i++ )
            {
                guesses--;
                Console.WriteLine("Guess a letter...");
                input = Console.ReadLine()?.ToLower();
                validatedInput = ValidateInput(input);

                if(validatedInput == null)
                {
                    Console.WriteLine("Great, you won the game and rescued the hangman!");
                    PlayAgain();
                    break;
                }
                if(validatedInput == true)
                {
                    inputToCheck = char.Parse(input);
                    compare = CompareInputLetters(inputToCheck);
                    usrReply = compare ? "Correct guess" : "Incorrect";
                }
                else
                {
                    usrReply = "You need to provide exactly one character, for example 'a' or 'b' or guess the correct word  ";
                }
                Console.WriteLine($"{usrReply}, you have {guesses} guesses left");
            }
            Console.WriteLine("Game over...");
            PlayAgain();
        }

        public bool? ValidateInput(string input)
        {
            string correctWord = secretWord.ToString();
            bool? returnValue;

            if (input == correctWord)
            {                
                returnValue = null;
            }
            else if (input.Length > 1 || input.Length == 0)
            {         
                returnValue = false;
            }
            else
            {
                returnValue = true;
            }

            return returnValue;
        }

        public bool CompareInputLetters(char guess)
        {                    
            bool returnValue;
            
                if(remainingLetters.Contains(guess))
                {
                    SetGuessedLetters(guess);
                    RemoveRemainingLetters(guess);
                    returnValue = true; 
                }
                else
                {
                    SetGuessedLetters(guess);
                    returnValue = false;                 
                }
           
            return returnValue;
        }
       

            public StringBuilder GetRandomWord()//TODO read from file
        {          
            Random random = new Random();
            List<string> wordList = new List<string> { "cat", "dog", "mouse", "wolf", "giraffe" };
            int index = random.Next(wordList.Count);
            StringBuilder randomWord = new StringBuilder(wordList[index]);
            
            return randomWord;
        }
       
        //Storing correct guessed characters in a list
        private readonly StringBuilder guessedLetters = new StringBuilder();
        private void SetGuessedLetters(char ltr)
        {
            guessedLetters.Append(ltr);
        }
        // TODO delete 
        public StringBuilder GetGuessedLetters()
        {
            return guessedLetters;
        }

        //Storing remaining characters in a list
        private readonly List<char> remainingLetters = new List<char>();
        public void RemoveRemainingLetters(char ltr)
        {
            
            remainingLetters.Remove(ltr);
        }

        private void SetRemainingLetters()
        {
           // Adds all char of secretWord to 
            for (int i = 0; i < secretWord.Length; i++)
            {
                // get char at position i
               char ltr = secretWord[i];
               remainingLetters.Add(ltr);
            }

                
        }
    }
}
