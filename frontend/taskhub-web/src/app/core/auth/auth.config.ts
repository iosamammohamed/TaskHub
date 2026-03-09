import { AuthConfig } from 'angular-oauth2-oidc';
import { environment } from '../../../environments/environment';

export const authConfig: AuthConfig = {
    // Auth0 / Identity Provider URL
    issuer: environment.authAuthority,

    // URL of the SPA to redirect the user to after login (matches Auth0 Allowed Callback URL)
    redirectUri: window.location.origin + '/',

    // URL to redirect after logout
    postLogoutRedirectUri: window.location.origin + '/',

    // The SPA's id. The SPA is registerd with this id at the auth-server
    clientId: environment.authClientId,







    // set the scope for the permissions the client should request
    scope: 'openid profile email offline_access',

    responseType: 'code',

    showDebugInformation: !environment.production,

    // Often needed for Auth0 / ADFS if the URL doesn't match exactly
    strictDiscoveryDocumentValidation: false,
    skipIssuerCheck: true,
};

