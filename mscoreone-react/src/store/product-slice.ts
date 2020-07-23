import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { AppThunk, RootState } from "./store";
import { AxiosHelper } from "../../src/helpers/axios-helper";

const axiosHelper = new AxiosHelper();

export interface IProduct {
  id: number;
  name: string;
  price: number;
  description: string;
  rating: number;
  brandId: number;
  categoryId: number;
  file: any | null;
  thumbnailImageUrl: string | null;
}

export const initIProduct = () => {
  return {
    id: 0,
    name: "",
    price: 0.0,
    description: "",
    rating: 0,
    brandId: 0,
    categoryId: 0,
    file: null,
    thumbnailImageUrl: null
  } as IProduct;
}

interface ProductState {
  readonly loading: boolean;
  readonly product: IProduct;
  readonly products: IProduct[];
}

const initialState: ProductState = {
  loading: true,
  product: initIProduct(),
  products: [] as IProduct[]
}

export const productSlice = createSlice({
  name: "product",
  initialState,
  reducers: {
    initProduct: (state) => {
      state.product = initIProduct();
    },

    setProduct: (state, { payload } : PayloadAction<any>) => {
      switch(payload.name)
      {
        case "name":
          state.product.name = payload.value;
          break;
        case "price":
          state.product.price = payload.value;
          break;
        case "description":
          state.product.description = payload.value;
          break;
        case "rating":
            state.product.rating = payload.value;
            break;
        case "brandId":
          state.product.brandId = payload.value;
          break;
        case "categoryId":
          state.product.categoryId = payload.value;
          break;
      }
    },

    fetchProductsSuccess: (state, { payload } : PayloadAction<IProduct[]>) => {
      state.loading = false;
      state.products = payload;
    },

    fetchProductById: (state, { payload } : PayloadAction<number>) => {
      let product = state.products.find(c => c.id === payload) as IProduct;
      
      state.product.id = product.id;
      state.product.name = product.name;
      state.product.price = product.price;
      state.product.rating = product.rating;
      state.product.brandId = product.brandId;
      state.product.categoryId = product.categoryId;
      state.product.description = product.description == null ? "" : product.description;
    },

    deleteProductSuccess: (state, { payload } : PayloadAction<number>) => {
      state.products = state.products.filter(c => c.id !== payload);
    }
  }
});

export const { initProduct, setProduct, fetchProductById, fetchProductsSuccess, deleteProductSuccess } = productSlice.actions;

export const fetchProducts = (): AppThunk => async (dispatch) => {
  return axiosHelper.getAsync('/api/products')
    .then(res => {
      dispatch(fetchProductsSuccess(res.data as IProduct[]));
    })
}

export const createProduct = (model: FormData): AppThunk => async(dispatch) => {
  return axiosHelper.postAsync('/api/products', model)
    .then(() => {
      dispatch(fetchProducts());
      dispatch(initProduct());
    });
}

export const updateProduct = (model: FormData): AppThunk => async(dispatch) => {
  return axiosHelper.putAsync('/api/products', model)
    .then(() => {
      dispatch(fetchProducts());
      dispatch(initProduct());
    })
}

export const deleteProductAsync = (id: number): AppThunk => async(dispatch) => {
  return axiosHelper.deleteAsync(`/api/products/${id}`)
    .then(() => {
      dispatch(deleteProductSuccess(id));
    })
}

export const selectProduct = (state: RootState) => state.product.product;
export const selectProducts = (state: RootState) => state.product.products;
export const selectLoading = (state: RootState) => state.product.loading;

export default productSlice.reducer;