
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tictactoe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members
        /// <summary>
        /// Holds the current results of cell sin the active game
        /// </summary>
        private MarkType[] results;

        /// <summary>
        /// True if it's player1's turn
        /// </summary>
        private bool player1Turn;

        /// <summary>
        /// True if game has ended
        /// </summary>
        private bool gameEnded;
        #endregion


        #region Constructor
        /// <summary>
        ///  Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            newGame();
        }
        #endregion
        /// <summary>
        /// Starts a new game and clears all values back to the start
        /// </summary>
        private void newGame()
        {
            //Create a new blank array of free cells
            results = new MarkType[9];

            for (var i = 0; i < results.Length; i++)
                results[i] = MarkType.Free;

            //player1 starts the game
            player1Turn = true;

            //iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                //clears every button on the grid
                //change bg, foreground and content to default values
                button.Content = String.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            //make sure the game hasn't finished
            gameEnded = false;
        }

        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The events of the click </param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //start new game on the click after it finished
            if (gameEnded)
                newGame();

            //cast sender to button
            var button = (Button)sender;

            //Find the button position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            //Don't do anything if cell already has value in it
            if (results[index] != MarkType.Free)
                return;

            //Set cell value based on owhich players turn it is
            results[index] = player1Turn ? MarkType.Cross : MarkType.Nought;

            //set button text to the result
            button.Content = player1Turn ? "X" : "O";

            //change noughts to green
            if (!player1Turn)
                button.Foreground = Brushes.Red;

            //change player's turn
            if (player1Turn)
                player1Turn = false;
            else
                player1Turn = true;

            //check for winner
            CheckForWinner();
        }

        /// <summary>
        /// Checks if there is a winner 
        /// </summary>
        private void CheckForWinner()
        {
            //check horizontal wins

            //Row 0
            var same0  = (results[0] & results[1] & results[2]) == results[0];
            if (results[0] != MarkType.Free && same0)
            {
                //game end
                gameEnded = true;

                //Highlight winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;

            }

            //Row 1
            var same1 = (results[3] & results[4] & results[5]) == results[3];
            if (results[3] != MarkType.Free && same1)
            {
                //game end
                gameEnded = true;

                //Highlight winning cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;

            }


            //Row 2
            var same2 = (results[6] & results[7] & results[8]) == results[6];
            if (results[6] != MarkType.Free && same2)
            {
                //game end
                gameEnded = true;

                //Highlight winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;

            }

            //check vertical wins

            //column0
            var sameCol0 = (results[0] & results[3] & results[6]) == results[0];
            if (results[0] != MarkType.Free && sameCol0)
            {
                //game end
                gameEnded = true;

                //Highlight winning cells in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;

            }

            //Column 1
            var sameCol1 = (results[1] & results[4] & results[7]) == results[1];
            if (results[1] != MarkType.Free && sameCol1)
            {
                //game end
                gameEnded = true;

                //Highlight winning cells in green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;

            }


            //Column2
            var sameCol2 = (results[2] & results[5] & results[8]) == results[2];
            if (results[2] != MarkType.Free && sameCol2)
            {
                //game end
                gameEnded = true;

                //Highlight winning cells in green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;

            }


            //Diagonal1
            var sameDiag1 = (results[0] & results[4] & results[8]) == results[0];
            if (results[0] != MarkType.Free && sameDiag1)
            {
                //game end
                gameEnded = true;

                //Highlight winning cells in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;

            }

            //Diagonal2
            var sameDiag2 = (results[2] & results[4] & results[6]) == results[2];
            if (results[2] != MarkType.Free && sameDiag2)
            {
                //game end
                gameEnded = true;

                //Highlight winning cells in green
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;

            }

            //checks for no winner
            if (!results.Any(result => result == MarkType.Free))
            {
                //game ends
                gameEnded = true;

                //turn all cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                    
                });

            }
                
        }


    }
}
