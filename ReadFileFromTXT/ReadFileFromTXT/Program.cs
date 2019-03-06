using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadFileFromTXT{

    class Program{

        


        static void Main(string[] args){
            ReadFileFromTXT rt = new ReadFileFromTXT();
            rt.ReadFile();
            rt.PrintToConsole();
            Console.ReadKey();
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
