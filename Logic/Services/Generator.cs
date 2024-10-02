using System.Text;

namespace Services
{
    public class Generator
    {
        private readonly Random _random;
        public Generator()
        {
            _random = new Random(); 
        } 
        public string GenerateString()
        {
            var sb = new StringBuilder(); 
            sb.Append(GenerateRandomDate())
                .Append("||")
                .Append(Generate10LatinLetters())
                .Append("||")
                .Append(Generate10CyrillicLetters())
                .Append("||")
                .Append(GenerateEvenNumber())
                .Append("||")
                .Append(GenerateFractionalNumber()); 
            return sb.ToString(); 
        }
        private string GenerateRandomDate()
        {
            DateTime startDate = DateTime.Now.AddYears(-5); 
            DateTime randomDate = startDate.AddDays(_random.Next(0, 365*5+1)); 
            return randomDate.ToString("dd.MM.yyyy"); 
        }
        private string Generate10LatinLetters()
        {
            StringBuilder letters = new StringBuilder(); 
            for(int i = 0; i<10; i++)
            {
                char letter = (_random.Next(2)==0)?
                    (char)_random.Next('a', 'z'+1):
                    (char)_random.Next('A','Z'+1); 
                letters.Append(letter); 
            }
            return letters.ToString(); 
        }
        private string Generate10CyrillicLetters()
        {
            StringBuilder letters = new StringBuilder(); 
            for(int i = 0; i<10; i++)
            {
                char letter = (char)_random.Next('А','я'+1);
                letters.Append(letter); 
            }
            return letters.ToString(); 
        }
        private string GenerateEvenNumber()
        {
            
            return (_random.Next(1, 50000001)*2).ToString(); 
        }
        private string GenerateFractionalNumber()
        {
            return Math.Round(_random.NextDouble() * 19 + 1, 8).ToString();
        }
    }
}