using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.IO;

namespace Positimeter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string[] positiveWordsList = File.ReadAllLines("positive.txt", Encoding.Default).Select(x => x.ToLower()).ToArray();
        static string[] negativeWordsList = File.ReadAllLines("negative.txt", Encoding.Default).Select(x => x.ToLower()).ToArray();

        public MainWindow()
        {
            InitializeComponent();

            //var f = TextHelper.GetWordsStatistics("Шла саша по шоссе и сосала хуилу. Саша шла.");
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            
            // Note that you can have more than one file.
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Count() != 1)
            {
                MessageBox.Show("Поддерживается обработка только одного файла.");
                return;
            }
            else
            {
                var text = File.ReadAllText(files[0], Encoding.Default);

                Positivity_Label.Content = $"Позитивность: {GetTextPositivity(text)}";
            }            
        }


        static int GetTextPositivity(string text)
        {
            var wordsStatistics = TextHelper.GetWordsStatistics(text);

            var positivity = 0;

            foreach(var pair in wordsStatistics)
            {
                if (positiveWordsList.Contains(pair.Key))
                {
                    positivity += pair.Value;
                    continue;
                }
                    

                if (negativeWordsList.Contains(pair.Key))
                    positivity -= pair.Value;
            }

            return positivity;
        }
    }
}
