import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { AppThunk, RootState } from "./store";
import { AxiosHelper } from "../../src/helpers/axios-helper";

const axiosHelper = new AxiosHelper();

export interface IBrand {
  id: string;
  name: string;
}

interface IBrandState {
  readonly brands: IBrand[];
}

const initialState: IBrandState = {
  brands: [] as IBrand[]
}

export const brandSlice = createSlice({
  name: "brand",
  initialState,
  reducers: {
    fetchBrandsSuccess: (state, { payload } : PayloadAction<IBrand[]>) => {
      state.brands = payload;
    }
  }
});

export const { fetchBrandsSuccess } = brandSlice.actions;

export const fetchBrands = (): AppThunk => async (dispatch) => {
  return axiosHelper.getAsync('/api/brands')
    .then(res => {
      dispatch(fetchBrandsSuccess(res.data as IBrand[]));
    })
}

export const selectBrands = (state: RootState) => state.brand.brands.map(b => {
    return {
      id: b.id,
      name: b.name
    }
  }
);

export default brandSlice.reducer;