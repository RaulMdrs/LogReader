using System;
using System.IO;

namespace Log {
    class Program
    {
        static void Main()
        {
            var option = "0";

            while (option != "5")
            {
                Console.Clear();
                Console.WriteLine("------------------------- Selecione uma opção -------------------------");
                Console.WriteLine("1 - Ler um arquivo");
                Console.WriteLine("2 - Ler todos os arquivos no diretório");
                Console.WriteLine("5 - Sair");

                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.WriteLine("Path do arquivo: ");
                        FileReader.ReadFile(Console.ReadLine());
                        break;
                    case "2":
                        Console.WriteLine("Qual o path do diretório? ");
                        FileReader.ReadFilesInDirectory(Console.ReadLine());
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}    
