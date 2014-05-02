using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindLogic
{
    public class Answer
    {
        private readonly int numberOfAnswerDigits;
        public IList<AnswerDigit> AnswerDigits { get; set; }

        public Answer(int numberOfAnswerDigits)
        {
            this.numberOfAnswerDigits = numberOfAnswerDigits;
            AnswerDigits = new AnswerDigit[numberOfAnswerDigits];
        }

        /// <summary>
        /// Constructor to help unit tests - predefine the random number with this method
        /// </summary>
        /// <param name="numberOfAnswerDigits"></param>
        /// <param name="generatedAnswer"></param>
        public Answer(int numberOfAnswerDigits, IEnumerable<int> generatedAnswer)
        {
            AnswerDigits = generatedAnswer.Select(i => new AnswerDigit(i)).Take(numberOfAnswerDigits).ToList();
        }

        /// <summary>
        /// Fill each answer number with a random number. If the random number is in use, generate another.
        /// </summary>
        public void GenerateRandomAnswer()
        {
            AnswerDigits = new RandomAnswerGenerator().GenerateRandomAnswers(numberOfAnswerDigits);
        }

        /// <summary>
        /// Check if the number equals any of th digits.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool IsInAnswer(int number)
        {
            return AnswerDigits.Any(t => t.Number == number);
        }

        /// <summary>
        /// Check it the user's input is a match - if they win.
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        public bool IsMatch(int[] userInput)
        {
            return !AnswerDigits.Where((t, currentAnswerDigit) => !t.Matches(userInput[currentAnswerDigit])).Any();
        }

        /// <summary>
        /// Process all the user's inputs
        /// </summary>
        /// <param name="userInput"></param>
        public void ProcessAnswer(int[] userInput)
        {
            for (int currentAnswerDigits = 0; currentAnswerDigits < AnswerDigits.Count; currentAnswerDigits++)
            {
                AnswerDigit answerDigit = AnswerDigits[currentAnswerDigits];
                answerDigit.MatchStatus = MatchInputAtCurrentPosition(answerDigit, userInput[currentAnswerDigits]);
            }
        }

        private AnswerDigit.Status MatchInputAtCurrentPosition(AnswerDigit answerDigit, int currentInput)
        {
            if (answerDigit.Matches(currentInput))
            {
                return AnswerDigit.Status.Match;
            }

            if (IsInAnswer(currentInput))
            {
                return AnswerDigit.Status.WrongPosition;
            }

            return AnswerDigit.Status.Nonmatch;
        }

        public override string ToString()
        {
            return string.Join("", AnswerDigits.Select(digit => digit.ToString()).ToArray());
        }
    }
}
