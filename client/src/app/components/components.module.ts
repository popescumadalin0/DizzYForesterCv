import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '../shared/shared.module';
import { ComponentsRoutingModule } from './components-routing.module';
import { HomePageComponent } from './home-page/home-page.component';
import { LoginComponent } from './login/login.component';
import { SkillsComponent } from './skills/skills.component';
import { NavbarContentComponent } from './navbar-content/navbar-content.component';
import { ProjectsComponent } from './projects/projects.component';
import { CoreModule } from '../core/core.module';
import { BaseComponent } from './base/base.component';

@NgModule({
  declarations: [
    HomePageComponent,
    LoginComponent,
    SkillsComponent,
    NavbarContentComponent,
    ProjectsComponent,

  ],
  imports: [ComponentsRoutingModule, CommonModule, SharedModule, CoreModule],
  providers: [],
  exports: [
    HomePageComponent,
    LoginComponent,
    SkillsComponent,
    NavbarContentComponent,
    ProjectsComponent,
    CoreModule,
    
  ],
})
export class ComponentsModule {}
