import { Rule, SchematicContext, Tree } from "@angular-devkit/schematics";
import modelScheme from "./model-scheme.json";

export default function(_options: any): Rule {
  return (tree: Tree, _context: SchematicContext) => {
    tree.create(
      "code-gen-model-scheme.json",
      JSON.stringify(modelScheme, null, 2) + "\n"
    );

    return tree;
  };
}
