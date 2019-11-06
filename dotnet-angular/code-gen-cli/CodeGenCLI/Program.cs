﻿using CodeGenCLI.CodeGenClasses;
using WebAPITemplates = CodeGenCLI.Templates.WebAPI;
using AngularTemplates = CodeGenCLI.Templates.Angular;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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

            App.Command("web-api", (generateCommand) => {
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
                    Console.WriteLine("### Generating Web API ###");

                    // Models
                    if (!Directory.Exists(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.ModelsPath) ? Config.WebAPI.ModelsPath : "Models")))
                    {
                        Directory.CreateDirectory(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.ModelsPath) ? Config.WebAPI.ModelsPath : "Models"));
                    }
                    foreach (CodeGenModel codeGenModel in Config.Models)
                    {
                        WebAPITemplates.ModelTemplate modelTemplate = new WebAPITemplates.ModelTemplate(Config, codeGenModel);
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
                        WebAPITemplates.ViewModelTemplate viewModelTemplate = new WebAPITemplates.ViewModelTemplate(Config, codeGenModel);
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
                    WebAPITemplates.DbContextTemplate dbContextTemplate = new WebAPITemplates.DbContextTemplate(Config);
                    string dbContextTemplateContent = dbContextTemplate.TransformText();

                    File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.DALPath) ? Config.WebAPI.DALPath : "DAL") + "\\" + Config.Name + "Context.cs", dbContextTemplateContent);
                    Console.WriteLine((!string.IsNullOrEmpty(Config.WebAPI.DALPath) ? Config.WebAPI.DALPath : "DAL") + "\\" + Config.Name + "Context.cs");

                    //// AutoMapper
                    WebAPITemplates.AutoMapperProfileTemplate autoMapperProfileTemplate = new WebAPITemplates.AutoMapperProfileTemplate(Config);
                    string autoMapperProfileTemplateContent = autoMapperProfileTemplate.TransformText();

                    File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.DALPath) ? Config.WebAPI.DALPath : "DAL") + "\\" + "AutoMapperProfile.cs", autoMapperProfileTemplateContent);
                    Console.WriteLine((!string.IsNullOrEmpty(Config.WebAPI.DALPath) ? Config.WebAPI.DALPath : "DAL") + "\\" + "AutoMapperProfile.cs");

                    //// Repositories
                    ////// Base
                    WebAPITemplates.RepositoryBaseTemplate repositoryBaseTemplate = new WebAPITemplates.RepositoryBaseTemplate(Config);
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
                        WebAPITemplates.RepositoryTemplate repositoryTemplate = new WebAPITemplates.RepositoryTemplate(Config, codeGenModel);
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
                        // Existing code
                        Dictionary<string, string> customBLLCodeBlocks = new Dictionary<string, string>();
                        if (File.Exists(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.BLLPath) ? Config.WebAPI.BLLPath : "BLL") + "\\" + codeGenModel.Name + "BLL.cs"))
                        {
                            string existingBLLTemplate = File.ReadAllText(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.BLLPath) ? Config.WebAPI.BLLPath : "BLL") + "\\" + codeGenModel.Name + "BLL.cs");
                            MatchCollection existingBLLCodeRegionMatches = Regex.Matches(existingBLLTemplate, @"#-#-#(.+?)#-#-#", RegexOptions.Singleline);
                            foreach (Match existingBLLCodeRegionMatch in existingBLLCodeRegionMatches)
                            {
                                customBLLCodeBlocks.Add(existingBLLCodeRegionMatch.Value.Substring(6, 38), existingBLLCodeRegionMatch.Value);
                            }
                        }

                        // Generate tempate
                        WebAPITemplates.BLLTemplate bllTemplate = new WebAPITemplates.BLLTemplate(Config, codeGenModel);
                        string bllTemplateContent = bllTemplate.TransformText();

                        // Replace custom code from existing code
                        MatchCollection bllCodeRegionMatches = Regex.Matches(bllTemplateContent, @"#-#-#(.+?)#-#-#", RegexOptions.Singleline);
                        foreach (Match bllCodeRegionMatch in bllCodeRegionMatches)
                        {
                            if (customBLLCodeBlocks.ContainsKey(bllCodeRegionMatch.Value.Substring(6, 38)))
                            {
                                bllTemplateContent = bllTemplateContent.Replace(bllCodeRegionMatch.Value, customBLLCodeBlocks.GetValueOrDefault(bllCodeRegionMatch.Value.Substring(6, 38)));
                            }
                        }

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
                        WebAPITemplates.ControllerTemplate controllerTemplate = new WebAPITemplates.ControllerTemplate(Config, codeGenModel);
                        string controllerTemplateContent = controllerTemplate.TransformText();

                        File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.ControllersPath) ? Config.WebAPI.ControllersPath : "Controllers") + "\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s") + "Controller.cs", controllerTemplateContent);
                        Console.WriteLine((!string.IsNullOrEmpty(Config.WebAPI.ControllersPath) ? Config.WebAPI.ControllersPath : "Controllers") + "\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s") + "Controller.cs");
                    }

                    // Startup
                    WebAPITemplates.StartupTemplate startupTemplate = new WebAPITemplates.StartupTemplate(Config);
                    string startupTemplateContent = startupTemplate.TransformText();

                    File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + "Startup.cs", startupTemplateContent);
                    Console.WriteLine("Startup.cs");

                    // TODO: Migrations?
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
            });

            App.Command("angular", (generateCommand) =>
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
                    Console.WriteLine("### Generating Angular ###");

                    // Models
                    if (!Directory.Exists(Config.Angular.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.Angular.ModelsPath) ? Config.Angular.ModelsPath : "src\\app\\shared\\models")))
                    {
                        Directory.CreateDirectory(Config.Angular.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.Angular.ModelsPath) ? Config.Angular.ModelsPath : "src\\app\\shared\\models"));
                    }
                    foreach (CodeGenModel codeGenModel in Config.Models)
                    {
                        AngularTemplates.ModelTemplate modelTemplate = new AngularTemplates.ModelTemplate(Config, codeGenModel);
                        string modelTemplateContent = modelTemplate.TransformText();

                        File.WriteAllText(Config.Angular.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.Angular.ModelsPath) ? Config.Angular.ModelsPath : "src\\app\\shared\\models") + "\\" + codeGenModel.Name + ".ts", modelTemplateContent);
                        Console.WriteLine((!string.IsNullOrEmpty(Config.Angular.ModelsPath) ? Config.Angular.ModelsPath : "src\\app\\shared\\models") + "\\" + codeGenModel.Name + ".ts");
                    }

                    // Services
                    if (!Directory.Exists(Config.Angular.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.Angular.ServicesPath) ? Config.Angular.ServicesPath : "src\\app\\shared\\services")))
                    {
                        Directory.CreateDirectory(Config.Angular.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.Angular.ServicesPath) ? Config.Angular.ServicesPath : "src\\app\\shared\\services"));
                    }
                    foreach (CodeGenModel codeGenModel in Config.Models)
                    {
                        AngularTemplates.DataServiceTemplate dataServiceTemplate = new AngularTemplates.DataServiceTemplate(Config, codeGenModel);
                        string dataServiceTemplateContent = dataServiceTemplate.TransformText();

                        File.WriteAllText(Config.Angular.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.Angular.ServicesPath) ? Config.Angular.ServicesPath : "src\\app\\shared\\services") + "\\" + codeGenModel.Name + "Service.ts", dataServiceTemplateContent);
                        Console.WriteLine((!string.IsNullOrEmpty(Config.Angular.ServicesPath) ? Config.Angular.ServicesPath : "src\\app\\shared\\services") + "\\" + codeGenModel.Name + "Service.ts");
                    }

                    // Modules

                    //// App
                    AngularTemplates.AppModuleTemplate appModuleTemplate = new AngularTemplates.AppModuleTemplate(Config);
                    string appModuleTemplateContent = appModuleTemplate.TransformText();

                    File.WriteAllText(Config.Angular.ProjectPath + "\\src\\app\\app.module.ts", appModuleTemplateContent);
                    Console.WriteLine("src\\app\\app.module.ts");

                    //// App Routing
                    AngularTemplates.AppRoutingModuleTemplate appRoutingModuleTemplate = new AngularTemplates.AppRoutingModuleTemplate(Config);
                    string appRoutingModuleTemplateContent = appRoutingModuleTemplate.TransformText();

                    File.WriteAllText(Config.Angular.ProjectPath + "\\src\\app\\app-routing.module.ts", appRoutingModuleTemplateContent);
                    Console.WriteLine("src\\app\\app-routing.module.ts");

                    //// Shared
                    AngularTemplates.SharedModuleTemplate sharedModuleTemplate = new AngularTemplates.SharedModuleTemplate(Config);
                    string sharedModuleTemplateContent = sharedModuleTemplate.TransformText();

                    File.WriteAllText(Config.Angular.ProjectPath + "\\src\\app\\shared\\shared.module.ts", sharedModuleTemplateContent);
                    Console.WriteLine("src\\app\\shared\\shared.module.ts");

                    // Components

                    //// Top navigation
                    if (!Directory.Exists(Config.Angular.ProjectPath + "\\src\\app\\shared\\top-nav"))
                    {
                        Directory.CreateDirectory("src\\app\\shared\\top-nav");
                    }

                    ////// HTML
                    AngularTemplates.TopNavComponentHTMLTemplate topNavComponentHTMLTemplate = new AngularTemplates.TopNavComponentHTMLTemplate(Config);
                    string topNavComponentHTMLTemplateContent = topNavComponentHTMLTemplate.TransformText();

                    File.WriteAllText(Config.Angular.ProjectPath + "\\src\\app\\shared\\top-nav\\top-nav.component.html", topNavComponentHTMLTemplateContent);
                    Console.WriteLine("src\\app\\shared\\top-nav\\top-nav.component.html");

                    ////// SCSS
                    AngularTemplates.TopNavComponentSCSSTemplate topNavComponentSCSSTemplate = new AngularTemplates.TopNavComponentSCSSTemplate(Config);
                    string topNavComponentSCSSTemplateContent = topNavComponentSCSSTemplate.TransformText();

                    File.WriteAllText(Config.Angular.ProjectPath + "\\src\\app\\shared\\top-nav\\top-nav.component.scss", topNavComponentSCSSTemplateContent);
                    Console.WriteLine("src\\app\\shared\\top-nav\\top-nav.component.scss");

                    ////// TS
                    AngularTemplates.TopNavComponentTSTemplate topNavComponentTSTemplate = new AngularTemplates.TopNavComponentTSTemplate(Config);
                    string topNavComponentTSTemplateContent = topNavComponentTSTemplate.TransformText();

                    File.WriteAllText(Config.Angular.ProjectPath + "\\src\\app\\shared\\top-nav\\top-nav.component.ts", topNavComponentTSTemplateContent);
                    Console.WriteLine("src\\app\\shared\\top-nav\\top-nav.component.ts");

                    // Per model
                    foreach (CodeGenModel codeGenModel in Config.Models)
                    {
                        if (!Directory.Exists(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower()))
                        {
                            Directory.CreateDirectory(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower());
                        }

                        //// Modules
                        AngularTemplates.DataModuleTemplate dataModuleTemplate = new AngularTemplates.DataModuleTemplate(Config, codeGenModel);
                        string dataModuleTemplateContent = dataModuleTemplate.TransformText();
                        
                        File.WriteAllText(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + ".module.ts", dataModuleTemplateContent);
                        Console.WriteLine("src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + ".module.ts");

                        AngularTemplates.DataRoutingModuleTemplate dataRoutingModuleTemplate = new AngularTemplates.DataRoutingModuleTemplate(Config, codeGenModel);
                        string dataRoutingModuleTemplateContent = dataRoutingModuleTemplate.TransformText();

                        File.WriteAllText(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "-routing.module.ts", dataRoutingModuleTemplateContent);
                        Console.WriteLine("src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "-routing.module.ts");

                        //// Components

                        ////// List
                        if (!Directory.Exists(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\list"))
                        {
                            Directory.CreateDirectory(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\list");
                        }

                        //////// HTML
                        AngularTemplates.DataListComponentHTMLTemplate dataListComponentHTMLTemplate = new AngularTemplates.DataListComponentHTMLTemplate(Config, codeGenModel);
                        string dataListComponentHTMLTemplateContent = dataListComponentHTMLTemplate.TransformText();

                        File.WriteAllText(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\list\\list.component.html", dataListComponentHTMLTemplateContent);
                        Console.WriteLine("src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\list\\list.component.html");

                        //////// SCSS
                        AngularTemplates.DataListComponentSCSSTemplate dataListComponentSCSSTemplate = new AngularTemplates.DataListComponentSCSSTemplate(Config, codeGenModel);
                        string dataListComponentSCSSTemplateContent = dataListComponentSCSSTemplate.TransformText();

                        File.WriteAllText(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\list\\list.component.scss", dataListComponentSCSSTemplateContent);
                        Console.WriteLine("src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\list\\list.component.scss");

                        //////// TS
                        AngularTemplates.DataListComponentTSTemplate dataListComponentTSTemplate = new AngularTemplates.DataListComponentTSTemplate(Config, codeGenModel);
                        string dataListComponentTSTemplateContent = dataListComponentTSTemplate.TransformText();

                        File.WriteAllText(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\list\\list.component.ts", dataListComponentTSTemplateContent);
                        Console.WriteLine("src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\list\\list.component.ts");

                        ////// Detail
                        if (!Directory.Exists(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\detail"))
                        {
                            Directory.CreateDirectory(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\detail");
                        }

                        //////// HTML
                        
                        // Existing code
                        Dictionary<string, string> customDataListComponentHTMLCodeBlocks = new Dictionary<string, string>();
                        if (File.Exists(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\detail\\detail.component.html"))
                        {
                            string existingDataListComponentHTMLTemplate = File.ReadAllText(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\detail\\detail.component.html");
                            MatchCollection existingDataListComponentHTMLCodeRegionMatches = Regex.Matches(existingDataListComponentHTMLTemplate, @"#-#-#(.+?)#-#-#", RegexOptions.Singleline);
                            foreach (Match existingDataListComponentHTMLCodeRegionMatch in existingDataListComponentHTMLCodeRegionMatches)
                            {
                                customDataListComponentHTMLCodeBlocks.Add(existingDataListComponentHTMLCodeRegionMatch.Value.Substring(6, 38), existingDataListComponentHTMLCodeRegionMatch.Value);
                            }
                        }

                        // Generate tempate
                        AngularTemplates.DataDetailComponentHTMLTemplate dataDetailComponentHTMLTemplate = new AngularTemplates.DataDetailComponentHTMLTemplate(Config, codeGenModel);
                        string dataDetailComponentHTMLTemplateContent = dataDetailComponentHTMLTemplate.TransformText();

                        // Replace custom code from existing code
                        MatchCollection dataListComponentHTMLCodeRegionMatches = Regex.Matches(dataDetailComponentHTMLTemplateContent, @"#-#-#(.+?)#-#-#", RegexOptions.Singleline);
                        foreach (Match dataListComponentHTMLCodeRegionMatch in dataListComponentHTMLCodeRegionMatches)
                        {
                            if (customDataListComponentHTMLCodeBlocks.ContainsKey(dataListComponentHTMLCodeRegionMatch.Value.Substring(6, 38)))
                            {
                                dataDetailComponentHTMLTemplateContent = dataDetailComponentHTMLTemplateContent.Replace(dataListComponentHTMLCodeRegionMatch.Value, customDataListComponentHTMLCodeBlocks.GetValueOrDefault(dataListComponentHTMLCodeRegionMatch.Value.Substring(6, 38)));
                            }
                        }

                        File.WriteAllText(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\detail\\detail.component.html", dataDetailComponentHTMLTemplateContent);
                        Console.WriteLine("src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\detail\\detail.component.html");

                        //////// SCSS
                        AngularTemplates.DataDetailComponentSCSSTemplate dataDetailComponentSCSSTemplate = new AngularTemplates.DataDetailComponentSCSSTemplate(Config, codeGenModel);
                        string dataDetailComponentSCSSTemplateContent = dataDetailComponentSCSSTemplate.TransformText();

                        File.WriteAllText(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\detail\\detail.component.scss", dataDetailComponentSCSSTemplateContent);
                        Console.WriteLine("src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\detail\\detail.component.scss");

                        //////// TS
                        AngularTemplates.DataDetailComponentTSTemplate dataDetailComponentTSTemplate = new AngularTemplates.DataDetailComponentTSTemplate(Config, codeGenModel);
                        string dataDetailComponentTSTemplateContent = dataDetailComponentTSTemplate.TransformText();

                        File.WriteAllText(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\detail\\detail.component.ts", dataDetailComponentTSTemplateContent);
                        Console.WriteLine("src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\detail\\detail.component.ts");

                        ////// Create
                        if (!Directory.Exists(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\create"))
                        {
                            Directory.CreateDirectory(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\create");
                        }

                        //////// HTML
                        AngularTemplates.DataCreateComponentHTMLTemplate dataCreateComponentHTMLTemplate = new AngularTemplates.DataCreateComponentHTMLTemplate(Config, codeGenModel);
                        string dataCreateComponentHTMLTemplateContent = dataCreateComponentHTMLTemplate.TransformText();

                        File.WriteAllText(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\create\\create.component.html", dataCreateComponentHTMLTemplateContent);
                        Console.WriteLine("src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\create\\create.component.html");

                        //////// SCSS
                        AngularTemplates.DataCreateComponentSCSSTemplate dataCreateComponentSCSSTemplate = new AngularTemplates.DataCreateComponentSCSSTemplate(Config, codeGenModel);
                        string dataCreateComponentSCSSTemplateContent = dataCreateComponentSCSSTemplate.TransformText();

                        File.WriteAllText(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\create\\create.component.scss", dataCreateComponentSCSSTemplateContent);
                        Console.WriteLine("src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\create\\create.component.scss");

                        //////// TS
                        AngularTemplates.DataCreateComponentTSTemplate dataCreateComponentTSTemplate = new AngularTemplates.DataCreateComponentTSTemplate(Config, codeGenModel);
                        string dataCreateComponentTSTemplateContent = dataCreateComponentTSTemplate.TransformText();

                        File.WriteAllText(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\create\\create.component.ts", dataCreateComponentTSTemplateContent);
                        Console.WriteLine("src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\create\\create.component.ts");
                    }

                    // Stop
                    Console.WriteLine("### DONE ###");
                    Console.WriteLine();

                    Console.WriteLine("### Dependencies ###");
                    Console.WriteLine("npm i --save @angular/cdk @angular/flex-layout");

                    return 0;
                });
            });

            //App.HelpOption("-x"); // top level help -- '-h | -? | --help would be consumed by 'dotnet run'
            App.Execute(args);
        }
    }
}
