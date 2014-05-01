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

        private int _number;
        private Status _matchStatus = new Status();

        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public Status MatchStatus
        {
            get { return _matchStatus; }
            set { _matchStatus = value; }
        }
        
        public AnswerDigit()
        {
        }

        public AnswerDigit(int number)
        {
            _number = number;
        }

        public bool Matches(int guess)
        {
            return _number == guess;
        }

        /// <summary>
        /// Generate a random digit. Seed with time function to avoid the same answers generated repeatedly.
        /// </summary>
        public void GenerateRamdomNumber()
        {
            _number = new Random((int)DateTime.Now.Ticks & 0x0000FFFF).Next(1, 9);
        }
    }
}
