import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-auth-callback',
  templateUrl: './auth-callback.component.html',
  styles: []
})
export class AuthCallbackComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.authService.signinRedirectCallback()
    .then(function (user) {
      console.log('user', user);
      window.history.replaceState({}, window.document.title, window.location.origin + window.location.pathname);
      window.location = user.state || "/";
  });
  }
}
