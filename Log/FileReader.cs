using Log.Models;

namespace Log
{
    public static class FileReader
    {
        public static void ReadFilesInDirectory(string directoryPath = "/Users/raulmdrs/RiderProjects/Log/Log/Files")
        {
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    string[] files = Directory.GetFiles(directoryPath, "*.txt");

                    foreach (string file in files)
                    {
                        ReadFile(file);
                    }
                }
                else
                {
                    Console.WriteLine($"Diretório não encontrado: {directoryPath}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocorreu um erro:");
                Console.WriteLine(e.Message);
            }
        }

        public static void ReadFile(string filePath = "/Users/raulmdrs/RiderProjects/Log/Log/Files/log1.txt")
        {
            if (File.Exists(filePath))
            {
                Console.WriteLine($"------------- Lendo arquivo: {filePath} ----------------");

                using StreamReader reader = new StreamReader(filePath);
                string line;
                var lineCount = 0;
                var logEntries = 0;
                var errorEntries = 0;
                var startTime = DateTime.MinValue;
                var endTime = DateTime.MinValue;

                while ((line = reader.ReadLine()) != null)
                {
                    lineCount++;
                    if (!line.Equals(""))
                    {
                        logEntries++;
                        Console.WriteLine($"Linha {lineCount}: {line}");
                        if (line.Contains("ERRO"))
                        {
                            errorEntries++;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Linha {lineCount}: __________________________________");
                    }

                    if (line.Contains("Iniciando a aplicação"))
                    {
                        var parse = line.Split(" ", 3, StringSplitOptions.RemoveEmptyEntries);

                        if (parse.Length >= 2 &&
                            DateTime.TryParse($"{parse[0]} {parse[1]}", out DateTime parsedStartTime))
                        {
                            startTime = parsedStartTime;
                        }
                    }

                    if (line.Contains("Finalizando a aplicação"))
                    {
                        var parse = line.Split(" ", 3, StringSplitOptions.RemoveEmptyEntries);

                        if (parse.Length >= 2 &&
                            DateTime.TryParse($"{parse[0]} {parse[1]}", out DateTime parsedStartTime))
                        {
                            endTime = parsedStartTime;
                        }

                        break;
                    }
                }
                
                var result = new FileLogResult
                {
                    FilePath = filePath,
                    TotalLogs = logEntries,
                    TotalErrors = errorEntries,
                    StartTime = startTime,
                    EndTime = endTime
                };

                ShowResults(new List<FileLogResult> { result });
            }
            else
            {
                Console.WriteLine($"Arquivo não encontrado: {filePath}");
                Console.ReadKey();
                return;
            }
        }

        private static void ShowResults(List<FileLogResult> results)
        {
            foreach (var result in results)
            {
                Console.WriteLine($"Arquivo: {result.FilePath}");
                Console.WriteLine($"Total de logs: {result.TotalLogs}");
                Console.WriteLine($"Total de erros: {result.TotalErrors}");
                ExecutionTime(result.StartTime, result.EndTime);
                Console.WriteLine("______________________________________________\n");
                Console.ReadKey();
            }
        }

        private static void ExecutionTime(DateTime startTime, DateTime endTime)
        {
            if (startTime > endTime)
            {
                Console.WriteLine("A data de inicio é maior que a de fim.");
                return;
            }

            var executionTime = endTime - startTime;
            var days = executionTime.Days;
            var hours = executionTime.Hours;
            var minutes = executionTime.Minutes;
            var seconds = executionTime.Seconds;

            var result = "Tempo de execução: ";

            if (days > 0)
            {
                result += $"{days} dias, ";
            }

            if (hours > 0)
            {
                result += $"{hours} horas, ";
            }

            if (minutes > 0)
            {
                result += $"{minutes} minutos, ";
            }

            if (seconds > 0 || (hours == 0 && minutes == 0))
            {
                result += $"{seconds} segundos";
            }

            result = result.TrimEnd(',', ' ');

            Console.WriteLine(result);
        }
    }
}