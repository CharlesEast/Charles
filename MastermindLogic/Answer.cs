using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindLogic
{
    public class Answer
    {
        private AnswerDigit[] _answerDigits;

        public AnswerDigit[] AnswerDigits
        {
            get { return _answerDigits; }
            set { _answerDigits = value; }
        }

        public Answer(int numberOfAnswerDigits)
        {
            _answerDigits = new AnswerDigit[numberOfAnswerDigits];
        }

        /// <summary>
        /// Constructor to help unit tests - predefine the random number with this method
        /// </summary>
        /// <param name="numberOfAnswerDigits"></param>
        /// <param name="generatedAnswer"></param>
        public Answer(int numberOfAnswerDigits, int[] generatedAnswer)
        {
            _answerDigits = new AnswerDigit[numberOfAnswerDigits];
            int currentAnswerDigits = 0;

            foreach (int answerDigit in generatedAnswer)
            {
                _answerDigits[currentAnswerDigits] = new AnswerDigit(answerDigit);
                currentAnswerDigits++;
            }
        }

        /// <summary>
        /// Fill each answer number with a random number. If the random number is in use, generate another.
        /// </summary>
        public void GenerateRandomAnswer()
        {
            for (int currentAnswerDigit = 0; currentAnswerDigit < _answerDigits.Length; currentAnswerDigit++)
            {
                _answerDigits[currentAnswerDigit] = new AnswerDigit();
                do
                {
                    _answerDigits[currentAnswerDigit].GenerateRamdomNumber();
                } while (IsAlreadyUsed(_answerDigits[currentAnswerDigit].Number, currentAnswerDigit));
            }
        }

        /// <summary>
        /// Check if an answer number is already in use within the collection of answer numbers.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="lastAnswerDigitPopulated"></param>
        /// <returns></returns>
        public bool IsAlreadyUsed(int number, int lastAnswerDigitPopulated)
        {
            for (int currentAnswerDigit = 0; currentAnswerDigit < lastAnswerDigitPopulated; currentAnswerDigit++)
            {
                if (number == _answerDigits[currentAnswerDigit].Number)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check if the number equals any of th digits.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool IsInAnswer(int number)
        {
            for (int currentAnswerDigit = 0; currentAnswerDigit < _answerDigits.Length; currentAnswerDigit++)
            {
                if (_answerDigits[currentAnswerDigit].Number == number)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check it the user's input is a match - if they win.
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        public bool IsMatch(int[] userInput)
        {
            for (int currentAnswerDigit = 0; currentAnswerDigit < _answerDigits.Length; currentAnswerDigit++)
            {
                if (!_answerDigits[currentAnswerDigit].Matches(userInput[currentAnswerDigit]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Process all the user's inputs
        /// </summary>
        /// <param name="userInput"></param>
        public void ProcessAnswer(int[] userInput)
        {
            for (int currentAnswerDigits = 0; currentAnswerDigits < _answerDigits.Length; currentAnswerDigits++)
            {
                int currentInput = userInput[currentAnswerDigits];

                if (_answerDigits[currentAnswerDigits].Matches(currentInput))
                {
                    _answerDigits[currentAnswerDigits].MatchStatus = AnswerDigit.Status.Match;
                }
                else if (IsInAnswer(currentInput))
                {
                    _answerDigits[currentAnswerDigits].MatchStatus = AnswerDigit.Status.WrongPosition;
                }
                else
                {
                    _answerDigits[currentAnswerDigits].MatchStatus = AnswerDigit.Status.Nonmatch;
                }
            }
        }

    }
}
