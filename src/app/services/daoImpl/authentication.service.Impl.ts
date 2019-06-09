import { IAuthenticationService } from '../dao/iauthentication.service';
import * as firebase from 'firebase';

export abstract class AuthenticationServiceImpl implements IAuthenticationService {
    
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