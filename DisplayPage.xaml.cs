using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FluentMetacritic;
using FluentMetacritic.Domain;
using IMDb_Scraper;


namespace ShouldIWatch
{
    /// <summary>
    /// Interaction logic for DisplayPage.xaml
    /// </summary>
    public partial class DisplayPage : Window
    {
        public static bool DannyAnswer;
        String[] GenresList;
        String gap = " ";
        String urlEnd = "trailer";
        Decimal finalimdbscore;

        public DisplayPage()
        {
            /*
            Basically sets up everything for the display page. All scores and information.
            */
            InitializeComponent(); //Shows the actually window.       
            MovieAnalysis(); //This gets the text from input and searches Metacritic website.
            SetMetacriticScore(MoviePrint.CriticRatingList[0]); //Gets the Critic rating.
            SetUserScore(MoviePrint.UserRatingList[0]); // Gets the Metacritic user rating.
            setIMDBRating(MainWindow.searchInput); //Gets the IMDB user rating.
            ShowMovieTrailer(); //Sets up a web browser and searches the movie's trailer onto YouTube.
            AggregateScore(); //Gets the Aggregate score of the 3 ratings. 
            SetAnswer(); //Sets the answer at the bottom true = I should watch. false = should not watch.
            SetMovieTitle(); //Gets the search input and assigns it to the top of the screen.
            AddGenres(); //Gets the Genres of the movie.
            //AddDirector(); //Option to display the director of the movie.
        }  

        public void AggregateScore()
        { //Critic rating has the most weight with 50% of the final score. User rating for MC and IMDB both have 25% weighting.
            int criticrating = MoviePrint.CriticRatingList[0];
            decimal userrating = (MoviePrint.UserRatingList[0] * 10);
            decimal imdbrating = (finalimdbscore * 10);

            decimal finalscore = (criticrating * 0.5M) + (userrating * 0.25M) + (imdbrating * 0.25M);

            if (finalscore >= 68.0M)//If final grade 68+ then it's 'good enough' to watch.
            {
                DannyAnswer = true;

            }
            else DannyAnswer = false;
        }

        private void ShowMovieTrailer()
        {
            MovieTrailer.Navigate("https://www.youtube.com/results?search_query=" + MainWindow.searchInput + gap + urlEnd);
        }

        public void MovieAnalysis()
        {
            var writer = new MoviePrint();

            var moviesearch = Metacritic.SearchFor().Movies().OrderedBy().Relevancy().UsingText(MainWindow.searchInput);
            writer.Output(moviesearch.ToList());
        }

        public void SetMovieTitle()
        {
            TextBlock header = new TextBlock();
            header.Text = ("Should you watch " + MainWindow.searchInput + "?");
            header.FontFamily = new FontFamily("Monotype Corsiva");
            header.FontSize = 32;
            header.TextAlignment = TextAlignment.Center;
            header.TextWrapping = TextWrapping.Wrap;
            header.FontWeight = FontWeights.UltraBold;
            movietitle.Children.Add(header);

        }

        public void SetAnswer()

        {
            if (DannyAnswer)
            {
                TextBlock answer = new TextBlock();
                answer.Text = ("Danny says WATCH THIS MOVIE");
                answer.FontFamily = new FontFamily("Monotype Corsiva");
                answer.FontSize = 32;
                answer.TextAlignment = TextAlignment.Center;
                answer.TextWrapping = TextWrapping.Wrap;
                answer.FontWeight = FontWeights.UltraBold;
                dannyanswer.Children.Add(answer);
            }
            else
            {
                TextBlock answer = new TextBlock();
                answer.Text = ("Danny says: DON'T WATCH THIS TRASH!");
                answer.FontFamily = new FontFamily("Monotype Corsiva");
                answer.FontSize = 32;
                answer.TextAlignment = TextAlignment.Center;
                answer.TextWrapping = TextWrapping.Wrap;
                answer.FontWeight = FontWeights.UltraBold;
                dannyanswer.Children.Add(answer);
            }


        }

        public void SetMetacriticScore(int Score)
        {
            if (Score >= 61)
            {
                //Green Background for Score.
                Label MetaCriticScore = new Label();
                MetaCriticScore.Content = (Score);
                MetaCriticScore.FontFamily = new FontFamily("Monotype Corsiva");
                MetaCriticScore.VerticalContentAlignment = VerticalAlignment.Bottom;
                MetaCriticScore.HorizontalContentAlignment = HorizontalAlignment.Center;
                MetaCriticScore.FontSize = 46;
                BrushConverter x = new BrushConverter();
                MetaCriticScorePanel.Background = (Brush)x.ConvertFrom("#66CC33");
                MetaCriticScorePanel.Children.Add(MetaCriticScore);
            }
            else if (Score >= 40 && Score <= 60)
            {
                //Orange Background for Score.
                Label MetaCriticScore = new Label();
                MetaCriticScore.Content = (Score);
                MetaCriticScore.FontFamily = new FontFamily("Monotype Corsiva");
                MetaCriticScore.VerticalContentAlignment = VerticalAlignment.Bottom;
                MetaCriticScore.HorizontalContentAlignment = HorizontalAlignment.Center;
                MetaCriticScore.FontSize = 46;
                BrushConverter x = new BrushConverter();
                MetaCriticScorePanel.Background = (Brush)x.ConvertFrom("#FFCC33");
                MetaCriticScorePanel.Children.Add(MetaCriticScore);
            }

            else if (Score >= 0 && Score <= 39)
            {
                //Red Background for score.
                Label MetaCriticScore = new Label();
                MetaCriticScore.Content = (Score);
                MetaCriticScore.FontFamily = new FontFamily("Monotype Corsiva");
                MetaCriticScore.VerticalContentAlignment = VerticalAlignment.Bottom;
                MetaCriticScore.HorizontalContentAlignment = HorizontalAlignment.Center;
                MetaCriticScore.FontSize = 46;
                BrushConverter x = new BrushConverter();
                MetaCriticScorePanel.Background = (Brush)x.ConvertFrom("#FF0000");
                MetaCriticScorePanel.Children.Add(MetaCriticScore);
            }
        }

        public void SetUserScore(decimal UserScore)
        {
            if (UserScore >= 7.0M)
            {
                //Green Background for Score.
                Label MetaUserScore = new Label();
                MetaUserScore.Content = (UserScore);
                MetaUserScore.FontFamily = new FontFamily("Monotype Corsiva");
                MetaUserScore.VerticalContentAlignment = VerticalAlignment.Bottom;
                MetaUserScore.HorizontalContentAlignment = HorizontalAlignment.Center;
                MetaUserScore.FontSize = 46;
                BrushConverter x = new BrushConverter();
                UserCriticScorePanel.Background = (Brush)x.ConvertFrom("#66CC33");
                UserCriticScorePanel.Children.Add(MetaUserScore);
            }
            else if (UserScore >= 5.0M && UserScore <= 6.9M)
            {
                //Orange Background for Score.
                Label MetaUserScore = new Label();
                MetaUserScore.Content = (UserScore);
                MetaUserScore.FontFamily = new FontFamily("Monotype Corsiva");
                MetaUserScore.VerticalContentAlignment = VerticalAlignment.Bottom;
                MetaUserScore.HorizontalContentAlignment = HorizontalAlignment.Center;
                MetaUserScore.FontSize = 46;
                BrushConverter x = new BrushConverter();
                UserCriticScorePanel.Background = (Brush)x.ConvertFrom("#FFCC33");
                UserCriticScorePanel.Children.Add(MetaUserScore);
            }


            else if (UserScore >= 0.0M && UserScore <= 4.9M)
            {
                //Red Background for score.
                Label MetaUserScore = new Label();
                MetaUserScore.Content = (UserScore);
                MetaUserScore.FontFamily = new FontFamily("Monotype Corsiva");
                MetaUserScore.VerticalContentAlignment = VerticalAlignment.Bottom;
                MetaUserScore.HorizontalContentAlignment = HorizontalAlignment.Center;
                MetaUserScore.FontSize = 46;
                BrushConverter x = new BrushConverter();
                UserCriticScorePanel.Background = (Brush)x.ConvertFrom("#FF0000");
                UserCriticScorePanel.Children.Add(MetaUserScore);
            }

        }
        
        private void AddGenres()
        {
            String Seperator = "\t\t";

            var query = Metacritic.SearchFor().Movies().OrderedBy().Relevancy().UsingText(MainWindow.searchInput);
            genreOutput(query.ToList());
            TextBlock Genres = new TextBlock();
            Genres.FontFamily = new FontFamily("Monotype Corsiva");
            Genres.FontSize = 18;
            Genres.TextAlignment = TextAlignment.Center;
            Genres.TextWrapping = TextWrapping.Wrap;
            Genres.FontWeight = FontWeights.UltraBold;

            Genres.Text = string.Join(Seperator, GenresList);

            //Genres.Text = (GenresList[0]+"\t\t"+GenresList[1]+"\t\t"+GenresList[2]);
            GenrePanel.Children.Add(Genres);

        }

        private void genreOutput(IEnumerable<IMovie> entities)
        {
            foreach (var entity in entities)
            {

                GenresList = entity.Genres.ToArray();
            }
        }

        public void setIMDBRating(String movie)
        {
            
                IMDb moviepicker = new IMDb(movie, false);
                String imdbscore = moviepicker.Rating;
                System.Console.WriteLine(moviepicker.Title);
                finalimdbscore = Decimal.Parse(imdbscore);
           
           

            if   (finalimdbscore >= 7.0M)
            {
                Label score = new Label();
                score.Content = (imdbscore);
                score.FontFamily = new FontFamily("Monotype Corsiva");
                score.VerticalContentAlignment = VerticalAlignment.Bottom;
                score.HorizontalContentAlignment = HorizontalAlignment.Center;
                score.FontSize = 46;
                BrushConverter x = new BrushConverter();
                IMDBScorePanel.Background = (Brush)x.ConvertFrom("#66CC33");
                IMDBScorePanel.Children.Add(score);
            }
            else if (finalimdbscore >= 5.0M && finalimdbscore <= 6.9M)
            {
                //Orange Background for Score.
                Label score = new Label();
                score.Content = (imdbscore);
                score.FontFamily = new FontFamily("Monotype Corsiva");
                score.VerticalContentAlignment = VerticalAlignment.Bottom;
                score.HorizontalContentAlignment = HorizontalAlignment.Center;
                score.FontSize = 46;
                BrushConverter x = new BrushConverter();
                IMDBScorePanel.Background = (Brush)x.ConvertFrom("#FFCC33");
                IMDBScorePanel.Children.Add(score);
            }


            else if (finalimdbscore >= 0.0M && finalimdbscore <= 4.9M)
            {
                //Red Background for score.
                Label score = new Label();
                score.Content = (imdbscore);
                score.FontFamily = new FontFamily("Monotype Corsiva");
                score.VerticalContentAlignment = VerticalAlignment.Bottom;
                score.HorizontalContentAlignment = HorizontalAlignment.Center;
                score.FontSize = 46;
                BrushConverter x = new BrushConverter();
                IMDBScorePanel.Background = (Brush)x.ConvertFrom("#FF0000");
                IMDBScorePanel.Children.Add(score);
            }

        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        { //When you press the back button it goes back to MainWindow and closes the current window/
            
            MainWindow back = new MainWindow();
            Close();            
            back.Show();        

        }

        private void AddDirector()
        {
            IMDb directorname = new IMDb(MainWindow.searchInput, false);
            System.Console.WriteLine(directorname.Directors[0]);
        }
    }
        
    }


    

