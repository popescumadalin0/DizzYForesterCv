import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { Constants } from 'src/app/models/Constants';
import { DropDownOptionModel } from 'src/app/models/DropDownOptionModel';

@Component({
  selector: 'app-language',
  templateUrl: './language.component.html',
  styleUrls: ['./language.component.scss'],
})
export class LanguageComponent implements OnInit {
  formGroup: FormGroup;
  languages: DropDownOptionModel<string, string>[] = [];
  constructor(
    public translateService: TranslateService,
    private formBuilder: FormBuilder
  ) {}
  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      formControl: [
        this.translateService.currentLang ?? this.translateService.defaultLang,
      ],
    });
    this.translateService.getLangs().forEach(lang => {
      this.languages.push({
        index: lang,
        value: this.translateService.instant(lang),
      });
    });

    this.formGroup.valueChanges.subscribe(event => {
      localStorage.setItem(Constants.LANGUAGE_KEY, event.formControl);
      this.translateService.use(event.formControl);
      this.translateService.setDefaultLang(event.formControl);
    });
  }
}
