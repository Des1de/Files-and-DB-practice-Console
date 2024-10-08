using System.Collections.Concurrent;
using System.Text;
using Interfaces;
using Services;

namespace Files
{
    public class FileHandler
    {
        private const string DIRECTORY_PATH_GENERATED = "content/generated/";
        private const string MAIN_FILE_PATH = "content/main.txt";
        private readonly int _fileNumber;
        private readonly int _stringNumber;
        private readonly IMessageHandlerFactory _messageHandlerFactory; 
        public FileHandler(int fileNumber, int stringNubmer, IMessageHandlerFactory messageHandlerFactory)
        {
            _messageHandlerFactory = messageHandlerFactory; 
            _fileNumber = fileNumber; 
            _stringNumber = stringNubmer; 
        }
        public async Task MergeInOneFileAsync(string deleteSubstring)
        {
            string[] files = Directory.GetFiles(DIRECTORY_PATH_GENERATED, "*.txt");
            if(files.Length == 0) throw new Exception("No files to merge"); 
            var linesToWrite = new ConcurrentBag<string>();
            _messageHandlerFactory.CreateMessageHandler().SendMessage("Merge started"); 
            await Task.WhenAll(files.Select(async filePath =>
            {
                var lines = await File.ReadAllLinesAsync(filePath);
                var newFileLines = new List<string>(); 
                int deletionCount = 0; 
                foreach (var line in lines)
                {
                    if (!line.Contains(deleteSubstring) || String.Equals(deleteSubstring, ""))
                    {
                        linesToWrite.Add(line);
                        newFileLines.Add(line); 
                    }
                    else
                    {
                        deletionCount++; 
                    }
                }
                await File.WriteAllLinesAsync(filePath, newFileLines); 
                _messageHandlerFactory.CreateMessageHandler().SendMessage($"File {filePath}: deleted {deletionCount} strings"); 
            }));
            File.Delete(MAIN_FILE_PATH);
            await File.WriteAllLinesAsync(MAIN_FILE_PATH, linesToWrite);
            _messageHandlerFactory.CreateMessageHandler().SendMessage("Merge ended"); 
        }
        public void CreateFiles()
        {
            Directory.CreateDirectory(DIRECTORY_PATH_GENERATED);
            _messageHandlerFactory.CreateMessageHandler().SendMessage("Generating files...");
            var tasks = new Task[_fileNumber];
            for (int i = 0; i < _fileNumber; i++)
            {
                tasks[i] = new Task(CreateFile, _stringNumber);
                tasks[i].Start();
            }
            Task.WaitAll(tasks);
            _messageHandlerFactory.CreateMessageHandler().SendMessage("Files were generated successfully");
        }

        private void CreateFile(object? stringNumber)
        {
            if(stringNumber is int v)
            {
                var generator = new Generator(); 
                string? fileName = Task.CurrentId.ToString();
                using (var fs = new FileStream($"{DIRECTORY_PATH_GENERATED}{fileName}.txt", FileMode.Create)){
                    for (int i = 0; i < v; i++)
                    {
                        AddText(fs, generator.GenerateString()+"\n");
                    }
                }
                _messageHandlerFactory.CreateMessageHandler().SendMessage($"File {fileName}.txt was created successfully");
            }
            else 
            {
                throw new Exception("Parameter stringNumber should be int."); 
            }
        }

        public async Task<List<string>> GetStringsFromFile(string filePath)
        {
            try
            {
                var strings = (await File.ReadAllLinesAsync(filePath)).ToList(); 
                return strings; 
            }
            catch
            {
                _messageHandlerFactory.CreateMessageHandler().SendMessage("Cant open file");
                return new List<string>(); 
            }
        }

        private void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info);
        }
    }
}