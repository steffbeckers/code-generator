import { Injectable } from "./edit-__entityName@dasherize__/node_modules/@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from "@env/environment";

// RxJS
import { of } from "rxjs";

// Models
import { Account } from "./__entityPluralized@dasherize__";

@Injectable()
export class AccountService {
  constructor(private http: HttpClient) {}

  // Create
  public create(account: Account) {
    return this.http.post(environment.api + "/accounts", account);
  }

  // Read
  public getAll() {
    return this.http.get(environment.api + "/accounts");
  }

  public getById(id: string) {
    return this.http.get(environment.api + `/accounts/${id}`);
  }

  public getByName(name: string) {
    if (name === "") {
      return of([]);
    }

    let params = new HttpParams();
    params.set("name", name);

    return this.http.get(environment.api + "/accounts", { params });
  }

  // Update
  public update(id: string, account: Account) {
    return this.http.put(environment.api + `/accounts/${id}`, account);
  }

  public patch(id: string, account: Account) {
    return this.http.patch(environment.api + `/accounts/${id}`, account);
  }

  // Delete
  public delete(id: string) {
    return this.http.delete(environment.api + `/accounts/${id}`);
  }
}
