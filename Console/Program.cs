using Files;
using Repositories;
using Services;

class Program
{
    private const string MENU = 
        "1. Merge generated files\n" + 
        "2. Import file's strings in database\n" + 
        "3. Get sum of even numbers\n" + 
        "4. Get median of fractional numbers\n" +
        "5. Exit\n"; 

    static async Task Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var fileHandler = new FileHandler(100, 100000, new ConsoleMessageHandlerFactory()); 
        var repository = new GeneratedStringRepository(new ConsoleMessageHandlerFactory()); 

        int selectedMenuItem;
        bool isExitRequested = false;  
        fileHandler.CreateFiles();

        while(!isExitRequested)
        {
            try
            {
                System.Console.WriteLine(MENU);
                selectedMenuItem = int.Parse(Console.ReadLine()); 
                switch(selectedMenuItem)
                {
                    case 1: 
                        System.Console.WriteLine("Enter substring for deletion or leave it empty if no deletion is required:");
                        var deleteSubstring = Console.ReadLine(); 
                        await fileHandler.MergeInOneFileAsync((deleteSubstring == null)?"":deleteSubstring);
                        break; 
                    case 2: 
                        System.Console.WriteLine("Enter file path (better use files from content folders):");
                        var filePath = Console.ReadLine();
                        await repository.InsertGeneratedStrings(
                            await fileHandler.GetStringsFromFile(filePath==null?"":filePath)
                        );
                        break;
                    case 3:
                        System.Console.WriteLine($"Sum of even numbers: {await repository.GetEvenNumbersSum()}");
                        break;
                    case 4: 
                        System.Console.WriteLine($"Median of fractional numbers: {await repository.GetFratcionalsMedian()}");
                        break; 
                    case 5: 
                        System.Console.WriteLine("End of work");
                        isExitRequested = true;
                        break; 
                    default:
                        System.Console.WriteLine("Wrong menu item");
                        break;
                }
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}