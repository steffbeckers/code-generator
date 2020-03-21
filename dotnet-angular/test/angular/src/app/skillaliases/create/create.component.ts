import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// Models
import { SkillAlias } from 'src/app/shared/models/SkillAlias';

// Services
import { SkillAliasService } from 'src/app/shared/services/SkillAliasService';

@Component({
  selector: 'app-skillalias-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class SkillAliasCreateComponent implements OnInit {
  // SkillAlias
  public skillAliasForm: FormGroup;
  public creating: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private skillAliasService: SkillAliasService
  ) {
    this.creating = false;
  }

  ngOnInit(): void {
    this.skillAliasForm = this.fb.group({
      name: ['', Validators.required],
      description: [''],
      skillId: ['', Validators.required],
    });

    // Patch query params to form from URL
    this.route.queryParams.subscribe((queryParams) => {
      this.skillAliasForm.patchValue(queryParams);
    });
  }

  public createSkillAlias(): void {
    // Validate
    if (this.skillAliasForm.invalid || this.creating) {
      return;
    }

    // Already creating check
    this.creating = true;

    this.skillAliasService.createSkillAlias(this.skillAliasForm.value).subscribe(
      (skillAlias: SkillAlias) => {
        this.creating = false;

        this.router.navigateByUrl('/skillaliases/' + skillAlias.id);
      },
      null,
      () => {
        this.creating = false;
      }
    );
  }
}
