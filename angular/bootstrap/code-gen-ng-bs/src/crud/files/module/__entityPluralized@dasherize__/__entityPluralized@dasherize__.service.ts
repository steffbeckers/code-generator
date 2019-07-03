import { Injectable } from "./edit-__entityName@dasherize__/node_modules/@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from "@env/environment";

// RxJS
import { of } from "rxjs";

// Models
import { <%= classify(entityName) %> } from "../shared/models/<%= dasherize(entityName) %>";

@Injectable()
export class <%= classify(entityName) %>Service {
  constructor(private http: HttpClient) {}

  // Create
  public create(<%= dasherize(entityName) %>: <%= classify(entityName) %>) {
    return this.http.post(environment.api + "/<%= dasherize(entityPluralized) %>", <%= dasherize(entityName) %>);
  }

  // Read
  public getAll() {
    return this.http.get(environment.api + "/<%= dasherize(entityPluralized) %>");
  }

  public getById(id: string) {
    return this.http.get(environment.api + `/<%= dasherize(entityPluralized) %>/${id}`);
  }

  public getByName(name: string) {
    if (name === "") {
      return of([]);
    }

    let params = new HttpParams();
    params.set("name", name);

    return this.http.get(environment.api + "/<%= dasherize(entityPluralized) %>", { params });
  }

  // Update
  public update(id: string, <%= dasherize(entityName) %>: <%= classify(entityName) %>) {
    return this.http.put(environment.api + `/<%= dasherize(entityPluralized) %>/${id}`, <%= dasherize(entityName) %>);
  }

  public patch(id: string, <%= dasherize(entityName) %>: <%= classify(entityName) %>) {
    return this.http.patch(environment.api + `/<%= dasherize(entityPluralized) %>/${id}`, <%= dasherize(entityName) %>);
  }

  // Delete
  public delete(id: string) {
    return this.http.delete(environment.api + `/<%= dasherize(entityPluralized) %>/${id}`);
  }
}
