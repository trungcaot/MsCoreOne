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
import { Button } from "@material-ui/core";
import SaveIcon from "@material-ui/icons/Save";
import DeleteIcon from '@material-ui/icons/Delete';
import ClearIcon from '@material-ui/icons/Clear';
import { useDispatch, useSelector } from "react-redux";
import Skeleton from '@material-ui/lab/Skeleton';
import  { setCategory, selectCategories, fetchCategories, selectLoading, 
          selectCategory, createCategory, updateCategory, fetchCategoryById,
          initCategory, deleteCategoryAsync
        } from "../../store/category-slice";
import { TextField } from "@material-ui/core";
import ConfirmDialog from "../shared/ConfirmDialog";

const useStyles = makeStyles({
  inputWidth: {
    width: "100%"
  }
});

const Category = () => {
  const classes = useStyles();

  const model = useSelector(selectCategory);
  const categories = useSelector(selectCategories);
  const loading = useSelector(selectLoading);
  const dispatch = useDispatch();

  const [editing, setEditing] = useState(false);
  const [open, setOpen] = useState(false);
  const [id, setId] = useState(0);
  
  useEffect(() => {
    dispatch(fetchCategories());
  }, [dispatch]);

  const change = (event: any) => {
    event.persist();
    dispatch(setCategory(event.target.value));
  };

  const clear = (event: any) => {
    event.persist();
    dispatch(initCategory());
    setEditing(false);
  }

  const edit = (id: number) => {
    dispatch(fetchCategoryById(id));
    setEditing(true);
  };

  const handleConfirm = () => {
    dispatch(deleteCategoryAsync(id));
  }

  const submit = () => { 
    if (editing){
      dispatch(updateCategory(model));
    } else {
      dispatch(createCategory(model));
    }
    setEditing(false);
  };

  return (
    <>
      <div className="row">
        <div className="col">
          <div className="card border-primary mb-3">
              <div className="card-header">{ editing ? "Edit category" : "Create category" }</div>
              <div className="card-body">
              <form>
                <div className="row">
                  <div className="col-8">
                    <div className="form-group row">
                      <label className="col-sm-3 col-form-label">Name</label>
                      <div className="col-sm-8">
                        <TextField
                          id="category-name"
                          name="name"
                          type="text"
                          variant="outlined"
                          size="small"
                          className={classes.inputWidth}
                          value={model.name}
                          onChange={change}
                        />
                      </div>
                    </div>
                  </div>
                  <div className="col-4">
                    <Button
                    id="btn-submit"
                    variant="outlined"
                    color="primary"
                    size="large"
                    className="float-right"
                    startIcon={<SaveIcon />}
                    onClick={submit}
                  >
                    Save
                  </Button>

                  <Button
                    id="btn-clear"
                    variant="outlined"
                    size="large"
                    className="float-right mr-2"
                    startIcon={<ClearIcon />}
                    onClick={clear}
                    >
                    Clear
                  </Button>
                  </div>
                </div>
              </form>
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
                            <TableCell>Id</TableCell>
                            <TableCell align="left">Name</TableCell>
                            <TableCell align="left">Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {categories.map(category => (
                            <TableRow key={category.id}>
                                <TableCell component="th" scope="row">
                                  {category.id}
                                </TableCell>                    
                                <TableCell align="left">{category.name}</TableCell>
                                <TableCell align="left">
                                  <Button
                                    id="btn-edit"
                                    variant="outlined"
                                    size="small"
                                    className="mr-2"
                                    startIcon={<EditIcon/>}
                                    onClick={() => edit(category.id)}
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
                                      setId(category.id);
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
          title="Delete Category?"
          open={open}
          setOpen={setOpen}
          onConfirm={handleConfirm}
          >
          Are you sure you want to delete this category?
        </ConfirmDialog>
      </div>
    </>
  )
}

export default Category;
