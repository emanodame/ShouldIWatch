using FluentMetacritic.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShouldIWatch
{

    public class MoviePrint {
        public static int MetaCriticScore;
        public static decimal UserCriticScore;
        public static List<int> CriticRatingList = new List<int>();
        public static List<decimal> UserRatingList = new List<decimal>();
        //public static String FinalMetaCriticScore;
        //public static String[] Genres;
        //public static int GenreLength;


        public void Output( IEnumerable<IMovie> entities)
        {


            foreach (var entity in entities)
            {
    
                if (entity.CriticScore.HasValue)
                {
                    MetaCriticScore = entity.CriticScore.Value;
                }

                if (entity.UserScore.HasValue)
                {
                    UserCriticScore = entity.UserScore.Value;
                }

                CriticRatingList.Add(MetaCriticScore);
                UserRatingList.Add(UserCriticScore);
                //Genres = entity.Genres.ToArray();
                //GenreLength = Genres.Length;
                

            }
            //System.Console.WriteLine("Hello Hello");
            //System.Console.WriteLine(GenreLength);
            //System.Console.WriteLine(CriticRatingList[0]);

        }

        //FinalMetaCriticScore = string.Join(",", ratinglist);
        //System.Console.Write("here"+FinalMetaCriticScore);

    }

    }
