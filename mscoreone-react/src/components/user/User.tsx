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
import  { fetchUsers, selectUsers, selectLoading, updateUser, deleteUserAsync,
          selectUser, initUser, setUser, fetchUserById, createUser
        } from "../../store/user-slice";
import { useDispatch, useSelector } from "react-redux";
import Skeleton from '@material-ui/lab/Skeleton';
import { TextField, Button } from "@material-ui/core";
import SaveIcon from "@material-ui/icons/Save";
import DeleteIcon from '@material-ui/icons/Delete';
import ClearIcon from '@material-ui/icons/Clear';
import ConfirmDialog from "../shared/ConfirmDialog";

const useStyles = makeStyles({
  table: {
    minWidth: 650,
  },
  marginRight: {
    marginRight: 10
  },
  inputWidth: {
    width: "100%"
  }
});

const User = () => {
  const classes = useStyles();

  const [editing, setEditing] = useState(false);
  const [open, setOpen] = useState(false);
  const [id, setId] = useState("");

  const model = useSelector(selectUser);

  const users = useSelector(selectUsers);
  const loading = useSelector(selectLoading);

  const dispatch = useDispatch();
  
  useEffect(() => {
    dispatch(fetchUsers());
  }, [dispatch]);

  const change = (event: any) => {
    event.persist();
    dispatch(setUser({name: event.target.name, value: event.target.value}));
  };

  const clear = (event: any) => {
    event.persist();
    setEditing(false);
    dispatch(initUser());
  }

  const handleConfirm = () => {
    dispatch(deleteUserAsync(id));
  }

  const edit = (id: string) => {
    dispatch(fetchUserById(id));
    setEditing(true);
  };

  const submit = () => { 
    if (editing){
      dispatch(updateUser(model));
    } else {
      dispatch(createUser(model));
    }
    setEditing(false);
  };

  return (
    <>
      <div className="row">
        <div className="col">
          <div className="card border-primary mb-3">
              <div className="card-header">{ editing ? "Edit user" : "Create user" }</div>
              <div className="card-body">
              <form>
                <div className="row">
                  <div className="col-6">
                    <div className="form-group row">
                    <label className="col-sm-3 col-form-label">First Name</label>
                    <div className="col-sm-9">
                      <TextField
                        id="first-name"
                        name="firstName"
                        type="text"
                        variant="outlined"
                        size="small"
                        className={classes.inputWidth}
                        value={model.firstName}
                        onChange={change}
                      />
                    </div>
                  </div>
                  </div>
                  <div className="col-6">
                    <div className="form-group row">
                    <label className="col-sm-3 col-form-label">Last Name</label>
                    <div className="col-sm-9">
                      <TextField
                        id="last-name"
                        name="lastName"
                        type="text"
                        variant="outlined"
                        size="small"
                        className={classes.inputWidth}
                        value={model.lastName}
                        onChange={change}
                      />
                    </div>
                  </div>
                  </div>
                </div>
                <div className="row">
                  <div className="col-6">
                    <div className="form-group row">
                      <label className="col-sm-3 col-form-label">User Name</label>
                      <div className="col-sm-9">
                        <TextField
                          id="username"
                          name="userName"
                          type="text"
                          variant="outlined"
                          disabled={editing}
                          size="small"
                          className={classes.inputWidth}
                          value={model.userName}
                          onChange={change}
                        />
                      </div>
                    </div>
                  </div>
                  <div className="col-6">
                    <div className="form-group row">
                      <label className="col-sm-3 col-form-label">Password</label>
                      <div className="col-sm-9">
                        <TextField
                          id="password"
                          name="password"
                          type="password"
                          variant="outlined"
                          disabled={editing}
                          size="small"
                          className={classes.inputWidth}
                          value={model.password}
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
                <Table className={classes.table} aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>First Name</TableCell>
                            <TableCell align="left">Last Name</TableCell>
                            <TableCell align="left">UserName</TableCell>
                            <TableCell align="left">Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {users.map(user => (
                            <TableRow key={user.id}>
                                <TableCell component="th" scope="row">
                                  {user.firstName}
                                </TableCell>                    
                                <TableCell align="left">{user.lastName}</TableCell>
                                <TableCell align="left">{user.userName}</TableCell>
                                <TableCell align="left">
                                  <Button
                                    id="btn-edit"
                                    variant="outlined"
                                    size="small"
                                    className="mr-2"
                                    startIcon={<EditIcon/>}
                                    onClick={() => edit(user.id)}
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
                                      setId(user.id);
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
          title="Delete User?"
          open={open}
          setOpen={setOpen}
          onConfirm={handleConfirm}
          >
          Are you sure you want to delete this user?
        </ConfirmDialog>
      </div>
    </>
  )
}

export default User;
