import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

// Services
import { AccountService } from '../account.service';

// Models
import { Account } from '../account';

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
