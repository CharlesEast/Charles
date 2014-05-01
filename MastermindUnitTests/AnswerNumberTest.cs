using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using MastermindLogic;

namespace MastermindUnitTests
{
    [TestFixture]
    public class AnswerDigitTest
    {
        [Test]
        public void AnswerDigitDoesNotMatch()
        {
            AnswerDigit answerDigit = new AnswerDigit(5);
            Assert.IsFalse(answerDigit.Matches(1), "Equality check does not indentify non-matches.");
        }

        [Test]
        public void AnswerDigitMatches()
        {
            AnswerDigit answerDigit = new AnswerDigit(5);
            Assert.That(answerDigit.Matches(5), "Equality check does not identify matches.");
        }

        [Test]
        public void AnswerDigitRandomlyGenerated()
        {
            AnswerDigit answerDigit = new AnswerDigit();
            answerDigit.GenerateRamdomNumber();

            Assert.That(answerDigit.Number > 0 && answerDigit.Number < 10, "Random answer number not between acceptable ranges.");
        }

    }
}

