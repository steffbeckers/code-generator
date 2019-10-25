using System;
using System.IO;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;

namespace CodeGenCLI
{
    class Program
    {
        static CommandLineApplication App { get; set; }
        static Config Config { get; set; }

        static void Main(string[] args)
        {
            // App
            App = new CommandLineApplication(throwOnUnexpectedArg: false);

            // Configuration
            IConfiguration codeGenConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("code-gen-config.json", false, true)
                .Build();
            Config = codeGenConfig.Get<Config>();

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
                        Console.WriteLine("### Config ###");
                        Console.WriteLine("Name:\t\t" + Config.Name);
                        Console.WriteLine("Description:\t" + Config.Description);
                        Console.WriteLine("Override:\t" + Config.Override);
                        Console.WriteLine("##############");



                        // Stop
                        return 0;
                    });
                }
            );

            //App.HelpOption("-x"); // top level help -- '-h | -? | --help would be consumed by 'dotnet run'
            App.Execute(args);
        }
    }
}
