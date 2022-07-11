using System.Collections.Generic;

namespace marvel.Models
{
    public class CharacterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Modified { get; set; }
        public List<string> UrlDetails { get; set; }
        public List<SeriesModel> Series { get; set; }
    }
}
