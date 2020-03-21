import { Component, OnInit } from '@angular/core';

// Models
import { Skill } from 'src/app/shared/models/Skill';

// Services
import { SkillService } from 'src/app/shared/services/SkillService';

@Component({
  selector: 'app-skills-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class SkillsListComponent implements OnInit {
  public skills: Skill[];

  constructor(private skillService: SkillService) {
    this.skills = null;
  }

  ngOnInit(): void {
    this.getSkills();
  }

  private getSkills(): void {
    this.skillService.getSkills().subscribe(
      (skills: Skill[]) => {
        this.skills = skills;
      }
    );
  }
}
