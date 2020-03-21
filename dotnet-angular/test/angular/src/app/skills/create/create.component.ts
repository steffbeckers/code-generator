import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// Models
import { Skill } from 'src/app/shared/models/Skill';

// Services
import { SkillService } from 'src/app/shared/services/SkillService';

@Component({
  selector: 'app-skill-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class SkillCreateComponent implements OnInit {
  // Skill
  public skillForm: FormGroup;
  public creating: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private skillService: SkillService
  ) {
    this.creating = false;
  }

  ngOnInit(): void {
    this.skillForm = this.fb.group({
      name: ['', Validators.required],
      description: [''],
      resumeId: [''],
      jobId: [''],
    });

    // Patch query params to form from URL
    this.route.queryParams.subscribe((queryParams) => {
      this.skillForm.patchValue(queryParams);
    });
  }

  public createSkill(): void {
    // Validate
    if (this.skillForm.invalid || this.creating) {
      return;
    }

    // Already creating check
    this.creating = true;

    this.skillService.createSkill(this.skillForm.value).subscribe(
      (skill: Skill) => {
        this.creating = false;

        this.router.navigateByUrl('/skills/' + skill.id);
      },
      null,
      () => {
        this.creating = false;
      }
    );
  }
}
