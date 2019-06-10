import { Injectable } from '@angular/core';
import * as firebase from 'firebase';
import { Property } from '../models/Property.model';
import { Subject } from 'rxjs';
import { IPropertyService } from './dao/iproperty.service';

@Injectable({
  providedIn: 'root'
})
export class PropertiesService implements IPropertyService {

  properties: Property[] = [];
  propertiesSubject = new Subject<Property[]>();

  constructor() { }

  emitProperties() {
    this.propertiesSubject.next(this.properties);
  }

  saveProperties() {
    firebase.database().ref('/properties').set(this.properties).then(
      (response) => {
        console.log('Enregistrement terminé, response: ' + response);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  createProperty(newProperty: Property) {
    this.properties.push(newProperty);
    this.saveProperties();
    this.emitProperties();
  }

  removeProperty(property: Property): void {
    const index = this.properties.findIndex(
      (value) => {
        if(value === property) {
          return true;
        }
      }
    );
    console.log(this.properties.splice(index, 1));
    this.saveProperties();
    this.emitProperties();
  }

  getProperties() {
    firebase.database().ref('/properties').on('value', 
    (data) => {
      this.properties = data.val()  ? data.val() 
                                    : [];
      this.emitProperties();
    })
  }

}
