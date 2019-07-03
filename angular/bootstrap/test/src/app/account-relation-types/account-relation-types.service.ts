import { Injectable } from "./edit-__entityName@dasherize__/node_modules/@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from "@env/environment";

// RxJS
import { of } from "rxjs";

// Models
import { AccountRelationType } from "../shared/models/account-relation-type";

@Injectable()
export class AccountRelationTypeService {
  constructor(private http: HttpClient) {}

  // Create
  public create(account-relation-type: AccountRelationType) {
    return this.http.post(environment.api + "/account-relation-types", account-relation-type);
  }

  // Read
  public getAll() {
    return this.http.get(environment.api + "/account-relation-types");
  }

  public getById(id: string) {
    return this.http.get(environment.api + `/account-relation-types/${id}`);
  }

  public getByName(name: string) {
    if (name === "") {
      return of([]);
    }

    let params = new HttpParams();
    params.set("name", name);

    return this.http.get(environment.api + "/account-relation-types", { params });
  }

  // Update
  public update(id: string, account-relation-type: AccountRelationType) {
    return this.http.put(environment.api + `/account-relation-types/${id}`, account-relation-type);
  }

  public patch(id: string, account-relation-type: AccountRelationType) {
    return this.http.patch(environment.api + `/account-relation-types/${id}`, account-relation-type);
  }

  // Delete
  public delete(id: string) {
    return this.http.delete(environment.api + `/account-relation-types/${id}`);
  }
}
