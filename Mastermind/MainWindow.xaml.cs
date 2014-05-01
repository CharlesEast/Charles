using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MastermindLogic;

namespace Mastermind
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Answer correctAnswer;
        int answerCount = 0;

        public MainWindow()
        {
            InitializeComponent();
            StartNewGame();
            BlankLabels();
        }

        private void BlankLabels()
        {
            for (int x = 1; x <= 4; x++)
            {
                ((Label)this.FindName("lblGuessOne" + x.ToString())).Content = String.Empty;
                ((Label)this.FindName("lblSymbolOne" + x.ToString())).Content = String.Empty;
                ((Label)this.FindName("lblGuessTwo" + x.ToString())).Content = String.Empty;
                ((Label)this.FindName("lblSymbolTwo" + x.ToString())).Content = String.Empty;
                ((Label)this.FindName("lblGuessThree" + x.ToString())).Content = String.Empty;
                ((Label)this.FindName("lblSymbolThree" + x.ToString())).Content = String.Empty;
                ((Label)this.FindName("lblGuessFour" + x.ToString())).Content = String.Empty;
                ((Label)this.FindName("lblSymbolFour" + x.ToString())).Content = String.Empty;
            }
            lblStatus.Content = String.Empty;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (btnSubmit.Content.ToString() == "New Game")
            {
                StartNewGame();
                txtGuessOne.Focus();
            }
            else if (txtGuessOne.Text != String.Empty
                && txtGuessTwo.Text != String.Empty
                && txtGuessThree.Text != String.Empty
                && txtGuessFour.Text != String.Empty)
            {
                answerCount++;
                int[] userAnswer = GetInput();
                correctAnswer.ProcessAnswer(userAnswer);
                ShowGuessesAndStatus(userAnswer);
                BlankTextBoxes();
                txtGuessOne.Focus();
                DisplayResults(userAnswer);
            }
        }

        private void StartNewGame()
        {
            answerCount = 0;
            btnSubmit.Content = "Guess";
            correctAnswer = new Answer(4);
            correctAnswer.GenerateRandomAnswer();
            BlankTextBoxes();
            BlankLabels();
            txtGuessOne.Focus();
        }

        private void BlankTextBoxes()
        {
            txtGuessOne.Text = String.Empty;
            txtGuessTwo.Text = String.Empty;
            txtGuessThree.Text = String.Empty;
            txtGuessFour.Text = String.Empty;
        }

        private string GetStatusSymbol(AnswerDigit.Status status)
        {
            switch (status)
            {
                case (AnswerDigit.Status.Match):
                    { return "✔"; }
                case (AnswerDigit.Status.Nonmatch):
                    { return "✗"; }
                default:
                    { return "O"; }
            }
        }

        private void ShowGuessesAndStatus(int[] userAnswer)
        {
            ((Label)this.FindName("lblGuessOne" + answerCount.ToString())).Content = userAnswer[0].ToString();
            ((Label)this.FindName("lblSymbolOne" + answerCount.ToString())).Content = GetStatusSymbol(correctAnswer.AnswerDigits[0].MatchStatus);
            ((Label)this.FindName("lblGuessTwo" + answerCount.ToString())).Content = userAnswer[1].ToString();
            ((Label)this.FindName("lblSymbolTwo" + answerCount.ToString())).Content = GetStatusSymbol(correctAnswer.AnswerDigits[1].MatchStatus);
            ((Label)this.FindName("lblGuessThree" + answerCount.ToString())).Content = userAnswer[2].ToString();
            ((Label)this.FindName("lblSymbolThree" + answerCount.ToString())).Content = GetStatusSymbol(correctAnswer.AnswerDigits[2].MatchStatus);
            ((Label)this.FindName("lblGuessFour" + answerCount.ToString())).Content = userAnswer[3].ToString();
            ((Label)this.FindName("lblSymbolFour" + answerCount.ToString())).Content = GetStatusSymbol(correctAnswer.AnswerDigits[3].MatchStatus);
        }

        private void DisplayResults(int[] userAnswer)
        {
            if (correctAnswer.IsMatch(userAnswer))
            {
                lblStatus.Content = "You win!";
                SuggestNewGame();
            }
            else
            {
                if (answerCount <= 3)
                {
                    lblStatus.Content = "Guess Again";
                }
                else
                {
                    lblStatus.Content = "You lose";
                    SuggestNewGame();
                }
            }
        }

        private void SuggestNewGame()
        {
            btnSubmit.Content = "New Game";
            btnSubmit.Focus();
        }

        private int[] GetInput()
        {
            int[] userAnswer = new int[4];
            userAnswer[0] = Convert.ToInt32(txtGuessOne.Text);
            userAnswer[1] = Convert.ToInt32(txtGuessTwo.Text);
            userAnswer[2] = Convert.ToInt32(txtGuessThree.Text);
            userAnswer[3] = Convert.ToInt32(txtGuessFour.Text);
            return userAnswer;
        }

        private void txtGuessOne_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtGuessTwo.Focus();
        }

        private void txtGuessTwo_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtGuessThree.Focus();
        }

        private void txtGuessThree_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtGuessFour.Focus();
        }

        private void txtGuessFour_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnSubmit.Focus();
        }

        private void NumericOnly(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = IsTextNumeric(e.Text);

        }
        
        private static bool IsTextNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^1-9]");
            return reg.IsMatch(str);
        }


    }
}
