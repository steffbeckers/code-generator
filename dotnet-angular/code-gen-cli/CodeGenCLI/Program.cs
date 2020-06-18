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
using System.Threading.Tasks;
using Medallion.Shell;
using System.Text;

namespace CodeGenCLI
{
    class Program
    {
        static CommandLineApplication App { get; set; }
        static CodeGenConfig Config { get; set; }
        static TextInfo TextInfo = new CultureInfo("en-US", false).TextInfo;

        // git checkout -p
        static string currentHunk = string.Empty;
        static bool writeYes = false;
        static bool writeNo = false;
        static bool done = false;

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
                        Authentication = Config.Authentication ?? new CodeGenConfigAuthentication(),
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
                                NamePlural = TextInfo.ToTitleCase(databaseTableName),
                                DatabaseTableName = databaseTableName,
                                Properties = new List<CodeGenModelProperty>()
                            };

                            List<CodeGenModelProperty> newCodeGenModelProperties =
                                databaseTableColumns[databaseTableName].Select(dtc => new CodeGenModelProperty() {
                                    Name = dtc[3].ToString(),
                                    Type = dtc[7].ToString().ToCSharpDataType(),
                                    Required = dtc[6].ToString() == "NO",
                                    DatabaseFieldName = dtc[3].ToString(),
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

                CommandOption addMigration = generateCommand.Option(
                    "-am |--add-migration",
                    "Add migration",
                    CommandOptionType.NoValue
                );

                // Help
                generateCommand.HelpOption("-h | -? | --help");

                generateCommand.OnExecute(() =>
                {
                    #region Git

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

                    #endregion

                    // Start generation
                    Console.WriteLine();
                    Console.WriteLine("### Generating Web API code ###");
                    Console.WriteLine("Override files: " + Config.Override.ToString().ToLower());
                    Console.WriteLine("Authentication: " + Config.Authentication.Enabled.ToString().ToLower());
                    Console.WriteLine();

                    #region Default directory paths to create

                    // Framework
                    Config.WebAPI.FrameworkPath = !string.IsNullOrEmpty(Config.WebAPI.FrameworkPath) ? Config.WebAPI.FrameworkPath : "Framework";
                    if (!Directory.Exists(Config.WebAPI.ProjectPath + "\\" + Config.WebAPI.FrameworkPath))
                    {
                        Directory.CreateDirectory(Config.WebAPI.ProjectPath + "\\" + Config.WebAPI.FrameworkPath);
                        Console.WriteLine(Config.WebAPI.FrameworkPath);
                    }
                    
                    // Models
                    Config.WebAPI.ModelsPath = !string.IsNullOrEmpty(Config.WebAPI.ModelsPath) ? Config.WebAPI.ModelsPath : "Models";
                    if (!Directory.Exists(Config.WebAPI.ProjectPath + "\\" + Config.WebAPI.ModelsPath))
                    {
                        Directory.CreateDirectory(Config.WebAPI.ProjectPath + "\\" + Config.WebAPI.ModelsPath);
                        Console.WriteLine(Config.WebAPI.ModelsPath);
                    }

                    // DAL
                    Config.WebAPI.DALPath = !string.IsNullOrEmpty(Config.WebAPI.DALPath) ? Config.WebAPI.DALPath : "DAL";
                    if (!Directory.Exists(Config.WebAPI.ProjectPath + "\\" + Config.WebAPI.DALPath))
                    {
                        Directory.CreateDirectory(Config.WebAPI.ProjectPath + "\\" + Config.WebAPI.DALPath + "\\Repositories"); // Also generate the Repositories directory
                        Console.WriteLine(Config.WebAPI.DALPath);
                        Console.WriteLine(Config.WebAPI.DALPath + "\\Repositories"); // Also generate the Repositories directory
                    }

                    // Services
                    Config.WebAPI.ServicesPath = !string.IsNullOrEmpty(Config.WebAPI.ServicesPath) ? Config.WebAPI.ServicesPath : "Services";
                    if (!Directory.Exists(Config.WebAPI.ProjectPath + "\\" + Config.WebAPI.ServicesPath))
                    {
                        Directory.CreateDirectory(Config.WebAPI.ProjectPath + "\\" + Config.WebAPI.ServicesPath);
                        Console.WriteLine(Config.WebAPI.ServicesPath);
                    }

                    // BLL
                    Config.WebAPI.BLLPath = !string.IsNullOrEmpty(Config.WebAPI.BLLPath) ? Config.WebAPI.BLLPath : "BLL";
                    if (!Directory.Exists(Config.WebAPI.ProjectPath + "\\" + Config.WebAPI.BLLPath))
                    {
                        Directory.CreateDirectory(Config.WebAPI.ProjectPath + "\\" + Config.WebAPI.BLLPath);
                        Console.WriteLine(Config.WebAPI.BLLPath);
                    }

                    // Controllers
                    Config.WebAPI.ControllersPath = !string.IsNullOrEmpty(Config.WebAPI.ControllersPath) ? Config.WebAPI.ControllersPath : "Controllers";
                    if (!Directory.Exists(Config.WebAPI.ProjectPath + "\\" + Config.WebAPI.ControllersPath))
                    {
                        Directory.CreateDirectory(Config.WebAPI.ProjectPath + "\\" + Config.WebAPI.ControllersPath);
                        Console.WriteLine(Config.WebAPI.ControllersPath);
                    }

                    // View models
                    Config.WebAPI.ViewModelsPath = !string.IsNullOrEmpty(Config.WebAPI.ViewModelsPath) ? Config.WebAPI.ViewModelsPath : "ViewModels";
                    if (!Directory.Exists(Config.WebAPI.ProjectPath + "\\" + Config.WebAPI.ViewModelsPath))
                    {
                        Directory.CreateDirectory(Config.WebAPI.ProjectPath + "\\" + Config.WebAPI.ViewModelsPath);
                        Console.WriteLine(Config.WebAPI.ViewModelsPath);
                    }

                    // GraphQL
                    Config.WebAPI.GraphQLPath = !string.IsNullOrEmpty(Config.WebAPI.GraphQLPath) ? Config.WebAPI.GraphQLPath : "GraphQL";
                    if (!Directory.Exists(Config.WebAPI.ProjectPath + "\\" + Config.WebAPI.GraphQLPath))
                    {
                        Directory.CreateDirectory(Config.WebAPI.ProjectPath + "\\" + Config.WebAPI.GraphQLPath + "\\Types"); // Also generate the Types directory
                        Directory.CreateDirectory(Config.WebAPI.ProjectPath + "\\" + Config.WebAPI.GraphQLPath + "\\Tests"); // Also generate the Types directory
                        Console.WriteLine(Config.WebAPI.GraphQLPath);
                        Console.WriteLine(Config.WebAPI.GraphQLPath + "\\Types"); // Also generate the Types directory
                        Console.WriteLine(Config.WebAPI.GraphQLPath + "\\Tests"); // Also generate the Types directory
                    }

                    #endregion

                    #region Root of project

                    //// appsettings.json
                    string rootAppSettingsPath = Config.WebAPI.ProjectPath + "\\" + "appsettings.json";
                    if (!File.Exists(rootAppSettingsPath) || Config.Override)
                    {
                        File.WriteAllText(rootAppSettingsPath, new WebAPITemplates.AppSettingsTemplate(Config).TransformText());
                        Console.WriteLine("appsettings.json");
                    }

                    //// Startup
                    string rootStartupPath = Config.WebAPI.ProjectPath + "\\" + "Startup.cs";
                    if (!File.Exists(rootStartupPath) || Config.Override)
                    {
                        File.WriteAllText(rootStartupPath, new WebAPITemplates.StartupTemplate(Config).TransformText());
                        Console.WriteLine("Startup.cs");
                    }

                    //// Dockerfile
                    string rootDockerfilePath = Config.WebAPI.ProjectPath + "\\" + "Dockerfile";
                    if (!File.Exists(rootDockerfilePath) || Config.Override)
                    {
                        File.WriteAllText(rootDockerfilePath, new WebAPITemplates.DockerfileTemplate(Config).TransformText());
                        Console.WriteLine("Dockerfile");
                    }

                    #endregion

                    #region Framework

                    //// Repository
                    string frameworkRepositoryPath = Config.WebAPI.FrameworkPath + "\\" + "Repository.cs";
                    if (!File.Exists(Config.WebAPI.ProjectPath + "\\" + frameworkRepositoryPath) || Config.Override)
                    {
                        File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + frameworkRepositoryPath, new WebAPITemplates.Framework.RepositoryTemplate(Config).TransformText());
                        Console.WriteLine(frameworkRepositoryPath);
                    }

                    //// Exceptions
                    string frameworkExceptionsPath = Config.WebAPI.FrameworkPath + "\\" + "Exceptions.cs";
                    if (!File.Exists(Config.WebAPI.ProjectPath + "\\" + frameworkExceptionsPath) || Config.Override)
                    {
                        File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + frameworkExceptionsPath, new WebAPITemplates.Framework.ExceptionsTemplate(Config).TransformText());
                        Console.WriteLine(frameworkExceptionsPath);
                    }

                    #endregion

                    #region Models

                    foreach (CodeGenModel codeGenModel in Config.Models)
                    {
                        string modelPath = Config.WebAPI.ModelsPath + "\\" + codeGenModel.Name + ".cs";
                        if (!File.Exists(Config.WebAPI.ProjectPath + "\\" + modelPath) || Config.Override)
                        {
                            File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + modelPath, new WebAPITemplates.Models.ModelTemplate(Config, codeGenModel).TransformText());
                            Console.WriteLine(modelPath);
                        }
                    }

                    #endregion

                    #region ViewModels

                    foreach (CodeGenModel codeGenModel in Config.Models.Where(m => !m.ManyToMany))
                    {
                        string viewModelPath = Config.WebAPI.ViewModelsPath + "\\" + codeGenModel.Name + "VM.cs";
                        if (!File.Exists(Config.WebAPI.ProjectPath + "\\" + viewModelPath) || Config.Override)
                        {
                            File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + viewModelPath, new WebAPITemplates.ViewModels.ViewModelTemplate(Config, codeGenModel).TransformText());
                            Console.WriteLine(viewModelPath);
                        }
                    }

                    #endregion

                    #region DAL

                    // DbContext
                    string dalDbContextPath = Config.WebAPI.DALPath + "\\" + Config.Name + "Context.cs";
                    if (!File.Exists(Config.WebAPI.ProjectPath + "\\" + dalDbContextPath) || Config.Override)
                    {
                        File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + dalDbContextPath, new WebAPITemplates.DAL.DbContextTemplate(Config).TransformText());
                        Console.WriteLine(dalDbContextPath);
                    }

                    // AutoMapper
                    string dalAutoMapperPath = Config.WebAPI.DALPath + "\\AutoMapperProfile.cs";
                    if (!File.Exists(Config.WebAPI.ProjectPath + "\\" + dalAutoMapperPath) || Config.Override)
                    {
                        File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + dalAutoMapperPath, new WebAPITemplates.DAL.AutoMapperProfileTemplate(Config).TransformText());
                        Console.WriteLine(dalAutoMapperPath);
                    }

                    // Repositories
                    foreach (CodeGenModel codeGenModel in Config.Models)
                    {
                        string repositoryPath = Config.WebAPI.DALPath + "\\Repositories\\" + codeGenModel.Name + "Repository.cs";
                        if (!File.Exists(Config.WebAPI.ProjectPath + "\\" + repositoryPath) || Config.Override)
                        {
                            File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + repositoryPath, new WebAPITemplates.DAL.Repositories.RepositoryTemplate(Config, codeGenModel).TransformText());
                            Console.WriteLine(repositoryPath);
                        }
                    }

                    #endregion

                    #region BLL

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
                        string bllTemplateContent = new WebAPITemplates.BLL.BLLTemplate(Config, codeGenModel).TransformText();

                        // Replace custom code from existing code
                        MatchCollection bllCodeRegionMatches = Regex.Matches(bllTemplateContent, @"#-#-#(.+?)#-#-#", RegexOptions.Singleline);
                        foreach (Match bllCodeRegionMatch in bllCodeRegionMatches)
                        {
                            if (customBLLCodeBlocks.ContainsKey(bllCodeRegionMatch.Value.Substring(6, 38)))
                            {
                                bllTemplateContent = bllTemplateContent.Replace(bllCodeRegionMatch.Value, customBLLCodeBlocks.GetValueOrDefault(bllCodeRegionMatch.Value.Substring(6, 38)));
                            }
                        }

                        File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + Config.WebAPI.BLLPath + "\\" + codeGenModel.Name + "BLL.cs", bllTemplateContent);
                        Console.WriteLine(Config.WebAPI.BLLPath + "\\" + codeGenModel.Name + "BLL.cs");
                    }

                    #endregion

                    #region Controllers

                    foreach (CodeGenModel codeGenModel in Config.Models.Where(m => !m.ManyToMany))
                    {
                        string controllerPath = Config.WebAPI.ControllersPath + "\\" + (!string.IsNullOrEmpty(codeGenModel.NamePlural) ? codeGenModel.NamePlural : codeGenModel.Name + "s") + "Controller.cs";
                        if (!File.Exists(Config.WebAPI.ProjectPath + "\\" + controllerPath) || Config.Override)
                        {
                            File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + controllerPath, new WebAPITemplates.Controllers.ControllerTemplate(Config, codeGenModel).TransformText());
                            Console.WriteLine(controllerPath);
                        }
                    }

                    #endregion

                    #region Services

                    // EmailService
                    string emailServicePath = Config.WebAPI.ServicesPath + "\\EmailService.cs";
                    if (!File.Exists(Config.WebAPI.ProjectPath + "\\" + emailServicePath) || Config.Override)
                    {
                        File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + emailServicePath, new WebAPITemplates.Services.EmailServiceTemplate(Config).TransformText());
                        Console.WriteLine(emailServicePath);
                    }

                    #endregion

                    #region GraphQL

                    // Schema
                    string graphQLSchemaPath = Config.WebAPI.GraphQLPath + "\\" + Config.Name + "Schema.cs";
                    if (!File.Exists(Config.WebAPI.ProjectPath + "\\" + graphQLSchemaPath) || Config.Override)
                    {
                        File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + graphQLSchemaPath, new WebAPITemplates.GraphQL.SchemaTemplate(Config).TransformText());
                        Console.WriteLine(graphQLSchemaPath);
                    }

                    // Query
                    string graphQLQueryPath = Config.WebAPI.GraphQLPath + "\\" + Config.Name + "Query.cs";
                    if (!File.Exists(Config.WebAPI.ProjectPath + "\\" + graphQLQueryPath) || Config.Override)
                    {
                        File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + graphQLQueryPath, new WebAPITemplates.GraphQL.QueryTemplate(Config).TransformText());
                        Console.WriteLine(graphQLQueryPath);
                    }

                    // Mutation
                    string graphQLMutationPath = Config.WebAPI.GraphQLPath + "\\" + Config.Name + "Mutation.cs";
                    if (!File.Exists(Config.WebAPI.ProjectPath + "\\" + graphQLMutationPath) || Config.Override)
                    {
                        File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + graphQLMutationPath, new WebAPITemplates.GraphQL.MutationTemplate(Config).TransformText());
                        Console.WriteLine(graphQLMutationPath);
                    }

                    // Types
                    foreach (CodeGenModel codeGenModel in Config.Models)
                    {
                        string graphQLTypePath = Config.WebAPI.GraphQLPath + "\\Types\\" + codeGenModel.Name + "Type.cs";
                        if (!File.Exists(Config.WebAPI.ProjectPath + "\\" + graphQLTypePath) || Config.Override)
                        {
                            File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + graphQLTypePath, new WebAPITemplates.GraphQL.Types.TypeTemplate(Config, codeGenModel).TransformText());
                            Console.WriteLine(graphQLTypePath);
                        }

                        string graphQLInputTypePath = Config.WebAPI.GraphQLPath + "\\Types\\" + codeGenModel.Name + "InputType.cs";
                        if (!File.Exists(Config.WebAPI.ProjectPath + "\\" + graphQLInputTypePath) || Config.Override)
                        {
                            File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + graphQLInputTypePath, new WebAPITemplates.GraphQL.Types.InputTypeTemplate(Config, codeGenModel).TransformText());
                            Console.WriteLine(graphQLInputTypePath);
                        }
                    }

                    // Tests
                    string graphQLTestsPath = Config.WebAPI.GraphQLPath + "\\Tests\\Mutations.txt";
                    if (!File.Exists(Config.WebAPI.ProjectPath + "\\" + graphQLTestsPath) || Config.Override)
                    {
                        File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + graphQLTestsPath, new WebAPITemplates.GraphQL.Tests.TestMutationsTemplate(Config).TransformText());
                        Console.WriteLine(graphQLTestsPath);
                    }

                    #endregion

                    #region Authentication with Identity

                    if (Config.Authentication.Enabled)
                    {
                        // User model
                        string userModelPath = Config.WebAPI.ModelsPath + "\\User.cs";
                        if (!File.Exists(Config.WebAPI.ProjectPath + "\\" + userModelPath) || Config.Override)
                        {
                            File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + userModelPath, new WebAPITemplates.Models.UserModelTemplate(Config).TransformText());
                            Console.WriteLine(userModelPath);
                        }

                        // View models
                        string identityViewModelPath = Config.WebAPI.ViewModelsPath + "\\IdentityVM.cs";
                        if (!File.Exists(Config.WebAPI.ProjectPath + "\\" + identityViewModelPath) || Config.Override)
                        {
                            File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + identityViewModelPath, new WebAPITemplates.ViewModels.IdentityViewModelTemplate(Config).TransformText());
                            Console.WriteLine(identityViewModelPath);
                        }

                        // UserType
                        string graphQLUserTypePath = Config.WebAPI.GraphQLPath + "\\Types\\UserType.cs";
                        if (!File.Exists(Config.WebAPI.ProjectPath + "\\" + graphQLUserTypePath) || Config.Override)
                        {
                            File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + graphQLUserTypePath, new WebAPITemplates.GraphQL.Types.UserTypeTemplate(Config).TransformText());
                            Console.WriteLine(graphQLUserTypePath);
                        }

                        // Auth BLL
                        string authBLLPath = Config.WebAPI.BLLPath + "\\AuthBLL.cs";
                        if (!File.Exists(Config.WebAPI.ProjectPath + "\\" + authBLLPath) || Config.Override)
                        {
                            File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + authBLLPath, new WebAPITemplates.BLL.AuthBLLTemplate(Config).TransformText());
                            Console.WriteLine(authBLLPath);
                        }

                        // Auth controller
                        string authControllerPath = Config.WebAPI.ControllersPath + "\\AuthController.cs";
                        if (!File.Exists(Config.WebAPI.ProjectPath + "\\" + authControllerPath) || Config.Override)
                        {
                            File.WriteAllText(Config.WebAPI.ProjectPath + "\\" + authControllerPath, new WebAPITemplates.Controllers.AuthControllerTemplate(Config).TransformText());
                            Console.WriteLine(authControllerPath);
                        }
                    }

                    #endregion

                    #region Git

                    Console.WriteLine();
                    Console.WriteLine("### git status ###");
                    ProcessStartInfo gitStatus = new ProcessStartInfo("git");
                    gitStatus.Arguments = @"status";
                    gitStatus.WorkingDirectory = Config.WebAPI.ProjectPath;
                    Process.Start(gitStatus).WaitForExit();


                    //Console.WriteLine();
                    //Console.WriteLine("### git diff > code-gen-patching.txt ###");

                    //Process gitDiffOut = new Process
                    //{
                    //    StartInfo = new ProcessStartInfo
                    //    {
                    //        FileName = "git",
                    //        Arguments = "diff > " + Config.WebAPI.ProjectPath + "code-gen-patching.txt",
                    //        WorkingDirectory = Config.WebAPI.ProjectPath,
                    //        CreateNoWindow = true,
                    //        UseShellExecute = false
                    //    }
                    //};

                    //gitDiffOut.Start();
                    //gitDiffOut.WaitForExit();


                    // CMD test
                    //Console.WriteLine();
                    //Console.WriteLine("### cmd => git status ###");

                    //Process cmdGitStatus = new Process
                    //{
                    //    StartInfo = new ProcessStartInfo
                    //    {
                    //        FileName = "cmd",
                    //        Arguments = "/c git status",
                    //        WorkingDirectory = Config.WebAPI.ProjectPath,
                    //        RedirectStandardOutput = true,
                    //        RedirectStandardInput = true,
                    //        CreateNoWindow = true,
                    //        UseShellExecute = false
                    //    }
                    //};

                    //cmdGitStatus.Start();

                    //Console.WriteLine(cmdGitStatus.StandardOutput.ReadToEnd());

                    //cmdGitStatus.WaitForExit();


                    Process gitCheckoutPOutput = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "git",
                            Arguments = "checkout -p",
                            //FileName = "cmd",
                            //Arguments = "/c git checkout -p",
                            WorkingDirectory = Config.WebAPI.ProjectPath,
                            RedirectStandardOutput = true,
                            RedirectStandardInput = false,
                            CreateNoWindow = true,
                            UseShellExecute = false
                        }
                    };

                    Process gitCheckoutPInput = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "git",
                            Arguments = "checkout -p",
                            //FileName = "cmd",
                            //Arguments = "/c git checkout -p",
                            WorkingDirectory = Config.WebAPI.ProjectPath,
                            RedirectStandardOutput = false,
                            RedirectStandardInput = true,
                            CreateNoWindow = true,
                            UseShellExecute = false
                        }
                    };

                    try
                    {
                        Console.WriteLine();
                        Console.WriteLine("### git checkout -p ###");

                        // Read output
                        gitCheckoutPOutput.Start();
                        string output = gitCheckoutPOutput.StandardOutput.ReadToEnd();
                        Console.WriteLine(output);
                        gitCheckoutPOutput.WaitForExit();
                        gitCheckoutPOutput.Kill();

                        // Supply input, based on output
                        gitCheckoutPInput.Start();

                        if (output.Contains("#-#-#"))
                        {
                            Console.WriteLine("y");
                            gitCheckoutPInput.StandardInput.WriteLine("y");
                            //gitCheckoutPInput.StandardInput.Flush();
                        }
                        else
                        {
                            Console.WriteLine("n");
                            gitCheckoutPInput.StandardInput.WriteLine("n");
                            //gitCheckoutPInput.StandardInput.Flush();
                        }

                        //gitCheckoutPInput.StandardInput.WriteLine("exit");
                        //gitCheckoutPInput.StandardInput.Flush();

                        Console.WriteLine("gitCheckoutPInput.WaitForExit()");
                        gitCheckoutPInput.WaitForExit();

                        Console.WriteLine("gitCheckoutPInput.Kill()");
                        gitCheckoutPInput.Kill();


                        //StreamReader sr = gitCheckoutP.StandardOutput;
                        //string line = string.Empty;
                        //string currentHunk = string.Empty;

                        //while (!sr.EndOfStream)
                        //{
                        //    char inputChar = (char)sr.Read();
                        //    line += inputChar;

                        //    if (line.EndsWith("Discard this hunk from worktree [y,n,q,a,d,j,J,g,/,e,?]? "))
                        //    {
                        //        Console.WriteLine(line);

                        //        if (currentHunk.Contains("#-#-#"))
                        //        {
                        //            Console.WriteLine("y");
                        //            gitCheckoutP.StandardInput.WriteLine("y");
                        //        }
                        //        else
                        //        {
                        //            Console.WriteLine("n");
                        //            gitCheckoutP.StandardInput.WriteLine("n");
                        //        }
                        //    }
                        //    else if (line.EndsWith(Environment.NewLine))
                        //    {
                        //        Console.WriteLine(line);

                        //        currentHunk += line;

                        //        line = string.Empty;
                        //    }
                        //}

                        //sr.Close();

                        //gitCheckoutP.WaitForExit();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    finally
                    {
                        gitCheckoutPOutput.Kill();
                    }


                    //Console.WriteLine();
                    //Console.WriteLine("### git checkout -p ###");

                    //using (Process gitCheckoutP = new Process
                    //{
                    //    StartInfo = new ProcessStartInfo
                    //    {
                    //        FileName = "git",
                    //        Arguments = "checkout -p",
                    //        WorkingDirectory = Config.WebAPI.ProjectPath,
                    //        RedirectStandardOutput = true,
                    //        RedirectStandardInput = true,
                    //        CreateNoWindow = true,
                    //        UseShellExecute = false
                    //    }
                    //})
                    //{
                    //    gitCheckoutP.Start();

                    //    ReadGitCheckoutPOutput(gitCheckoutP.StandardOutput);

                    //    while (!done)
                    //    {
                    //        Task.Run(() =>
                    //        {
                    //            Task.Delay(50);

                    //            if (writeYes)
                    //            {
                    //                gitCheckoutP.StandardInput.WriteLine("y");
                    //                writeYes = false;
                    //            }
                    //            else if (writeNo)
                    //            {
                    //                gitCheckoutP.StandardInput.WriteLine("n");
                    //                writeNo = false;
                    //            }
                    //        }).Wait();
                    //    }
                    //}

                    //using (Process gitCheckoutP = new Process {
                    //    StartInfo = new ProcessStartInfo
                    //    {
                    //        FileName = "git",
                    //        //Arguments = "checkout -p",
                    //        Arguments = "log",
                    //        WorkingDirectory = Config.WebAPI.ProjectPath,
                    //        RedirectStandardOutput = true,
                    //        RedirectStandardError = true,
                    //        RedirectStandardInput = true,
                    //        CreateNoWindow = true,
                    //        UseShellExecute = false
                    //    }
                    //})
                    //{
                    //    StringBuilder output = new StringBuilder();
                    //    StringBuilder error = new StringBuilder();

                    //    using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
                    //    using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
                    //    {
                    //        gitCheckoutP.OutputDataReceived += (object sender, DataReceivedEventArgs e) => {
                    //            if (e.Data == null)
                    //            {
                    //                outputWaitHandle.Set();
                    //            }
                    //            else
                    //            {
                    //                Console.WriteLine("OutputDataReceived: " + e.Data);
                    //                output.AppendLine(e.Data);
                    //            }
                    //        };
                    //        gitCheckoutP.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                    //        {
                    //            if (e.Data == null)
                    //            {
                    //                errorWaitHandle.Set();
                    //            }
                    //            else
                    //            {
                    //                Console.WriteLine("ErrorDataReceived: " + e.Data);
                    //                error.AppendLine(e.Data);
                    //            }
                    //        };

                    //        gitCheckoutP.Start();

                    //        gitCheckoutP.BeginOutputReadLine();
                    //        gitCheckoutP.BeginErrorReadLine();

                    //        gitCheckoutP.WaitForExit();

                    //        //if (gitCheckoutP.WaitForExit(20000) &&
                    //        //    outputWaitHandle.WaitOne(20000) &&
                    //        //    errorWaitHandle.WaitOne(20000))
                    //        //{
                    //        //    // Process completed. Check process.ExitCode here.
                    //        //    Console.WriteLine("Process completed. ExitCode: " + gitCheckoutP.ExitCode);
                    //        //    Console.WriteLine("Output:");
                    //        //    Console.WriteLine(output);
                    //        //}
                    //        //else
                    //        //{
                    //        //    // Timed out.
                    //        //    Console.WriteLine("Timed out.");
                    //        //}
                    //    }
                    //}


                    //Console.WriteLine();
                    //Console.WriteLine("### git log ###");

                    //Command gitLogCommand = Command.Run("git", "log");
                    //ICollection<string> gitLogCommandLines = new List<string>();
                    //gitLogCommand.StandardOutput.PipeToAsync(gitLogCommandLines);
                    //gitLogCommand.Wait();
                    //CommandResult gitLogCommandResult = gitLogCommand.Result;


                    //Console.WriteLine();
                    //Console.WriteLine("### git checkout -p ###");

                    //Command gitCheckoutPCommand = Command.Run("git", "checkout", "-p");
                    //ICollection<string> gitCheckoutPCommandLines = new List<string>();
                    //gitCheckoutPCommand.StandardOutput.PipeToAsync(gitCheckoutPCommandLines);
                    //gitCheckoutPCommand.Wait();
                    //CommandResult gitCheckoutPCommandResult = gitCheckoutPCommand.Result;


                    //Process gitCheckoutP = new Process
                    //{
                    //    StartInfo = new ProcessStartInfo
                    //    {
                    //        FileName = "cmd",
                    //        Arguments = "/c git checkout -p",
                    //        WorkingDirectory = Config.WebAPI.ProjectPath,
                    //        RedirectStandardOutput = true,
                    //        RedirectStandardInput = false,
                    //        CreateNoWindow = true,
                    //        UseShellExecute = false
                    //    }
                    //};

                    //gitCheckoutP.Start();

                    //string line;
                    //string currentHunk = string.Empty;

                    //while (gitCheckoutP.StandardOutput.Peek() > -1)
                    //{
                    //    line = gitCheckoutP.StandardOutput.ReadLine();

                    //    if (line.Equals("Discard this hunk from worktree [y,n,q,a,d,j,J,g,/,e,?]? "))
                    //    {
                    //        if (currentHunk.Contains("#-#-#"))
                    //        {
                    //            gitCheckoutP.StandardInput.WriteLine("y");
                    //            currentHunk = string.Empty;
                    //        }
                    //        else
                    //        {
                    //            gitCheckoutP.StandardInput.WriteLine("n");
                    //            currentHunk = string.Empty;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        currentHunk += line + Environment.NewLine;
                    //    }
                    //}

                    //gitCheckoutP.WaitForExit();

                    // CASE 1

                    //gitCheckoutP.Start();

                    //string line;
                    //string currentHunk = string.Empty;

                    //while ((line = gitCheckoutP.StandardOutput.ReadLine()) != null)
                    //{
                    //    Console.WriteLine(line);

                    //    if (line.Equals("Discard this hunk from worktree [y,n,q,a,d,j,J,g,/,e,?]? "))
                    //    {
                    //        if (currentHunk.Contains("#-#-#"))
                    //        {
                    //            gitCheckoutP.StandardInput.WriteLine("y");
                    //            currentHunk = string.Empty;
                    //        }
                    //        else
                    //        {
                    //            gitCheckoutP.StandardInput.WriteLine("n");
                    //            currentHunk = string.Empty;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        currentHunk += line + Environment.NewLine;
                    //    }
                    //}

                    //gitCheckoutP.WaitForExit();

                    // CASE 1

                    // CASE 2

                    //gitCheckoutP.Start();

                    //string line;
                    //string currentHunk = string.Empty;

                    //while (gitCheckoutP.StandardOutput.Peek() > -1)
                    //{
                    //    line = gitCheckoutP.StandardOutput.ReadLine();

                    //    if (line.Equals("Discard this hunk from worktree [y,n,q,a,d,j,J,g,/,e,?]? "))
                    //    {
                    //        if (currentHunk.Contains("#-#-#"))
                    //        {
                    //            gitCheckoutP.StandardInput.WriteLine("y");
                    //            currentHunk = string.Empty;
                    //        }
                    //        else
                    //        {
                    //            gitCheckoutP.StandardInput.WriteLine("n");
                    //            currentHunk = string.Empty;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        currentHunk += line + Environment.NewLine;
                    //    }
                    //}

                    //gitCheckoutP.WaitForExit();

                    // CASE 2

                    //bool watchingOutput = true;
                    //string currentHunk = string.Empty;
                    //bool undoHunk = false;
                    //bool acceptHunk = false;

                    //var outputTask = Task.Run(() =>
                    //{
                    //    while (gitCheckoutP.StandardOutput.Peek() > -1)
                    //    {
                    //        Thread.Sleep(200);

                    //        string line = gitCheckoutP.StandardOutput.ReadLine();

                    //        if (line.Equals("Discard this hunk from worktree [y,n,q,a,d,j,J,g,/,e,?]? "))
                    //        {
                    //            if (currentHunk.Contains("#-#-#"))
                    //            {
                    //                undoHunk = true;
                    //                currentHunk = string.Empty;
                    //            }
                    //            else
                    //            {
                    //                acceptHunk = true;
                    //                currentHunk = string.Empty;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            currentHunk += line + Environment.NewLine;
                    //        }
                    //    }

                    //    watchingOutput = false;
                    //});

                    //var inputTask = Task.Run(() =>
                    //{
                    //    while (watchingOutput)
                    //    {
                    //        Thread.Sleep(50);

                    //        if (undoHunk)
                    //        {
                    //            gitCheckoutP.StandardInput.WriteLine("y");
                    //            undoHunk = false;
                    //        }
                    //        if (acceptHunk)
                    //        {
                    //            gitCheckoutP.StandardInput.WriteLine("n");
                    //            acceptHunk = false;
                    //        }
                    //    }
                    //});

                    //Task.WaitAll(outputTask, inputTask);

                    //gitCheckoutP.WaitForExit();


                    //string line;
                    //string currentHunk = string.Empty;

                    //while ((line = gitCheckoutP.StandardOutput.ReadLine()) != null)
                    //{
                    //    Console.WriteLine(line);

                    //    if (line.Equals("Discard this hunk from worktree [y,n,q,a,d,j,J,g,/,e,?]? "))
                    //    {
                    //        if (currentHunk.Contains("#-#-#"))
                    //        {
                    //            gitCheckoutP.StandardInput.WriteLine("y");
                    //            currentHunk = string.Empty;
                    //        }
                    //        else
                    //        {
                    //            gitCheckoutP.StandardInput.WriteLine("n");
                    //            currentHunk = string.Empty;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        currentHunk += line + Environment.NewLine;
                    //    }
                    //}


                    //string output = gitCheckoutP.StandardOutput.ReadToEnd();

                    //bool watchingOutput = true;
                    //string currentHunk = string.Empty;
                    //bool undoHunk = false;
                    //bool acceptHunk = false;

                    //new Thread(() =>
                    //{
                    //    while (watchingOutput)
                    //    {
                    //        Thread.Sleep(100);

                    //        if (undoHunk)
                    //        {
                    //            gitCheckoutP.StandardInput.WriteLine("y");
                    //            undoHunk = false;
                    //        }
                    //        if (acceptHunk)
                    //        {
                    //            gitCheckoutP.StandardInput.WriteLine("n");
                    //            acceptHunk = false;
                    //        }
                    //    }
                    //}).Start();

                    //while (gitCheckoutP.StandardOutput.Peek() > -1)
                    //{
                    //    Thread.Sleep(200);

                    //    string line = gitCheckoutP.StandardOutput.ReadLine();

                    //    if (line.Equals("Discard this hunk from worktree [y,n,q,a,d,j,J,g,/,e,?]? "))
                    //    {
                    //        if (currentHunk.Contains("#-#-#"))
                    //        {
                    //            undoHunk = true;
                    //            currentHunk = string.Empty;
                    //        }
                    //        else
                    //        {
                    //            acceptHunk = true;
                    //            currentHunk = string.Empty;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        currentHunk += line + Environment.NewLine;
                    //    }
                    //}

                    //watchingOutput = false;
                    //gitCheckoutP.WaitForExit();

                    #endregion;

                    // Stop generation
                    Console.WriteLine();
                    Console.WriteLine("### DONE ###");

                    #region Migrations

                    Console.WriteLine();

                    //Console.WriteLine("Add migration? y/N");

                    //if (Console.ReadKey().KeyChar.ToString().Equals("y", StringComparison.OrdinalIgnoreCase))
                    if (addMigration.HasValue())
                    {
                        Console.WriteLine("### Adding migration ###");
                        Console.Write("Name: ");
                        string migrationName = Console.ReadLine();
                        if (!string.IsNullOrEmpty(migrationName))
                            migrationName = migrationName.Replace(" ", "").Trim();

                        ProcessStartInfo addInitialMigration = new ProcessStartInfo("dotnet");
                        addInitialMigration.Arguments = @"ef migrations add " + (!string.IsNullOrEmpty(migrationName) ? migrationName : Guid.NewGuid().ToString().Replace("-", "").ToUpper());
                        addInitialMigration.WorkingDirectory = Config.WebAPI.ProjectPath;
                        Process.Start(addInitialMigration).WaitForExit();

                        ProcessStartInfo updateDatabaseMigration = new ProcessStartInfo("dotnet");
                        updateDatabaseMigration.Arguments = @"ef database update";
                        updateDatabaseMigration.WorkingDirectory = Config.WebAPI.ProjectPath;
                        Process.Start(updateDatabaseMigration).WaitForExit();
                    }

                    #endregion

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
                    Console.WriteLine("### Generating Angular code ###");
                    Console.WriteLine("Override files: " + Config.Override.ToString().ToLower());
                    Console.WriteLine("Authentication: " + Config.Authentication.Enabled.ToString().ToLower());
                    Console.WriteLine();

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

        //private static void ReadGitCheckoutPOutput(StreamReader standardOutput)
        //{
        //    Task.Run(() =>
        //    {
        //        while (standardOutput.Peek() > -1)
        //        {
        //            Task.Delay(200);

        //            string line = standardOutput.ReadLine();

        //            if (line.Equals("Discard this hunk from worktree [y,n,q,a,d,j,J,g,/,e,?]? "))
        //            {
        //                if (currentHunk.Contains("#-#-#"))
        //                {
        //                    writeYes = true;
        //                    currentHunk = string.Empty;
        //                }
        //                else
        //                {
        //                    writeNo = true;
        //                    currentHunk = string.Empty;
        //                }
        //            }
        //            else
        //            {
        //                currentHunk += line + Environment.NewLine;
        //            }
        //        }

        //        done = true;
        //    }).Wait();
        //}
    }
}
