import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

// Models
import { SkillAlias } from 'src/app/shared/models/SkillAlias';

// Services
import { SkillAliasService } from 'src/app/shared/services/SkillAliasService';

@Component({
  selector: 'app-skillalias-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class SkillAliasDetailComponent implements OnInit {
  // SkillAlias
  public skillAlias: SkillAlias;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private skillAliasService: SkillAliasService
  ) {
    this.skillAlias = null;
  }

  ngOnInit(): void {
    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getSkillAlias(routeParams.id);
    });
  }

  private getSkillAlias(id: string): void {
    this.skillAliasService.getSkillAlias(id).subscribe(
      (skillAlias: SkillAlias) => {
        this.skillAlias = skillAlias;
      },
      (error: any) => {
        if (error.status === 404) {
          alert('SkillAlias could not be found.');
          this.router.navigateByUrl('/skillaliases');
        }
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
