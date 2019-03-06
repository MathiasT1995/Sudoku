using System;
using System.Collections.Generic;
using System.Globalization;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            insertButtons();
        }

        public void insertButtons() {
            for (int x = 0; x < 9; x++) {
                for (int y = 0; y < 9; y++) {
                    System.Windows.Controls.Button btn = new Button();
                    btn.Name = "btn" + x.ToString() + y.ToString();
                    btn.Content = "" + x.ToString() + y.ToString();
                    Grid.SetColumn(btn, x);
                    Grid.SetRow(btn, y);
                    
                    grid.Children.Add(btn);


                }
            }
        }
    }
    
}
