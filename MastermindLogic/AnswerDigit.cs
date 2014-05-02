using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindLogic
{
    public class AnswerDigit
    {
        public enum Status
        {
            Match,
            Nonmatch,
            WrongPosition
        }

        public int Number { get; set; }

        public Status MatchStatus { get; set; }

        public AnswerDigit()
        {
            MatchStatus = new Status();
        }

        public AnswerDigit(int number)
        {
            MatchStatus = new Status();
            Number = number;
        }

        public bool Matches(int guess)
        {
            return Number == guess;
        }

        /// <summary>
        /// Generate a random digit. Seed with time function to avoid the same answers generated repeatedly.
        /// </summary>
        public void GenerateRamdomNumber()
        {
            Number = new Random((int) DateTime.Now.Ticks & 0x0000FFFF).Next(1, 9);
        }

        public static AnswerDigit GenerateRamdomAnswerDigit()
        {
            var digit = new AnswerDigit();
            digit.GenerateRamdomNumber();
            return digit;
        }

        public override string ToString()
        {
            return string.Format("{0}", Number);
        }
    }
}
