export interface CodeGenOptions {
  metadata: string;

  saveMetadataAsXML: boolean;
  saveMetadataAsJSON: boolean;
  saveCRUDOptions: boolean;

  debug: boolean;
}

export interface CodeGenCrudOptions {
  entities: any[];
  enums: any[];
}
