import { Component, OnInit } from '@angular/core';
import { Profile } from 'oidc-client';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  profile: Profile = null;
  constructor(private authService: AuthService) { }

  ngOnInit(): void {
   this.authService.user$().subscribe(data => {
     this.profile = data.profile;
   });
  }

}
