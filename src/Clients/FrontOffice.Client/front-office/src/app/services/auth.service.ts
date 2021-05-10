import { User, UserManager, UserManagerSettings } from 'oidc-client';
import { environment } from 'src/environments/environment';

import { Injectable } from '@angular/core';
import { from, Observable } from 'rxjs';

export { User };

@Injectable({
  providedIn: 'root'
})
export class AuthService {

   userManager: UserManager;
   private user: User = null;

  constructor() {
    this.userManager = new UserManager(getClientSettings());
  }

  signinRedirectCallback() {
     return this.userManager.signinRedirectCallback();
  }

  isLoggedIn(): boolean {
    return this.user != null && !this.user.expired;
  }


public getUser(): Promise<User> {
  return this.userManager.getUser();
}

user$() {
  return from(this.userManager.getUser().then((user: User) => {
                                                                if (user && !user.expired)
                                                                  return user;
                                                                return this.renewToken();
        }));
}

public login(): Promise<void> {
  return this.userManager.signinRedirect();
}


  public renewToken(): Promise<User> {
    return this.userManager.signinSilent();
  }

  public logout(): Promise<void> {
    return this.userManager.signoutRedirect();
  }

}

export function getClientSettings(): UserManagerSettings {
  return {
    authority: environment.stsAuthority,
    client_id: environment.clientId,
    redirect_uri: `${environment.clientRoot}auth-callback`,
    post_logout_redirect_uri: `${environment.clientRoot}`,
    response_type: environment.responseType,
    scope: environment.clientScope,
    automaticSilentRenew: false,
    filterProtocolClaims: environment.filterProtocolClaims,
    loadUserInfo: environment.loadUserInfo,
    userStore: environment.userStore
  };
}
