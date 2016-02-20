
using FluentMetacritic;
using FluentMetacritic.Domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;



namespace ShouldIWatch
   
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    
    {
        public static String searchInput;
        public MainWindow()
        {

            InitializeComponent();
            /*
            Option to get list of movies from either MySQL database or a textfile. Textfile has 
            1.5million movies. Database has 3million titles. This is for autocomplete only.
            */
            // addDbComboBox(); 
             //addtxtComboBox();                    
        }

        private void addTxtComboBox()
        {
            
         try //Try opening the textfile.
         {

             using (StreamReader reader = new StreamReader(@"C:\Users\Eman\Google Drive\Personal stuff\C#\ShouldIWatch\ShouldIWatch\movies.txt"))
             {
                 String line;
                 while ((line = reader.ReadLine()) != null)
                 {
                     String editline = line.TrimStart();
                     //System.Console.WriteLine(editline);                                         
                     combobox.Items.Add(editline);

                 }
             }
         }

         catch (IOException e)
         {
             System.Console.WriteLine("Error opening the file of movies! " + e);
         }
        }

        private void addDbComboBox()
        {
            String constring = "server=localhost;port=3306;username=root;password=root";
            String Query = "SELECT * FROM jmdb.movies";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand(Query, conDataBase);
            MySqlDataReader myReader;

            try //Try opening the database.
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();

                while (myReader.Read())
                {
                    String sName = myReader.GetString("title");
                    combobox.Items.Add(sName);
                }
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e);
            }


        }


        public void DannyBrownButton(object sender, RoutedEventArgs e)
        {
            //This is the 'Go' search button.
            searchInput = combobox.Text;
            DisplayPage display = new DisplayPage();
            Close();
            display.ShowDialog();
                    
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }
    }

  
    
}
