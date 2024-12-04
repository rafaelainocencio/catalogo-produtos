import React, { useState, useEffect } from "react";

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
        tipo: form.documentoTipo,
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
    setEditing(item);
  };

  return (
    <div>
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
        <input
          type="number"
          placeholder="Tipo de Documento"
          value={form.documentoTipo}
          onChange={(e) => setForm({ ...form, documentoTipo: Number(e.target.value) })}
          required
        />
        <button type="submit">{editing ? "Atualizar" : "Adicionar"}</button>
      </form>

      <ul>
        {items.map((item) => (
          <li key={item.id}>
            <strong>{item.nome}:</strong> {item.email}
            <button onClick={() => handleEdit(item)}>Edit</button>
            <button onClick={() => handleAtivarOuDesativar(item.id, item.desativado)}>
              {item.desativado ? "Ativar" : "Desativar"}
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default App;
