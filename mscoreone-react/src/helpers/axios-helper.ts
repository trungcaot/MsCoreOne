import axios from 'axios';
import { Constants } from '../constants';
import { AuthService } from '../services/auth-service';

const authService = new AuthService();

export class AxiosHelper {

  public async getAsync(url: string) {
    return axios.get(Constants.apiRoot + url);
  }

  public async postAsync(url: string, model: any) {
    const headers = await this.getHeaders();
    return axios.post(Constants.apiRoot + url, model, { headers });
  }

  public async putAsync(url: string, model: any) {
    const headers = await this.getHeaders();
    return axios.put(Constants.apiRoot + url, model, { headers });
  }

  public async deleteAsync(url: string){
    const headers = await this.getHeaders();
    return axios.delete(Constants.apiRoot + url, { headers });
  }

  private getTokenAsync() {
    return authService.getUserAsync().then(user => {
      if (user && user.access_token) {
        return user.access_token;
      } 
      else if (user) {
        return authService.renewTokenAsync()
          .then(renewedUser => {
            return renewedUser.access_token;
          });
      } else {
        window.location.href = "/unauthorized";
      }
    });
  }

  private async getHeaders() {
    var token = await this.getTokenAsync();
    return {
      Accept: 'application/json',
      Authorization: 'Bearer ' + token
    };
  }
}
