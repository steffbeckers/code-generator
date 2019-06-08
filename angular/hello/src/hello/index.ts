import {
  Rule,
  SchematicContext,
  Tree,
  apply,
  mergeWith,
  template,
  url
} from "@angular-devkit/schematics";

import { strings } from "@angular-devkit/core";

import { Schema } from "./schema";

export function hello(_options: Schema): Rule {
  return (tree: Tree, _context: SchematicContext) => {
    // const { name } = _options;

    // // Hello world!
    // tree.create("hello.js", `console.log("Hello ${name}!");`);

    // return tree;

    const sourceTemplates = url("./templates");

    const sourceParametrizedTemplates = apply(sourceTemplates, [
      template({
        ..._options,
        ...strings
      })
    ]);

    return mergeWith(sourceParametrizedTemplates)(tree, _context);
  };
}
