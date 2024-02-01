using Newtonsoft.Json;
using QuizApp;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalQuizGame.Uis;

public class MainUi
{
    public async Task Run()
    {
        var textQuizes = new TextQuizesUi();
        var keepRunning = true;
        while (keepRunning)
        {
            Console.Clear();

            var selectedChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("* Main Ui *")
                    .PageSize(4)
                    .AddChoices(new[]
                    {
                    "Information", "Text based Questions", "Exit"
                    }));

            switch (selectedChoice)
            {
                case "Information":
                    Console.WriteLine("Displaying information...");
                    await Console.Out.WriteLineAsync();
                    await Console.Out.WriteLineAsync("This is Console Aplication which uses API to create questions,\nthis game has a lot of different types of questions. \nThe GAME is TEXT BASED!");
                    break;
                case "Text based Questions":
                    await textQuizes.Run();
                    break;
                case "Exit":
                    keepRunning = false;
                    break;
            }
            await Console.Out.WriteLineAsync();
            await Console.Out.WriteLineAsync("Enter to continue...");
            Console.ReadKey();
        }
    }
}
