import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { AppThunk, RootState } from "./store";
import { AxiosHelper } from "../../src/helpers/axios-helper";

const axiosHelper = new AxiosHelper();

export interface ICategory {
  id: number;
  name: string;
}

export const initICategory = () => {
  return {
    id: 0,
    name: ""
  } as ICategory;
}

interface ICategoryState {
  readonly loading: boolean;
  readonly categories: ICategory[];
  readonly category: ICategory
}

const initialState: ICategoryState = {
  loading: true,
  categories: [] as ICategory[],
  category: initICategory()
}

export const categorySlice = createSlice({
  name: "category",
  initialState,
  reducers: {
    initCategory: (state) => {
      state.category = initICategory()
    },

    setCategory: (state, { payload } : PayloadAction<string>) => {
      state.category.name = payload;
    },

    fetchCategoriesSuccess: (state, { payload } : PayloadAction<ICategory[]>) => {
      state.loading = false;
      state.categories = payload;
    },

    fetchCategoryById: (state, { payload } : PayloadAction<number>) => {
      let category = state.categories.find(c => c.id === payload) as ICategory;

      state.category.id = category.id;
      state.category.name = category.name;
    },

    deleteCategorySuccess: (state, { payload } : PayloadAction<number>) => {
      state.categories = state.categories.filter(c => c.id !== payload);
    }
  }
});

export const { fetchCategoriesSuccess, fetchCategoryById, setCategory, initCategory, deleteCategorySuccess } = categorySlice.actions;

export const fetchCategories = (): AppThunk => async (dispatch) => {
  return axiosHelper.getAsync('/api/categories')
    .then(res => {
      dispatch(fetchCategoriesSuccess(res.data as ICategory[]));
    })
}

export const createCategory = (model: ICategory): AppThunk => async(dispatch) => {
  return axiosHelper.postAsync('/api/categories', model)
    .then(() => {
      dispatch(fetchCategories());
      dispatch(initCategory());
    })
}

export const updateCategory = (model: ICategory): AppThunk => async(dispatch) => {
  return axiosHelper.putAsync('/api/categories', model)
    .then(() => {
      dispatch(fetchCategories());
      dispatch(initCategory());
    })
}

export const deleteCategoryAsync = (id: number): AppThunk => async(dispatch) => {
  return axiosHelper.deleteAsync(`/api/categories/${id}`)
    .then(() => {
      dispatch(deleteCategorySuccess(id));
    })
}

export const selectLoading = (state: RootState) => state.category.loading;
export const selectCategory = (state: RootState) => state.category.category;
export const selectCategories = (state: RootState) => state.category.categories.map(b => {
    return {
      id: b.id,
      name: b.name
    }
  }
);

export default categorySlice.reducer;