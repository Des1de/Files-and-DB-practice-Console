using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Entities
{
    public class GeneratedStringEntity
    {
        public DateTime Date { get; set; }
        public string Latin { get; set; } = null!;
        public string Cyrillic { get; set; } = null!; 
        public int EvenNumber { get; set; }
        public double Fractional { get; set; }
        
        private const string PATTERN = @"^(?<date>\d{2}\.\d{2}\.\d{4})\|\|(?<latin>[^\|]+)\|\|(?<cyrillic>[^\|]+)\|\|(?<evennumber>\d+)\|\|(?<fractional>[\d,]+)\|\|$";

        public GeneratedStringEntity(string GeneratedString)
        {
            GeneratedString.Trim('\n'); 
            var regex = new Regex(PATTERN); 
            var match = regex.Match(GeneratedString); 
            if(match.Success)
            {
                Date = DateTime.Parse(match.Groups["date"].Value);
                Latin = match.Groups["latin"].Value; 
                Cyrillic = match.Groups["cyrillic"].Value; 
                EvenNumber = int.Parse(match.Groups["evennumber"].Value);
                Fractional = double.Parse(match.Groups["fractional"].Value);
            }
            else 
            {
                throw new Exception("WrongStringFormat"); 
            }
        }

        public override string ToString()
        {
            return $"{Date.ToString("dd.MM.yyyy")}||{Latin}||{Cyrillic}||{EvenNumber}||{Fractional}||";
        }
    }
}