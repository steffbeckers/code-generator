export interface IModelScheme {
  models: IModel[];
}

export interface IModel {
  name: string;
  plural: string;
  entity: string;
  api: IEndpoint;
  filter: string[];
  fields: IField[];
}

export interface IEndpoint {
  url: string;
}

export interface IField {
  name: string;
  label: string;
  type: TypeOptions;
  control: ControlOptions | string;
  isId: boolean;
  readonly: boolean;
  default: any;
  validation: string;
  show: ShowOptions[] | string;
}

export type TypeOptions = "text" | "number" | "Date";
export type ShowOptions = "filter" | "list" | "insert" | "update";
export type ControlOptions = "text" | "number" | "lookup";
