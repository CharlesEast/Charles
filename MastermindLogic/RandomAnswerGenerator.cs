using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindLogic
{
    public class RandomAnswerGenerator
    {
        public IList<AnswerDigit> GenerateRandomAnswers(int count)
        {
            var digits = new Dictionary<int, AnswerDigit>();

            while (digits.Count < count)
            {
                AnswerDigit digit = AnswerDigit.GenerateRamdomAnswerDigit();

                if (!digits.ContainsKey(digit.Number))
                {
                    digits.Add(digit.Number, digit);
                }
            }

            return digits.Values.ToList();
        }
    }
}
