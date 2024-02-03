using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        Button[] buttons;
        Random random = new Random();
        bool isPlayerTurn;
        bool isGameStarted;

        public MainWindow()
        {
            InitializeComponent();
            buttons = new Button[9] { Cl_1, Cl_2, Cl_3, Cl_4, Cl_5, Cl_6, Cl_7, Cl_8, Cl_9 };
            isGameStarted = false;
        }

        private void prosto_click(object sender, RoutedEventArgs e)
        {
            if (!isGameStarted)
            {
                MessageBox.Show("Нажмите 'Начать игру заново', чтобы начать новую игру.");
                return;
            }

            Button clickedButton = (sender as Button);

            if (isPlayerTurn && clickedButton.IsEnabled)
            {
                clickedButton.Content = "X";
                clickedButton.IsEnabled = false;
                isPlayerTurn = false;
            }

            if (CheckForWin("X"))
            {
                MessageBox.Show("Вы победили!");
                RestartGame();
                return;
            }
            else if (!CheckForFreeCells())
            {
                MessageBox.Show("Ничья!");
                RestartGame();
                return;
            }

            ComputerTurn();
        }

        private void ComputerTurn()
        {
            if (CheckForFreeCells())
            {
                int randomIndex;
                do
                {
                    randomIndex = random.Next(0, buttons.Length);
                } while (!buttons[randomIndex].IsEnabled);

                buttons[randomIndex].Content = "O";
                buttons[randomIndex].IsEnabled = false;

                if (CheckForWin("O"))
                {
                    MessageBox.Show("Победил компьютер!");
                    RestartGame();
                    return;
                }
                else if (!CheckForFreeCells())
                {
                    MessageBox.Show("Ничья!");
                    RestartGame();
                    return;
                }

                isPlayerTurn = true;
            }
        }

        private void RestartGame()
        {
            foreach (var button in buttons)
            {
                button.Content = "";
                button.IsEnabled = true;
            }

            isPlayerTurn = true;
            isGameStarted = false;
        }

        private bool CheckForWin(string value)
        {
            if ((buttons[0].Content?.ToString().Trim() == value && buttons[1].Content?.ToString().Trim() == value && buttons[2].Content?.ToString().Trim() == value) ||
                (buttons[0].Content?.ToString().Trim() == value && buttons[3].Content?.ToString().Trim() == value && buttons[6].Content?.ToString().Trim() == value) ||
                (buttons[0].Content?.ToString().Trim() == value && buttons[4].Content?.ToString().Trim() == value && buttons[8].Content?.ToString().Trim() == value) ||
                (buttons[3].Content?.ToString().Trim() == value && buttons[4].Content?.ToString().Trim() == value && buttons[5].Content?.ToString().Trim() == value) ||
                (buttons[6].Content?.ToString().Trim() == value && buttons[7].Content?.ToString().Trim() == value && buttons[8].Content?.ToString().Trim() == value) ||
                (buttons[1].Content?.ToString().Trim() == value && buttons[4].Content?.ToString().Trim() == value && buttons[7].Content?.ToString().Trim() == value) ||
                (buttons[2].Content?.ToString().Trim() == value && buttons[5].Content?.ToString().Trim() == value && buttons[8].Content?.ToString().Trim() == value) ||
                (buttons[6].Content?.ToString().Trim() == value && buttons[4].Content?.ToString().Trim() == value && buttons[2].Content?.ToString().Trim() == value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckForFreeCells()
        {
            foreach (var button in buttons)
            {
                if (button.IsEnabled)
                    return true;
            }

            return false;
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            RestartGame();
            isGameStarted = true;
        }
    }
}