import { Injectable } from "./edit-__entityName@dasherize__/node_modules/@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from "@env/environment";

// RxJS
import { of } from "rxjs";

// Models
import { Contact } from "../shared/models/contact";

@Injectable()
export class ContactService {
  constructor(private http: HttpClient) {}

  // Create
  public create(contact: Contact) {
    return this.http.post(environment.api + "/contacts", contact);
  }

  // Read
  public getAll() {
    return this.http.get(environment.api + "/contacts");
  }

  public getById(id: string) {
    return this.http.get(environment.api + `/contacts/${id}`);
  }

  public getByName(name: string) {
    if (name === "") {
      return of([]);
    }

    let params = new HttpParams();
    params.set("name", name);

    return this.http.get(environment.api + "/contacts", { params });
  }

  // Update
  public update(id: string, contact: Contact) {
    return this.http.put(environment.api + `/contacts/${id}`, contact);
  }

  public patch(id: string, contact: Contact) {
    return this.http.patch(environment.api + `/contacts/${id}`, contact);
  }

  // Delete
  public delete(id: string) {
    return this.http.delete(environment.api + `/contacts/${id}`);
  }
}
