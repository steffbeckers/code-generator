import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";

// Models
import { Account } from "src/app/shared/models/Account";
import { Note } from "src/app/shared/models/Note";

// Services
import { AccountService } from "src/app/shared/services/AccountService";
import { NoteService } from "src/app/shared/services/NoteService";

@Component({
  selector: "app-account-link",
  templateUrl: "./link.component.html",
  styleUrls: ["./link.component.scss"]
})
export class AccountLinkComponent implements OnInit {
  // Account
  public account: Account;

  // Notes
  public notes: Note[];

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private accountService: AccountService,
    private noteService: NoteService
  ) {
    this.account = null;
    this.notes = null;
  }

  ngOnInit(): void {
    // Get params
    this.route.params.subscribe(routeParams => {
      this.getAccount(routeParams.id);

      switch (routeParams.model) {
        case "note":
          this.getNotes();
          break;
      }
    });
  }

  private getAccount(id: string): void {
    this.accountService.getAccount(id).subscribe(
      (account: Account) => {
        this.account = account;
      },
      (error: any) => {
        if (error.status === 404) {
          alert("Account could not be found.");
          this.router.navigateByUrl("/accounts");
        }
      }
    );
  }

  private getNotes(): void {
    this.noteService.getNotes().subscribe((notes: Note[]) => {
      this.notes = notes;
    });
  }
}
