import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { AuthGuard } from '../core/guards/auth.guard';
import { HomePageComponent } from './home-page/home-page.component';
import { LoginComponent } from './login/login.component';
import { NavbarContentComponent } from './navbar-content/navbar-content.component';
import { SkillsComponent } from './skills/skills.component';

const routes: Routes = [
  {
    path: 'sysadmin/login',
    component: LoginComponent,
    canActivate: [AuthGuard],
  },
  { path: ':sectionId', component: NavbarContentComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '**', redirectTo: '/home', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [],
})
export class ComponentsRoutingModule {}
