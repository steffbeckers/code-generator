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

// code generator
import { CodeGenOptions } from "./schema";
import { IModelScheme } from "../model-scheme/model-scheme";

export default function(options: CodeGenOptions): Rule {
  return (tree: Tree, context: SchematicContext) => {
    // load model scheme JSON config
    const modelSchemeFile = `/${options.modelScheme}`;

    const modelSchemeBuffer = tree.read(modelSchemeFile);
    if (modelSchemeBuffer === null) {
      throw new SchematicsException(
        `Model file ${options.modelScheme} does not exist.`
      );
    }

    const modelSchemeJson = modelSchemeBuffer.toString("utf-8");
    const modelScheme = JSON.parse(modelSchemeJson) as IModelScheme;

    // Rule chain
    let rules: Rule[] = [];

    // For each model in model scheme, generate the files
    modelScheme.models.forEach(model => {
      // templates in ./files/
      const templateSource = apply(url("./files"), [
        template({
          ...stringUtils,
          ...options,
          entity: model.entity,
          model
        }),
        move("/src/app/" + model.entityPlural || "")
      ]);

      rules.push(
        branchAndMerge(
          // chain([mergeWith(templateSource), addImportToParentModule(options)])
          chain([mergeWith(templateSource)])
        )
      );
    });

    let rule: Rule = chain(rules);

    return rule(tree, context);
  };
}
