import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

// Models
import { Skill } from 'src/app/shared/models/Skill';

// Services
import { SkillService } from 'src/app/shared/services/SkillService';

@Component({
  selector: 'app-skill-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class SkillUpdateComponent implements OnInit {
  // Skill
  public skill: Skill;
  public skillForm: FormGroup;
  public updating: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private skillService: SkillService
  ) {
    this.skill = null;
    this.updating = false;
  }

  ngOnInit(): void {
    this.skillForm = this.fb.group({
      id: ['', Validators.required],
      name: ['', Validators.required],
      description: [''],
    });

    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getSkill(routeParams.id);
    });
  }

  private getSkill(id: string): void {
    this.skillService.getSkill(id).subscribe(
      (skill: Skill) => {
        this.skill = skill;
        this.skillForm.patchValue(this.skill);
      },
      (error: any) => {
        if (error.status === 404) {
          alert('Skill could not be found.');
          this.router.navigateByUrl('/skills');
        }
      }
    );
  }

  public updateSkill(andClose: boolean = false): void {
    // Validate
    if (this.skillForm.invalid || this.updating) {
      return;
    }

    // Only close when nothing changed
    if (this.skillForm.pristine && andClose) {
      this.router.navigateByUrl('/skills/' + this.skill.id);
      return;
    }

    // Already updating check
    this.updating = true;

    this.skillService.updateSkill(this.skillForm.value).subscribe(
      (skill: Skill) => {
        if (andClose) {
          this.router.navigateByUrl('/skills/' + skill.id);
        }

        this.skill = skill;
        this.skillForm.patchValue(this.skill);
      },
      null,
      () => {
        this.updating = false;
      }
    );
  }

  public deleteSkill(): void {
    // Validate
    if (!this.skill && !this.skill.id) {
      return;
    }

    // Confirmation
    if (confirm('Are you sure you want to delete skill: ' + this.skill.id + '?')) {
      this.skillService.deleteSkill(this.skill.id).subscribe(
        () => {
          this.router.navigateByUrl('/skills');
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
}
