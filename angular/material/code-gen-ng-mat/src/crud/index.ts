// angular devkit schematics
import {
  Rule,
  SchematicContext,
  Tree,
  SchematicsException,
  apply,
  url,
  template,
  move,
  chain,
  branchAndMerge,
  mergeWith
} from "@angular-devkit/schematics";
import { strings as stringUtils } from "@angular-devkit/core";

// schematics Angular
import { getWorkspace } from "@schematics/angular/utility/config";
import { parseName } from "@schematics/angular/utility/parse-name";

// code generator
import * as codeGeneratorModelUtils from "../utils/code-generator-model-utils";
import { CodeGenOptions } from "./schema";
import { IModelScheme } from "../model-scheme/model-scheme";

// import { addImportToParentModule } from "../utils/ng-module-utils";
// import { findModuleFromOptions } from "../schematics-angular-utils/find-module";

function setupOptions(options: CodeGenOptions, tree: Tree): void {
  // retrieve the workspace
  const workspace = getWorkspace(tree);

  // check project(s)
  if (!options.project) {
    options.project = Object.keys(workspace.projects)[0];
  }
  const project = workspace.projects[options.project];

  // if path is not provided
  if (options.path === undefined) {
    const projectDirName =
      project.projectType === "application" ? "app" : "lib";
    options.path = `/${project.root}/src/${projectDirName}`;
  }

  const parsedPath = parseName(options.path, options.modelScheme);
  options.modelScheme = parsedPath.name;
  options.path = parsedPath.path;
}

export default function(options: CodeGenOptions): Rule {
  return (tree: Tree, context: SchematicContext) => {
    // first setup or set defaults to the options object
    setupOptions(options, tree);
    // options.module = findModuleFromOptions(tree, options, true) || "";

    // load model scheme JSON config
    const modelSchemeFile = `/${options.path}/${options.modelScheme}`;

    const modelSchemeBuffer = tree.read(modelSchemeFile);
    if (modelSchemeBuffer === null) {
      throw new SchematicsException(
        `Model file ${options.modelScheme} does not exist.`
      );
    }

    const modelSchemeJson = modelSchemeBuffer.toString("utf-8");
    const modelScheme = JSON.parse(modelSchemeJson) as IModelScheme;

    // Rule chain
    let rule = chain([]);

    // For each model in model scheme, generate the files
    modelScheme.models.forEach(model => {
      // templates in ./files/
      const templateSource = apply(url("./files"), [
        template({
          ...stringUtils,
          ...options,
          ...(codeGeneratorModelUtils as any),
          model
        }),
        move(options.path || "")
      ]);

      rule = chain([
        branchAndMerge(
          // chain([mergeWith(templateSource), addImportToParentModule(options)])
          chain([mergeWith(templateSource)])
        )
      ]);
    });

    return rule(tree, context);
  };
}
