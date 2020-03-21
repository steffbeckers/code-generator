import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

// Models
import { SkillAlias } from 'src/app/shared/models/SkillAlias';

// Services
import { SkillAliasService } from 'src/app/shared/services/SkillAliasService';

@Component({
  selector: 'app-skillalias-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class SkillAliasUpdateComponent implements OnInit {
  // SkillAlias
  public skillAlias: SkillAlias;
  public skillAliasForm: FormGroup;
  public updating: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private skillAliasService: SkillAliasService
  ) {
    this.skillAlias = null;
    this.updating = false;
  }

  ngOnInit(): void {
    this.skillAliasForm = this.fb.group({
      id: ['', Validators.required],
      name: ['', Validators.required],
      description: [''],
      skillId: ['', Validators.required],
    });

    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getSkillAlias(routeParams.id);
    });
  }

  private getSkillAlias(id: string): void {
    this.skillAliasService.getSkillAlias(id).subscribe(
      (skillAlias: SkillAlias) => {
        this.skillAlias = skillAlias;
        this.skillAliasForm.patchValue(this.skillAlias);
      },
      (error: any) => {
        if (error.status === 404) {
          alert('SkillAlias could not be found.');
          this.router.navigateByUrl('/skillaliases');
        }
      }
    );
  }

  public updateSkillAlias(andClose: boolean = false): void {
    // Validate
    if (this.skillAliasForm.invalid || this.updating) {
      return;
    }

    // Only close when nothing changed
    if (this.skillAliasForm.pristine && andClose) {
      this.router.navigateByUrl('/skillaliases/' + this.skillAlias.id);
      return;
    }

    // Already updating check
    this.updating = true;

    this.skillAliasService.updateSkillAlias(this.skillAliasForm.value).subscribe(
      (skillAlias: SkillAlias) => {
        if (andClose) {
          this.router.navigateByUrl('/skillaliases/' + skillAlias.id);
        }

        this.skillAlias = skillAlias;
        this.skillAliasForm.patchValue(this.skillAlias);
      },
      null,
      () => {
        this.updating = false;
      }
    );
  }

  public deleteSkillAlias(): void {
    // Validate
    if (!this.skillAlias && !this.skillAlias.id) {
      return;
    }

    // Confirmation
    if (confirm('Are you sure you want to delete skillalias: ' + this.skillAlias.id + '?')) {
      this.skillAliasService.deleteSkillAlias(this.skillAlias.id).subscribe(
        () => {
          this.router.navigateByUrl('/skillaliases');
        },
        (error: any) => {
          if (error.status === 404) {
            alert('SkillAlias could not be found.');
            this.router.navigateByUrl('/skillaliases');
          }
        }
      );
    }
  }
}
