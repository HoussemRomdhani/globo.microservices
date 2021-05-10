export const environment = {
  production: false,
  stsAuthority: 'https://localhost:5001',
  clientId: 'angularClient',
  clientRoot: 'http://localhost:4200/',
  clientScope: 'openid profile offline_access eventcatalog.fullaccess',
  apiRoot: 'https://localhost:5010/api',
  responseType: "code",
  filterProtocolClaims: true,
  loadUserInfo: true
};
