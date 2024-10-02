using Files;
using Services;

var fileHandler = new FileHandler(100, 100000, new ConsoleMessageHandlerFactory());
fileHandler.CreateFiles(); 
await fileHandler.MergeInOneFileAsync("");  
