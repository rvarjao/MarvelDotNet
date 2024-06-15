using System.Text.Json.Nodes;

namespace Marvel.Models.Marvel
{
    public class Character
    {

        public int id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public Dictionary<string, string>? thumbnail { get; set; }

        public Character()
        {

        }

        // set thumbnail
        public string thumbnailUrl()
        {
            if (thumbnail == null)
            {
                return "no-image.png";
            }
            return thumbnail["path"] + "." + thumbnail["extension"];
        }
    }
}
