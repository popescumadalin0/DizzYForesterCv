import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '../shared/shared.module';
import { LanguageComponent } from './components/language/language.component';
import { LoaderService } from './services/loader.service';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { LoadingInterceptor } from './interceptors/loading.interceptor';
import { HttpService } from './services/http.service';
import { TokenService } from './services/token.service';
import { AuthService } from './services/auth.service';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { AuthGuard } from './guards/auth.guard';

@NgModule({
  declarations: [LanguageComponent, SpinnerComponent],
  imports: [SharedModule, CommonModule],
  providers: [
    LoaderService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoadingInterceptor,
      multi: true,
    },
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    HttpService,
    TokenService,
    AuthService,
    AuthGuard,
  ],
  exports: [LanguageComponent, SpinnerComponent],
})
export class CoreModule {}
