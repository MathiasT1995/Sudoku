using SudokuLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadFileFromTXT{

    class Program{

        


        static void Main(string[] args){
            /*
            ReadFileFromTXT rt = new ReadFileFromTXT();
            rt.ReadFile();
            rt.PrintToConsole();
            Console.ReadKey();
            */
                // Lots of Sudoku here:
                // https://github.com/ralli/sudoku/blob/master/data/top1465.txt
                // https://www.telegraph.co.uk/news/science/science-news/9359579/Worlds-hardest-sudoku-can-you-crack-it.html
                // http://lipas.uwasa.fi/~timan/sudoku/
                // 17-cell Sudoku (No valid Sudoku has fewer cells filled)
                // http://theconversation.com/good-at-sudoku-heres-some-youll-never-complete-5234
                // Loading a Sudoku. This Sudoku uses '0' as indication of unfilled cell
                // You must translate to this format your self
                var s = SudokuFactory.CreateSudoku("042000005000632080080040200000000000715068340908350761091006000000020190006100050");
                Console.WriteLine(s);
                // Print how many cells are filled/unfilled
                Console.WriteLine("Filled Cells : " + s.NumberOfFilledCells());
                Console.WriteLine("Open Cells : " + s.NumberOfOpenCells());
                Console.WriteLine();
                // Do we have any obvious conflicts?
                // column, row, and local block conflicts?
                Console.WriteLine("s.HasConflicts() = " + s.HasConflicts());
                // What numbers can be placed at position 0,0 (remember we count from 0 and not from 1)
                Console.Write("s.PossibleNumbers(0, 0) : ");
                foreach (var n in s.PossibleNumbers(0, 0))
                    Console.Write(n + ", ");
                Console.WriteLine();
                // At what position are the fewest possible numbers?
                // (Easiest place to start filling in)
                int I = 0;
                int J = 0;
                int numberCount = s.OptimalPosition(out I, out J);
                Console.WriteLine($"{numberCount} numbers possible at optimal position {I},{J}");
                if (numberCount == 1) {
                    int n = s.PossibleNumbers(I, J)[0];
                    Console.WriteLine("Only one number possible, so you must fill this cell with the number " + n);
                    // Lets do this, by the way
                    s[I, J] = (byte)n;
                }
                Console.WriteLine();
                // Solve the Sudoku (if possible) and dump the result
                var s_clone = s.Clone();
                var s_solved = s_clone.Solve(); // You could try to give this delegate to Solve : (txt) => Console.WriteLine(txt)
                if (s_solved != null) {
                    Console.WriteLine("SOLVED");
                    Console.WriteLine(s_solved);
                } else {
                    Console.WriteLine("No solution exists");
                }
                Console.WriteLine("The original Sudoku is still here:");
                Console.WriteLine(s);
                Console.ReadLine();
        }
    }

    public class ReadFileFromTXT {
       int[,] SudokuArray = new int[9,9];

        public ReadFileFromTXT() {
        }

        public void ReadFile() {
            

            string[] lines = System.IO.File.ReadAllLines(@"C:\4Semester\CSharp\Sudoku\Sudoku\Sudoku\top1465.txt");

            //Read string and put it in a two dimensional array
            int x = 0;
            for (int i = 0; i < 9; i++) {
                for(int j = 0; j < 9; j++) {
                    if (lines[0][x].Equals('.')) {
                        SudokuArray[i,j] = 0;
                    } else {
                        SudokuArray[i, j] = int.Parse(lines[0][x] + "");
                    }
                    x++;
                }
            }
              
        }
        //To print the array in the console
        public void PrintToConsole() {
            for (int i = 0; i< 9; i++) {
                for (int j = 0; j< 9; j++) {
                    Console.Write(SudokuArray[i, j] + " ");    
                }
            Console.Write("\n");
            }
        }
    
    }
}
