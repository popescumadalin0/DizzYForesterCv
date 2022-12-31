import { Component, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormControl } from '@angular/forms';
import { DropDownOptionModel } from 'src/app/models/DropDownOptionModel';

@Component({
  selector: 'app-dropdown-input',
  templateUrl: './dropdown-input.component.html',
  styleUrls: ['./dropdown-input.component.scss'],
})
export class DropdownInputComponent<T, TItem> implements OnInit {
  @Input() placeholder: string;
  @Input() options: DropDownOptionModel<T, TItem>[];
  @Input() control: FormControl;
  constructor() {
    this.control = new FormControl('');
  }
  ngOnInit(): void {}
}
