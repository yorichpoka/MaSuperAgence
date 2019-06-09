import { Component } from '@angular/core';
import * as firebase from 'firebase';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {

  title = 'maSuperAgence';

  constructor() {
    this.initFirebase();
  }

  initFirebase() {
    // Your web app's Firebase configuration
    var firebaseConfig = {
      apiKey: "AIzaSyBArAY9lT7ApMJaWwhCuQsul6whiR7yM_M",
      authDomain: "masuperagence-f7d51.firebaseapp.com",
      databaseURL: "https://masuperagence-f7d51.firebaseio.com",
      projectId: "masuperagence-f7d51",
      storageBucket: "masuperagence-f7d51.appspot.com",
      messagingSenderId: "620253849389",
      appId: "1:620253849389:web:046339a20b4a196c"
    };
    // Initialize Firebase
    firebase.initializeApp(firebaseConfig);
  }

}
