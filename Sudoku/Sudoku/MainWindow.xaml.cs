using Microsoft.Win32;
using SudokuLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        Button selectedButton = null;
        int[,] sudokuArray;
        ISudoku sudo;

        public MainWindow(){
            InitializeComponent();
            InsertButtons();
        }

        public void buttonClicked(object sender, RoutedEventArgs e) {
            Button srcButton = e.Source as Button;
            selectedButton = srcButton;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if(selectedButton != null) {
                if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back | e.Key == Key.Space) {
                    e.Handled = true;
                    Stat.Text = "NIGGA, YOU DONE GOOFED";
                } else {
                    byte inputKey = byte.Parse(e.Key.ToString()[1] + "");
                    ButtonPressed(inputKey);
                }
            }
        }

        public void ButtonPressed(byte input) {
            int row = int.Parse(selectedButton.Name[3] + "");
            int column = int.Parse(selectedButton.Name[4] + "");

            if(input == 0) {
                Stat.Text = "Nigga, you cannot put 0 in a sudoku";
                return;
            }

            ISudoku tmpSudo = sudo.Clone();
            tmpSudo.SetNumberAt(column, row, input);

            if (tmpSudo.IsSolvable()) {

                sudokuArray[column, row] = (int)input;
                selectedButton.Content = input;
                selectedButton.IsEnabled = false;
                sudo.SetNumberAt(column, row, input);
                Stat.Text = "GJ NIGGA";
                if (sudo.IsSolved()) {
                    Stat.Text = "YOU DONE DID NIGGA, FRIED CHICKEN FOR EVERYONE!";
                }
                return;

            } else {
                Stat.Text = "NIGGA YOU STUPID AS HELL";
                return;
            }
        }


        public void InitializeSudoku(int[,] input) {
            for (int column = 0; column < 9; column++) {
                for (int row = 0; row < 9; row++) {
                    string tempBtnName = "btn" + column.ToString() + row.ToString();
                    Button tempBtn = (Button)FindName(tempBtnName);
                    tempBtn.Content = "";
                    tempBtn.IsEnabled = true;

                    if (input[row,column] != 0) {
                        tempBtn.Content = input[row,column];
                        tempBtn.IsEnabled = false;
                    }
                }
            }
        }

        public void InsertButtons() {
            for (int x = 0; x < 9; x++) {
                for (int y = 0; y < 9; y++) {
                    
                    //Create a new instance of a button
                    Button btn = new Button();
                    btn.Name = "btn" + x.ToString() + y.ToString();
                    btn.Click += buttonClicked;
                    btn.Content = "";
                    
                    //Puts the button in the correc grid position
                    Grid.SetColumn(btn, x);
                    Grid.SetRow(btn, y);
                    grid.Children.Add(btn);
                    grid.RegisterName(btn.Name, btn);

                    //Set margin between the boxes 
                    if(x % 3 == 0) {
                        Thickness margin = btn.Margin;
                        margin.Left = 2;
                        btn.Margin = margin;
                    }
                    if(y % 3 == 0) {
                        Thickness margin = btn.Margin;
                        margin.Top = 2;
                        btn.Margin = margin;
                    }
                }
            }
        }
       
        public string LoadSudoku(string path) {
            string[] lines = File.ReadAllLines(path);
            int index = new Random().Next(0, lines.Length-1);
            return lines[index];
        }

        public string ConvertDotToZero(string input) {
            return input.Replace(".", "0");
        }

        public int[,] CreateArrayFromString(string inputString) {
            int[,] SudokuArray = new int[9, 9];
            int counter = 0;
            for (int row = 0; row < 9; row++) {
                for (int column = 0; column < 9; column++) {
                        SudokuArray[row, column] = int.Parse(inputString[counter] + "");
                    counter++;
                }
            }
            return SudokuArray;
        }


        public void LoadSudokuDialog(object sender, RoutedEventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();

            //filter to only accept .txt
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true) {
                //Fails if the selected file is not a correct sudoku format
                try {
                    string sudokuString = ConvertDotToZero(LoadSudoku(dlg.FileName));
                    //Initialize the attributes we need
                    sudokuArray = CreateArrayFromString(sudokuString);
                    sudo = SudokuFactory.CreateSudoku(sudokuString);
                    //Initialize the sudoku based on the loaded sudoku
                    InitializeSudoku(sudokuArray);
                } catch {
                    MessageBox.Show("Den valgte fil kan ikke læses", "error");
                }
            }
        }

        public void SaveSudokuDialog(object sender, RoutedEventArgs e) {
            string tmpSudokuString = Regex.Replace(sudo.ToString(), @"\t|\n|\r|\s", "");
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "txt";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, tmpSudokuString);
        }

        public void SolveSudokuDialog(object sender, RoutedEventArgs e) {
            try {
            sudo = sudo.Solve();
            string solvedString = Regex.Replace(sudo.ToString(), @"\t|\n|\r|\s", "");
            InitializeSudoku(CreateArrayFromString(solvedString));
            } catch {
                Stat.Text = "YOU NEED TO START A SUDOKU FIRST, MY NIGGA!";
            }
        }
    }
}
    

