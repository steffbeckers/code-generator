// tslint:disable: no-console
// tslint:disable: typedef

import { Rule, Tree } from "@angular-devkit/schematics";
import { Observable } from "rxjs/internal/Observable";

const util = require("util");

// code generator
import { CodeGenOptions, CodeGenModelScheme } from "./schema";

// to retrieve OData metadata
import fetch from "node-fetch";
import * as XMLJS from "xml-js";

function odataTypeToTypescriptType(type: string): string {
  switch (type) {
    case "Edm.Int64":
      return "number";
    case "Edm.Boolean":
      return "bool";
    case "Edm.String":
      return "string";
    case "Edm.Date":
      return "Date";
    case "Edm.Decimal":
      return "number";
    case "Edm.Double":
      return "number";
    case "Edm.Int32":
      return "number";
    case "Edm.Int16":
      return "number";
    case "Edm.DateTimeOffset":
      return "Date";
    case "Edm.Guid":
      return "string";
    default:
      return type;
  }
}

export default function(options: CodeGenOptions): Rule {
  let modelScheme: CodeGenModelScheme = {
    entities: [],
    enums: []
  };

  return (tree: Tree) => {
    if (options.debug) {
      console.debug("OPTIONS");
      console.debug(options);
      console.debug();
    }

    // if no endpoint specified, don't do anything
    if (!options.odataMetadata) {
      console.error(
        "Please provide an OData metadata endpoint with the --metadata=http://.../$metadata parameter."
      );
      return tree;
    }

    // when the $metadata is cut from CLI input
    if (!options.odataMetadata.includes("$metadata")) {
      options.odataMetadata += "$metadata";
    }

    return new Observable<Tree>(observer => {
      // retrieve the OData $metadata
      fetch(options.odataMetadata, {
        method: "GET",
        headers: {
          "Content-Type": "application/xml; charset=utf-8"
        }
      })
        .then((res: any) => res.text())
        .then((xml: string) => {
          if (options.debug) {
            console.debug("RETRIEVED XML");
            console.debug(xml);
            console.debug();
          }

          // convert the metadata XML to JSON
          let metadataString: any = XMLJS.xml2json(xml);

          // parse as JSON
          let metadata = JSON.parse(metadataString);

          if (options.debug) {
            console.debug("ODATA METADATA AS JSON");
            console.debug(metadata);
            console.debug();
          }

          // get the data we need from this metadata
          if (options.debug) {
            console.debug(
              util.inspect(
                metadata.elements[0].elements[0].elements[0].elements[0],
                false,
                null,
                true /* enable colors */
              )
            );
            console.debug();
          }

          // all data elements
          metadata.elements[0].elements[0].elements[0].elements.forEach(
            (element: any) => {
              // entities
              if (element.name === "EntityType") {
                let entity: any = {
                  name: element.attributes.Name,
                  pluralized: element.attributes["codegen:Pluralized"],
                  id: null,
                  fields: [],
                  relations: []
                };

                // entity fields
                element.elements.forEach((fieldElement: any) => {
                  // key
                  if (fieldElement.name === "Key") {
                    entity.key = fieldElement.elements[0].attributes.Name;
                  }

                  // properties
                  if (fieldElement.name === "Property") {
                    let prop: any = {};
                    prop.name = fieldElement.attributes.Name;
                    prop.dataType = odataTypeToTypescriptType(
                      fieldElement.attributes.Type
                    );
                    prop.required =
                      fieldElement.attributes.Nullable === "false"
                        ? true
                        : false;

                    // custom for code generator
                    prop.inputType =
                      fieldElement.attributes["codegen:InputType"] || null;
                    prop.displayName =
                      fieldElement.attributes["codegen:DisplayName"] || null;
                    prop.sort = fieldElement.attributes["codegen:Sort"]
                      ? parseInt(fieldElement.attributes["codegen:Sort"])
                      : null;
                    prop.hidden =
                      fieldElement.attributes["codegen:Hidden"] === "true"
                        ? true
                        : false;

                    entity.fields.push(prop);
                  }

                  // relations
                  if (fieldElement.name === "NavigationProperty") {
                    // collection
                    if (fieldElement.attributes.Type.includes("Collection(")) {
                      let relation: any = {
                        name: fieldElement.attributes.Name
                      };

                      // type
                      let type = fieldElement.attributes.Type.replace(
                        "Collection(",
                        ""
                      );
                      type = type.replace(")", "");
                      let typeArr = type.split(".");
                      relation.dataType = typeArr[typeArr.length - 1];

                      // custom for code generator
                      relation.sort = fieldElement.attributes["codegen:Sort"]
                        ? parseInt(fieldElement.attributes["codegen:Sort"])
                        : null;

                      entity.relations.push(relation);
                    } else {
                      // lookup
                      let lookup: any = {
                        name: fieldElement.attributes.Name,
                        lookup: true
                      };

                      // type
                      let typeArr = fieldElement.attributes.Type.split(".");
                      lookup.dataType = typeArr[typeArr.length - 1];

                      // custom for code generator
                      lookup.forId =
                        fieldElement.attributes["codegen:LookupId"] || null;

                      entity.fields.push(lookup);
                    }
                  }
                });

                modelScheme.entities.push(entity);
              }

              // enums
              if (element.name === "EnumType") {
                let enumerable: any = {
                  name: element.attributes.Name,
                  values: []
                };

                element.elements.forEach((enumElement: any) => {
                  enumerable.values.push({
                    name: enumElement.attributes.Name,
                    value: enumElement.attributes.Value
                  });
                });

                modelScheme.enums.push(enumerable);
              }
            }
          );

          if (options.debug) {
            console.debug("MODEL SCHEME");
            console.debug(util.inspect(modelScheme, false, null, true));
            console.debug();
          }

          if (!tree.exists("code-gen-model-scheme.json")) {
            tree.create(
              "code-gen-model-scheme.json",
              JSON.stringify(modelScheme, null, 2)
            );
          } else {
            tree.overwrite(
              "code-gen-model-scheme.json",
              JSON.stringify(modelScheme, null, 2)
            );
          }
          observer.next(tree);
          observer.complete();
        })
        .catch((err: any) => {
          observer.error(err);
        });
    });
  };
}
