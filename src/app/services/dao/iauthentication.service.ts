
export interface IAuthenticationService {

    signInUser(email: string, password: string): Promise<any>;
    signOutUser(): void;

}