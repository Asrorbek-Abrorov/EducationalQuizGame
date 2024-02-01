using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EducationalQuizGame.Uis;
using Newtonsoft.Json;
using Spectre.Console;

namespace EducationalQuizGame;

class Program
{
    static async Task Main(string[] args)
    {
        var mainUi = new MainUi();
        await mainUi.Run();
    }
}