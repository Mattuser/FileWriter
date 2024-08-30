using System.Text;

namespace WWF;
public class FileWriter
{
    public static void WriteToFileAtomically(string filePath, string content)
    {
        if (File.Exists(filePath))
        {
            TryWriteFileAtomically(filePath, content);   
        }
        else
        {
            Console.WriteLine("Arquivo não existe");
        }

    }

    public static void WriteToFileWithExceptionHandling(string filePath, string content)
    {
        FileStream fileStream = null;

        try
        {
            using (fileStream = new(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                byte[] contentBytes = Encoding.UTF8.GetBytes(content);

                fileStream.Write(contentBytes, 0, contentBytes.Length);
            };

        }
        catch (IOException ioEx) 
        {
            Console.WriteLine($"Erro de E/S: {ioEx.Message}");
            throw;
        }
        catch(UnauthorizedAccessException uaEx)
        {
            Console.WriteLine($"Acesso não autorizado: {uaEx.Message}");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Erro inesperado: {ex.Message}");
        }
        finally
        {
            fileStream?.Close();
        }
    }

    public void WriteToFileWithManualFlushing(string filePath, string[] lines)
    {
        int bufferSize = 4096;
    }

    private static void TryWriteFileAtomically(string filePath, string content)
    {
        string tempFilePath = filePath + ".tmp";
        try
        {
            File.WriteAllText(tempFilePath, content);

            File.Replace(tempFilePath, filePath, null);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao escrever no arquivo: {ex.Message}");
        }
        finally
        {
            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath);
            }
        }
    }

}
