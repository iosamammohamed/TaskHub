import { Injectable } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { authConfig } from './auth.config';
import { environment } from '../../../environments/environment';


@Injectable({
    providedIn: 'root'
})
export class AuthService {

    constructor(private oauthService: OAuthService) { }

    public async runInitialLoginSequence(): Promise<void> {
        this.oauthService.configure(authConfig);
        await this.oauthService.loadDiscoveryDocumentAndTryLogin();

        if (this.oauthService.hasValidAccessToken()) {
            this.oauthService.setupAutomaticSilentRefresh();
        }
    }


    public login() {
        (this.oauthService as any).initCodeFlow(undefined, {
            audience: environment.authAudience,
            prompt: 'select_account'
        });
    }



    public logout() {
        this.oauthService.logOut();
    }

    public get identityClaims() {
        return this.oauthService.getIdentityClaims();
    }

    public get isLoggedIn(): boolean {
        return this.oauthService.hasValidAccessToken();
    }
}
