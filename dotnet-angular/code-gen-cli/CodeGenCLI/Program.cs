using CodeGenCLI.CodeGenClasses;
using CodeGenCLI.Templates;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
                        if (!Directory.Exists(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.ModelsPath) ? Config.WebAPI.ModelsPath : "Models")))
                        {
                            Directory.CreateDirectory(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.ModelsPath) ? Config.WebAPI.ModelsPath : "Models"));
                        }
                        foreach (CodeGenModel codeGenModel in Config.Models)
                        {
                            ModelTemplate modelTemplate = new ModelTemplate(Config, codeGenModel);
                            string modelTemplateContent = modelTemplate.TransformText();

                            // TODO: For all file writes?
                            //if (!File.Exists(...path...) || Config.Override)
                            File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.ModelsPath) ? Config.WebAPI.ModelsPath : "Models") + "\\" + codeGenModel.Name + ".cs", modelTemplateContent);
                            Console.WriteLine((!string.IsNullOrEmpty(Config.WebAPI.ModelsPath) ? Config.WebAPI.ModelsPath : "Models") + "\\" + codeGenModel.Name + ".cs");
                        }

                        // ViewModels
                        if (!Directory.Exists(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.ViewModelsPath) ? Config.WebAPI.ViewModelsPath : "ViewModels")))
                        {
                            Directory.CreateDirectory(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.ViewModelsPath) ? Config.WebAPI.ViewModelsPath : "ViewModels"));
                        }
                        foreach (CodeGenModel codeGenModel in Config.Models.Where(m => !m.ManyToMany))
                        {
                            ViewModelTemplate viewModelTemplate = new ViewModelTemplate(Config, codeGenModel);
                            string viewModelTemplateContent = viewModelTemplate.TransformText();

                            File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.ViewModelsPath) ? Config.WebAPI.ViewModelsPath : "ViewModels") + "\\" + codeGenModel.Name + "VM.cs", viewModelTemplateContent);
                            Console.WriteLine((!string.IsNullOrEmpty(Config.WebAPI.ViewModelsPath) ? Config.WebAPI.ViewModelsPath : "ViewModels") + "\\" + codeGenModel.Name + "VM.cs");
                        }

                        // DAL
                        if (!Directory.Exists(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.DALPath) ? Config.WebAPI.DALPath : "DAL")))
                        {
                            Directory.CreateDirectory(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.DALPath) ? Config.WebAPI.DALPath : "DAL"));
                        }

                        //// DbContext
                        DbContextTemplate dbContextTemplate = new DbContextTemplate(Config);
                        string dbContextTemplateContent = dbContextTemplate.TransformText();
                        
                        File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.DALPath) ? Config.WebAPI.DALPath : "DAL") + "\\" + Config.Name + "Context.cs", dbContextTemplateContent);
                        Console.WriteLine((!string.IsNullOrEmpty(Config.WebAPI.DALPath) ? Config.WebAPI.DALPath : "DAL") + "\\" + Config.Name + "Context.cs");

                        //// AutoMapper
                        AutoMapperProfileTemplate autoMapperProfileTemplate = new AutoMapperProfileTemplate(Config);
                        string autoMapperProfileTemplateContent = autoMapperProfileTemplate.TransformText();

                        File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.DALPath) ? Config.WebAPI.DALPath : "DAL") + "\\" + "AutoMapperProfile.cs", autoMapperProfileTemplateContent);
                        Console.WriteLine((!string.IsNullOrEmpty(Config.WebAPI.DALPath) ? Config.WebAPI.DALPath : "DAL") + "\\" + "AutoMapperProfile.cs");

                        //// Repositories
                        ////// Base
                        RepositoryBaseTemplate repositoryBaseTemplate = new RepositoryBaseTemplate(Config);
                        string repositoryBaseTemplateContent = repositoryBaseTemplate.TransformText();

                        File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.DALPath) ? Config.WebAPI.DALPath : "DAL") + "\\" + "Repository.cs", repositoryBaseTemplateContent);
                        Console.WriteLine((!string.IsNullOrEmpty(Config.WebAPI.DALPath) ? Config.WebAPI.DALPath : "DAL") + "\\" + "Repository.cs");

                        ////// Per model
                        if (!Directory.Exists(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.DALPath) ? Config.WebAPI.DALPath : "DAL") + "\\" + "Repositories"))
                        {
                            Directory.CreateDirectory(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.DALPath) ? Config.WebAPI.DALPath : "DAL") + "\\" + "Repositories");
                        }
                        foreach (CodeGenModel codeGenModel in Config.Models)
                        {
                            RepositoryTemplate repositoryTemplate = new RepositoryTemplate(Config, codeGenModel);
                            string repositoryTemplateContent = repositoryTemplate.TransformText();

                            File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.DALPath) ? Config.WebAPI.DALPath : "DAL") + "\\" + "Repositories" + "\\" + codeGenModel.Name + "Repository.cs", repositoryTemplateContent);
                            Console.WriteLine((!string.IsNullOrEmpty(Config.WebAPI.DALPath) ? Config.WebAPI.DALPath : "DAL") + "\\" + "Repositories" + "\\" + codeGenModel.Name + "Repository.cs");
                        }

                        // BLL
                        if (!Directory.Exists(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.BLLPath) ? Config.WebAPI.BLLPath : "BLL")))
                        {
                            Directory.CreateDirectory(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.BLLPath) ? Config.WebAPI.BLLPath : "BLL"));
                        }
                        foreach (CodeGenModel codeGenModel in Config.Models.Where(m => !m.ManyToMany))
                        {
                            // Existing template
                            List<string> customBLLCodeBlocks = new List<string>();
                            if (File.Exists(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.BLLPath) ? Config.WebAPI.BLLPath : "BLL") + "\\" + codeGenModel.Name + "BLL.cs"))
                            {
                                string existingBLLTemplate = File.ReadAllText(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.BLLPath) ? Config.WebAPI.BLLPath : "BLL") + "\\" + codeGenModel.Name + "BLL.cs");

                            }

                            BLLTemplate bllTemplate = new BLLTemplate(Config, codeGenModel);
                            string bllTemplateContent = bllTemplate.TransformText();

                            File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.BLLPath) ? Config.WebAPI.BLLPath : "BLL") + "\\" + codeGenModel.Name + "BLL.cs", bllTemplateContent);
                            Console.WriteLine((!string.IsNullOrEmpty(Config.WebAPI.BLLPath) ? Config.WebAPI.BLLPath : "BLL") + "\\" + codeGenModel.Name + "BLL.cs");
                        }

                        // Controllers
                        if (!Directory.Exists(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.ControllersPath) ? Config.WebAPI.ControllersPath : "Controllers")))
                        {
                            Directory.CreateDirectory(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.ControllersPath) ? Config.WebAPI.ControllersPath : "Controllers"));
                        }
                        foreach (CodeGenModel codeGenModel in Config.Models.Where(m => !m.ManyToMany))
                        {
                            ControllerTemplate controllerTemplate = new ControllerTemplate(Config, codeGenModel);
                            string controllerTemplateContent = controllerTemplate.TransformText();

                            File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.ControllersPath) ? Config.WebAPI.ControllersPath : "Controllers") + "\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s") + "Controller.cs", controllerTemplateContent);
                            Console.WriteLine((!string.IsNullOrEmpty(Config.WebAPI.ControllersPath) ? Config.WebAPI.ControllersPath : "Controllers") + "\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s") + "Controller.cs");
                        }

                        // Startup
                        StartupTemplate startupTemplate = new StartupTemplate(Config);
                        string startupTemplateContent = startupTemplate.TransformText();

                        File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + "Startup.cs", startupTemplateContent);
                        Console.WriteLine("Startup.cs");

                        // TODO: Migrations
                        //ProcessStartInfo removeInitialMigration = new ProcessStartInfo("Remove-Migration");
                        //removeInitialMigration.WorkingDirectory = Config.WebAPI.ProjectPath;
                        //Process.Start(removeInitialMigration);

                        //ProcessStartInfo addInitialMigration = new ProcessStartInfo("Add-Migration");
                        //addInitialMigration.Arguments = @"Initial";
                        //addInitialMigration.WorkingDirectory = Config.WebAPI.ProjectPath;
                        //Process.Start(addInitialMigration);

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
