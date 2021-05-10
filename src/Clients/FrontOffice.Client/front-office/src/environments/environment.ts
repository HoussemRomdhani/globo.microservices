// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

import { WebStorageStateStore } from "oidc-client";

export const environment = {
  production: false,
  responseType: "id_token token",
  stsAuthority: 'https://localhost:5010',
  clientId: 'front',
  clientRoot: 'http://localhost:4200/',
  clientScope: 'openid profile address email phone basket.fullaccess ordering.fullaccess',
  apiRoot: 'http://localhost:5000/api',
  basketRoot: 'http://localhost:5002/api',
  orderingRoot: 'http://localhost:5003/api',
  countiesApiRoot: 'https://countriesnow.space/api/v0.1',
  filterProtocolClaims: true,
  loadUserInfo: true,
  userStore: new WebStorageStateStore({ store: window.localStorage })
};
