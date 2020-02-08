using CodeGenCLI.CodeGenClasses;
using WebAPITemplates = CodeGenCLI.Templates.WebAPI;
using AngularTemplates = CodeGenCLI.Templates.Angular;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using CodeGenCLI.Extensions;
using System.Globalization;
using System.Threading;
using System.Diagnostics;

namespace CodeGenCLI
{
    class Program
    {
        static CommandLineApplication App { get; set; }
        static CodeGenConfig Config { get; set; }
        static TextInfo TextInfo = new CultureInfo("en-US", false).TextInfo;

        static void Main(string[] args)
        {
            Console.WriteLine("### CodeGenCLI - " + DateTime.Now.ToString("s", CultureInfo.InvariantCulture) + " ###");
            Console.WriteLine();

            // App
            App = new CommandLineApplication(throwOnUnexpectedArg: false);

            // Configuration
            IConfiguration codeGenConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("code-gen-config.json", false, true)
                .Build();
            Config = codeGenConfig.Get<CodeGenConfig>();

            App.Command("config", (generateCommand) => {
                // Arguments
                //CommandArgument name = generateCommand.Argument(
                //    "name",
                //    "Enter the name of the application"
                //);
                //CommandOption greeting = generateCommand.Option(
                //    "-$|-g |--greeting <greeting>",
                //    "The greeting to display. The greeting supports"
                //    + " a format string where {fullname} will be "
                //    + "substituted with the full name.",
                //    CommandOptionType.SingleValue);
                //CommandOption uppercase = generateCommand.Option(
                //    "-u | --uppercase", "Display the greeting in uppercase.",
                //    CommandOptionType.NoValue);

                CommandOption databaseType = generateCommand.Option(
                    "-db |--database-type <type>",
                    "Database type",
                    CommandOptionType.SingleValue
                );

                // Help
                generateCommand.HelpOption("-h | -? | --help");

                generateCommand.OnExecute(() =>
                {
                    Console.WriteLine("### Generating code-gen-config.json ###");

                    // New code-gen-config.json file
                    CodeGenConfig newCodeGenConfig = new CodeGenConfig() {
                        // Default values
                        Name = Config.Name ?? "Generated",
                        Description = Config.Description,
                        Override = Config.Override,
                        Models = new List<CodeGenModel>(),
                        WebAPI = Config.WebAPI ?? new CodeGenConfigWebAPI(),
                        Angular = Config.Angular ?? new CodeGenConfigAngular()
                    };

                    // Database type
                    if (databaseType.Values.Count > 0)
                    {
                        Console.WriteLine("Scaffolding config based on database schema");

                        // Tables
                        List<string> databaseTableNames = new List<string>();
                        Dictionary<string, List<DataRow>> databaseTableColumns = new Dictionary<string, List<DataRow>>();

                        if (databaseType.Values[0].ToString() == "mssql")
                        {
                            // MS SQL Server
                            Console.WriteLine("Database type: MS SQL Server");

                            using (SqlConnection databaseConnection = new SqlConnection(Config.WebAPI.DatabaseConnection))
                            {
                                // Connect
                                databaseConnection.Open();

                                // Retrieve table information
                                DataTable tablesDataTable = databaseConnection.GetSchema("Tables", new string[] { null, null, null, "BASE TABLE" });

                                foreach (DataRow tablesDataTableDataRow in tablesDataTable.Rows)
                                {
                                    databaseTableNames.Add(tablesDataTableDataRow["TABLE_NAME"].ToString());
                                    databaseTableColumns.Add(tablesDataTableDataRow["TABLE_NAME"].ToString(), new List<DataRow>());
                                }

                                // Retrieve column information for each table
                                foreach (string databaseTableName in databaseTableNames)
                                {
                                    DataTable columnsDataTable = databaseConnection.GetSchema("Columns", new string[] { null, null, databaseTableName });

                                    foreach (DataRow columnsDataTableDataRow in columnsDataTable.Rows)
                                    {
                                        databaseTableColumns[databaseTableName].Add(columnsDataTableDataRow);
                                    }
                                }
                            }
                        }

                        // Tables

                        // Exclude __EFMigrationsHistory table
                        databaseTableNames.Remove("__EFMigrationsHistory");

                        Console.WriteLine("Found tables:");
                        foreach (string databaseTableName in databaseTableNames)
                        {
                            Console.WriteLine(databaseTableName);

                            CodeGenModel newCodeGenModel = new CodeGenModel()
                            {
                                Name = TextInfo.ToTitleCase(databaseTableName).ToSingular(),
                                NamePlural = databaseTableName,
                                Properties = new List<CodeGenModelProperty>()
                            };

                            List<CodeGenModelProperty> newCodeGenModelProperties =
                                databaseTableColumns[databaseTableName].Select(dtc => new CodeGenModelProperty() {
                                    Name = dtc[3].ToString(),
                                    Type = dtc[7].ToString().ToCSharpDataType(),
                                    //Required = dtc[6] // TODO
                                }).ToList();

                            newCodeGenModel.Properties = newCodeGenModelProperties;
                            newCodeGenConfig.Models.Add(newCodeGenModel);
                        }
                    }

                    // Save the new code-gen-config.json
                    string newCodeGenConfigAsJson = JsonConvert.SerializeObject(newCodeGenConfig, Formatting.Indented);
                    File.WriteAllText("code-gen-config.g.json", newCodeGenConfigAsJson);

                    // Stop
                    Console.WriteLine("### DONE ###");
                    return 0;
                });
            });

            App.Command("web-api", (generateCommand) => {
                // Arguments
                //CommandArgument name = generateCommand.Argument(
                //    "name",
                //    "Enter the name of the application"
                //);
                //CommandOption greeting = generateCommand.Option(
                //    "-$|-g |--greeting <greeting>",
                //    "The greeting to display. The greeting supports"
                //    + " a format string where {fullname} will be "
                //    + "substituted with the full name.",
                //    CommandOptionType.SingleValue);
                //CommandOption uppercase = generateCommand.Option(
                //    "-u | --uppercase", "Display the greeting in uppercase.",
                //    CommandOptionType.NoValue);

                // Help
                generateCommand.HelpOption("-h | -? | --help");

                generateCommand.OnExecute(() =>
                {
                    // Git
                    Console.WriteLine("### git add . ###");
                    ProcessStartInfo gitAddBefore = new ProcessStartInfo("git");
                    gitAddBefore.Arguments = "add .";
                    gitAddBefore.WorkingDirectory = Config.WebAPI.ProjectPath;
                    Process.Start(gitAddBefore).WaitForExit();

                    Console.WriteLine("### git commit -m \"Save before generating Web API\" ###");
                    ProcessStartInfo gitCommitBefore = new ProcessStartInfo("git");
                    gitCommitBefore.Arguments = "commit -m \"Save before generating Web API\"";
                    gitCommitBefore.WorkingDirectory = Config.WebAPI.ProjectPath;
                    Process.Start(gitCommitBefore).WaitForExit();

                    Console.WriteLine();
                    Console.WriteLine("### Generating Web API ###");

                    // Startup
                    WebAPITemplates.AppSettingsTemplate appSettingsTemplate = new WebAPITemplates.AppSettingsTemplate(Config);
                    string appSettingsTemplateContent = appSettingsTemplate.TransformText();

                    File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + "appsettings.json", appSettingsTemplateContent);
                    Console.WriteLine("appsettings.json");

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
                        WebAPITemplates.Controllers.ControllerTemplate controllerTemplate = new WebAPITemplates.Controllers.ControllerTemplate(Config, codeGenModel);
                        string controllerTemplateContent = controllerTemplate.TransformText();

                        File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.ControllersPath) ? Config.WebAPI.ControllersPath : "Controllers") + "\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s") + "Controller.cs", controllerTemplateContent);
                        Console.WriteLine((!string.IsNullOrEmpty(Config.WebAPI.ControllersPath) ? Config.WebAPI.ControllersPath : "Controllers") + "\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s") + "Controller.cs");
                    }

                    //// AuthController
                    WebAPITemplates.Controllers.AuthControllerTemplate authControllerTemplate = new WebAPITemplates.Controllers.AuthControllerTemplate(Config);
                    string authControllerTemplateContent = authControllerTemplate.TransformText();

                    File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.ControllersPath) ? Config.WebAPI.ControllersPath : "Controllers") + "\\AuthController.cs", authControllerTemplateContent);
                    Console.WriteLine((!string.IsNullOrEmpty(Config.WebAPI.ControllersPath) ? Config.WebAPI.ControllersPath : "Controllers") + "\\AuthController.cs");

                    // Startup
                    WebAPITemplates.StartupTemplate startupTemplate = new WebAPITemplates.StartupTemplate(Config);
                    string startupTemplateContent = startupTemplate.TransformText();

                    File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + "Startup.cs", startupTemplateContent);
                    Console.WriteLine("Startup.cs");

                    // GraphQL
                    if (!Directory.Exists(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.GraphQLPath) ? Config.WebAPI.GraphQLPath : "GraphQL")))
                    {
                        Directory.CreateDirectory(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.GraphQLPath) ? Config.WebAPI.GraphQLPath : "GraphQL"));
                    }

                    //// Schema
                    WebAPITemplates.GraphQL.SchemaTemplate graphQLSchemaTemplate = new WebAPITemplates.GraphQL.SchemaTemplate(Config);
                    string graphQLSchemaTemplateContent = graphQLSchemaTemplate.TransformText();

                    File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.GraphQLPath) ? Config.WebAPI.GraphQLPath : "GraphQL") + "\\" + Config.Name + "Schema.cs", graphQLSchemaTemplateContent);
                    Console.WriteLine((!string.IsNullOrEmpty(Config.WebAPI.GraphQLPath) ? Config.WebAPI.GraphQLPath : "GraphQL") + "\\" + Config.Name + "Schema.cs");

                    //// Query
                    WebAPITemplates.GraphQL.QueryTemplate graphQLQueryTemplate = new WebAPITemplates.GraphQL.QueryTemplate(Config);
                    string graphQLQueryTemplateContent = graphQLQueryTemplate.TransformText();

                    File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.GraphQLPath) ? Config.WebAPI.GraphQLPath : "GraphQL") + "\\" + Config.Name + "Query.cs", graphQLQueryTemplateContent);
                    Console.WriteLine((!string.IsNullOrEmpty(Config.WebAPI.GraphQLPath) ? Config.WebAPI.GraphQLPath : "GraphQL") + "\\" + Config.Name + "Query.cs");

                    //// Mutation
                    WebAPITemplates.GraphQL.MutationTemplate graphQLMutationTemplate = new WebAPITemplates.GraphQL.MutationTemplate(Config);
                    string graphQLMutationTemplateContent = graphQLMutationTemplate.TransformText();

                    File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.GraphQLPath) ? Config.WebAPI.GraphQLPath : "GraphQL") + "\\" + Config.Name + "Mutation.cs", graphQLMutationTemplateContent);
                    Console.WriteLine((!string.IsNullOrEmpty(Config.WebAPI.GraphQLPath) ? Config.WebAPI.GraphQLPath : "GraphQL") + "\\" + Config.Name + "Mutation.cs");

                    //// Types
                    if (!Directory.Exists(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.GraphQLPath) ? Config.WebAPI.GraphQLPath : "GraphQL") + "\\" + "Types"))
                    {
                        Directory.CreateDirectory(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.GraphQLPath) ? Config.WebAPI.GraphQLPath : "GraphQL") + "\\" + "Types");
                    }
                    foreach (CodeGenModel codeGenModel in Config.Models)
                    {
                        WebAPITemplates.GraphQL.Types.TypeTemplate graphQLTypeTemplate = new WebAPITemplates.GraphQL.Types.TypeTemplate(Config, codeGenModel);
                        string graphQLTypeTemplateContent = graphQLTypeTemplate.TransformText();

                        File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.GraphQLPath) ? Config.WebAPI.GraphQLPath : "GraphQL") + "\\" + "Types" + "\\" + codeGenModel.Name + "Type.cs", graphQLTypeTemplateContent);
                        Console.WriteLine((!string.IsNullOrEmpty(Config.WebAPI.GraphQLPath) ? Config.WebAPI.GraphQLPath : "GraphQL") + "\\" + "Types" + "\\" + codeGenModel.Name + "Type.cs");

                        WebAPITemplates.GraphQL.Types.InputTypeTemplate graphQLInputTypeTemplate = new WebAPITemplates.GraphQL.Types.InputTypeTemplate(Config, codeGenModel);
                        string graphQLInputTypeTemplateContent = graphQLInputTypeTemplate.TransformText();

                        File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.GraphQLPath) ? Config.WebAPI.GraphQLPath : "GraphQL") + "\\" + "Types" + "\\" + codeGenModel.Name + "InputType.cs", graphQLInputTypeTemplateContent);
                        Console.WriteLine((!string.IsNullOrEmpty(Config.WebAPI.GraphQLPath) ? Config.WebAPI.GraphQLPath : "GraphQL") + "\\" + "Types" + "\\" + codeGenModel.Name + "InputType.cs");
                    }

                    //// Tests
                    if (!Directory.Exists(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.GraphQLPath) ? Config.WebAPI.GraphQLPath : "GraphQL") + "\\" + "Tests"))
                    {
                        Directory.CreateDirectory(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.GraphQLPath) ? Config.WebAPI.GraphQLPath : "GraphQL") + "\\" + "Tests");
                    }

                    WebAPITemplates.GraphQL.Tests.TestMutationsTemplate graphQLTestMutationsTemplate = new WebAPITemplates.GraphQL.Tests.TestMutationsTemplate(Config);
                    string graphQLTestMutationsTemplateContent = graphQLTestMutationsTemplate.TransformText();

                    File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.WebAPI.GraphQLPath) ? Config.WebAPI.GraphQLPath : "GraphQL") + "\\Tests\\Mutations.txt", graphQLTestMutationsTemplateContent);
                    Console.WriteLine((!string.IsNullOrEmpty(Config.WebAPI.GraphQLPath) ? Config.WebAPI.GraphQLPath : "GraphQL") + "\\Tests\\Mutations.txt");


                    // TODO: Migrations?
                    //ProcessStartInfo removeInitialMigration = new ProcessStartInfo("Remove-Migration");
                    //removeInitialMigration.WorkingDirectory = Config.WebAPI.ProjectPath;
                    //Process.Start(removeInitialMigration).WaitForExit();

                    //ProcessStartInfo addInitialMigration = new ProcessStartInfo("Add-Migration");
                    //addInitialMigration.Arguments = @"Initial";
                    //addInitialMigration.WorkingDirectory = Config.WebAPI.ProjectPath;
                    //Process.Start(addInitialMigration).WaitForExit();

                    // Stop
                    Console.WriteLine();
                    Console.WriteLine("### DONE ###");

                    // Git
                    Console.WriteLine();
                    Console.WriteLine("### git status ###");
                    ProcessStartInfo gitStatus = new ProcessStartInfo("git");
                    gitStatus.Arguments = @"status";
                    gitStatus.WorkingDirectory = Config.WebAPI.ProjectPath;
                    Process.Start(gitStatus).WaitForExit();

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
                //CommandOption greeting = generateCommand.Option(
                //    "-$|-g |--greeting <greeting>",
                //    "The greeting to display. The greeting supports"
                //    + " a format string where {fullname} will be "
                //    + "substituted with the full name.",
                //    CommandOptionType.SingleValue);
                //CommandOption uppercase = generateCommand.Option(
                //    "-u | --uppercase", "Display the greeting in uppercase.",
                //    CommandOptionType.NoValue);

                // Help
                generateCommand.HelpOption("-h | -? | --help");

                generateCommand.OnExecute(() =>
                {
                    // Git
                    Console.WriteLine("### git add . ###");
                    ProcessStartInfo gitAddBefore = new ProcessStartInfo("git");
                    gitAddBefore.Arguments = "add .";
                    gitAddBefore.WorkingDirectory = Config.Angular.ProjectPath;
                    Process.Start(gitAddBefore).WaitForExit();

                    Console.WriteLine("### git commit -m \"Save before generating Angular\" ###");
                    ProcessStartInfo gitCommitBefore = new ProcessStartInfo("git");
                    gitCommitBefore.Arguments = "commit -m \"Save before generating Angular\"";
                    gitCommitBefore.WorkingDirectory = Config.Angular.ProjectPath;
                    Process.Start(gitCommitBefore).WaitForExit();

                    Console.WriteLine();
                    Console.WriteLine("### Generating Angular ###");

                    // Models
                    if (!Directory.Exists(Config.Angular.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.Angular.ModelsPath) ? Config.Angular.ModelsPath : "src\\app\\shared\\models")))
                    {
                        Directory.CreateDirectory(Config.Angular.ProjectPath + "\\" + (!string.IsNullOrEmpty(Config.Angular.ModelsPath) ? Config.Angular.ModelsPath : "src\\app\\shared\\models"));
                    }
                    foreach (CodeGenModel codeGenModel in Config.Models)
                    {
                        // Exclude many-to-many models
                        if (codeGenModel.ManyToMany) { continue; }

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
                        // Exclude many-to-many models
                        if (codeGenModel.ManyToMany) { continue; }

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
                        // Exclude many-to-many models
                        if (codeGenModel.ManyToMany) { continue; }

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
                        AngularTemplates.DataDetailComponentHTMLTemplate dataDetailComponentHTMLTemplate = new AngularTemplates.DataDetailComponentHTMLTemplate(Config, codeGenModel);
                        string dataDetailComponentHTMLTemplateContent = dataDetailComponentHTMLTemplate.TransformText();

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


                        ////// Update
                        if (!Directory.Exists(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\update"))
                        {
                            Directory.CreateDirectory(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\update");
                        }

                        //////// HTML
                        AngularTemplates.DataUpdateComponentHTMLTemplate dataUpdateComponentHTMLTemplate = new AngularTemplates.DataUpdateComponentHTMLTemplate(Config, codeGenModel);
                        string dataUpdateComponentHTMLTemplateContent = dataUpdateComponentHTMLTemplate.TransformText();

                        File.WriteAllText(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\update\\update.component.html", dataUpdateComponentHTMLTemplateContent);
                        Console.WriteLine("src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\update\\update.component.html");

                        //////// SCSS
                        AngularTemplates.DataUpdateComponentSCSSTemplate dataUpdateComponentSCSSTemplate = new AngularTemplates.DataUpdateComponentSCSSTemplate(Config, codeGenModel);
                        string dataUpdateComponentSCSSTemplateContent = dataUpdateComponentSCSSTemplate.TransformText();

                        File.WriteAllText(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\update\\update.component.scss", dataUpdateComponentSCSSTemplateContent);
                        Console.WriteLine("src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\update\\update.component.scss");

                        //////// TS
                        AngularTemplates.DataUpdateComponentTSTemplate dataUpdateComponentTSTemplate = new AngularTemplates.DataUpdateComponentTSTemplate(Config, codeGenModel);
                        string dataUpdateComponentTSTemplateContent = dataUpdateComponentTSTemplate.TransformText();

                        File.WriteAllText(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\update\\update.component.ts", dataUpdateComponentTSTemplateContent);
                        Console.WriteLine("src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\update\\update.component.ts");


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


                        ////// Link
                        if (!Directory.Exists(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\link"))
                        {
                            Directory.CreateDirectory(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\link");
                        }

                        //////// HTML
                        AngularTemplates.DataLinkComponentHTMLTemplate dataLinkComponentHTMLTemplate = new AngularTemplates.DataLinkComponentHTMLTemplate(Config, codeGenModel);
                        string dataLinkComponentHTMLTemplateContent = dataLinkComponentHTMLTemplate.TransformText();

                        File.WriteAllText(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\link\\link.component.html", dataLinkComponentHTMLTemplateContent);
                        Console.WriteLine("src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\link\\link.component.html");

                        //////// SCSS
                        AngularTemplates.DataLinkComponentSCSSTemplate dataLinkComponentSCSSTemplate = new AngularTemplates.DataLinkComponentSCSSTemplate(Config, codeGenModel);
                        string dataLinkComponentSCSSTemplateContent = dataLinkComponentSCSSTemplate.TransformText();

                        File.WriteAllText(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\link\\link.component.scss", dataLinkComponentSCSSTemplateContent);
                        Console.WriteLine("src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\link\\link.component.scss");

                        //////// TS
                        AngularTemplates.DataLinkComponentTSTemplate dataLinkComponentTSTemplate = new AngularTemplates.DataLinkComponentTSTemplate(Config, codeGenModel);
                        string dataLinkComponentTSTemplateContent = dataLinkComponentTSTemplate.TransformText();

                        File.WriteAllText(Config.Angular.ProjectPath + "\\src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\link\\link.component.ts", dataLinkComponentTSTemplateContent);
                        Console.WriteLine("src\\app\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s").ToLower() + "\\link\\link.component.ts");
                    }

                    // Stop
                    Console.WriteLine();
                    Console.WriteLine("### DONE ###");

                    Console.WriteLine();
                    Console.WriteLine("### Dependencies ###");
                    Console.WriteLine("npm i --save @angular/cdk @angular/flex-layout");

                    // Git
                    Console.WriteLine();
                    Console.WriteLine("### git status ###");
                    ProcessStartInfo gitStatus = new ProcessStartInfo("git");
                    gitStatus.Arguments = @"status";
                    gitStatus.WorkingDirectory = Config.WebAPI.ProjectPath;
                    Process.Start(gitStatus).WaitForExit();

                    return 0;
                });
            });

            //App.HelpOption("-x"); // top level help -- '-h | -? | --help would be consumed by 'dotnet run'
            App.Execute(args);
        }
    }
}
