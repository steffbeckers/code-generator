import { Component, OnInit } from './node_modules/@angular/core';
import { ActivatedRoute } from './node_modules/@angular/router';

// Services
import { UserService } from '../__entityName@dasherize__.service';

// Models
import { User } from '../__entityName@dasherize__';

@Component({
  selector: 'app-users-detail',
  templateUrl: './detail-user.component.html',
  styleUrls: ['./detail-user.component.scss']
})
export class DetailUserComponent implements OnInit {
  user: User;

  constructor(private route: ActivatedRoute, private userService: UserService) {}

  ngOnInit() {
    this.getUser();
  }

  getUser() {
    let id = this.route.snapshot.params.id;
    if (id) {
      this.userService.getById(id).subscribe((user: User) => {
        this.user = user;
      });
    }
  }
}
