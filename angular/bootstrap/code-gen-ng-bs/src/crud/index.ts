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
  mergeWith
} from "@angular-devkit/schematics";
import { strings as stringUtils } from "@angular-devkit/core";

// code generator
import { CodeGenCRUDOptions } from "./schema";

export default function(options: CodeGenCRUDOptions): Rule {
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
    const modelScheme = JSON.parse(modelSchemeJson) as any; // TODO: Create model scheme typings

    // Rule chain
    let rules: Rule[] = [];

    // For each entity in model scheme, generate the models
    modelScheme.entities.forEach((entity: any) => {
      // TODO: Create model scheme typings
      // templates in ./files/models
      const templateSource = apply(url("./files/models"), [
        template({
          ...stringUtils,
          ...options,
          entity,
          entityName: entity.name,
          entityPluralized: entity.pluralized
        }),
        move("/src/app/shared/models")
      ]);

      rules.push(mergeWith(templateSource));
    });

    // For each entity in model scheme, generate the modules
    modelScheme.entities.forEach((entity: any) => {
      // TODO: Create model scheme typings
      // templates in ./files/module
      const templateSource = apply(url("./files/module"), [
        template({
          ...stringUtils,
          ...options,
          entity,
          entityName: entity.name,
          entityPluralized: entity.pluralized
        }),
        move("/src/app")
      ]);

      rules.push(mergeWith(templateSource));
    });

    let rule: Rule = chain(rules);

    return rule(tree, context);
  };
}
