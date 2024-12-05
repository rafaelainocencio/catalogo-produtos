import React from "react";
import "bootstrap/dist/css/bootstrap.min.css";

const Popup = ({show, message, onClose }) => {

  const handleClose = () => {
    onClose();
  };

  return (
    show && ( <div className={`modal fade ${show ? "show" : ""} d-block`} aria-labelledby="popupModal" aria-hidden="true"
        style={{
            display: show ? "block" : "none",
            backgroundColor: "rgba(0, 0, 0, 0.5)",
          }}
          tabIndex="-1"
          role="dialog"
    >
      <div className="modal-dialog modal-dialog-centered" role="document">
        <div className={`modal-content border-0`}>
          <div className="modal-header">
            <h5 className="modal-title" id="popupModal">Mensagem</h5>
          </div>
          <div className="modal-body">
            <p>{message}</p>
          </div>
          <div className="modal-footer">
            <button type="button" className="btn btn-secondary" onClick={handleClose}>
              Fechar
            </button>
          </div>
        </div>
      </div>
    </div>
    )
  );
};

export default Popup;
