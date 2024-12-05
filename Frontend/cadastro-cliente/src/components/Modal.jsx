import React, { useState, useEffect } from 'react';

const TesteModal = ({ show, onClose, form, setForm, handleSubmit }) => {

  const [documentoTipo, setDocumentoTipo] = useState(1); // RG por padrão

  useEffect(() => {
    if (form) {
      setDocumentoTipo(form.documentoTipo);
    }
  }, [form]);

  const handleClose = () => {
    onClose();
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm((prevForm) => ({ ...prevForm, [name]: value }));
  };

  return (
    show && (
      <div className={`modal fade ${show ? "show" : ""} d-block`} tabIndex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true"
      style={{
        display: show ? "block" : "none",
        backgroundColor: "rgba(0, 0, 0, 0.5)",
      }}
      tabIndex="-1"
      role="dialog"
    >
      >
        <div className="modal-dialog modal-dialog-centered" role="document">
          <div className="modal-content">
            <div className="modal-header">
              <h5 className="modal-title">{form.id ? 'Atualizar' : 'Adicionar'} Cliente</h5>
            </div>
            <div className="modal-body">
              <form onSubmit={handleSubmit} autoComplete="off">
                <div className="form-group">
                  <label htmlFor="nome">Nome</label>
                  <input
                    type="text"
                    id="nome"
                    name="nome"
                    className="form-control"
                    placeholder="Nome"
                    value={form.nome}
                    onChange={handleChange}
                    required
                  />
                </div>

                <div className="form-group">
                  <label htmlFor="sobrenome">Sobrenome</label>
                  <input
                    type="text"
                    id="sobrenome"
                    name="sobrenome"
                    className="form-control"
                    placeholder="Sobrenome"
                    value={form.sobrenome}
                    onChange={handleChange}
                    required
                  />
                </div>

                <div className="form-group">
                  <label htmlFor="email">Email</label>
                  <input
                    type="email"
                    id="email"
                    name="email"
                    className="form-control"
                    placeholder="Email"
                    value={form.email}
                    onChange={handleChange}
                    required
                  />
                </div>

                <div className="form-group">
                  <label htmlFor="documentoNumero">Número do Documento</label>
                  <input
                    type="text"
                    id="documentoNumero"
                    name="documentoNumero"
                    className="form-control"
                    placeholder="Número do Documento"
                    value={form.documentoNumero}
                    onChange={handleChange}
                    required
                  />
                </div>

                <div className="form-group m-3">
                  <label>Tipo do Documento:</label>
                  <div className="btn-group m-3" role="group" aria-label="Tipo de Documento">
                    <button
                      type="button"
                      className={`btn ${documentoTipo === 1 ? 'btn-success' : 'btn-light'}`}
                      onClick={() => setForm((prevForm) => ({ ...prevForm, documentoTipo: 1 }))}
                    >
                      RG
                    </button>
                    <button
                      type="button"
                      className={`btn ${documentoTipo === 2 ? 'btn-success' : 'btn-light'}`}
                      onClick={() => setForm((prevForm) => ({ ...prevForm, documentoTipo: 2 }))}
                    >
                      CPF
                    </button>
                  </div>
                </div>

                <div className="form-group text-center">
                  <button type="submit" className="btn btn-primary">
                    {form.id ? 'Atualizar' : 'Adicionar'}
                  </button>
                </div>
              </form>
            </div>
            <div className="modal-footer">
              <button type="button" className="btn btn-secondary" onClick={handleClose}>Fechar</button>
            </div>
          </div>
        </div>
      </div>
    )
  );
};

export default TesteModal;
