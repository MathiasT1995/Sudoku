using Microsoft.Win32;
using SudokuLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        Button selectedButton = null;

        int[,] sudokuArray;
        string sudokuString;
        string sudokuPath = "C:\\Users\\Woller\\Dropbox\\Datamatiker\\C# & .net\\Projekt - Sudoku\\Sudoku\\Sudoku\\Sudoku\\top1465.txt";
        // "C:\\Users\\Woller\\Dropbox\\Datamatiker\\C# & .net\\Projekt - Sudoku\\Sudoku\\Sudoku\\Sudoku\\top1465.txt"
        // "C:\\4Semester\\CSharp\\Sudoku\\Sudoku\\Sudoku\\top1465.txt"
        public MainWindow(){

            sudokuArray = loadSudoku(sudokuPath);
            sudokuString = convertArrayToString(sudokuArray);
            var s = SudokuFactory.CreateSudoku(sudokuString);
            s.Solve();
            Console.WriteLine(s.Solve().ToString());

            PrintToConsole(sudokuArray);
            InitializeComponent();
            insertButtons();
            initializeSudoku(sudokuArray);
        }

        public void buttonClicked(object sender, RoutedEventArgs e) {
            Button srcButton = e.Source as Button;
            selectedButton = srcButton;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if(selectedButton != null) {
                if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back | e.Key == Key.Space) {
                    e.Handled = true;
                    MessageBox.Show("I only accept numbers, sorry. :(", "Error");
                } else {
                    int inputKey = int.Parse(e.Key.ToString()[1] + "");
                    CanIPressThisButton((byte)inputKey);
                }
            }
        }

        public bool CanIPressThisButton(byte input) {
            //Again, this is swaped because int[] is fucked
            int y = int.Parse(selectedButton.Name[3] + "");
            int x = int.Parse(selectedButton.Name[4] + "");
            Console.WriteLine("X: " + x + " - Y: " + y);

            List<byte> allowedNumbers = SudokuFactory.CreateSudoku(sudokuString).PossibleNumbers(x,y);
            if(allowedNumbers != null) {

            Console.WriteLine(string.Join("", allowedNumbers.ToArray()));
            if (allowedNumbers.Contains(input)) {
                sudokuArray[x, y] = (int)input;
                sudokuString = convertArrayToString(sudokuArray);
                selectedButton.Content = input;
                selectedButton.IsEnabled = false;
                return true;
            } else {
                MessageBox.Show("You cannot input this here, pass along please", "Error");
            }
            }else {
                Console.WriteLine("Her fucker det up!! my dude");
            }
            return false;
        }

        public void PrintToConsole(int[,] input) {
            for (int i = 0; i < 9; i++) {
                for (int j = 0; j < 9; j++) {
                    Console.Write(input[i, j] + " ");
                }
                Console.Write("\n");
            }
        }

        public string convertArrayToString(int[,] input) {
        string res = "";
        for(int x = 0; x < 9; x++) {
            for(int y = 0; y < 9; y++) {
                res += input[x, y];
            }
        }
        return res;
        }

        public void initializeSudoku(int[,] input) {
            for (int x = 0; x < 9; x++) {
                for (int y = 0; y < 9; y++) {
                    string tempBtnName = "btn" + x.ToString() + y.ToString();
                    Button tempBtn = (Button)FindName(tempBtnName);
                    tempBtn.Content = "";
                    tempBtn.IsEnabled = true;
                    //This is swaped because rows and collums in C# is fucked
                    if(input[y,x] != 0) {
                        tempBtn.Content = input[y,x];
                        tempBtn.IsEnabled = false;
                    }
                }
            }
        }

        public void insertButtons() {
            for (int x = 0; x < 9; x++) {
                for (int y = 0; y < 9; y++) {
                    System.Windows.Controls.Button btn = new Button();
                    btn.Name = "btn" + x.ToString() + y.ToString();
                    Grid.SetColumn(btn, x);
                    Grid.SetRow(btn, y);
                    btn.Click += buttonClicked;
                    grid.Children.Add(btn);
                    grid.RegisterName(btn.Name, btn);
                    if(x % 3 == 0) {
                        Thickness margin = btn.Margin;
                        margin.Left = 2;
                        btn.Margin = margin;
                    }
                    if(y % 3 == 0) {
                    Console.WriteLine(y.ToString());
                        Thickness margin = btn.Margin;
                        margin.Top = 2;
                        btn.Margin = margin;
                    }
                }
            }
        }

        public void loadWindow(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true) {
                loadSudoku(openFileDialog.FileName);
            }
        }

        public int[,] loadSudoku(string path) {
            int[,] SudokuArray = new int[9, 9];
               string[] lines = File.ReadAllLines(path);
               int index = new Random().Next(0, lines.Length-1);

               //Read string and put it in a two dimensional array
               int x = 0;
               for (int i = 0; i < 9; i++) {
                   for (int j = 0; j < 9; j++) {
                       if (lines[index][x].Equals('.')) {
                           SudokuArray[i, j] = 0;
                       } else {
                           SudokuArray[i, j] = int.Parse(lines[index][x] + "");
                       }
                       x++;
                   }
               }
        return SudokuArray;
            }

        public void loadSudokuDialog(object sender, RoutedEventArgs e) {
            OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            //filter to only accept .txt
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();



            if (result == true) {
                // Open document 
                string filename = dlg.FileName;
                sudokuPath = filename;
                sudokuArray = loadSudoku(filename);
                sudokuString = convertArrayToString(sudokuArray);
                initializeSudoku(sudokuArray);

            }

    }
    }
}
    

