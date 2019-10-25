using CodeGenCLI.CodeGenClasses;
using CodeGenCLI.Templates;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace CodeGenCLI
{
    class Program
    {
        static CommandLineApplication App { get; set; }
        static CodeGenConfig Config { get; set; }

        static void Main(string[] args)
        {
            // App
            App = new CommandLineApplication(throwOnUnexpectedArg: false);

            // Configuration
            IConfiguration codeGenConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("code-gen-config.json", false, true)
                .Build();
            Config = codeGenConfig.Get<CodeGenConfig>();

            App.Command("generate",
                (generateCommand) =>
                {
                    // Arguments
                    //CommandArgument name = generateCommand.Argument(
                    //    "name",
                    //    "Enter the name of the application"
                    //);
                    //CommandOption greeting = codeGenCommand.Option(
                    //    "-$|-g |--greeting <greeting>",
                    //    "The greeting to display. The greeting supports"
                    //    + " a format string where {fullname} will be "
                    //    + "substituted with the full name.",
                    //    CommandOptionType.SingleValue);
                    //CommandOption uppercase = codeGenCommand.Option(
                    //    "-u | --uppercase", "Display the greeting in uppercase.",
                    //    CommandOptionType.NoValue);

                    // Help
                    generateCommand.HelpOption("-h | -? | --help");

                    generateCommand.OnExecute(() =>
                    {
                        Console.WriteLine("### Generating ###");

                        // Models
                        foreach (CodeGenModel codeGenModel in Config.Models)
                        {
                            ModelTemplate modelTemplate = new ModelTemplate(Config, codeGenModel);
                            string modelTemplateContent = modelTemplate.TransformText();
                            File.WriteAllText(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "../../../../Generated/Models/" + codeGenModel.Name + ".cs", modelTemplateContent);
                            Console.WriteLine("Models/" + codeGenModel.Name + ".cs");
                        }

                        // Stop
                        Console.WriteLine("### DONE ###");
                        return 0;
                    });
                }
            );

            //App.HelpOption("-x"); // top level help -- '-h | -? | --help would be consumed by 'dotnet run'
            App.Execute(args);
        }
    }
}
