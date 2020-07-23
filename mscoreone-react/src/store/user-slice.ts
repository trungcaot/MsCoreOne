import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { AppThunk, RootState } from "./store";
import { AxiosHelper } from "../../src/helpers/axios-helper";

const axiosHelper = new AxiosHelper();

export interface IUser {
  id: string;
  firstName: string;
  lastName: string;
  userName: string;
  email: string;
  password: string;
}

export const initIUser = () => {
  return {
    id: "",
    firstName: "",
    lastName: "",
    userName: "",
    password: "",
    email: ""
  } as IUser;
}

interface IUserState {
  readonly loading: boolean;
  readonly users: IUser[];
  readonly user: IUser;
}

const initialState: IUserState = {
  loading: true,
  users: [] as IUser[],
  user: initIUser()  
}

export const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    initUser: (state) => {
      state.user = initIUser();
    },

    setUser: (state, { payload } : PayloadAction<any>) => {
      switch(payload.name)
      {
        case "firstName":
          state.user.firstName = payload.value;
          break;
        case "lastName":
          state.user.lastName = payload.value;
          break;
        case "userName":
          state.user.userName = payload.value;
          break;
        case "password":
          state.user.password = payload.value;
          break;
      }
    },

    fetchUsersSuccess: (state, { payload } : PayloadAction<IUser[]>) => {
      state.loading = false;
      state.users = payload.map(p => {
        return {
          id: p.id,
          firstName: p.firstName,
          lastName: p.lastName,
          userName: p.userName,
          password: ""
        } as IUser
      });
    },

    fetchUserById: (state, { payload } : PayloadAction<string>) => {
      let user = state.users.find(c => c.id === payload) as IUser;

      state.user.id = user.id;
      state.user.firstName = user.firstName || "";
      state.user.lastName = user.lastName || "";
      state.user.userName = user.userName;
      state.user.password = user.password;
    },
    
    deleteUserSuccess: (state, { payload } : PayloadAction<string>) => {
      state.users = state.users.filter(c => c.id !== payload);
    }
  }
});

export const { initUser, fetchUsersSuccess, deleteUserSuccess, fetchUserById, setUser } = userSlice.actions;

export const fetchUsers = (): AppThunk => async (dispatch) => {
  return axiosHelper.getAsync('/api/users')
    .then(res => {
      dispatch(fetchUsersSuccess(res.data as IUser[]));
    });
}

export const createUser = (model: IUser): AppThunk => async(dispatch) => {
  return axiosHelper.postAsync('/api/users', model)
    .then(() => {
      dispatch(fetchUsers());
      dispatch(initUser());
    })
}

export const updateUser = (model: IUser): AppThunk => async(dispatch) => {
  return axiosHelper.putAsync('/api/users', model)
    .then(() => {
      dispatch(fetchUsers());
      dispatch(initUser());
    })
}

export const deleteUserAsync = (id: string): AppThunk => async(dispatch) => {
  return axiosHelper.deleteAsync(`/api/users/${id}`)
    .then(() => {
      dispatch(deleteUserSuccess(id));
    })
}

export const selectUsers = (state: RootState) => state.user.users;
export const selectLoading = (state: RootState) => state.user.loading;
export const selectUser = (state: RootState) => state.user.user;

export default userSlice.reducer;