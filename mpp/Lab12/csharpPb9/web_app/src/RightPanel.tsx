import type { Proba } from "./proba";
import { useEffect } from 'react'

function RightPanel(props: { 
    probaList: Proba[], 
    setProbaList: (list: Proba[]) => void, 
    loadList: () => void }) 
{
  useEffect(() => {
    props.loadList();
  }, []);


  return (
    <div className="right-panel">
      <h2>Probe</h2>
        <ul>
            {props.probaList.map((proba) => {
                let categorieName : string;
                switch (proba.categorie) {
                    case 0:
                        categorieName = 'Ciclism';
                        break;
                    case 1:
                        categorieName = 'Inot';
                        break;
                    case 2:
                        categorieName = 'Alergat';
                        break;
                    default:
                        categorieName = 'Necunoscut';
                }
                return(
            <li key={proba.id}>
                {proba.nume} (ID: {proba.id}, Arbitru ID: {proba.id_arbitru}, Categorie: {categorieName})
            </li>
            )} 
        )}
        </ul>
      <button onClick={props.loadList}>Click Me</button>
    </div>
  );
}

export default RightPanel;