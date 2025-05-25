import { useState } from 'react';

function LeftPanel(props: { loadList: () => void }) {

    const [idInput, setIdInput] = useState<string>('');

    const [numeInput, setNumeInput] = useState<string>('');
    const [categorieInput, setCategorieInput] = useState<string>('');

    async function handleFind(id: string) {
        if (!id || !['s', 'c', 'i'].includes(id[0].toLowerCase()) || id.length < 2 || isNaN(Number(id.slice(1)))) {
            alert("Please enter an ID starting with 's', 'c', or 'i' and follow by number to find.");
            return;
        }

        fetch(`https://localhost:7063/api/proba/${id}`)
            .then(response => {
                if (response.status === 404) {
                    throw new Error('Proba not found');
                }

                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => {
                setNumeInput(data.nume);
                
                switch (data.categorie){
                    case 0:
                        setCategorieInput('Ciclism');
                        break;
                    case 1:
                        setCategorieInput('Inot');
                        break;
                    case 2:
                        setCategorieInput('Alergat');
                        break;
                    default:
                        setCategorieInput('Necunoscut');
                }
            })
            .catch(error => {
                console.error('Error fetching proba:', error);
                setNumeInput('');
                setCategorieInput('');
            });
    }

    async function handleDelete(id: string) {
        if (!id || !['s', 'c', 'i'].includes(id[0].toLowerCase()) || id.length < 2 || isNaN(Number(id.slice(1)))) {
            alert("Please enter an ID starting with 's', 'c', or 'i' and follow by number to delete.");
            return;
        }

        try {
            const response = await fetch(`https://localhost:7063/api/proba/${id}`, {
                method: 'DELETE',
            });

            if (response.status === 404) {
                throw new Error('Proba not found');
            }

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            alert("Proba deleted successfully");
            props.loadList();
            setIdInput('');
            setNumeInput('');
            setCategorieInput('');
        }
        catch (error) {
            console.error('Error deleting proba:', error);
            alert("Error deleting proba:\n " + error);
        }
    }

    async function handleSave(nume: string, categorie: string) {
        if (!nume || !categorie) {
            alert("Please enter both Nume and Categorie to save.");
            return;
        }
        if (!['ciclism', 'inot', 'alergat'].includes(categorie.toLowerCase())) {

            alert("Please enter a valid Categorie: 'ciclism', 'inot', or 'alergat'.");
            return;
        }

        const probaDTO = {
            nume: nume,
            categorie: categorie.toLowerCase()
        };

        try {
            const response = await fetch('https://localhost:7063/api/proba', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(probaDTO),
            });

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            alert("Proba saved successfully");
            props.loadList();
            setNumeInput('');
            setCategorieInput('');
        } catch (error) {
            console.error('Error saving proba:', error);
            alert("Error saving proba:\n " + error);
        }
    }

    async function handleUpdate(id :string, nume: string, categorie: string) {
        if (!id || !['s', 'c', 'i'].includes(id[0].toLowerCase()) || id.length < 2 || isNaN(Number(id.slice(1)))) {
            alert("Please enter an ID starting with 's', 'c', or 'i' and follow by number to find.");
            return;
        }
         if (!nume || !categorie) {
            alert("Please enter both Nume and Categorie to save.");
            return;
        }
        if (!['ciclism', 'inot', 'alergat'].includes(categorie.toLowerCase())) {

            alert("Please enter a valid Categorie: 'ciclism', 'inot', or 'alergat'.");
            return;
        }

        const probaDTO = {
            nume: nume,
            categorie: categorie.toLowerCase()
        };

        try{
            const response = await fetch(`https://localhost:7063/api/proba/${idInput}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(probaDTO),
            });

            if (response.status === 404) {
                throw new Error('Proba not found');
            }

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            alert("Proba updated successfully");
            props.loadList();
            setIdInput('');
            setNumeInput('');
            setCategorieInput('');
        }
        catch (error) {
            console.error('Error updating proba:', error);
            alert(error);
        }
    }

    return (
        <div className="left-panel">
            <label htmlFor="idInput"> Id:</label>
            <input
                id="idInput"
                type="text"
                value={idInput}
                onChange={(e) => setIdInput(e.target.value)}
                placeholder="Enter ID"
            />
            <br />
            <button onClick={() => handleFind(idInput || '0')} disabled={!idInput}>Find</button>
            <button onClick={() => handleDelete(idInput || '0')} disabled={!idInput}>Delete</button>
            <br />
            <label htmlFor="numeInput"> Nume:</label>
            <input
                id="numeInput"
                type="text"
                value={numeInput}
                onChange={(e) => setNumeInput(e.target.value)}
                placeholder="Enter Nume"
            />
            <br />
            <label htmlFor="categorieInput"> Categorie:</label>
            <input
                id="categorieInput"
                type="text"
                value={categorieInput}
                onChange={(e) => setCategorieInput(e.target.value)}
                placeholder="Enter Categorie"
            />
            <br />
            <button onClick={() =>handleSave(numeInput || '',categorieInput || '')} disabled={!numeInput || !categorieInput}>Save</button>
            <button onClick={() =>handleUpdate(idInput || '0',numeInput || '',categorieInput || '')} disabled={!numeInput || !categorieInput}>Update </button>
        </div>
    );
}

export default LeftPanel;