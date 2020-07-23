import React from 'react';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';

interface IConfirmDialogProps {
  title: string;
  children: any,
  open: any,
  setOpen: any,
  onConfirm: any
}

const ConfirmDialog = (props: IConfirmDialogProps) => {
  const { title, children, open, setOpen, onConfirm } = props;
  
  return (
      <Dialog
        open={open}
        onClose={() => setOpen(false)}
        aria-labelledby="confirm-dialog"
      >
        <DialogTitle id="confirm-dialog">{title}</DialogTitle>
        <DialogContent>{children}</DialogContent>
        <DialogActions>
          <Button 
            variant="outlined"
            onClick={() => setOpen(false)}
            color="default"
          >
            No
          </Button>
          <Button
            variant="outlined"
            color="primary"
            onClick={() => {
              setOpen(false);
              onConfirm();
            }}
          >
            Yes
          </Button>
        </DialogActions>
      </Dialog>
  )
};
export default ConfirmDialog;