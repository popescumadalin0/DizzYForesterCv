import { Component, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})
export class TextInputComponent implements OnInit {
  @Input() control: FormControl;
  @Input() typeInput: string = 'text';
  @Input() placeholder: string;
  @Input() inputStyle: string;

  constructor() {
    this.control = new FormControl('');
  }
  ngOnInit(): void {
  }
}
