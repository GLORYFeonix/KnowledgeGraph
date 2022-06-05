using System.Diagnostics;

namespace Api.DAL
{
    public class NLP
    {
        public static void RunDeepKE()
        {
            // Set working directory and create process
            // var workingDirectory = Path.GetFullPath("Scripts");
            var workingDirectory = "C:/Users/gzy88/Desktop/csv/";
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    // FileName = "cmd.exe",
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    WorkingDirectory = workingDirectory
                }
            };
            process.Start();
            // Pass multiple commands to cmd.exe
            using (var sw = process.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    sw.WriteLine("conda activate deepke");
                    sw.WriteLine("cd C:/Users/gzy88/Desktop/DeepKE/example/ner/standard");
                    sw.WriteLine("python mine.py");
                    sw.WriteLine("cd C:/Users/gzy88/Desktop/DeepKE/example/re/standard");
                    sw.WriteLine("python mine.py");
                    sw.WriteLine("copy C:\\Users\\gzy88\\Desktop\\KnowledgeGraph\\API.DAL\\NLP\\data\\re_result.csv C:\\Users\\gzy88\\.Neo4jDesktop\\relate-data\\dbmss\\dbms-708cde5f-5be7-433b-9a62-bfd670fbe38e\\import\\single.csv /y");
                }
            }

            // read multiple output lines
            while (!process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();
                Console.WriteLine(line);
            }
        }
    }
}