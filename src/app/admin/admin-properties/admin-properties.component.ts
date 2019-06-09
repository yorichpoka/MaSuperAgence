import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-admin-properties',
  templateUrl: './admin-properties.component.html',
  styleUrls: ['./admin-properties.component.css']
})

export class AdminPropertiesComponent implements OnInit {

  propertyForm: FormGroup;

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.initForm();
  }

  initForm() {
    this.propertyForm = this.formBuilder.group({
      title: [
        'aaa',
        Validators.required
      ],
      category: [
        '',
        Validators.required
      ],
      surface: [
        '',
        Validators.required
      ],
      rooms: [
        '',
        Validators.required
      ],
      description: [
        ''
      ]
    });
  }

  onSaveProperty() {
    const title = this.propertyForm.get('title').value;
    const category = this.propertyForm.get('category').value;
    const surface = this.propertyForm.get('surface').value;
    const rooms = this.propertyForm.get('rooms').value;
    const description = this.propertyForm.get('description').value;

    console.log(this.propertyForm.value);
  }

}
