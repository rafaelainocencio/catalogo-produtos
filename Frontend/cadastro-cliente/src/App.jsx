import React, { useState, useEffect } from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap-icons/font/bootstrap-icons.css';
import TesteModal from "./components/Modal";
import Popup from "./components/Popup";
import axios from "axios";

const App = () => {
  const [items, setItems] = useState([]);
  const [form, setForm] = useState({
    nome: "",
    sobrenome: "",
    email: "",
    documentoNumero: "",
    documentoTipo: 1, // RG por padrão
  });

  const [showModal, setShowModal] = useState(false);
  const [filter, setFilter] = useState(1); // 1: Ativos, 2: Desativados, 3: Todos
  const apiUrl = "http://localhost:5198/cliente";

  const [showPopup, setShowPopup] = useState(false);
  const [popupMessage, setPopupMessage] = useState("");


  useEffect(() => {
    buscarClientes();
  }, [filter]);

  const buscarClientes = () => {
    let filtroClientes = "";
    switch (filter) {
      case 1:
        filtroClientes = "?desativado=false";
        break;
      case 2:
        filtroClientes = "?desativado=true";
        break;
      default:
        filtroClientes = "";
    }

    axios.get(`${apiUrl}${filtroClientes}`)
    .then((response) => {
      setItems(response.data.multipleData || []);
    })
    .catch((error) => console.error("Error fetching items:", error));
    };

  const handleSubmit = (e) => {
    e.preventDefault();

    const isEditing = !!form.id;
    const formattedData = {
      Id: form.id || "",
      nome: form.nome,
      sobrenome: form.sobrenome,
      email: form.email,
      documento: {
        numero: form.documentoNumero,
        tipo: form.documentoTipo,
      },
    };

    const url = isEditing ? `${apiUrl}/${form.id}` : apiUrl;


    if (isEditing) {
      axios.put(url, formattedData)
      .then((response) => {
        console.log("data: ", response.data);
            if (response.data.success) {
              buscarClientes();
              setPopupMessage(
                `Cliente ${isEditing ? "atualizado" : "cadastrado"} com sucesso!`
              );
              setShowPopup(true);
              setShowModal(false);
    
              setForm({
                id: "",
                nome: "",
                sobrenome: "",
                email: "",
                documentoNumero: "",
                documentoTipo: 1,
              });
    
            } else {
              setPopupMessage(`Erro: ${response.data.mensage}`);
              setShowPopup(true);
            }
          })
          .catch((error) => {
            console.error("Error saving item:", error);
            setPopupMessage(`Erro: ${error.response.data.mensage}`);
            setShowPopup(true);
          });
    } else {
      axios.post(url, formattedData)
      .then((response) => {
            if (response.data.success) {
              buscarClientes();
              setPopupMessage(
                `Cliente ${isEditing ? "atualizado" : "cadastrado"} com sucesso!`
              );
              setShowPopup(true);
              setShowModal(false);
    
              setForm({
                id: "",
                nome: "",
                sobrenome: "",
                email: "",
                documentoNumero: "",
                documentoTipo: 1,
              });
    
            } else {
              setPopupMessage(`Erro: ${response.data.mensage}`);
              setShowPopup(true);
            }
          })
          .catch((error) => {
            console.error("Error saving item:", error);
            setPopupMessage(`Erro: ${error.response.data.mensage}`);
            setShowPopup(true);
          });
    };
  };

  const handleAtivarOuDesativar = (id, isDesativado) => {
    const action = isDesativado ? "ativar" : "desativar";

    axios
    .patch(`${apiUrl}/${action}/${id}`)
    .then(() => buscarClientes())
    .catch((error) => console.error("Erro ao atualizar cliente:", error.response.data.mensage));
  };

  const toggleModal = () => {
    setShowModal(!showModal);
    setForm({
      nome: "",
      sobrenome: "",
      email: "",
      documentoNumero: "",
      documentoTipo: 1, // RG por padrão
    });
  };

  return (
    <div className="container mt-5">

      <TesteModal
        show={showModal}
        onClose={toggleModal}
        form={form}
        setForm={setForm}
        handleSubmit={handleSubmit}
      />

      <Popup 
        show={showPopup}
        message={popupMessage}
        type="success" 
        onClose={() => setShowPopup(false)} 
      />


      <h1 className="text-center">Clientes</h1>
      <div className="d-flex justify-content-between align-items-center mb-3">
      <div className="filter-section mb-3">
      <label htmlFor="clientFilter" className="me-3 fs-4">Filtrar clientes:</label>
      <select id="clientFilter" value={filter} onChange={(e) => setFilter(Number(e.target.value))} className="form-select">
        <option value={1}>Ativos</option>
        <option value={2}>Desativados</option>
        <option value={3}>Todos</option>
      </select>
    </div>
      <div className="add-client-section">
        <button type="button" onClick={() => setShowModal(true)} className="btn btn-primary">
        <i className="bi bi-plus-circle-fill"></i> Adicionar Cliente
        </button>
      </div>
    </div>

      <table className="container table table-striped table-bordered mt-3">
        <thead>
          <tr>
            <th>Nome Completo</th>
            <th>Documento</th>
            <th>Email</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {items.map((item, index) => (
            <tr key={item.Id || index}>
              <td>{item.nome} {item.sobrenome}</td>
              <td>{item.documentoNumero}</td>
              <td>{item.email}</td>
              <td className="d-flex">
                <button 
                  onClick={() => {
                    setShowModal(true);
                    setForm(item);
                  }} 
                  className="btn btn-warning m-1">
                  <i className="bi bi-pencil-fill"></i>
                </button>
                <button 
                  onClick={() => handleAtivarOuDesativar(item.id, item.desativado)} 
                  className={`btn ${item.desativado ? 'btn-success' : 'btn-danger'} w-100 m-1`}
                >
                  <i className={`bi ${item.desativado ? 'bi-check-circle-fill' : 'bi-x-circle-fill'}`}></i> 
                  {item.desativado ? " Ativar" : " Desativar"}
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default App;
