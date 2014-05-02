using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MastermindLogic;

namespace MastermindUnitTests
{
    [TestFixture]
    public class AnswerTest
    {
        [Test]
        public void FirstNumberIsPopulated()
        {
            Answer answer = new Answer(4);
            answer.GenerateRandomAnswer();

            int firstRandomNumber = answer.AnswerDigits[0].Number;
            Assert.That(firstRandomNumber, Is.GreaterThan(0).And.LessThan(10), "First answer number does not contain a number.");
        }

        [Test]
        public void LastNumberIsPopulated()
        {
            Answer answer = new Answer(4);
            answer.GenerateRandomAnswer();

            int lastRandomNumber = answer.AnswerDigits[answer.AnswerDigits.Count - 1].Number;
            Assert.That(lastRandomNumber,Is.GreaterThan(0).And.LessThan(10), "Last answer number does not contain a number.");
        }

        [Test]
        public void MatchIsIdentified()
        {
            Answer answer = new Answer(4, new int[] { 1, 2, 3, 4 });
            Assert.That(answer.IsMatch(new int[] { 1, 2, 3, 4 }), "Match not correctly identified");
        }

        [Test]
        public void NonMatchIsIdentified()
        {
            Answer answer = new Answer(4, new int[] { 2, 2, 3, 4 });
            Assert.IsFalse(answer.IsMatch(new int[] { 1, 2, 3, 4 }), "Match not correctly identified");
        }

        [Test]
        public void NumberIsIncludedInAnswer()
        {
            Answer answer = new Answer(4, new int[] { 1, 2, 3, 4 });
            Assert.That(answer.IsInAnswer(2), "A number is not correctly identified as being in the results");
        }

        [Test]
        public void NumberIsNotIncludedInAnswer()
        {
            Answer answer = new Answer(4, new int[] { 1, 2, 3, 4 });
            Assert.IsFalse(answer.IsInAnswer(5), "A number is not correctly identified as not being in the results");
        }

        [Test]
        public void ResultsAreProcessedCorrectly()
        {
            Answer answer = new Answer(4, new int[] { 1, 2, 3, 4 });
            answer.ProcessAnswer(new int[] { 4, 5, 3, 7 });
            Assert.That(answer.AnswerDigits[0].MatchStatus == AnswerDigit.Status.WrongPosition, "A wrong position match is not identified correctly");
            Assert.That(answer.AnswerDigits[1].MatchStatus == AnswerDigit.Status.Nonmatch, "A non match is not identified correctly");
            Assert.That(answer.AnswerDigits[2].MatchStatus == AnswerDigit.Status.Match, "A match is not identified correctly");
        }
    }
}
