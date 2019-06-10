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

  emitProperties(): void {
    this.propertiesSubject.next(this.properties);
  }

  saveProperties(): void {
    firebase.database().ref('/properties').set(this.properties).then(
      (response) => {
        console.log('Enregistrement terminé, response: ' + response);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  createProperty(newProperty: Property): void {
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

  getProperties(): void {
    firebase.database().ref('/properties').on('value', 
    (data) => {
      this.properties = data.val()  ? data.val() 
                                    : [];
      this.emitProperties();
    })
  }

  updateProperty(property: Property, id: number): void {
    firebase.database().ref('/properties/' + id).update(property);
  }

  uploadFile(file:File): Promise<any> {
    return new Promise(
      (functionResolve, functionReject) => {
        const uniqueId = Date.now().toString();
        const upload = firebase.storage().ref().child('images/properties/' + uniqueId + file.name).put(file);
        upload.on(firebase.storage.TaskEvent.STATE_CHANGED,
          () => {
            console.log('Loading...');
          },
          (error) => {
            functionReject();
            console.log('Error');
          },
          () => {
            upload.snapshot.ref.getDownloadURL().then(
              (downloadUrl) => {
                console.log('Uploaded...');
                functionResolve(downloadUrl);
              }
            );
          }
        );
      }
    );
  }

  removePropertyPhoto(photoLink: string) : void {
    if (photoLink) {
      const storageRef = firebase.storage().refFromURL(photoLink);
      storageRef.delete().then(
        () => {
          console.log('File deleted');
        }
      ).catch(
        (error) => {
          console.log('File not found : ' + error);
        }
      )
    }
  }

}
