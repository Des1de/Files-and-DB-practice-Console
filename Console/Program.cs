using Repositories;

var generatedStringRepository = new GeneratedStringRepository(); 
var strings = await generatedStringRepository.GetGeneratedStrings(); 
foreach(var str in strings)
{
    System.Console.WriteLine(str);
}
System.Console.WriteLine(await generatedStringRepository.GetEvenNumbersSum());

System.Console.WriteLine(await generatedStringRepository.GetFratcionalsMedian());