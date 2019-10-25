import { Component, OnInit } from './node_modules/@angular/core';
import { ActivatedRoute } from './node_modules/@angular/router';

// Services
import { AccountService } from '../__entityName@dasherize__.service';

// Models
import { Account } from '../__entityName@dasherize__';

@Component({
  selector: 'app-accounts-detail',
  templateUrl: './detail-account.component.html',
  styleUrls: ['./detail-account.component.scss']
})
export class DetailAccountComponent implements OnInit {
  account: Account;

  constructor(private route: ActivatedRoute, private accountService: AccountService) {}

  ngOnInit() {
    this.getAccount();
  }

  getAccount() {
    let id = this.route.snapshot.params.id;
    if (id) {
      this.accountService.getById(id).subscribe((account: Account) => {
        this.account = account;
      });
    }
  }
}
