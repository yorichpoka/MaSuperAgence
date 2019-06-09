import { Injectable } from '@angular/core';
import * as firebase from 'firebase';
import { IAuthenticationService } from './dao/iauthentication.service';

@Injectable({
  providedIn: 'root'
})

export class AuthenticationService implements IAuthenticationService {

  constructor() { }

  signInUser(email: string, password: string) {
    return new Promise(
      (functionResolve, functionReject) => {
        firebase.auth().signInWithEmailAndPassword(email, password).then(
          () => {
            functionResolve();
            console.log('connecté');
          },
          (error) => {
            functionReject(error);
          }
        )
      }
    );
  }

  signOutUser() {
    firebase.auth().signOut();
  }

}
