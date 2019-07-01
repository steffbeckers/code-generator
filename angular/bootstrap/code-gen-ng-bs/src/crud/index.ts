//import { Rule, Tree, SchematicContext } from "@angular-devkit/schematics";
import { Rule, Tree } from "@angular-devkit/schematics";
import { Observable } from "rxjs/internal/Observable";

const util = require("util");

// code generator
import { CodeGenOptions, CodeGenCrudOptions } from "./schema";

// To retrieve OData metadata
import fetch from "node-fetch";
import * as XMLJS from "xml-js";

export default function(options: CodeGenOptions): Rule {
  let crudOptions: CodeGenCrudOptions = {
    entities: [],
    enums: []
  };

  // return (tree: Tree, context: SchematicContext) => {
  return (tree: Tree) => {
    if (options.debug) {
      console.debug("OPTIONS");
      console.debug(options);
      console.debug();
    }

    // If no endpoint specified, don't do anything
    if (!options.metadata) {
      console.error(
        "Please provide an OData metadata endpoint with the --metadata=http://.../$metadata parameter."
      );
      return tree;
    }

    // When the $metadata is cut from CLI input
    if (!options.metadata.includes("$metadata")) {
      options.metadata += "$metadata";
    }

    return new Observable<Tree>(observer => {
      // Retrieve the OData $metadata
      fetch(options.metadata, {
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

          if (options.saveMetadataAsXML) {
            tree.create("metadata.xml", xml);
          }

          // Convert the metadata XML to JSON
          let metadataString: any = XMLJS.xml2json(xml);

          // Parse as JSON
          let metadata = JSON.parse(metadataString);

          if (options.debug) {
            console.debug("ODATA METADATA AS JSON");
            console.debug(metadata);
            console.debug();
          }

          if (options.saveMetadataAsJSON) {
            tree.create("metadata.json", JSON.stringify(metadata, null, 2));
          }

          // Get the data we need from this metadata
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

          // All data elements
          metadata.elements[0].elements[0].elements[0].elements.forEach(
            (element: any) => {
              // Entities
              if (element.name === "EntityType") {
                let entity: any = {
                  name: element.attributes.Name,
                  id: null,
                  fields: [],
                  relations: []
                };

                // Entity fields
                element.elements.forEach((fieldElement: any) => {
                  // Key
                  if (fieldElement.name === "Key") {
                    entity.key = fieldElement.elements[0].attributes.Name;
                  }

                  // Properties
                  if (fieldElement.name === "Property") {
                    let prop: any = {};
                    prop.name = fieldElement.attributes.Name;
                    prop.dataType = fieldElement.attributes.Type;
                    prop.required =
                      fieldElement.attributes.Nullable === "false"
                        ? true
                        : false;

                    // Custom for code generator
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

                  // Relations
                  if (fieldElement.name === "NavigationProperty") {
                    // Collection
                    if (fieldElement.attributes.Type.includes("Collection(")) {
                      let relation: any = {
                        name: fieldElement.attributes.Name
                      };

                      // Type
                      let type = fieldElement.attributes.Type.replace(
                        "Collection(",
                        ""
                      );
                      type = type.replace(")", "");
                      let typeArr = type.split(".");
                      relation.dataType = typeArr[typeArr.length - 1];

                      // Custom for code generator
                      relation.sort = fieldElement.attributes["codegen:Sort"]
                        ? parseInt(fieldElement.attributes["codegen:Sort"])
                        : null;

                      entity.relations.push(relation);
                    } else {
                      // Lookup
                      let lookup: any = {
                        name: fieldElement.attributes.Name
                      };

                      // Type
                      let typeArr = fieldElement.attributes.Type.split(".");
                      lookup.dataType = typeArr[typeArr.length - 1];

                      // Custom for code generator
                      lookup.forId =
                        fieldElement.attributes["codegen:LookupId"] || null;

                      entity.fields.push(lookup);
                    }
                  }
                });

                crudOptions.entities.push(entity);
              }

              // Enums
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

                crudOptions.enums.push(enumerable);
              }
            }
          );

          if (options.debug) {
            console.debug("CRUD OPTIONS");
            console.debug(util.inspect(crudOptions, false, null, true));
            console.debug();
          }

          if (options.saveCRUDOptions) {
            tree.create(
              "code-gen-model-scheme.json",
              JSON.stringify(crudOptions, null, 2)
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
