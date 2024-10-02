using Entities;
using Files;
using Services;

var entity = new GeneratedStringEntity("new Generator().GenerateString()");

System.Console.WriteLine(entity);

// var fileHandler = new FileHandler(100, 100000, new ConsoleMessageHandlerFactory());
// fileHandler.CreateFiles(); 
// await fileHandler.MergeInOneFileAsync("");  
