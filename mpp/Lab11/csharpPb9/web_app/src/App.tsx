import { useCallback, useState } from 'react'
import './App.css'
import RightPanel from './RightPanel'
import type { Proba } from './proba'
import LeftPanel from './LeftPanel'

function App() {
    const [probaList, setProbaList] = useState<Proba[]>([])

    const getAllProba = useCallback(() => {
        fetch('https://localhost:7063/api/proba')
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok')
                }
                return response.json()
            .then(data => {
                setProbaList(data)
            })
            .catch(error => {
                console.error('Error fetching proba:', error)
            })
        })
    },[]);

    return (
        <>
            <LeftPanel
                loadList={getAllProba}
            />
            <RightPanel 
                probaList={probaList}
                setProbaList={setProbaList}
                loadList={getAllProba}
            />
        </>
    )
}

export default App
