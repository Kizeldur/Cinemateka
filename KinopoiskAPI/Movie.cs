using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinopoiskAPI
{
    public class Movie
    {
        public string Id { get; set; }
        public string Id_Kinopoisk { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Title_Alternative { get; set; }
        public string Description { get; set; }
        public string Producers { get; set; }
        public string Rating_Kinopoisk { get; set; }
        public string Rating_IMDb { get; set; }
    }
}
