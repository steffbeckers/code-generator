import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

// Models
import { Skill } from 'src/app/shared/models/Skill';

// Services
import { SkillService } from 'src/app/shared/services/SkillService';

@Component({
  selector: 'app-skill-link',
  templateUrl: './link.component.html',
  styleUrls: ['./link.component.scss']
})
export class SkillLinkComponent implements OnInit {
  // Skill
  public skill: Skill;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private skillService: SkillService
  ) {
    this.skill = null;
  }

  ngOnInit(): void {
    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getSkill(routeParams.id);
    });
  }

  private getSkill(id: string): void {
    this.skillService.getSkill(id).subscribe(
      (skill: Skill) => {
        this.skill = skill;
      },
      (error: any) => {
        if (error.status === 404) {
          alert('Skill could not be found.');
          this.router.navigateByUrl('/skills');
        }
      }
    );
  }
}
