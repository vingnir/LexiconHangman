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
        private char[] secretWord;
        public char[] SecretWord
        {
            get { return secretWord; }
            set { secretWord = value; }
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
            //StringBuilder sb = new StringBuilder();
            
            string input;
            char inputToCheck;
            bool compare;
            int guesses;
            guesses = SecretWord.Length;
           
            for (int i=0; i < SecretWord.Length; i++ )
            {
                Console.WriteLine("Guess a letter...");
                input = Console.ReadLine()?.ToLower();
                if(input.Length > 1 || input.Length ==0)
                {
                    Console.WriteLine("You need to provide exactly one character, for example 'a' or 'b' ");
                    HandleUserInput();
                    break;
                }
                else
                {                                    
                inputToCheck = char.Parse(input);
                compare = CompareInputLetters(inputToCheck);
               
                if(remainingLetters.Count == 0)
                {
                    Console.WriteLine("Great, you won the game and rescued the hangman!");
                    PlayAgain();
                    break;

                }
                   else if (compare)
                    {
                        Console.WriteLine("Great, you guessed correct! enter next letter...");
                        i--; // Allow extra loop if the answer is correct
                        
                    }
                    else
                    {
                        guesses--;
                        Console.WriteLine($"Wrong guess, try again... You have {guesses} guesses left");
                    }
                }
            }
            Console.WriteLine("Game over...");

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
                else if(remainingLetters.Count == 0)
                {
                returnValue = true;
                }
                else
                {
                    SetGuessedLetters(guess);
                    returnValue = false;                 
                }
            // TEST REMOVE
            foreach (var val in guessedLetters)
                Console.WriteLine(val);
            return returnValue;
        }

        public char[] GetRandomWord()
        {
            //StringBuilder sb = new StringBuilder();
            Random random = new Random();
            List<string> wordList = new List<string> { "cat", "dog", "mouse", "wolf", "giraffe" };
            int index = random.Next(wordList.Count);
            string randomWord = wordList[index];
            char[] letterByLetter = new char[randomWord.Length];

            

            // Copy character by character into array 
            for (int i = 0; i < randomWord.Length; i++)
            {
                letterByLetter[i] = randomWord[i];
            }
            return letterByLetter;
        }

        //Set up collections 
        

        //Storing correct guessed characters in a list
        private readonly List<char> guessedLetters = new List<char>();
        public void SetGuessedLetters(char ltr)
        {
            guessedLetters.Add(ltr);
        }

        public List<char> GetGuessedLetters()
        {
            return guessedLetters;
        }

        //List for the char left to guess
        //Storing correct guessed characters in a list
        private readonly List<char> remainingLetters = new List<char>();
        public void RemoveRemainingLetters(char ltr)
        {
            remainingLetters.Remove(ltr);
        }

        public void SetRemainingLetters()
        {
            foreach (var ltr in secretWord)
                remainingLetters.Add(ltr);
        }
    }
}
