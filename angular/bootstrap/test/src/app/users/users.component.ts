import { Component, OnInit } from '@angular/core';

// Models
import { User } from './user';

// Services
import { UserService } from './user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  users: User[];

  constructor(private userService: UserService) {}

  ngOnInit() {
    this.userService.getAll().subscribe((users: User[]) => {
      this.users = users;
    });
  }
}
