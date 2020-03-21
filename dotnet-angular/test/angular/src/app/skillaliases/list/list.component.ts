import { Component, OnInit } from '@angular/core';

// Models
import { SkillAlias } from 'src/app/shared/models/SkillAlias';

// Services
import { SkillAliasService } from 'src/app/shared/services/SkillAliasService';

@Component({
  selector: 'app-skillaliases-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class SkillAliasesListComponent implements OnInit {
  public skillaliases: SkillAlias[];

  constructor(private skillAliasService: SkillAliasService) {
    this.skillaliases = null;
  }

  ngOnInit(): void {
    this.getSkillAliases();
  }

  private getSkillAliases(): void {
    this.skillAliasService.getSkillAliases().subscribe(
      (skillaliases: SkillAlias[]) => {
        this.skillaliases = skillaliases;
      }
    );
  }
}
