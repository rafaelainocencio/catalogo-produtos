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

  const apiUrl = "http://localhost:5198/cliente";

  let buscarClientesDesativados = 3; //1- apenas ativos 2- apenas desativados 3- todos
  let filtroClientes = "";

  switch (buscarClientesDesativados) {
    case 1:
      filtroClientes = "?desativado=false";
      break;
    case 2:
      filtroClientes = "?desativado=true";
      break;
    default:
      filtroClientes = "";
      break;
  }

  useEffect(() => {
    BuscarClientes();
  }, []);

  // Ler itens da API
function BuscarClientes() {
  fetch(`${apiUrl}${filtroClientes}`)
      .then((response) => response.json())
      .then((data) => {
        console.log("Dados recebidos:", data);
        setItems(data.multipleData);
      })
      .catch((error) => console.error("Error fetching items:", error));
}

  // Criar ou atualizar item
  const handleSubmit = (e) => {
    e.preventDefault();
  
    // Reestruturar os dados
    console.log("editing:", editing);
  
    let id = "";
    if (editing !== undefined && editing !== null) {
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
  
    if (editing) {
      fetch(`${apiUrl}/${editing.id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(formattedData),
      })
        .then((response) => response.json())
        .then((data) => {
          if (data.success) {
            // Adicionar item ao estado
            setItems((prev) => [...prev, data.singleData]);
            setForm({
              id: "",
              nome: "",
              sobrenome: "",
              email: "",
              documentoNumero: "",
              documentoTipo: "",
            });
          } else {
            alert(`Erro: ${data.mensage}`);
          }
        })
        .catch((error) => console.error("Error updating item:", error));
    } else {
      fetch(apiUrl, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(formattedData),
      })
        .then((response) => response.json())
        .then((data) => {
          if (data.success) {
            // Adicionar item ao estado
            setItems((prev) => [...prev, data.singleData]);
            setForm({
              id: "",
              nome: "",
              sobrenome: "",
              email: "",
              documentoNumero: "",
              documentoTipo: "",
            });
          } else {
            alert(`Erro: ${data.mensage}`);
          }
        })
        .catch((error) => console.error("Error creating item:", error));
    }
  };
  
  

  // Deletar item
  const handleAtivarOuDesativar  = (id, isDesativado) => {

    let filtro = isDesativado ? "ativar" : "desativar";

    fetch(`${apiUrl}/${filtro}/${id}`, { method: "PATCH" }).then(() =>
      {
        BuscarClientes();
      }
    );
  };

  // Editar item
  const handleEdit = (item) => {
    setForm(item);
    setEditing(item);
  };

  return (
    <div>
      <h1>CRUD com React</h1>
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
