using System;
using System.Collections.Generic;
using System.Text;

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

        //Set up collections 
        private char[] secretWord;
        public char[] SecretWord
        {
            get { return secretWord; }
            set { secretWord = value; }
        }

        //Storing correct guessed characters in a list
        private readonly List<char> guessedLetters = new List<char>();
        public void SetGuessedLetters(char ltr)
        {
           guessedLetters.Add(ltr);
            Console.WriteLine(ltr + "test");
        }

        public List<char> GetGuessedLetters()
        {
            return guessedLetters;
        }

        //List for the char left to guess
        private readonly List<char> leftToGuess = new List<char>();
        public void SetLeftToGuess(char ltr)
        {
            leftToGuess.Add(ltr);
        }

        public List<char> GetLeftToGuess()
        {
            return leftToGuess;
        }



        //private readonly List<char> correctGuesses = new List<char>();
        public void RunGame() 
        {
            Console.WriteLine("Lexicon Hangman");
            Console.WriteLine("Press enter to start game...");
            SecretWord = GetRandomWord();
            Console.WriteLine("The word is " + SecretWord[0] + SecretWord[1]);
            Console.ReadLine();
            GetUserInput();
        }
    

        public void GetUserInput()
        {
            StringBuilder sb = new StringBuilder();
            List<char> inputList = new List<char>();
            string input;
            char inputToCheck;
            //bool compare;
            int guesses;
            guesses = SecretWord.Length;
            //Console.WriteLine(guesses);
            for (int i=0; i < guesses; i++ )
            {
                Console.WriteLine("Guess a letter...");
                input = Console.ReadLine();
                inputToCheck = char.Parse(input);
                var compare = CompareInputLetters(inputToCheck);
                if (compare)
                {
                    Console.WriteLine("Great, you guessed correct! enter next letter...");
                    guesses++;

                }
                else
                {
                    guesses--;
                    Console.WriteLine($"Guess again! You have {guesses} left");
                    //compare = false;
                }
                
            }
            Console.WriteLine("Game over...");

        }
        
        public bool CompareInputLetters(char guess)//TODO fix order
        {
            char letterToCheck = guess;
            
            char[] wordToGuess = SecretWord;
            bool returnValue = false;
            foreach(var character in wordToGuess)
            {
                if(letterToCheck == character)
                {
                    SetGuessedLetters(letterToCheck);

                    returnValue = true;
                    
                }
                else
                {
                    returnValue = false;
                    
                }
            }
            return returnValue;
        }

        public char[] GetRandomWord()
        {
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
    }
}
