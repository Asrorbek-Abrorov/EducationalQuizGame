using EducationalQuizGame.Extensions;
using Newtonsoft.Json;
using QuizApp;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EducationalQuizGame.Uis;

public partial class TextQuizesUi
{
    public async Task Run()
    {
        var keepRunning = true;
        while (keepRunning)
        {
            Console.Clear();

            var category = await GetQuizCategory();
            var questions = await FetchQuizQuestionsAsync(10, category, "easy");

            int correctAnswers = 0;

            foreach (var question in questions)
            {
                var choices = question.IncorrectAnswers;
                choices.Add(question.CorrectAnswer);

                if (choices.Count < 3)
                {
                    continue;
                }

                choices.Shuffle();
                Console.WriteLine();
                var selectedChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title(question.Question)
                        .PageSize(choices.Count)
                        .AddChoices(choices));

                if (selectedChoice == question.CorrectAnswer)
                {
                    AnsiConsole.MarkupLine("[green]Correct![/]");
                    correctAnswers++;
                }
                else
                {
                    AnsiConsole.MarkupLine($"[red]Incorrect! [/] The correct answer is:{question.CorrectAnswer}");
                }
            }
            await Console.Out.WriteLineAsync();
            AnsiConsole.MarkupLine($"[yellow]Your score:[/] [green]{correctAnswers}[/]/[red]{questions.Count(q => q.IncorrectAnswers.Count >= 3)}[/]");
            await Console.Out.WriteLineAsync();
            var exitChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Do you want to continue or exit?")
                    .PageSize(3)
                    .AddChoices("[green]Continue[/]", "[red]Exit[/]"));
            if (exitChoice == "[red]Exit[/]")
            {
                keepRunning = false;
            }
            await Console.Out.WriteLineAsync();
            await Console.Out.WriteLineAsync("Press any key to continue...");
            Console.ReadKey();
        }
    }

    private static async Task<int> GetQuizCategory()
    {
        var categories = await FetchQuizCategoriesAsync();
        var categoryChoices = new List<string>();
        var categoryIds = new List<int>();

        foreach (var category in categories)
        {
            categoryChoices.Add(category.Value);
            categoryIds.Add(category.Key);
        }

        var selectedCategory = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a Quiz Category")
                .PageSize(categoryChoices.Count)
                .AddChoices(categoryChoices));

        var selectedCategoryId = 0;
        foreach (var category in categories)
        {
            if (category.Value == selectedCategory)
            {
                selectedCategoryId = category.Key;
                break;
            }
        }
        return selectedCategoryId;
    }

    private static async Task<Dictionary<int, string>> FetchQuizCategoriesAsync()
    {
        using (var client = new HttpClient())
        {
            var apiUrl = "https://opentdb.com/api_category.php";
            var response = await client.GetStringAsync(apiUrl);
            var categoryResponse = JsonConvert.DeserializeObject<CategoryResponse>(response);

            var categoryDictionary = new Dictionary<int, string>();
            foreach (var category in categoryResponse.Categories)
            {
                categoryDictionary.Add(category.Id, category.Name);
            }

            return categoryDictionary;
        }
    }

    private static async Task<List<QuizQuestion>> FetchQuizQuestionsAsync(int amount, int category, string difficulty)
    {
        using (var client = new HttpClient())
        {
            var apiUrl = $"https://opentdb.com/api.php?amount={amount}&category={category}&difficulty={difficulty}";
            var response = await client.GetStringAsync(apiUrl);
            var quizResponse = JsonConvert.DeserializeObject<QuizResponse>(response);
            return quizResponse.Results;
        }
    }
}