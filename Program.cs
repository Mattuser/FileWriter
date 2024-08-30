using WWF;

var currentDirectory = Directory.GetCurrentDirectory();
var path = Path.Combine(currentDirectory, "file.txt");

FileWriter.WriteToFileWithExceptionHandling(path, "Conteudo...");
FileWriter.WriteToFileAtomically(path, "Conteudo novo");