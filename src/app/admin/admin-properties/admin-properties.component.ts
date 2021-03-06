import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { PropertiesService } from 'src/app/services/properties.service';
import { Property } from 'src/app/models/Property.model';
import * as $ from 'jquery';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-admin-properties',
  templateUrl: './admin-properties.component.html',
  styleUrls: ['./admin-properties.component.css']
})

export class AdminPropertiesComponent implements OnInit, OnDestroy {

  propertyForm: FormGroup;
  properties: Property[];
  propertiersSubcription: Subscription;
  editProperty: boolean = false;
  photoUploading: boolean = false;
  photoUploaded: boolean = false;
  photosAdded: any[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private propertiesService: PropertiesService) { }

  ngOnInit() {
    this.initForm();
    this.propertiersSubcription = this.propertiesService.propertiesSubject.subscribe(
      (values: Property[]) => {
        this.properties = values;
      }
    );
    this.propertiesService.getProperties();
    this.propertiesService.emitProperties();
    this.photosAdded = [];
  }

  ngOnDestroy() {
    this.propertiersSubcription.unsubscribe();
  }

  initForm() {
    this.propertyForm = this.formBuilder.group({
      id: [''],
      title: [
        '',
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
      ],
      price: [
        0
      ]
    });
  }

  onSaveProperty() {
    const id = this.propertyForm.get('id').value;
    const title = this.propertyForm.get('title').value;
    const category = this.propertyForm.get('category').value;
    const surface = this.propertyForm.get('surface').value;
    const rooms = this.propertyForm.get('rooms').value;
    const description = this.propertyForm.get('description').value;
    const price = this.propertyForm.get('price').value;
    const photos = this.photosAdded ? this.photosAdded 
                                    : [];

    const newProperty = new Property(title, category, surface, rooms, description, photos, price);

    if(this.editProperty === true) {
      this.propertiesService.updateProperty(newProperty, id);
    } else {
      this.propertiesService.createProperty(newProperty);
    }

    $('#propertiesFormModal').modal('hide');

    this.propertyForm.reset();
    this.photoUploaded = false;
  }

  onDeleteProperty(property: Property) {
    if (property.photos) {
      property.photos.forEach(photo => {
        this.propertiesService.removePropertyPhoto(photo);
      });
    }
    this.propertiesService.removeProperty(property);
  }

  onEditProperty(property: Property, id: number) {
    this.propertyForm.get('id').setValue(id);
    this.propertyForm.get('category').setValue(property.category);
    this.propertyForm.get('description').setValue(property.description);
    this.propertyForm.get('rooms').setValue(property.rooms);
    this.propertyForm.get('surface').setValue(property.surface);
    this.propertyForm.get('title').setValue(property.title);
    this.propertyForm.get('price').setValue(property.price);
    
    this.photosAdded = property.photos  ? property.photos 
                                        : [];
    $('#propertiesFormModal').modal('show');
    this.editProperty = true;
  }

  resetPropertyForm() {
    this.editProperty = false;
    this.photoUploaded = false;
    this.photoUploading = false;
    this.propertyForm.reset();
  }

  detectFile(event) {
    this.photoUploading = true;
    this.propertiesService.uploadFile(event.target.files[0]).then(
      (url: string) => {
        console.log('File uploaded: ' + url);
        this.onAddPhoto(url);
        this.photoUploading = false;
        this.photoUploaded = true;
      }
    )
  }

  onAddPhoto(url: string) : void {
    this.photosAdded.push(url);
  }

  onRemoveAddedPhoto(id: number) : void {
    this.propertiesService.removePropertyPhoto(
      this.photosAdded[id]
    );
    this.photosAdded.splice(id, 1);
  }

}