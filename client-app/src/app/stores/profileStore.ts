import {RootStore} from './rootStore'
import { observable, action, runInAction } from 'mobx'
import { IProfile } from '../models/profile'
import agent from '../api/agent'

export default class ProfileStore {
    rootStore: RootStore
    constructor(rootStore: RootStore) {
        this.rootStore =  rootStore
    }

    @observable profile: IProfile | null = null;
    @observable loadingProfile = true;

    @action loadProfile = async (username: string) => {
        
            try {
                const profile = await agent.Profiles.get(username);
                runInAction(()=>{
                    this.profile = profile;
                    this.loadingProfile = false;
                    console.log('completed loading profile without error');
                })
            } catch (error) {
                runInAction(()=>{
                    this.loadingProfile = false;
                })
                console.log(error);
            }
            
            console.log(`completed loading profile. loadingProfile set to ${this.loadingProfile}
                        profile is ${this.profile?.displayName}
            `)
    }
}