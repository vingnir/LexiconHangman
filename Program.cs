﻿using System;
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
            Console.ReadLine();
            GetUserInput();
        }
    

        public void GetUserInput()
        {
            StringBuilder sb = new StringBuilder();
            List<char> inputList = new List<char>();
            Console.WriteLine("Enter the first letter:");
            string input = Console.ReadLine();
            char inputToCheck = char.Parse(input);
            bool compare = CompareInputLetters(inputToCheck);
            SecretWord = GetRandomWord();
            int guesses = SecretWord.Length;
            while (compare)
            {
                if (compare)
                {
                    Console.WriteLine("Great, you guessed correct! enter next letter...");
                    input = Console.ReadLine();
                    inputToCheck = char.Parse(input);
                    compare = CompareInputLetters(inputToCheck);
                }
                else
                {
                    guesses--;
                    Console.WriteLine($"Guess again! You have {guesses} left");
                    compare = false;
                }
            }

        }
        
        public bool CompareInputLetters(char guess)
        {
            char letterToCheck = guess;
            List<char> guessedLetters = new List<char>();
            List<char> correctGuesses = new List<char>();
            char[] wordToGuess = GetRandomWord();
            bool returnValue = false;
            foreach(var character in wordToGuess)
            {
                if(letterToCheck == character)
                {
                    Console.WriteLine(character);
                    returnValue = true;
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong guess! try again...");
                    returnValue = false;
                    break;
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
            //test 
            Console.WriteLine($" Word of the day is: {randomWord}");
            // Copy character by character into array 
            for (int i = 0; i < randomWord.Length; i++)
            {
                letterByLetter[i] = randomWord[i];
            }

            return letterByLetter;
        }
    }
}
