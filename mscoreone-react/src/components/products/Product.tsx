import React, { useEffect, useState } from "react";
import { makeStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import EditIcon from '@material-ui/icons/Edit';
import { TextField, Button, TextareaAutosize } from "@material-ui/core";
import SaveIcon from "@material-ui/icons/Save";
import ClearIcon from '@material-ui/icons/Clear';
import DeleteIcon from '@material-ui/icons/Delete';
import { useSelector, useDispatch } from "react-redux";
import { selectProducts, selectLoading, selectProduct, fetchProducts,
  initProduct, setProduct, deleteProductAsync, fetchProductById,
  createProduct, updateProduct } from "../../store/product-slice";
import { fetchBrands, selectBrands } from "../../store/brand-slice";
import { selectCategories, fetchCategories } from "../../store/category-slice";
import Skeleton from '@material-ui/lab/Skeleton';
import Select from '@material-ui/core/Select';
import MenuItem from '@material-ui/core/MenuItem';
import ConfirmDialog from "../shared/ConfirmDialog";
import { Constants } from "../../constants";
import IconButton from '@material-ui/core/IconButton';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import Collapse from '@material-ui/core/Collapse';

const useStyles = makeStyles({
  formInput: {
    width: "100%"
  }
});

const Product = () => {
  const classes = useStyles();

  const model = useSelector(selectProduct);
  const brands = useSelector(selectBrands);
  const categories = useSelector(selectCategories);
  const products = useSelector(selectProducts);
  const loading = useSelector(selectLoading);

  const [editing, setEditing] = useState(false);
  const [open, setOpen] = useState(false);
  const [id, setId] = useState(0);
  const [file, setFile] = useState();
  const [expanded, setExpanded] = useState(true);

  const dispatch = useDispatch();
  
  useEffect(() => {
    dispatch(fetchProducts());
    dispatch(fetchBrands());
    dispatch(fetchCategories());
  }, [dispatch]);

  const clear = (event: any) => {
    event.persist();
    dispatch(initProduct());
    setEditing(false);
  }

  const handleExpandClick = () => {
    setExpanded(!expanded);
  };

  const change = (event: any) => {
    event.persist();
    dispatch(setProduct({name: event.target.name, value: event.target.value}));
  };

  const handleConfirm = () => {
    dispatch(deleteProductAsync(id));
  }

  const submit = () => { 
    const formData = new FormData();
    formData.append("id", model.id.toString());
    formData.append("name", model.name);
    formData.append("price", model.price.toString());
    formData.append("rating", model.rating.toString());
    formData.append("file", file);
    formData.append("brandId", model.brandId.toString());
    formData.append("categoryId", model.categoryId.toString());
    formData.append("description", model.description);

    if (editing){
      dispatch(updateProduct(formData));
    } else {
      dispatch(createProduct(formData));
    }
    setEditing(false);
  };

  const changeFile = (event: any) => {
    setFile(event.target.files[0]);
  }

  const edit = (id: number) => {
    setEditing(true);
    dispatch(fetchProductById(id));
    setFile(undefined);
  };

  return (
    <>
      <div className="row">
        <div className="col">
          <div className="card border-primary mb-3">
              <div className="card-header"><span>{ editing ? "Edit product" : "Create product" }</span> 
              <IconButton
                onClick={handleExpandClick}
                className="float-right"
                aria-expanded={expanded}
                aria-label="show more"
              >
                <ExpandMoreIcon />
              </IconButton>
              </div>
              <div className="card-body">
                <Collapse in={expanded} timeout="auto">
                  <form>
                    <div className="row">
                      <div className="col-6">
                        <div className="form-group row">
                          <label className="col-sm-3 col-form-label">Product</label>
                          <div className="col-sm-9">
                            <TextField
                              id="name"
                              name="name"
                              type="text"
                              value={model.name}
                              className={classes.formInput}
                              variant="outlined"
                              size="small"
                              onChange={change}
                            />
                          </div>
                        </div>
                      </div>
                      <div className="col-6">
                        <div className="form-group row">
                          <label className="col-sm-3 col-form-label">Price</label>
                          <div className="col-sm-9">
                          <TextField
                            id="price"
                            name="price"
                            type="number"
                            value={model.price}
                            className={classes.formInput}
                            variant="outlined"
                            size="small"
                            onChange={change}
                          />
                          </div>
                        </div>
                      </div>
                    </div>
                    <div className="row">
                      <div className="col-6">
                        <div className="form-group row">
                          <label className="col-sm-3 col-form-label">Rating</label>
                          <div className="col-sm-9">
                          <TextField
                            id="rating"
                            name="rating"
                            type="number"
                            value={model.rating}
                            className={classes.formInput}
                            variant="outlined"
                            size="small"
                            onChange={change}
                          />
                          </div>
                        </div>
                      </div>
                      <div className="col-6">
                        <div className="form-group row">
                          <label className="col-sm-3 col-form-label">Image</label>
                          <div className="col-sm-9">
                          <TextField
                            id="file"
                            name="file"
                            type="file"
                            variant="outlined"
                            size="small"
                            className={classes.formInput}
                            onChange={changeFile}
                          />
                          </div>
                        </div>
                      </div>
                    </div>
                    <div className="row">
                      <div className="col-6">
                        <div className="form-group row">
                          <label className="col-sm-3 col-form-label">Brand</label>
                          <div className="col-sm-9">
                            <Select
                              name="brandId"
                              id="brand-id"
                              onChange={change}
                              value={model.brandId}
                              className={classes.formInput}
                              >
                                <MenuItem value="0">None</MenuItem>
                              {
                                brands.map(brand => (
                                  <MenuItem key={ brand.id } value={brand.id}>{ brand.name }</MenuItem>
                                ))
                              }
                            </Select>
                          </div>
                        </div>
                      </div>
                      <div className="col-6">
                        <div className="form-group row">
                          <label className="col-sm-3 col-form-label">Category</label>
                          <div className="col-sm-9">
                            <Select
                              id="category-id"
                              name="categoryId"
                              onChange={change}
                              value={model.categoryId}
                              className={classes.formInput}
                              >
                                <MenuItem value="0">None</MenuItem>
                              {
                                categories.map(category => (
                                  <MenuItem key={ category.id } value={category.id}>{ category.name }</MenuItem>
                                ))
                              }
                            </Select>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div className="row">
                      <div className="col-6">
                        <div className="form-group row">
                          <label className="col-sm-3 col-form-label">Description</label>
                          <div className="col-sm-9">
                            <TextareaAutosize
                              id="description"
                              name="description"
                              value={model.description}
                              className={classes.formInput}
                              rowsMin={3}
                              onChange={change}
                            />
                          </div>
                        </div>
                      </div>
                    </div>
                    <div className="row">
                      <div className="col">
                        <Button
                          id="btn-submit"
                          variant="outlined"
                          color="primary"
                          size="small"
                          className="float-right"
                          startIcon={<SaveIcon />}
                          onClick={submit}
                        >
                          Save
                        </Button>

                        <Button
                          id="btn-clear"
                          variant="outlined"
                          size="small"
                          className="float-right mr-2"
                          startIcon={<ClearIcon />}
                          onClick={clear}
                          >
                          Clear
                        </Button>
                      </div>
                    </div>
                  </form>
                </Collapse>
              </div>
          </div>
        </div>
      </div>

      <div className="row">
        <div className="col">
          {
            loading ?
            (
              <div>
                <Skeleton />
                <Skeleton />
                <Skeleton />  
              </div>
            ) : (
              <TableContainer component={Paper}>
                <Table aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Name</TableCell>
                            <TableCell>Image</TableCell>
                            <TableCell align="left">Price</TableCell>
                            <TableCell align="left">Description</TableCell>
                            <TableCell align="left">Rating</TableCell>
                            <TableCell align="left"></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {products.map(product => (
                            <TableRow key={product.id}>
                                <TableCell component="th" scope="row">
                                {product.name}
                                </TableCell>
                                <TableCell>
                                  <img width="60" height="80" alt={String(product.name)} src={Constants.apiRoot + String(product.thumbnailImageUrl)} ></img>
                                </TableCell>                    
                                <TableCell align="left">{product.price}</TableCell>
                                <TableCell align="left">{product.description}</TableCell>
                                <TableCell align="left">{product.rating}</TableCell>
                                <TableCell align="left">
                                  <Button
                                    id="btn-edit"
                                    variant="outlined"
                                    size="small"
                                    className="mr-2"
                                    startIcon={<EditIcon/>}
                                    onClick={() => edit(product.id)}
                                    >
                                    Edit
                                  </Button>
                                  <Button
                                    id="btn-delete"
                                    variant="outlined"
                                    color="secondary"
                                    size="small"
                                    className="mr-0"
                                    startIcon={<DeleteIcon/>}
                                    onClick={() => {
                                      setOpen(true);
                                      setId(product.id);
                                    }}
                                    >
                                    Delete
                                  </Button>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
            )
          } 
        </div>
          <ConfirmDialog
            title="Delete Product?"
            open={open}
            setOpen={setOpen}
            onConfirm={handleConfirm}
            >
            Are you sure you want to delete this product?
          </ConfirmDialog>
      </div>
    </>
  )
}

export default Product;
