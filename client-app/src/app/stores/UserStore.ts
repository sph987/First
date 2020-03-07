import { IUser, IUserFormValues } from "../models/user";
import { observable, computed, action, runInAction } from "mobx";
import agent from "../api/agent";
import { RootStore } from "./rootStore";
import { history } from "../..";

export default class UserStore {
  rootStore: RootStore;
  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
  }

  @observable user: IUser | null = null;

  @computed get isLoggedIn() {
    return !!this.user;
  }

  @action login = async (values: IUserFormValues) => {
    try {
      const user = await agent.User.login(values);
      runInAction(()=>{
        this.user = user;
        history.push('/activities')
      })

      console.log(user);
    } catch (error) {
      throw error;
    }
  };
}
