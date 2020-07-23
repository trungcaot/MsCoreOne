import { Log, User, UserManager } from "oidc-client";
import { Constants } from '../constants';

const oidcSettings = {
  authority: Constants.authority,
  client_id: Constants.clientId,
  redirect_uri: `${Constants.clientRoot}/authentication/login-callback`,
  post_logout_redirect_uri: `${Constants.clientRoot}/authentication/logout-callback`,
  response_type: "code",
  scope: Constants.clientScope,
  automaticSilentRenew: true,
  includeIdTokenInSilentRenew: true,
};

export class AuthService {
  public userManager: UserManager;

  constructor() {
    this.userManager = new UserManager(oidcSettings);

    Log.logger = console;
    Log.level = Log.INFO;
  }

  public getUserAsync(): Promise<User | null> {
    return this.userManager.getUser();
  }

  public loginAsync(): Promise<void> {
    return this.userManager.signinRedirect();
  }

  public completeLoginAsync(url: string): Promise<User> {
    return this.userManager.signinCallback(url);
  }

  public renewTokenAsync(): Promise<User> {
    return this.userManager.signinSilent();
  }

  public logoutAsync(): Promise<void> {
    return this.userManager.signoutRedirect();
  }

  public async completeLogoutAsync(url: string): Promise<void> {
    await this.userManager.signoutCallback(url);
  }
}
