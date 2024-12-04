import React, { useState, useEffect } from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap-icons/font/bootstrap-icons.css';


const App = () => {
  const [items, setItems] = useState([]);
  const [form, setForm] = useState({
    nome: "",
    sobrenome: "",
    email: "",
    documentoNumero: "",
    documentoTipo: "",
  });
  const [editing, setEditing] = useState(null);
  const [filter, setFilter] = useState(1); // 1: Ativos, 2: Desativados, 3: Todos
  const [documentoTipo, setDocumentoTipo] = useState(1); // 1: RG, 2: CPF

  const apiUrl = "http://localhost:5198/cliente";

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

    fetch(`${apiUrl}${filtroClientes}`)
      .then((response) => response.json())
      .then((data) => {
        console.log("Dados recebidos:", data);
        setItems(data.multipleData || []);
      })
      .catch((error) => console.error("Error fetching items:", error));
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    let id = "";
    if (editing) {
      id = editing.id.toUpperCase();
    }

    const formattedData = {
      Id: id,
      nome: form.nome,
      sobrenome: form.sobrenome,
      email: form.email,
      documento: {
        numero: form.documentoNumero,
        tipo: documentoTipo,
      },
    };

    const method = editing ? "PUT" : "POST";
    const url = editing ? `${apiUrl}/${editing.id}` : apiUrl;

    fetch(url, {
      method,
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(formattedData),
    })
      .then((response) => response.json())
      .then((data) => {
        if (data.success) {
          buscarClientes();
          setForm({
            nome: "",
            sobrenome: "",
            email: "",
            documentoNumero: "",
            documentoTipo: "",
          });
          setEditing(null);
        } else {
          alert(`Erro: ${data.mensage}`);
        }
      })
      .catch((error) => console.error("Error saving item:", error));
  };

  const handleAtivarOuDesativar = (id, isDesativado) => {
    const action = isDesativado ? "ativar" : "desativar";
    fetch(`${apiUrl}/${action}/${id}`, { method: "PATCH" })
      .then(() => buscarClientes())
      .catch((error) => console.error("Erro ao atualizar cliente:", error));
  };

  const handleEdit = (item) => {
    setForm(item);
    setDocumentoTipo(item.documentoTipo);
    setEditing(item);
  };

  return (
    <div class="container">
      <h1>CRUD com React</h1>
      <div>
        <label>Filtrar clientes:</label>
        <select value={filter} onChange={(e) => setFilter(Number(e.target.value))}>
          <option value={1}>Ativos</option>
          <option value={2}>Desativados</option>
          <option value={3}>Todos</option>
        </select>
      </div>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="Nome"
          value={form.nome}
          onChange={(e) => setForm({ ...form, nome: e.target.value })}
          required
        />
        <input
          type="text"
          placeholder="Sobrenome"
          value={form.sobrenome}
          onChange={(e) => setForm({ ...form, sobrenome: e.target.value })}
          required
        />
        <input
          type="email"
          placeholder="Email"
          value={form.email}
          onChange={(e) => setForm({ ...form, email: e.target.value })}
          required
        />
        <input
          type="text"
          placeholder="Número do Documento"
          value={form.documentoNumero}
          onChange={(e) => setForm({ ...form, documentoNumero: e.target.value })}
          required
        />
        <div>
          <label>Tipo do Documento: </label>
          <button 
            type="button" 
            onClick={() => setDocumentoTipo(1)} 
            style={{ backgroundColor: documentoTipo === 1 ? 'lightgreen' : 'lightgray' }}
          >
            RG
          </button>
          <button 
            type="button" 
            onClick={() => setDocumentoTipo(2)} 
            style={{ backgroundColor: documentoTipo === 2 ? 'lightgreen' : 'lightgray' }}
          >
            CPF
          </button>
        </div>
        <button type="submit">{editing ? "Atualizar" : "Adicionar"}</button>
      </form>

      <table className=" container table table-striped table-bordered">
        <thead>
          <tr>
            <th>Nome Completo</th>
            <th>Documento</th>
            <th>Email</th>
          </tr>
        </thead>
        <tbody>
  {items.map((item) => (
    <tr key={item.Id}>
      <td>{item.nome} {item.sobrenome}</td>
      <td>{item.documentoNumero}</td>
      <td>{item.email}</td>
      <td>
        <button 
          onClick={() => handleEdit(item)} 
          className="btn btn-warning"
        >
          <i className="bi bi-pencil-fill"></i>
        </button>
      </td>
      <td>
        <button 
          onClick={() => handleAtivarOuDesativar(item.id, item.desativado)} 
          className={`btn ${item.desativado ? 'btn-success' : 'btn-danger'} w-100`}
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
